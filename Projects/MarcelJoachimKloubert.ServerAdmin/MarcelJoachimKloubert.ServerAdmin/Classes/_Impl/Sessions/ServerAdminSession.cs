// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.ServerAdmin.Classes.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace MarcelJoachimKloubert.ServerAdmin.Classes._Impl.Sessions
{
    [Export(typeof(global::MarcelJoachimKloubert.ServerAdmin.Classes.Sessions.ISession))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class ServerAdminSession : TMObject, ISession
    {
        #region Constructors (1)

        [ImportingConstructor]
        internal ServerAdminSession()
        {

        }

        #endregion Constructors

        #region Properties (3)

        internal HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public Guid Id
        {
            get { return (Guid?)this.Context.Session[Global.SESSION_VAR_SESSIONID] ?? Guid.Empty; }
        }

        public string SystemId
        {
            get { return this.Context.Session.SessionID; }
        }

        #endregion Properties

        #region Methods (13)

        // Public Methods (11) 

        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        public bool Equals(Guid other)
        {
            return other == this.Id;
        }

        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals((IIdentifiable)other);
            }

            if (other is Guid)
            {
                return this.Equals((Guid)other);
            }

            return base.Equals(other);
        }

        public object Get(IEnumerable<char> name)
        {
            return this.Get<object>(name);
        }

        public T Get<T>(IEnumerable<char> name)
        {
            T result;
            if (this.TryGet<T>(name, out result) == false)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Has(IEnumerable<char> name)
        {
            return this.TryGetRealSessionKey(name) != null;
        }

        public bool Remove(IEnumerable<char> name)
        {
            try
            {
                this.Context
                    .Session
                    .Remove(ParseVarName(name));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ISession Set(IEnumerable<char> name, object value)
        {
            this.Context
                .Session[ParseVarName(name)] = value;

            return this;
        }

        public bool TryGet(IEnumerable<char> name, out object value, object defValue = null)
        {
            return this.TryGet<object>(name: name,
                                       value: out value,
                                       defValue: defValue);
        }

        public bool TryGet<T>(IEnumerable<char> name, out T value, T defValue = default(T))
        {
            var key = this.TryGetRealSessionKey(name);
            if (key != null)
            {
                value = GlobalConverter.Current
                                       .ChangeType<T>(this.Context.Session[key]);

                return true;
            }

            value = defValue;
            return false;
        }
        // Private Methods (2) 

        private static string ParseVarName(IEnumerable<char> name, bool ignoreReservedNames = false)
        {
            var result = (name.AsString() ?? string.Empty).ToUpper().Trim();
            if (result == string.Empty)
            {
                throw new ArgumentException("name");
            }

            switch (result)
            {
                case Global.SESSION_VAR_SESSIONID:
                    if (ignoreReservedNames == false)
                    {
                        throw new ArgumentException("name");
                    }
                    break;
            }

            return result;
        }

        private string TryGetRealSessionKey(IEnumerable<char> name)
        {
            var strName = ParseVarName(name, false);

            return CollectionHelper.SingleOrDefault(this.Context.Session.Keys.Cast<string>(),
                                                    k => ParseVarName(k, true) == strName);
        }

        #endregion Methods
    }
}
