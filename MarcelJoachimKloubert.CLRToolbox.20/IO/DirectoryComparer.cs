// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Class that compares two directories.
    /// </summary>
    public sealed partial class DirectoryComparer
    {
        #region Fields (2)

        private readonly string _DESTINATION;
        private readonly string _SOURCE;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of <see cref="DirectoryComparer" /> class.
        /// </summary>
        /// <param name="src">The path of the source directory.</param>
        /// <param name="dest">The path of the destination directory.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public DirectoryComparer(IEnumerable<char> src, IEnumerable<char> dest)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (dest == null)
            {
                throw new ArgumentNullException("dest");
            }

            this._SOURCE = Path.GetFullPath(StringHelper.AsString(src));
            this._DESTINATION = Path.GetFullPath(StringHelper.AsString(src));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DirectoryComparer" /> class.
        /// </summary>
        /// <param name="src">The path of the source directory.</param>
        /// <param name="dest">The destination directory.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dest" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        public DirectoryComparer(DirectoryInfo src, IEnumerable<char> dest)
            : this(src.FullName, dest)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="DirectoryComparer" /> class.
        /// </summary>
        /// <param name="src">The source directory.</param>
        /// <param name="dest">The path of the destination directory.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="src" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="dest" /> is <see langword="null" />.
        /// </exception>
        public DirectoryComparer(IEnumerable<char> src, DirectoryInfo dest)
            : this(src, dest.FullName)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="DirectoryComparer" /> class.
        /// </summary>
        /// <param name="src">The source directory.</param>
        /// <param name="dest">The destination directory.</param>
        /// <exception cref="NullReferenceException">
        /// <paramref name="src" /> and/or <paramref name="dest" /> are <see langword="null" />.
        /// </exception>
        public DirectoryComparer(DirectoryInfo src, DirectoryInfo dest)
            : this(src.FullName, dest.FullName)
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the full path of the destination directory.
        /// </summary>
        public string Destination
        {
            get { return this._DESTINATION; }
        }

        /// <summary>
        /// Gets the full path of the source directory.
        /// </summary>
        public string Source
        {
            get { return this._SOURCE; }
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (8) 

        /// <summary>
        /// Creates the context for the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext CreateContext()
        {
            return this.CreateContext<object>(null);
        }

        /// <summary>
        /// Creates the context for the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext CreateContext(bool recursive)
        {
            return this.CreateContext<object>(null, recursive);
        }

        /// <summary>
        /// Creates the context for the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <typeparam name="TState">Type of <paramref name="state" />.</typeparam>
        /// <param name="state">The object that should be linked with the result context.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext<TState> CreateContext<TState>(TState state)
        {
            return this.CreateContext<TState>(state, false);
        }

        /// <summary>
        /// Creates the context for the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <typeparam name="TState">Type of <paramref name="state" />.</typeparam>
        /// <param name="state">The object that should be linked with the result context.</param>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext<TState> CreateContext<TState>(TState state, bool recursive)
        {
            DirectoryComparerExecutionContext<TState> result = new DirectoryComparerExecutionContext<TState>(this);
            result.Recursive = recursive;
            result.State = state;

            return result;
        }

        /// <summary>
        /// Starts the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext Execute()
        {
            return this.Execute(false);
        }

        /// <summary>
        /// Starts the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext Execute(bool recursive)
        {
            return this.Execute<object>(null, recursive);
        }

        /// <summary>
        /// Starts the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <typeparam name="TState">Type of <paramref name="state" />.</typeparam>
        /// <param name="state">The object that should be linked with the result context.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext<TState> Execute<TState>(TState state)
        {
            return this.Execute<TState>(state, false);
        }

        /// <summary>
        /// Starts the comparison of two directories based on the current settings of that instance.
        /// </summary>
        /// <typeparam name="TState">Type of <paramref name="state" />.</typeparam>
        /// <param name="state">The object that should be linked with the result context.</param>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The execution context.</returns>
        public IDirectoryComparerExecutionContext<TState> Execute<TState>(TState state, bool recursive)
        {
            IDirectoryComparerExecutionContext<TState> ctx = this.CreateContext<TState>(state, recursive);
            ctx.Start();

            return ctx;
        }

        #endregion Methods

#if !NET2 && !NET20 && !NET35 && !WINDOWS_PHONE
        /// <summary>
        /// Creates a task that executes a comparer process async.
        /// </summary>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext> CreateContextAsync()
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext>(() =>
                {
                    return this.Execute();
                });
        }

        /// <summary>
        /// Creates a task that executes a comparer process async.
        /// </summary>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext> CreateContextAsync(bool recursive)
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext>(() =>
                {
                    return this.Execute(recursive: recursive);
                });
        }

        /// <summary>
        /// Creates a task that executes a comparer process async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>> CreateContextAsync<TState>(TState state)
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>>(() =>
                {
                    return this.Execute(state: state);
                });
        }

        /// <summary>
        /// Creates a task that executes a comparer process async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>> CreateContextAsync<TState>(TState state, bool recursive)
        {
            return new global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>>(() =>
                {
                    return this.Execute(state: state,
                                        recursive: recursive);
                });
        }

        /// <summary>
        /// Executes <see cref="DirectoryComparer.Execute()" /> method async.
        /// </summary>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext> ExecuteAsync()
        {
            var task = this.CreateContextAsync();
            task.Start();

            return task;
        }

        /// <summary>
        /// Executes <see cref="DirectoryComparer.Execute(bool)" /> method async.
        /// </summary>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext> ExecuteAsync(bool recursive)
        {
            var task = this.CreateContextAsync(recursive: recursive);
            task.Start();

            return task;
        }

        /// <summary>
        /// Executes <see cref="DirectoryComparer.Execute{T}(T)" /> method async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>> ExecuteAsync<TState>(TState state)
        {
            var task = this.CreateContextAsync<TState>(state);
            task.Start();

            return task;
        }

        /// <summary>
        /// Executes <see cref="DirectoryComparer.Execute{T}(T, bool)" /> method async.
        /// </summary>
        /// <param name="state">The additional state object that should be linked with the result context.</param>
        /// <param name="recursive">Also compare sub directories or not.</param>
        /// <returns>The underlying task.</returns>
        public global::System.Threading.Tasks.Task<global::MarcelJoachimKloubert.CLRToolbox.IO.IDirectoryComparerExecutionContext<TState>> ExecuteAsync<TState>(TState state, bool recursive)
        {
            var task = this.CreateContextAsync<TState>(state, recursive);
            task.Start();

            return task;
        }
#endif
    }
}