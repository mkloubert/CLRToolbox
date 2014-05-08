// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Handles an unique list of <see cref="IDisposable" /> objects, that are all disposed if
    /// an instance of that class is disposed.
    /// </summary>
    public sealed class AggregateDisposer : DisposableBase
    {
        #region Fields (1)

        private readonly List<IDisposable> _OBJECTS = new List<IDisposable>();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of <see cref="AggregateDisposer" /> class.
        /// </summary>
        /// <param name="list">The inital items to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> is <see langword="null" />.
        /// </exception>
        public AggregateDisposer(IEnumerable<IDisposable> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            foreach (IDisposable obj in list)
            {
                if (obj == null)
                {
                    continue;
                }

                this.AddInner(obj);
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AggregateDisposer" /> class.
        /// </summary>
        public AggregateDisposer()
            : this(CollectionHelper.Empty<IDisposable>())
        {
        }

        #endregion Constructors

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked if an object of that list is disposed and/or removed.
        /// </summary>
        public event EventHandler<DisposeObjectEventArgs> DisposingObject;

        #endregion Delegates and Events

        #region Methods (8)

        // Public Methods (5) 

        /// <summary>
        /// Adds a new disposable object.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        /// <returns>Object was added or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public bool Add(IDisposable obj)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this.AddInner(obj);
            }

            return result;
        }

        /// <summary>
        /// Adds a list of disposable objects.
        /// </summary>
        /// <param name="list">The objects to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list" /> or at least one item in
        /// <paramref name="list" /> is <see langword="null" />.
        /// </exception>
        public void AddRange(IEnumerable<IDisposable> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            lock (this._SYNC)
            {
                foreach (IDisposable obj in list)
                {
                    this.AddInner(obj);
                }
            }
        }

        /// <summary>
        /// Clears all disposable objects from list WITHOUT disposing them.
        /// </summary>
        public void Clear()
        {
            lock (this._SYNC)
            {
                this._OBJECTS.Clear();
            }
        }

        /// <summary>
        /// Returns a new list of all currently stored objects.
        /// </summary>
        /// <returns>The new list of stored objects.</returns>
        public List<IDisposable> GetObjects()
        {
            List<IDisposable> result;

            lock (this._SYNC)
            {
                result = new List<IDisposable>(this._OBJECTS);
            }

            return result;
        }

        /// <summary>
        /// Removes a disposable object from list WITHOUT disposing it.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        /// <returns>Object was removed or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public bool Remove(IDisposable obj)
        {
            bool result;

            lock (this._SYNC)
            {
                result = this.RemoveInner(obj);
            }

            return result;
        }

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnDispose(bool disposing)
        {
            List<Exception> errors = new List<Exception>();

            try
            {
                var eventHandler = this.DisposingObject;

                foreach (IDisposable obj in this._OBJECTS)
                {
                    try
                    {
                        DisposeObjectEventArgs e = new DisposeObjectEventArgs(obj, disposing);
                        if (eventHandler != null)
                        {
                            eventHandler(this, e);
                        }

                        if (e.Cancel)
                        {
                            continue;
                        }

                        try
                        {
                            if (e.IsDispoing)
                            {
                                bool doDispose = true;

                                ITMDisposable tmDisp = obj as ITMDisposable;
                                if (tmDisp != null)
                                {
                                    // only if disposed
                                    doDispose = tmDisp.IsDisposed == false;
                                }

                                if (doDispose)
                                {
                                    obj.Dispose();
                                }
                            }
                        }
                        finally
                        {
                            this._OBJECTS
                                .Remove(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }

            if (disposing)
            {
                // rethrow exceptions only if Dispose() method is called
                // otherwise might crash

                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }
        }

        // Private Methods (2) 

        private bool AddInner(IDisposable obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (this._OBJECTS.Contains(obj) == false)
            {
                this._OBJECTS.Add(obj);
                return true;
            }

            return false;
        }

        private bool RemoveInner(IDisposable obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return this._OBJECTS.Remove(obj);
        }

        #endregion Methods
    }
}