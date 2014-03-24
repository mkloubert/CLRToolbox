// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class StreamCopier
    {
        #region Nested Classes (1)

        private sealed class StreamCopyExecutionContext<TState> : IStreamCopyExecutionContext<TState>
        {
            #region Fields (8)

            private bool _canceled;
            private readonly StreamCopier _COPIER;
            private IList<Exception> _errors;
            private bool _failed;
            private bool _isCancelling;
            private bool _isRunning;
            private readonly TState _STATE;
            private readonly object _SYNC = new object();

            #endregion Fields

            #region Constructors (1)

            internal StreamCopyExecutionContext(StreamCopier copier, TState state)
            {
                this._COPIER = copier;
                this._STATE = state;
            }

            #endregion Constructors

            #region Properties (6)

            public bool Canceled
            {
                get { return this._canceled; }

                private set { this._canceled = value; }
            }

            public IList<Exception> Errors
            {
                get { return this._errors; }

                private set { this._errors = value; }
            }

            public bool Failed
            {
                get { return this._failed; }

                private set { this._failed = value; }
            }

            public bool IsCancelling
            {
                get { return this._isCancelling; }

                private set { this._isCancelling = value; }
            }

            public bool IsRunning
            {
                get { return this._isRunning; }

                private set { this._isRunning = value; }
            }

            public TState State
            {
                get { return this._STATE; }
            }

            #endregion Properties

            #region Delegates and Events (5)

            // Events (5) 

            public event EventHandler<StreamCopyBeforeWriteEventArgs> BeforeWrite;

            public event EventHandler Completed;

            public event EventHandler<StreamCopyProgressEventArgs> Progress;

            public event EventHandler<StreamCopyProgressErrorEventArgs> ProgressError;

            public event EventHandler Started;

            #endregion Delegates and Events

            #region Methods (6)

            // Public Methods (2) 

            public void Cancel()
            {
                this.IsCancelling = true;
            }

            public void Start()
            {
                lock (this._SYNC)
                {
                    this.IsCancelling = false;
                    this.Failed = false;
                    this.Canceled = false;

                    this.Errors = null;

                    List<Exception> occuredExceptions = new List<Exception>();

                    try
                    {
                        this.IsRunning = true;

                        this.RaiseEventHandler(this.Started);
                        this.OnProgress(new StreamCopyProgressEventArgs(this._COPIER,
                                                                        0, 0));

                        byte[] buffer = new byte[this._COPIER._BUFFER_SIZE];

                        long totalCopied = 0;

                        while (true)
                        {
                            int bytesRead = 0;
                            try
                            {
                                bytesRead = this._COPIER.Source.Read(buffer, 0, buffer.Length);
                                if (bytesRead < 1)
                                {
                                    break;
                                }

                                if (this.IsCancelling)
                                {
                                    break;
                                }

                                StreamCopyBeforeWriteEventArgs bwe = new StreamCopyBeforeWriteEventArgs(CollectionHelper.Take(buffer, bytesRead),
                                                                                                        this._COPIER,
                                                                                                        totalCopied);
                                this.OnBeforeWrite(bwe);

                                if (bwe.Cancel)
                                {
                                    this.IsCancelling = true;
                                    break;
                                }

                                if (bwe.Skip)
                                {
                                    // skip write operation
                                    continue;
                                }

                                int bytesCopied = 0;

                                byte[] dataToWrite = CollectionHelper.AsArray(bwe.Data);
                                if (dataToWrite != null)
                                {
                                    if (dataToWrite.Length > 0)
                                    {
                                        bytesCopied = dataToWrite.Length;

                                        this._COPIER.Destination.Write(dataToWrite, 0, bytesCopied);
                                        totalCopied += bytesCopied;
                                    }
                                }

                                StreamCopyProgressEventArgs e = new StreamCopyProgressEventArgs(this._COPIER,
                                                                                                bytesCopied, totalCopied);
                                this.OnProgress(e);

                                if (e.Cancel)
                                {
                                    this.IsCancelling = true;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                StreamCopyProgressErrorEventArgs e = new StreamCopyProgressErrorEventArgs(ex,
                                                                                                          this._COPIER,
                                                                                                          bytesRead, totalCopied);
                                e.Cancel = true;
                                e.LogErrors = true;

                                this.OnProgressError(e);

                                if (e.LogErrors)
                                {
                                    occuredExceptions.Add(e.Errors);
                                }

                                if (e.Handled == false)
                                {
                                    // (re-)throw
                                    throw e.Errors;
                                }

                                if (e.Cancel)
                                {
                                    this.IsCancelling = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        occuredExceptions.Add(ex);

                        this.Failed = true;
                    }
                    finally
                    {
                        this.Errors = new ReadOnlyCollection<Exception>(occuredExceptions);

                        if (this.IsCancelling)
                        {
                            this.Canceled = true;
                        }
                        this.IsCancelling = false;

                        this.IsRunning = false;
                        this.RaiseEventHandler(this.Completed);
                    }
                }
            }
            // Private Methods (4) 

            private bool OnBeforeWrite(StreamCopyBeforeWriteEventArgs e)
            {
                EventHandler<StreamCopyBeforeWriteEventArgs> handler = this.BeforeWrite;
                if (handler != null)
                {
                    handler(this, e);
                    return true;
                }

                return false;
            }

            private bool OnProgress(StreamCopyProgressEventArgs e)
            {
                EventHandler<StreamCopyProgressEventArgs> handler = this.Progress;
                if (handler != null)
                {
                    handler(this, e);
                    return true;
                }

                return false;
            }

            private bool OnProgressError(StreamCopyProgressErrorEventArgs e)
            {
                EventHandler<StreamCopyProgressErrorEventArgs> handler = this.ProgressError;
                if (handler != null)
                {
                    handler(this, e);
                    return true;
                }

                return false;
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

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
