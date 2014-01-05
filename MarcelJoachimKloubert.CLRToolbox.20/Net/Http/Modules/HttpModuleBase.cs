// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Resources;

namespace MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules
{
    /// <summary>
    /// A basic HTTP module.
    /// </summary>
    public abstract partial class HttpModuleBase : TMObject, IHttpModule
    {
        #region Fields (1)

        private Guid _id;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpModuleBase" /> class.
        /// </summary>
        /// <param name="id">The value for the <see cref="HttpModuleBase.Id" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HttpModuleBase(Guid id, object syncRoot)
            : base(syncRoot)
        {
            this._id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpModuleBase" /> class.
        /// </summary>
        /// <param name="id">The value for the <see cref="HttpModuleBase.Id" /> property.</param>
        protected HttpModuleBase(Guid id)
            : this(id, new object())
        {

        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IIdentifiable.Id" />
        public Guid Id
        {
            get { return this._id; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public virtual string Name
        {
            get { return this.GetType().Name; }
        }

        #endregion Properties

        #region Methods (13)

        // Public Methods (7) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals(other as IIdentifiable);
            }

            if (other is Guid)
            {
                return this.Equals((Guid)other);
            }

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(Guid other)
        {
            return this.Id == other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return StringHelper.AsString(this.OnGetDisplayName(culture));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHttpModule.HandleRequest(IHttpRequestContext)" />
        public IHttpModuleHandleRequestResult HandleRequest(IHttpRequestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpModuleHandleRequestResult result = new HttpModuleHandleRequestResult();
            result.RequestContext = context;

            try
            {
                BeforeHandleRequestContext beforeHandleCtx = new BeforeHandleRequestContext();
                {
                    beforeHandleCtx.Http = context;
                    beforeHandleCtx.InvokeAfterHandleRequest = true;
                    beforeHandleCtx.InvokeHandleRequest = true;

                    this.OnBeforeHandleRequest(beforeHandleCtx);
                }

                HandleRequestContext handleCtx = new HandleRequestContext();
                bool requestWasHandled = false;
                {
                    handleCtx.Http = context;
                    handleCtx.InvokeAfterHandleRequest = beforeHandleCtx.InvokeAfterHandleRequest;

                    if (beforeHandleCtx.InvokeHandleRequest)
                    {
                        this.OnHandleRequest(handleCtx);
                        requestWasHandled = true;
                    }
                }

                if (handleCtx.InvokeAfterHandleRequest)
                {
                    AfterHandleRequestContext afterHandleCtx = new AfterHandleRequestContext();
                    afterHandleCtx.RequestWasHandled = requestWasHandled;
                    {
                        afterHandleCtx.Http = context;

                        this.OnAfterHandleRequest(afterHandleCtx);
                    }
                }

                result.Errors = new Exception[0];
            }
            catch (Exception ex)
            {
                AggregateException aggEx = ex as AggregateException;
                if (aggEx == null)
                {
                    aggEx = new AggregateException(new Exception[] { ex });
                }

                result.Errors = CollectionHelper.AsArray(aggEx.Flatten().InnerExceptions);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IResourceLocator.TryGetResourceStream(IEnumerable{char})" />
        public Stream TryGetResourceStream(IEnumerable<char> resourceName)
        {
            IEnumerable<char> parsedResName = this.ParseResourcesName(StringHelper.AsString(resourceName));

            Stream result = null;
            this.OnTryGetResourceStream(StringHelper.AsString(parsedResName),
                                        ref result);

            return result;
        }
        // Protected Methods (6) 

        /// <summary>
        /// Is invoked AFTER <see cref="HttpModuleBase.OnHandleRequest(IHandleRequestContext)" /> has been called.
        /// </summary>
        /// <param name="context">The underlying context.</param>
        protected virtual void OnAfterHandleRequest(IAfterHandleRequestContext context)
        {
            // dummy
        }

        /// <summary>
        /// Is invoked BEFORE <see cref="HttpModuleBase.OnHandleRequest(IHandleRequestContext)" /> is called.
        /// </summary>
        /// <param name="context">The underlying context.</param>
        protected virtual void OnBeforeHandleRequest(IBeforeHandleRequestContext context)
        {
            // dummy
        }

        /// <summary>
        /// The logic for <see cref="HttpModuleBase.GetDisplayName(CultureInfo)" />.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The display name based on <paramref name="culture" />.</returns>
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return this.Name;
        }

        /// <summary>
        /// The logic for <see cref="HttpModuleBase.HandleRequest(IHttpRequestContext)" /> method.
        /// </summary>
        /// <param name="context">The underlying context.</param>
        protected abstract void OnHandleRequest(IHandleRequestContext context);

        /// <summary>
        /// The logic for <see cref="HttpModuleBase.TryGetResourceStream(IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="resName">The name of the stream.</param>
        /// <param name="stream">The variable where to write the found stream to (if found).</param>
        protected virtual void OnTryGetResourceStream(string resName, ref Stream stream)
        {
            // dummy
        }

        /// <summary>
        /// Parses a resource name.
        /// </summary>
        /// <param name="resName">The input value.</param>
        /// <returns>The parsed value.</returns>
        protected virtual IEnumerable<char> ParseResourcesName(string resName)
        {
            if (StringHelper.IsNullOrWhiteSpace(resName))
            {
                return null;
            }

            return resName.Trim();
        }

        #endregion Methods
    }
}
