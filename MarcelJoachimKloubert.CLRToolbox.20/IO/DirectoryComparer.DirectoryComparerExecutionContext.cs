// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class DirectoryComparer
    {
        #region Nested Classes (1)

        private sealed class DirectoryComparerExecutionContext<TState> : IDirectoryComparerExecutionContext<TState>
        {
            #region Fields (9)

            private readonly DirectoryComparer _COMPARER;
            private DateTimeOffset? _endTime;
            private IList<Exception> _errors;
            private bool _isCanceling;
            private bool _isRunning;
            private bool _recurive;
            private DateTimeOffset? _startTime;
            private TState _state;
            private readonly object _SYNC = new object();

            #endregion Fields

            #region Constructors (1)

            internal DirectoryComparerExecutionContext(DirectoryComparer comparer)
            {
                this._COMPARER = comparer;
            }

            #endregion Constructors

            #region Properties (8)

            public TimeSpan? Duration
            {
                get
                {
                    DateTimeOffset? st = this._startTime;
                    DateTimeOffset? et = this._endTime;

                    if (st.HasValue && et.HasValue)
                    {
                        return et.Value - st.Value;
                    }

                    return null;
                }
            }

            public DateTimeOffset? EndTime
            {
                get { return this._endTime; }

                private set { this._endTime = value; }
            }

            public IList<Exception> Errors
            {
                get { return this._errors; }

                private set { this._errors = value; }
            }

            public bool IsCanceling
            {
                get { return this._isCanceling; }

                private set { this._isCanceling = value; }
            }

            public bool IsRunning
            {
                get { return this._isRunning; }

                internal set { this._isRunning = value; }
            }

            public bool Recursive
            {
                get { return this._recurive; }

                internal set { this._recurive = value; }
            }

            public DateTimeOffset? StartTime
            {
                get { return this._startTime; }

                private set { this._startTime = value; }
            }

            public TState State
            {
                get { return this._state; }

                internal set { this._state = value; }
            }

            #endregion Properties

            #region Delegates and Events (4)

            // Events (4) 

            public event EventHandler<CompareFileSystemItemsEventArgs> ComparingItems;

            public event EventHandler Completed;

            public event EventHandler<FoundDifferentFileSystemItemsEventArgs> DifferentItemsFound;

            public event EventHandler Started;

            #endregion Delegates and Events

            #region Methods (7)

            // Public Methods (3) 

            public void Cancel()
            {
                this.Cancel(true);
            }

            public void Cancel(bool wait)
            {
                if (this.IsCanceling == false)
                {
                    this.IsCanceling = true;
                }

                if (wait)
                {
                    while (this.IsRunning && this.IsCanceling)
                    { }

                    this.IsCanceling = false;
                }
            }

            public void Start()
            {
                lock (this._SYNC)
                {
                    this._startTime = AppTime.Now;
                    List<Exception> occuredErrors = new List<Exception>();

                    try
                    {
                        this._isCanceling = false;
                        this._isRunning = true;
                        this._errors = null;

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
                        this._errors = occuredErrors.ToArray();
                        this._isRunning = false;
                        this._isCanceling = false;

                        this._endTime = AppTime.Now;
                        this.RaiseEventHandler(this.Completed);
                    }
                }
            }
            // Private Methods (4) 

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
#if !WINDOWS_PHONE
                                    if (NormalizeTimestamp(file.LastWriteTimeUtc) != NormalizeTimestamp(destFile.LastWriteTimeUtc))
#else
                                    if (NormalizeTimestamp(file.LastWriteTime) != NormalizeTimestamp(destFile.LastWriteTime))
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

            private bool RaiseEventHandler(EventHandler handler)
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                    return true;
                }

                return false;
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