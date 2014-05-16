// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration
{
    /// <summary>
    /// A basic repository that provides configuration data stored in categories as key/value pairs.
    /// </summary>
    public abstract partial class ConfigRepositoryBase : IConfigRepository
    {
        #region Fields (2)

        private readonly bool _IS_THREAD_SAFE;

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryBase" /> class.
        /// </summary>
        /// <param name="sync">The value for <see cref="ConfigRepositoryBase._SYNC" /> field.</param>
        /// <param name="isThreadSafe">The value for <see cref="ConfigRepositoryBase.Synchronized" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ConfigRepositoryBase(object sync, bool isThreadSafe)
        {
            if (sync == null)
            {
                throw new ArgumentNullException("sync");
            }

            this._SYNC = sync;
            this._IS_THREAD_SAFE = isThreadSafe;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryBase" /> class.
        /// </summary>
        /// <param name="sync">The value for <see cref="ConfigRepositoryBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ConfigRepositoryBase(object sync)
            : this(sync, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryBase" /> class.
        /// </summary>
        protected ConfigRepositoryBase()
            : this(new object())
        {
        }

        #endregion Constructors

        #region Delegates and events (1)

        private delegate TResult RepoFunc<T1, T2, T3, T4, TResult>(Func<ConfigRepositoryBase, T1, T2, T3, T4, TResult> func,
                                                                   T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        #endregion Delegates and events

        #region Properties (2)

        /// <inheriteddoc />
        public virtual bool CanRead
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public virtual bool CanWrite
        {
            get { return true; }
        }

        /// <inheriteddoc />
        public bool Synchronized
        {
            get { return this._IS_THREAD_SAFE; }
        }

        #endregion Properties

        #region Methods (40)

        // Public Methods (21) 

        /// <inheriteddoc />
        public bool Clear()
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo)
                                       {
                                           repo.ThrowIfNotWritable();

                                           bool result = false;
                                           repo.OnClear(ref result);

                                           return result;
                                       });
        }

        /// <inheriteddoc />
        public bool ContainsValue(IEnumerable<char> name)
        {
            return this.ContainsValue(name, null);
        }

        /// <inheriteddoc />
        public bool ContainsValue(IEnumerable<char> name, IEnumerable<char> category)
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo, IEnumerable<char> n, IEnumerable<char> c)
                                       {
                                           string configCategory;
                                           string configName;
                                           repo.PrepareCategoryAndName(c, n,
                                                                       out configCategory, out configName);

                                           bool result = false;
                                           repo.OnContainsValue(configCategory, configName,
                                                                ref result);

                                           return result;
                                       }, name, category);
        }

        /// <inheriteddoc />
        public bool DeleteValue(IEnumerable<char> name)
        {
            return this.DeleteValue(name, null);
        }

        /// <inheriteddoc />
        public bool DeleteValue(IEnumerable<char> name, IEnumerable<char> category)
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo, IEnumerable<char> n, IEnumerable<char> c)
                                       {
                                           repo.ThrowIfNotWritable();

                                           string configCategory;
                                           string configName;
                                           repo.PrepareCategoryAndName(c, n,
                                                                       out configCategory, out configName);

                                           bool result = false;
                                           repo.OnDeleteValue(configCategory, configName,
                                                              ref result);

                                           return result;
                                       }, name, category);
        }

        /// <inheriteddoc />
        public IList<string> GetCategoryNames()
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo)
                                       {
                                           List<IEnumerable<char>> names = new List<IEnumerable<char>>();
                                           repo.OnGetCategoryNames(names);

                                           List<string> result = new List<string>();
                                           foreach (IEnumerable<char> name in names)
                                           {
                                               result.Add(StringHelper.AsString(name));
                                           }

                                           return result;
                                       });
        }

        /// <inheriteddoc />
        public object GetValue(IEnumerable<char> name)
        {
            return this.GetValue(name, null);
        }

        /// <inheriteddoc />
        public T GetValue<T>(IEnumerable<char> name)
        {
            return this.GetValue<T>(name, null);
        }

        /// <inheriteddoc />
        public object GetValue(IEnumerable<char> name, IEnumerable<char> category)
        {
            return this.GetValue<object>(name, category);
        }

        /// <inheriteddoc />
        public T GetValue<T>(IEnumerable<char> name, IEnumerable<char> category)
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo, IEnumerable<char> n, IEnumerable<char> c)
                                       {
                                           T result;
                                           if (repo.TryGetValue<T>(n, out result, c) == false)
                                           {
                                               throw new ArgumentOutOfRangeException("name+category");
                                           }

                                           return result;
                                       }, name, category);
        }

        /// <inheriteddoc />
        public IConfigRepository MakeReadOnly()
        {
            if (this.CanWrite == false)
            {
                // already read-only
                return this;
            }

            return new ReadOnlyConfigRepositoryWrapper(this);
        }

        /// <inheriteddoc />
        public bool SetValue(IEnumerable<char> name, object value)
        {
            return this.SetValue(name, value, null);
        }

        /// <inheriteddoc />
        public bool SetValue<T>(IEnumerable<char> name, T value)
        {
            return this.SetValue<T>(name, value, null);
        }

        /// <inheriteddoc />
        public bool SetValue(IEnumerable<char> name, object value, IEnumerable<char> category)
        {
            return this.SetValue<object>(name, value, category);
        }

        /// <inheriteddoc />
        public bool SetValue<T>(IEnumerable<char> name, T value, IEnumerable<char> category)
        {
            return this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo, IEnumerable<char> n, T v, IEnumerable<char> c)
                                       {
                                           repo.ThrowIfNotWritable();

                                           string configCategory;
                                           string configName;
                                           repo.PrepareCategoryAndName(c, n,
                                                                       out configCategory, out configName);

                                           bool result = false;
                                           repo.OnSetValue<T>(configCategory, configName,
                                                              v,
                                                              ref result);

                                           return result;
                                       }, name, value, category);
        }

        /// <inheriteddoc />
        public bool TryGetValue<T>(IEnumerable<char> name, out T value)
        {
            return this.TryGetValue<T>(name, out value, null);
        }

        /// <inheriteddoc />
        public bool TryGetValue(IEnumerable<char> name, out object value)
        {
            return this.TryGetValue(name, out value, null);
        }

        /// <inheriteddoc />
        public bool TryGetValue<T>(IEnumerable<char> name, out T value, IEnumerable<char> category)
        {
            return this.TryGetValue<T>(name, out value, category, default(T));
        }

        /// <inheriteddoc />
        public bool TryGetValue(IEnumerable<char> name, out object value, IEnumerable<char> category)
        {
            return this.TryGetValue(name, out value, category, null);
        }

        /// <inheriteddoc />
        public bool TryGetValue(IEnumerable<char> name, out object value, IEnumerable<char> category, object defaultVal)
        {
            return this.TryGetValue<object>(name,
                                            out value,
                                            category,
                                            defaultVal);
        }

        /// <inheriteddoc />
        public bool TryGetValue<T>(IEnumerable<char> name, out T value, IEnumerable<char> category, T defaultVal)
        {
            T temp = defaultVal;
            bool result = this.InvokeRepoFunc(delegate(ConfigRepositoryBase repo, IEnumerable<char> n, IEnumerable<char> c, T dv)
                                              {
                                                  repo.ThrowIfNotReadable();

                                                  bool r = false;

                                                  string configCategory;
                                                  string configName;
                                                  repo.PrepareCategoryAndName(c, n,
                                                                              out configCategory, out configName);

                                                  T foundValue = default(T);
                                                  repo.OnTryGetValue<T>(configCategory, configName,
                                                                        ref foundValue,
                                                                        ref r);

                                                  if (r)
                                                  {
                                                      temp = foundValue;
                                                  }
                                                  else
                                                  {
                                                      // not found => use default
                                                      temp = defaultVal;
                                                  }

                                                  return r;
                                              }, name, category, defaultVal);

            value = temp;
            return result;
        }

        // Protected Methods (17) 

        /// <summary>
        /// Creates a key/value pair collection for a new category.
        /// </summary>
        /// <param name="category">The name of the category.</param>
        /// <returns>The dictionary of the category or <see langword="null" /> if no category should be created.</returns>
        protected virtual IDictionary<string, object> CreateEmptyDictionaryForCategory(string category)
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Converts an object that is stored in that repository to a target type.
        /// </summary>
        /// <typeparam name="T">Target type of the value.</typeparam>
        /// <param name="input">The input value.</param>
        /// <param name="category">The category from where the input value comes from.</param>
        /// <param name="name">The name where the input value is stored in.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="input" /> could not be converted.</exception>
        protected virtual T FromConfigValue<T>(string category, string name, object input)
        {
            if (DBNull.Value.Equals(input))
            {
                input = null;
            }

            return GlobalConverter.Current.ChangeType<T>(input);
        }

        /// <summary>
        /// Invokes a function for that repository.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeRepoFunc<TResult>(Func<ConfigRepositoryBase, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeRepoFunc<Func<ConfigRepositoryBase, TResult>, TResult>(
                   delegate(ConfigRepositoryBase r, Func<ConfigRepositoryBase, TResult> f)
                   {
                       return f(r);
                   }, func);
        }

        /// <summary>
        /// Invokes a function for that repository.
        /// </summary>
        /// <typeparam name="T1">Type of the 1st argument.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="arg1">The 1st argument.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeRepoFunc<T1, TResult>(Func<ConfigRepositoryBase, T1, TResult> func,
                                                      T1 arg1)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeRepoFunc<T1, Func<ConfigRepositoryBase, T1, TResult>, TResult>(
                   delegate(ConfigRepositoryBase r, T1 a1, Func<ConfigRepositoryBase, T1, TResult> f)
                   {
                       return f(r, a1);
                   }, arg1, func);
        }

        /// <summary>
        /// Invokes a function for that repository.
        /// </summary>
        /// <typeparam name="T1">Type of the 1st argument.</typeparam>
        /// <typeparam name="T2">Type of the 2nd argument.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="arg1">The 1st argument.</param>
        /// <param name="arg2">The 2nd argument.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeRepoFunc<T1, T2, TResult>(Func<ConfigRepositoryBase, T1, T2, TResult> func,
                                                          T1 arg1, T2 arg2)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeRepoFunc<T1, T2, Func<ConfigRepositoryBase, T1, T2, TResult>, TResult>(
                delegate(ConfigRepositoryBase r, T1 a1, T2 a2, Func<ConfigRepositoryBase, T1, T2, TResult> f)
                {
                    return f(r, a1, a2);
                }, arg1, arg2, func);
        }
        
        /// <summary>
        /// Invokes a function for that repository.
        /// </summary>
        /// <typeparam name="T1">Type of the 1st argument.</typeparam>
        /// <typeparam name="T2">Type of the 2nd argument.</typeparam>
        /// <typeparam name="T3">Type of the 3rd argument.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="arg1">The 1st argument.</param>
        /// <param name="arg2">The 2nd argument.</param>
        /// <param name="arg3">The 3rd argument.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeRepoFunc<T1, T2, T3, TResult>(Func<ConfigRepositoryBase, T1, T2, T3, TResult> func,
                                                              T1 arg1, T2 arg2, T3 arg3)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeRepoFunc<T1, T2, T3, Func<ConfigRepositoryBase, T1, T2, T3, TResult>, TResult>(
                delegate(ConfigRepositoryBase r, T1 a1, T2 a2, T3 a3, Func<ConfigRepositoryBase, T1, T2, T3, TResult> f)
                {
                    return f(r, a1, a2, a3);
                }, arg1, arg2, arg3, func);
        }

        /// <summary>
        /// Invokes a function for that repository.
        /// </summary>
        /// <typeparam name="T1">Type of the 1st argument.</typeparam>
        /// <typeparam name="T2">Type of the 2nd argument.</typeparam>
        /// <typeparam name="T3">Type of the 3rd argument.</typeparam>
        /// <typeparam name="T4">Type of the 4th argument.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="arg1">The 1st argument.</param>
        /// <param name="arg2">The 2nd argument.</param>
        /// <param name="arg3">The 3rd argument.</param>
        /// <param name="arg4">The 4th argument.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeRepoFunc<T1, T2, T3, T4, TResult>(Func<ConfigRepositoryBase, T1, T2, T3, T4, TResult> func,
                                                                  T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            RepoFunc<T1, T2, T3, T4, TResult> funcToInvoke;
            if (this._IS_THREAD_SAFE)
            {
                funcToInvoke = this.InvokeRepoAction_ThreadSafe;
            }
            else
            {
                funcToInvoke = this.InvokeRepoAction_NonThreadSafe;
            }

            return funcToInvoke(func,
                                arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.Clear()" /> method.
        /// </summary>
        /// <param name="wasCleared">Was cleared or not.</param>
        protected abstract void OnClear(ref bool wasCleared);

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.ContainsValue(IEnumerable{char}, IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="exists">
        /// Defines if value was found or not.
        /// That value is <see langword="false" /> by default.
        /// </param>
        protected virtual void OnContainsValue(string category, string name, ref bool exists)
        {
            object dummy;
            exists = this.TryGetValue(name, out dummy, category);
        }

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.DeleteValue(IEnumerable{char}, IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="deleted">
        /// Defines if value was deleted or not.
        /// That value is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnDeleteValue(string category, string name, ref bool deleted);

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.GetCategoryNames()" />
        /// </summary>
        /// <param name="names">The target list where the names should be stored.</param>
        protected abstract void OnGetCategoryNames(ICollection<IEnumerable<char>> names);

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.SetValue{T}(IEnumerable{char}, T, IEnumerable{char})" /> method.
        /// </summary>
        /// <typeparam name="T">Type of rhe value to set.</typeparam>
        /// <param name="category">The category where the value should be stored to.</param>
        /// <param name="name">The name in the category where the value should be stored to.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="valueWasSet">
        /// Defines if value was set or not.
        /// Is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet);

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.TryGetValue{T}(IEnumerable{char}, out T, IEnumerable{char}, T)" /> method.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="foundValue">Defines the found value.</param>
        /// <param name="valueWasFound">
        /// Defines if value was found or not.
        /// Is <see langword="false" /> bye default.
        /// </param>
        protected abstract void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound);

        /// <summary>
        /// Parses a category and name value for use in this config repository.
        /// </summary>
        /// <param name="category">The category value to parse.</param>
        /// <param name="name">The name value to parse.</param>
        /// <param name="newCategory">The target category value.</param>
        /// <param name="newName">The target name value.</param>
        protected virtual void PrepareCategoryAndName(IEnumerable<char> category, IEnumerable<char> name,
                                                      out string newCategory, out string newName)
        {
            newCategory = (StringHelper.AsString(category) ?? string.Empty).ToLower().Trim();
            if (newCategory == string.Empty)
            {
                newCategory = null;
            }

            newName = (StringHelper.AsString(name) ?? string.Empty).ToLower().Trim();
            if (newName == string.Empty)
            {
                newName = null;
            }
        }

        /// <summary>
        /// Throws an exception if that repository is not readable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        protected void ThrowIfNotReadable()
        {
            if (this.CanRead == false)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Throws an exception if that repository is not written.
        /// </summary>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        protected void ThrowIfNotWritable()
        {
            if (this.CanWrite == false)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Converts a value
        /// </summary>
        /// <param name="category">The category where the input value should be stored in.</param>
        /// <param name="name">The name in the category where the input value should be stored in.</param>
        /// <param name="input">The input value</param>
        /// <returns></returns>
        protected virtual object ToConfigValue(string category, string name, object input)
        {
            object result = input;
            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        // Private Methods (2) 

        private TResult InvokeRepoAction_NonThreadSafe<T1, T2, T3, T4, TResult>(Func<ConfigRepositoryBase, T1, T2, T3, T4, TResult> func,
                                                                                T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return func(this,
                        arg1, arg2, arg3, arg4);
        }

        private TResult InvokeRepoAction_ThreadSafe<T1, T2, T3, T4, TResult>(Func<ConfigRepositoryBase, T1, T2, T3, T4, TResult> func,
                                                                             T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            TResult result;

            lock (this._SYNC)
            {
                result = this.InvokeRepoAction_NonThreadSafe<T1, T2, T3, T4, TResult>(func,
                                                                                      arg1, arg2, arg3, arg4);
            }

            return result;
        }

        #endregion Methods
    }
}