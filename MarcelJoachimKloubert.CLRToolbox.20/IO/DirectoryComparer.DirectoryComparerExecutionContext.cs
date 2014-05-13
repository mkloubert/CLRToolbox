// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.ComponentModel;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class DirectoryComparer
    {
        #region Nested Classes (1)

        private sealed class DirectoryComparerExecutionContext<TState> : NotificationObjectBase, IDirectoryComparerExecutionContext<TState>
        {
            #region Fields (1)

            private readonly DirectoryComparer _COMPARER;

            #endregion Fields

            #region Constructors (1)

            internal DirectoryComparerExecutionContext(DirectoryComparer comparer)
            {
                this._COMPARER = comparer;
            }

            #endregion Constructors

            #region Properties (8)

            [ReceiveNotificationFrom("EndTime")]
            [ReceiveNotificationFrom("StartTime")]
            public TimeSpan? Duration
            {
                get
                {
                    DateTimeOffset? st = this.StartTime;
                    DateTimeOffset? et = this.EndTime;

                    if (st.HasValue && et.HasValue)
                    {
                        return et.Value - st.Value;
                    }

                    return null;
                }
            }

            public DateTimeOffset? EndTime
            {
                get { return this.Get<DateTimeOffset?>("EndTime"); }

                private set { this.Set(value, "EndTime"); }
            }

            public IList<Exception> Errors
            {
                get { return this.Get<IList<Exception>>("Errors"); }

                private set { this.Set(value, "Errors"); }
            }

            public bool IsCanceling
            {
                get { return this.Get<bool>("IsCanceling"); }

                private set { this.Set(value, "IsCanceling"); }
            }
            
            [ReceiveNotificationFrom("EndTime")]
            [ReceiveNotificationFrom("StartTime")]
            public bool IsRunning
            {
                get
                {
                    DateTimeOffset? st = this.StartTime;
                    DateTimeOffset? et = this.EndTime;

                    return st.HasValue &&
                           (et.HasValue == false);
                }
            }

            public bool Recursive
            {
                get { return this.Get<bool>("Recursive"); }

                internal set { this.Set(value, "Recursive"); }
            }

            public DateTimeOffset? StartTime
            {
                get { return this.Get<DateTimeOffset?>("StartTime"); }

                private set { this.Set(value, "StartTime"); }
            }

            public TState State
            {
                get { return this.Get<TState>("State"); }

                internal set { this.Set(value, "State"); }
            }

            #endregion Properties

            #region Delegates and Events (4)

            // Events (4) 

            public event EventHandler<CompareFileSystemItemsEventArgs> ComparingItems;

            public event EventHandler Completed;

            public event EventHandler<FoundDifferentFileSystemItemsEventArgs> DifferentItemsFound;

            public event EventHandler Started;

            #endregion Delegates and Events

            #region Methods (6)

            // Public Methods (3) 

            public void Cancel()
            {
                this.Cancel(true);
            }

            public void Cancel(bool wait)
            {
                this.IsCanceling = true;

                if (wait)
                {
                    try
                    {
                        while (this.IsRunning && this.IsCanceling)
                        { }
                    }
                    finally
                    {
                        this.IsCanceling = false;
                    }
                }
            }

            public void Start()
            {
                if (this.IsRunning)
                {
                    throw new InvalidOperationException();
                }

                this.EndTime = null;
                this.StartTime = AppTime.Now;

                List<Exception> occuredErrors = new List<Exception>();

                try
                {
                    this.IsCanceling = false;
                    this.Errors = null;

                    this.RaiseEventHandler(this.Started);

                    DirectoryInfo src = new DirectoryInfo(this._COMPARER.Source);
                    DirectoryInfo dest = new DirectoryInfo(this._COMPARER.Destination);
                    this.CompareDirectories(src, dest, occuredErrors);
                }
                catch (Exception ex)
                {
                    occuredErrors.Add(ex);
                }
                finally
                {
                    this.EndTime = AppTime.Now;

                    this.Errors = occuredErrors.ToArray();
                    this.IsCanceling = false;
                        
                    this.RaiseEventHandler(this.Completed);
                }
            }

            // Private Methods (3) 

            private void CompareDirectories(DirectoryInfo src, DirectoryInfo dest, ICollection<Exception> errList)
            {
                if (this.IsCanceling)
                {
                    return;
                }

                EventHandler<CompareFileSystemItemsEventArgs> compareHandler = this.ComparingItems;

                if (src.Exists == false || dest.Exists == false)
                {
                    if (compareHandler != null)
                    {
                        FileSystemItemDifferences diffs = FileSystemItemDifferences.None;

                        if (src.Exists == false)
                        {
                            diffs |= FileSystemItemDifferences.SourceDoesNotExist;
                        }

                        if (dest.Exists == false)
                        {
                            diffs |= FileSystemItemDifferences.DestionationDoesNotExist;
                        }

                        CompareFileSystemItemsEventArgs e = new CompareFileSystemItemsEventArgs(src, dest);
                        compareHandler(this, e);
                    }

                    return;
                }

                if (this.IsCanceling)
                {
                    return;
                }

                IEnumerable<DirectoryInfo> subDirsOfSource;
                try
                {
                    subDirsOfSource = src.GetDirectories();
                }
                catch (Exception ex)
                {
                    errList.Add(ex);

                    subDirsOfSource = CollectionHelper.Empty<DirectoryInfo>();
                }

                if (this.IsCanceling)
                {
                    return;
                }

                // compare directories
                try
                {
                    foreach (DirectoryInfo subDir in subDirsOfSource)
                    {
                        if (this.IsCanceling)
                        {
                            return;
                        }

                        try
                        {
                            DirectoryInfo destDir = new DirectoryInfo(Path.Combine(dest.FullName,
                                                                                   subDir.Name));

                            CompareFileSystemItemsEventArgs e = new CompareFileSystemItemsEventArgs(subDir, destDir);
                            if (compareHandler != null)
                            {
                                compareHandler(this, e);
                            }

                            if (e.Handled == false)
                            {
                                // use default logic

                                if (destDir.Exists)
                                {
                                }
                                else
                                {
                                    e.Differences |= FileSystemItemDifferences.IsMissing;
                                }
                            }

                            this.RaiseEventIfDifferent(e);
                        }
                        catch (Exception ex)
                        {
                            errList.Add(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errList.Add(ex);
                }

                if (this.IsCanceling)
                {
                    return;
                }

                // check for extra directories
                try
                {
                    foreach (DirectoryInfo subDir in dest.GetDirectories())
                    {
                        if (this.IsCanceling)
                        {
                            return;
                        }

                        try
                        {
                            DirectoryInfo srcDir = new DirectoryInfo(Path.Combine(src.FullName,
                                                                                  subDir.Name));

                            CompareFileSystemItemsEventArgs e = new CompareFileSystemItemsEventArgs(srcDir, subDir);
                            if (compareHandler != null)
                            {
                                compareHandler(this, e);
                            }

                            if (e.Handled == false)
                            {
                                // use default logic

                                if (srcDir.Exists)
                                {
                                }
                                else
                                {
                                    e.Differences |= FileSystemItemDifferences.IsExtra;
                                }
                            }

                            this.RaiseEventIfDifferent(e);
                        }
                        catch (Exception ex)
                        {
                            errList.Add(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errList.Add(ex);
                }

                if (this.IsCanceling)
                {
                    return;
                }

                // compare files
                try
                {
                    foreach (FileInfo file in src.GetFiles())
                    {
                        if (this.IsCanceling)
                        {
                            return;
                        }

                        try
                        {
                            FileInfo destFile = new FileInfo(Path.Combine(dest.FullName,
                                                                          file.Name));

                            CompareFileSystemItemsEventArgs e = new CompareFileSystemItemsEventArgs(file, destFile);
                            if (compareHandler != null)
                            {
                                compareHandler(this, e);
                            }

                            if (e.Handled == false)
                            {
                                // use default logic

                                if (destFile.Exists)
                                {
#if WINDOWS_PHONE
                                    
                                    if (NormalizeTimestamp(file.LastWriteTime) != NormalizeTimestamp(destFile.LastWriteTime))
#else
                                    if (NormalizeTimestamp(file.LastWriteTimeUtc) != NormalizeTimestamp(destFile.LastWriteTimeUtc))
#endif

                                    {
                                        e.Differences |= FileSystemItemDifferences.LastWriteTime;
                                    }
                                }
                                else
                                {
                                    e.Differences |= FileSystemItemDifferences.IsMissing;
                                }
                            }

                            this.RaiseEventIfDifferent(e);
                        }
                        catch (Exception ex)
                        {
                            errList.Add(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errList.Add(ex);
                }

                if (this.IsCanceling)
                {
                    return;
                }

                // check for extra files
                try
                {
                    foreach (FileInfo file in dest.GetFiles())
                    {
                        if (this.IsCanceling)
                        {
                            return;
                        }

                        try
                        {
                            FileInfo srcFile = new FileInfo(Path.Combine(src.FullName,
                                                                         file.Name));

                            CompareFileSystemItemsEventArgs e = new CompareFileSystemItemsEventArgs(srcFile, file);
                            if (compareHandler != null)
                            {
                                compareHandler(this, e);
                            }

                            if (e.Handled == false)
                            {
                                // use default logic

                                if (srcFile.Exists)
                                {
                                }
                                else
                                {
                                    e.Differences |= FileSystemItemDifferences.IsExtra;
                                }
                            }

                            this.RaiseEventIfDifferent(e);
                        }
                        catch (Exception ex)
                        {
                            errList.Add(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errList.Add(ex);
                }

                if (this.IsCanceling)
                {
                    return;
                }

                if (this.Recursive)
                {
                    foreach (DirectoryInfo subDir in subDirsOfSource)
                    {
                        if (this.IsCanceling)
                        {
                            return;
                        }

                        try
                        {
                            DirectoryInfo destDir = new DirectoryInfo(Path.Combine(dest.FullName,
                                                                                   subDir.Name));

                            this.CompareDirectories(subDir, destDir, errList);
                        }
                        catch (Exception ex)
                        {
                            errList.Add(ex);
                        }
                    }
                }
            }

            private static DateTime NormalizeTimestamp(DateTime input)
            {
                return new DateTime(input.Year, input.Month, input.Day,
                                    input.Hour, input.Minute, input.Second);
            }

            private bool? RaiseEventIfDifferent(CompareFileSystemItemsEventArgs e)
            {
                FileSystemItemDifferences diffs = e.Differences;
                if (diffs != FileSystemItemDifferences.None)
                {
                    EventHandler<FoundDifferentFileSystemItemsEventArgs> diffHandler = this.DifferentItemsFound;
                    if (diffHandler != null)
                    {
                        FoundDifferentFileSystemItemsEventArgs de = new FoundDifferentFileSystemItemsEventArgs(e.Source, e.Destination, diffs);
                        diffHandler(this, de);

                        return true;
                    }

                    return false;
                }

                return null;
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}