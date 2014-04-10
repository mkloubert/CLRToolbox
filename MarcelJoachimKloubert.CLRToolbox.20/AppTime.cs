// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Handles application time.
    /// </summary>
    public static class AppTime
    {
        /// <summary>
        /// Decribes a method or function that returns a time.
        /// </summary>
        /// <returns>The time.</returns>
        public delegate DateTimeOffset TimeProvider();

        /// <summary>
        /// Decribes a method or function that returns a run time.
        /// </summary>
        /// <returns>The run time.</returns>
        public delegate TimeSpan TimeSpanProvider();

        /// <summary>
        /// Stores the real start time of the app.
        /// </summary>
        public static readonly DateTimeOffset START_TIME;

        private static TimeProvider _nowProvider;
        private static TimeSpanProvider _runTimeProvider;

        /// <summary>
        /// Initilizes the <see cref="AppTime" /> class.
        /// </summary>
        static AppTime()
        {
#if !WINDOWS_PHONE
            try
            {
                START_TIME = global::System.Diagnostics.Process.GetCurrentProcess().StartTime;
            }
            catch
            {
                START_TIME = global::System.DateTimeOffset.Now;
            }
#else
            START_TIME = global::System.DateTimeOffset.Now;
#endif
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        public static DateTimeOffset Now
        {
            get { return (NowProvider ?? GetNow)(); }
        }

        /// <summary>
        /// Gets or sets a custom handler that returns the value of <see cref="AppTime.Now" />.
        /// If the value is <see langword="null" /> the default logic is called.
        /// </summary>
        public static TimeProvider NowProvider
        {
            get { return _nowProvider; }

            set { _nowProvider = value; }
        }

        /// <summary>
        /// Gets the time the app is running.
        /// </summary>
        public static TimeSpan RunTime
        {
            get { return (RunTimeProvider ?? GetRunTime)(); }
        }

        /// <summary>
        /// Gets or sets a custom handler that returns the value of <see cref="AppTime.RunTime" />.
        /// If the value is <see langword="null" /> the default logic is called.
        /// </summary>
        public static TimeSpanProvider RunTimeProvider
        {
            get { return _runTimeProvider; }

            set { _runTimeProvider = value; }
        }

        /// <summary>
        /// Gets the date part of <see cref="App.Now" />.
        /// </summary>
        public static DateTime Today
        {
            get { return Now.Date; }
        }

        private static DateTimeOffset GetNow()
        {
            return DateTimeOffset.Now;
        }

        private static TimeSpan GetRunTime()
        {
            return DateTimeOffset.Now - START_TIME;
        }
    }
}