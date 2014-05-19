// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

#if !(NET2 || NET20 || NET35 || NET4 || NET40 || MONO2 || MONO20 || MONO4 || MONO40 || WINDOWS_PHONE)
#define KNOWS_CALLER_MEMBER_NAME
#endif

#if !(NET2 || NET20 || MONO2 || MONO20)
#define KNOWS_LAMBDA_EXPRESSIONS
#endif

#if !(WINDOWS_PHONE)
#define KNOWS_TYPE_DESCRIPTOR
#endif

#define KNOWS_PROPERTY_CHANGING

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    /// <summary>
    /// A basic notification object.
    /// </summary>
    public abstract class NotificationObjectBase : ErrorHandlerBase, INotificationObject
    {
        #region Fields (1)

        private readonly IDictionary<string, object> _PROPERTY_VALUES;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected NotificationObjectBase(bool invokeOnConstructor, object syncRoot)
            : base(syncRoot)
        {
            this._PROPERTY_VALUES = this.CreatePropertyStorage() ?? new Dictionary<string, object>();

            if (invokeOnConstructor)
            {
                this.OnConstructor();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="invokeOnConstructor">
        /// Invoke <see cref="NotificationObjectBase.OnConstructor()" /> method or not.
        /// </param>
        protected NotificationObjectBase(bool invokeOnConstructor)
            : this(invokeOnConstructor, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected NotificationObjectBase(object syncRoot)
            : this(true, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationObjectBase"/> class.
        /// </summary>
        protected NotificationObjectBase()
            : this(true)
        {
        }

        #endregion Constructors

        #region Delegates and Events (3)

        // Events (2) 

        /// <summary>
        ///
        /// </summary>
        /// <see cref="INotificationObject.Closing" />
        public event CancelEventHandler Closing;

#if KNOWS_PROPERTY_CHANGING

        /// <summary>
        ///
        /// </summary>
        /// <see cref="global::System.ComponentModel.INotifyPropertyChanging.PropertyChanging" />
        public event global::System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

#endif

        /// <summary>
        ///
        /// </summary>
        /// <see cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Delegates and Events

        #region Properties (1)

        /// <inheriteddoc />
        public override object Tag
        {
            get { return this.Get<object>("Tag"); }

            set { this.Set(value, "Tag"); }
        }

        #endregion Properties

        #region Methods (27)

        // Public Methods (1)

        /// <summary>
        /// Returns the current list of properties and their values as NEW dictionary.
        /// </summary>
        /// <returns>The new list of properties and their values.</returns>
        public virtual IDictionary<string, object> GetPropertyValues()
        {
            IDictionary<string, object> result = null;
            this.InvokeForPropertyStorage(delegate(IDictionary<string, object> propertyValues, NotificationObjectBase obj)
                                          {
                                              result = obj.CreatePropertyStorage();

                                              CollectionHelper.ForEach(propertyValues,
                                                                       delegate(IForEachItemExecutionContext<KeyValuePair<string, object>, IDictionary<string, object>> ctx)
                                                                       {
                                                                           IDictionary<string, object> r = ctx.State;
                                                                           KeyValuePair<string, object> item = ctx.Item;

                                                                           r.Add(item.Key,
                                                                                 item.Value);
                                                                       }, result);
                                          }, this);

            return result;
        }

        // Protected Methods (22) 

        /// <summary>
        /// Creates a new dictionary especially for use in <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <returns>The created instance.</returns>
        protected virtual IDictionary<string, object> CreatePropertyStorage()
        {
            // create default type
            return null;
        }

#if KNOWS_LAMBDA_EXPRESSIONS

        /// <summary>
        /// Reads a value from <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="expr">The property expression from where to get the property name from.</param>
        /// <returns>The value or <see langword="null" /> if not found.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> is NO member expression.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// <paramref name="expr" /> does NOT represent a property expression.
        /// </exception>
        protected T Get<T>(global::System.Linq.Expressions.Expression<global::System.Func<T>> expr)
        {
            return this.Get<T>(GetPropertyName<T>(expr));
        }

#endif

        /// <summary>
        /// Reads a value from <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value or <see langword="null" /> if not found.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="propertyName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        protected T Get<T>(
#if KNOWS_CALLER_MEMBER_NAME
                           [global::System.Runtime.CompilerServices.CallerMemberName]
                           global::System.Collections.Generic.IEnumerable<char> propertyName = null
#else
                           global::System.Collections.Generic.IEnumerable<char> propertyName
#endif
)
        {
            object value = this.GetPropertyValue(propertyName);

            return global::MarcelJoachimKloubert.CLRToolbox.Data.GlobalConverter
                                                                .Current
                                                                .ChangeType<T>(value);
        }

#if KNOWS_LAMBDA_EXPRESSIONS

        private static string GetPropertyName<T>(global::System.Linq.Expressions.Expression<global::System.Func<T>> expr)
        {
            if (expr == null)
            {
                throw new global::System.ArgumentNullException("expr");
            }

            var memberExpr = expr.Body as global::System.Linq.Expressions.MemberExpression;
            if (memberExpr == null)
            {
                throw new global::System.ArgumentException("expr.Body");
            }

            var property = memberExpr.Member as global::System.Reflection.PropertyInfo;
            if (property == null)
            {
                throw new global::System.InvalidCastException("expr.Body.Member");
            }

            return property.Name;
        }

#endif

        /// <summary>
        /// Reads a value from <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value or <see langword="null" /> if not found.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="propertyName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        protected object GetPropertyValue(IEnumerable<char> propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            string pn = StringHelper.AsString(propertyName).Trim();
            if (pn == null)
            {
                throw new ArgumentException("propertyName");
            }

            return this.InvokeForPropertyStorage(delegate(IDictionary<string, object> propertyValues)
                                                 {
                                                     object value;
                                                     propertyValues.TryGetValue(pn, out value);

                                                     return value;
                                                 });
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeForPropertyStorage(Action<IDictionary<string, object>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForPropertyStorage<object>(delegate(IDictionary<string, object> dict)
                                                  {
                                                      action(dict);
                                                      return null;
                                                  });
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <returns>The result of <paramref name="func" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeForPropertyStorage<TResult>(Func<IDictionary<string, object>, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return this.InvokeForPropertyStorage<object, TResult>(delegate(IDictionary<string, object> dict, object state)
                                                                  {
                                                                      return func(dict);
                                                                  },
                                                                  (object)null);
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="S">Type of the state object for <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionState">
        /// The state object for <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> is <see langword="null" />.
        /// </exception>
        protected void InvokeForPropertyStorage<S>(Action<IDictionary<string, object>, S> action, S actionState)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForPropertyStorage<S, object>(delegate(IDictionary<string, object> dict, S state)
                                                     {
                                                         action(dict, state);
                                                         return null;
                                                     }, actionState);
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="S">Type of the state object for <paramref name="action" />.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <param name="actionStateFactory">
        /// The function that creates the state object for <paramref name="action" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action" /> and/or <paramref name="actionStateFactory" /> are <see langword="null" />.
        /// </exception>
        protected void InvokeForPropertyStorage<S>(Action<IDictionary<string, object>, S> action, Func<S> actionStateFactory)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.InvokeForPropertyStorage<S, object>(delegate(IDictionary<string, object> dict, S state)
                                                     {
                                                         action(dict, state);
                                                         return null;
                                                     }, actionStateFactory);
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="S">Type of the state object for <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcState">
        /// The state object for <paramref name="func" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected TResult InvokeForPropertyStorage<S, TResult>(Func<IDictionary<string, object>, S, TResult> func, S funcState)
        {
            return this.InvokeForPropertyStorage<S, TResult>(func,
                                                             delegate()
                                                             {
                                                                 return funcState;
                                                             });
        }

        /// <summary>
        /// Invokes logic for the <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="S">Type of the state object for <paramref name="func" />.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="func">The function to invoke.</param>
        /// <param name="funcStateFactory">
        /// The function that creates the state object for <paramref name="func" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="func" /> is <see langword="null" />.
        /// </exception>
        protected virtual TResult InvokeForPropertyStorage<S, TResult>(Func<IDictionary<string, object>, S, TResult> func, Func<S> funcStateFactory)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (funcStateFactory == null)
            {
                throw new ArgumentNullException("actionStateFactory");
            }

            TResult result;

            lock (this._SYNC)
            {
                result = func(this._PROPERTY_VALUES,
                              funcStateFactory());
            }

            return result;
        }

        /// <summary>
        /// Is voked after object has been closed.
        /// </summary>
        protected virtual void OnClosed()
        {
            // dummy
        }

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.Closing" /> event.
        /// </summary>
        /// <returns>Event was raised or not.</returns>
        protected bool OnClosing()
        {
            return this.OnClosing(false);
        }

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.Closing" /> event.
        /// </summary>
        /// <param name="cancel">The start value for <see cref="CancelEventArgs.Cancel" /> property.</param>
        /// <returns>Event was raised or not.</returns>
        protected bool OnClosing(bool cancel)
        {
            bool result = false;

            bool hasCanceled = false;
            CancelEventHandler handler = this.Closing;
            if (handler != null)
            {
                CancelEventArgs e = new CancelEventArgs(cancel);
                handler(this, e);

                hasCanceled = e.Cancel;
                result = true;
            }

            if (!hasCanceled)
            {
                this.OnClosed();
            }

            return result;
        }

        /// <summary>
        /// Contains additional logic for the constructor.
        /// </summary>
        protected virtual void OnConstructor()
        {
            // dummy
        }

#if KNOWS_PROPERTY_CHANGING

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanging" /> event.
        /// </summary>
        /// <param name="propertyName">The name of the property to raise the event for.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="global::System.ArgumentException"><paramref name="propertyName" /> is <see langword="null" />.</exception>
        /// <exception cref="global::System.ArgumentNullException">
        /// <paramref name="propertyName" /> has an invalid format.
        /// </exception>
        protected bool OnPropertyChanging(IEnumerable<char> propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            string pn = global::MarcelJoachimKloubert.CLRToolbox.Helpers.StringHelper.AsString(propertyName).Trim();
            if (pn == string.Empty)
            {
                throw new global::System.ArgumentException("propertyName");
            }

#if KNOWS_TYPE_DESCRIPTOR && DEBUG

            if (global::System.ComponentModel.TypeDescriptor.GetProperties(this)[pn] == null)
            {
                throw new global::System.MissingMemberException(this.GetType().FullName,
                                                                pn);
            }

#endif

            global::System.ComponentModel.PropertyChangingEventHandler handler = this.PropertyChanging;
            if (handler != null)
            {
                handler(this, new global::System.ComponentModel.PropertyChangingEventArgs(pn));
                return true;
            }

            // no delegate linked
            return false;
        }

#endif

#if KNOWS_LAMBDA_EXPRESSIONS

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanged" /> event by using a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="T">Type of the underlying property.</typeparam>
        /// <param name="expr">The property expression.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="global::System.ArgumentException">
        /// <paramref name="expr" /> contains no <see cref="global::System.Linq.Expressions.MemberExpression" />.
        /// </exception>
        /// <exception cref="global::System.ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="global::System.InvalidCastException">
        /// <paramref name="expr" /> is no no property expression.
        /// </exception>
        protected bool OnPropertyChanged<T>(global::System.Linq.Expressions.Expression<global::System.Func<T>> expr)
        {
            return this.OnPropertyChanged(GetPropertyName<T>(expr));
        }

#if KNOWS_PROPERTY_CHANGING

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanging" /> event by using a LINQ compiler expression.
        /// </summary>
        /// <typeparam name="T">Type of the underlying property.</typeparam>
        /// <param name="expr">The property expression.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="global::System.ArgumentException">
        /// <paramref name="expr" /> contains no <see cref="global::System.Linq.Expressions.MemberExpression" />.
        /// </exception>
        /// <exception cref="global::System.ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="global::System.InvalidCastException">
        /// <paramref name="expr" /> is no no property expression.
        /// </exception>
        protected bool OnPropertyChanging<T>(global::System.Linq.Expressions.Expression<global::System.Func<T>> expr)
        {
            return this.OnPropertyChanging(GetPropertyName<T>(expr));
        }

#endif

        /// <summary>
        /// Writes a value to <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="expr">The property expression from where to get the property name from.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Old and new value are different or not.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="expr" /> is NO member expression.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expr" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// <paramref name="expr" /> does NOT represent a property expression.
        /// </exception>
        protected bool Set<T>(T value,
                              global::System.Linq.Expressions.Expression<global::System.Func<T>> expr)
        {
            return this.Set<T>(value,
                               GetPropertyName<T>(expr));
        }

#endif

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanged" /> event.
        /// </summary>
        /// <param name="propertyName">The name of the property to raise the event for.</param>
        /// <returns>Event was raised or not because no delegate is linked with it.</returns>
        /// <exception cref="ArgumentException"><paramref name="propertyName" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="propertyName" /> has an invalid format.
        /// </exception>
        protected bool OnPropertyChanged(IEnumerable<char> propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            string pn = StringHelper.AsString(propertyName).Trim();
            if (pn == string.Empty)
            {
                throw new ArgumentException("propertyName");
            }

#if KNOWS_TYPE_DESCRIPTOR && DEBUG

            if (global::System.ComponentModel.TypeDescriptor.GetProperties(this)[pn] == null)
            {
                throw new global::System.MissingMemberException(this.GetType().FullName,
                                                                pn);
            }

#endif

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(pn));
                return true;
            }

            // no delegate linked
            return false;
        }

        /// <summary>
        /// Writes a value to <see cref="NotificationObjectBase._PROPERTY_VALUES" /> field.
        /// </summary>
        /// <typeparam name="T">The type of the (new) value.</typeparam>
        /// <param name="value">The value to set.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Old and new value are different or not.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="propertyName" /> is invalid.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="propertyName" /> is <see langword="null" />.
        /// </exception>
        protected bool Set<T>(T value,
#if KNOWS_CALLER_MEMBER_NAME
                              [global::System.Runtime.CompilerServices.CallerMemberName]
                              global::System.Collections.Generic.IEnumerable<char> propertyName = null
#else
                              global::System.Collections.Generic.IEnumerable<char> propertyName
#endif
)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            string pn = StringHelper.AsString(propertyName).Trim();
            if (pn == null)
            {
                throw new ArgumentException("propertyName");
            }

            object oldValue = null;
            bool result = this.InvokeForPropertyStorage(delegate(IDictionary<string, object> propertyValues)
                {
                    bool areDifferent = true;

                    if (propertyValues.TryGetValue(pn, out oldValue))
                    {
                        if (object.Equals(value, oldValue))
                        {
                            areDifferent = false;
                        }
                    }

                    if (areDifferent)
                    {
#if KNOWS_PROPERTY_CHANGING
                        this.OnPropertyChanging(pn);
#endif
                        propertyValues[pn] = value;
                        this.OnPropertyChanged(pn);
                    }

                    return areDifferent;
                });

            this.Handle_ReceiveValueFrom<T>(pn, result, value, oldValue);
            this.Handle_ReceiveNotificationFrom(pn, result);

            return result;
        }

        /// <summary>
        /// Raises the <see cref="NotificationObjectBase.PropertyChanged" /> and <see cref="NotificationObjectBase.PropertyChanging" />
        /// events if two values are different and automatically overwrites the underlying field.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">The field where the (current) value of the property is stored.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">
        /// Name of the property. Should be automatically be set by compiler if that
        /// method is called from inside the underlying property.
        /// </param>
        /// <returns>
        /// <paramref name="field" /> and <paramref name="newValue" /> are different and events were raised.
        /// </returns>
        protected bool SetProperty<T>(ref T field,
                                      T newValue,
#if KNOWS_CALLER_MEMBER_NAME
                                      [global::System.Runtime.CompilerServices.CallerMemberName]
                                      global::System.Collections.Generic.IEnumerable<char> propertyName = null
#else
                                      global::System.Collections.Generic.IEnumerable<char> propertyName
#endif
        )
        {
            if (object.Equals(field, newValue) == false)
            {
#if KNOWS_PROPERTY_CHANGING
                this.OnPropertyChanging(propertyName);
#endif
                field = newValue;
                this.OnPropertyChanged(propertyName);

                return true;
            }

            // both are the same
            return false;
        }

        // Private Methods (4) 

        private void Handle_ReceiveNotificationFrom(string nameOfSender, bool hasBeenNotified)
        {
            IEnumerable<PropertyInfo> receiveFromProperties =
                CollectionHelper.Where(this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
                           delegate(PropertyInfo property)
                           {
                               // search for 'ReceiveNotificationFromAttribute'
                               object[] attribs = property.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.ComponentModel.ReceiveNotificationFromAttribute),
                                                                               true);

                               // strong typed sequence
                               IEnumerable<ReceiveNotificationFromAttribute> receiveFromAttribs =
                                   CollectionHelper.OfType<ReceiveNotificationFromAttribute>(attribs);

                               IEnumerable<ReceiveNotificationFromAttribute> receiveFromAttribsForProperty =
                                   CollectionHelper.Where(receiveFromAttribs,
                                                          delegate(ReceiveNotificationFromAttribute a)
                                                          {
                                                              if (a.SenderName != nameOfSender)
                                                              {
                                                                  return false;
                                                              }

                                                              ReceiveNotificationFromOptions aOpts = a.Options;

                                                              if (HasOptionFlag(aOpts, ReceiveNotificationFromOptions.Default))
                                                              {
                                                                  aOpts = ReceiveNotificationFromOptions.IfDifferent;
                                                              }

                                                              if (HasOptionFlag(aOpts, ReceiveNotificationFromOptions.IfEqual))
                                                              {
                                                                  // 'has been notified' means: different values
                                                                  if (hasBeenNotified == false)
                                                                  {
                                                                      return true;
                                                                  }
                                                              }

                                                              if (HasOptionFlag(aOpts, ReceiveNotificationFromOptions.IfDifferent))
                                                              {
                                                                  // 'has been notified' means: different values
                                                                  if (hasBeenNotified)
                                                                  {
                                                                      return true;
                                                                  }
                                                              }

                                                              return false;
                                                          });

                               // only if there is at least one attribute
                               // that has the 'SenderName' of 'nameOfSender'
                               return CollectionHelper.Any(receiveFromAttribsForProperty);
                           });

            CollectionHelper.ForAll(receiveFromProperties,
                                    delegate(IForAllItemExecutionContext<PropertyInfo> ctx)
                                    {
                                        PropertyInfo prop = ctx.Item;

#if KNOWS_PROPERTY_CHANGING
                                        this.OnPropertyChanging(prop.Name);
#endif
                                        this.OnPropertyChanged(prop.Name);
                                    });
        }

        private void Handle_ReceiveValueFrom<T>(string nameOfSender, bool hasBeenNotified, T newValue, object oldValue)
        {
            BindingFlags memberBindFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                           BindingFlags.Instance | BindingFlags.Static;

            IEnumerable<MemberInfo> members = CollectionHelper.Concat<MemberInfo>(this.GetType().GetFields(memberBindFlags),
                                                                                  this.GetType().GetMethods(memberBindFlags),
                                                                                  this.GetType().GetProperties(memberBindFlags));

            IEnumerable<MemberInfo> receiveFromMembers =
                CollectionHelper.Where(members,
                                       delegate(MemberInfo member)
                                       {
                                           // search for 'ReceiveValueFromAttribute'
                                           object[] attribs = member.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.ComponentModel.ReceiveValueFromAttribute),
                                                                                         true);

                                           // strong typed sequence
                                           IEnumerable<ReceiveValueFromAttribute> receiveFromAttribs =
                                               CollectionHelper.OfType<ReceiveValueFromAttribute>(attribs);

                                           IEnumerable<ReceiveValueFromAttribute> receiveFromAttribsForMember =
                                               CollectionHelper.Where(receiveFromAttribs,
                                                                      delegate(ReceiveValueFromAttribute a)
                                                                      {
                                                                          if (a.SenderName != nameOfSender)
                                                                          {
                                                                              return false;
                                                                          }

                                                                          ReceiveValueFromOptions aOpts = a.Options;

                                                                          if (HasOptionFlag(aOpts, ReceiveValueFromOptions.Default))
                                                                          {
                                                                              aOpts = ReceiveValueFromOptions.IfDifferent;
                                                                          }

                                                                          if (HasOptionFlag(aOpts, ReceiveValueFromOptions.IfEqual))
                                                                          {
                                                                              // 'has been notified' means: different values
                                                                              if (hasBeenNotified == false)
                                                                              {
                                                                                  return true;
                                                                              }
                                                                          }

                                                                          if (HasOptionFlag(aOpts, ReceiveValueFromOptions.IfDifferent))
                                                                          {
                                                                              // 'has been notified' means: different values
                                                                              if (hasBeenNotified)
                                                                              {
                                                                                  return true;
                                                                              }
                                                                          }

                                                                          return false;
                                                                      });

                                           // only if there is at least one attribute
                                           // that has the 'SenderName' of 'nameOfSender'
                                           return CollectionHelper.Any(receiveFromAttribsForMember);
                                       });

            CollectionHelper.ForAll(receiveFromMembers,
                                    delegate(IForAllItemExecutionContext<MemberInfo> ctx)
                                    {
                                        MemberInfo m = ctx.Item;
                                        Type valueType = typeof(T);

                                        if (m is FieldInfo)
                                        {
                                            FieldInfo field = (FieldInfo)m;

                                            field.SetValue(this, newValue);
                                        }
                                        else if (m is MethodBase)
                                        {
                                            MethodBase method = (MethodBase)m;
                                            object[] invokationParams;

                                            ParameterInfo[] methodParams = method.GetParameters();
                                            if (methodParams.Length < 1)
                                            {
                                                // no parameters
                                                invokationParams = new object[0];
                                            }
                                            else if (methodParams.Length == 1)
                                            {
                                                // (T newValue)
                                                invokationParams = new object[] { newValue };
                                            }
                                            else if (methodParams.Length == 2)
                                            {
                                                // (T newValue, T oldValue)
                                                invokationParams = new object[] { newValue, oldValue };
                                            }
                                            else if (methodParams.Length == 3)
                                            {
                                                // (T newValue, T oldValue, string senderName)
                                                invokationParams = new object[] { newValue, oldValue, nameOfSender };
                                            }
                                            else if (methodParams.Length == 4)
                                            {
                                                // (T newValue, T oldValue, string senderName, NotificationObjectBase obj)
                                                invokationParams = new object[] { newValue, oldValue, nameOfSender, this };
                                            }
                                            else
                                            {
                                                // (T newValue, T oldValue, string senderName, NotificationObjectBase obj, MemberTypes senderType)
                                                invokationParams = new object[] { newValue, oldValue, nameOfSender, this, MemberTypes.Property };
                                            }

                                            method.Invoke(this,
                                                          invokationParams);
                                        }
                                        else if (m is PropertyInfo)
                                        {
                                            PropertyInfo property = (PropertyInfo)m;

                                            property.SetValue(this, newValue, null);
                                        }
                                    });
        }

        private static bool HasOptionFlag(ReceiveNotificationFromOptions opts, ReceiveNotificationFromOptions value)
        {
            return (opts == value) ||
                   ((opts & value) != 0);
        }

        private static bool HasOptionFlag(ReceiveValueFromOptions opts, ReceiveValueFromOptions value)
        {
            return (opts == value) ||
                   ((opts & value) != 0);
        }

        #endregion Methods
    }
}