// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.aggregateexception%28v=vs.110%29.aspx" />
    public partial class AggregateException : Exception
    {
        #region Fields (2)

        private readonly ReadOnlyCollection<Exception> _INNER_EXCEPTIONS;
        /// <summary>
        /// Default exception message.
        /// </summary>
        public const string DEFAULT_EXCEPTION_MESSAGE = "At least one exception has been thrown.";

        #endregion Fields

        #region Constructors (5)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414847%28v=vs.110%29.aspx" />
        public AggregateException(string message, params Exception[] innerExceptions)
            : this(message, (IEnumerable<Exception>)innerExceptions)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd414746%28v=vs.110%29.aspx" />
        public AggregateException(string message, IEnumerable<Exception> innerExceptions)
            : base(message)
        {
            if (innerExceptions == null)
            {
                throw new ArgumentNullException("innerExceptions");
            }

            this._INNER_EXCEPTIONS = innerExceptions as ReadOnlyCollection<Exception>;
            if (this._INNER_EXCEPTIONS == null)
            {
                // needs to be converted

                IList<Exception> list = innerExceptions as IList<Exception>;
                if (list == null)
                {
                    list = CollectionHelper.AsArray(innerExceptions);
                }

                this._INNER_EXCEPTIONS = new ReadOnlyCollection<Exception>(list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd383498%28v=vs.110%29.aspx" />
        public AggregateException(IEnumerable<Exception> innerExceptions)
            : this(DEFAULT_EXCEPTION_MESSAGE, innerExceptions)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd386887%28v=vs.110%29.aspx" />
        public AggregateException(params Exception[] innerExceptions)
            : this(DEFAULT_EXCEPTION_MESSAGE, innerExceptions)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd387311%28v=vs.110%29.aspx" />
        public AggregateException()
            : this(new Exception[0])
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<Exception> InnerExceptions
        {
            get
            {
                return this._INNER_EXCEPTIONS;
            }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.aggregateexception.flatten%28v=vs.110%29.aspx" />
        public AggregateException Flatten()
        {
            List<Exception> flattenList = new List<Exception>();

            List<AggregateException> aggExList = new List<AggregateException>();
            aggExList.Add(this);

            int num = 0;
            while (aggExList.Count > num)
            {
                IList<Exception> innerExceptions = aggExList[num++].InnerExceptions;
                for (int i = 0; i < innerExceptions.Count; i++)
                {
                    Exception innerEx = innerExceptions[i];
                    if (innerEx == null)
                    {
                        continue;
                    }

                    AggregateException aggEx = innerEx as AggregateException;
                    if (aggEx != null)
                    {
                        aggExList.Add(aggEx);
                    }
                    else
                    {
                        flattenList.Add(innerEx);
                    }
                }
            }

            return new AggregateException(this.Message, flattenList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.aggregateexception.getbaseexception%28v=vs.110%29.aspx" />
        public override Exception GetBaseException()
        {
            Exception result = this;

            AggregateException baseEx = this;
            while (baseEx != null &&
                   baseEx.InnerExceptions.Count == 1)
            {
                result = result.InnerException;
                baseEx = result as AggregateException;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.aggregateexception.handle%28v=vs.110%29.aspx" />
        public void Handle(Func<Exception, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            List<Exception> list = null;
            for (int i = 0; i < this._INNER_EXCEPTIONS.Count; i++)
            {
                if (!predicate(this._INNER_EXCEPTIONS[i]))
                {
                    if (list == null)
                    {
                        list = new List<Exception>();
                    }

                    list.Add(this._INNER_EXCEPTIONS[i]);
                }
            }

            if (list != null)
            {
                throw new AggregateException(this.Message, list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.aggregateexception.tostring%28v=vs.110%29.aspx" />
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(base.ToString());

            for (int i = 0; i < this._INNER_EXCEPTIONS.Count; i++)
            {
                result.AppendFormat(CultureInfo.InvariantCulture,
                                    "{0}---> (Inner exception #{1}) {2}{3}{4}",
                                    Environment.NewLine,
                                    i,
                                    this._INNER_EXCEPTIONS[i].ToString(),
                                    "<---",
                                    Environment.NewLine);
            }

            return result.ToString();
        }

        #endregion Methods
    }
}
