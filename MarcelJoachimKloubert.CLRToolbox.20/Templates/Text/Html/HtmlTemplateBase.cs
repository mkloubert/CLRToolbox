// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html
{
    /// <summary>
    /// A basic template that renders HTML content.
    /// </summary>
    public abstract class HtmlTemplateBase : StringTemplateBase, IHtmlTemplate
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTemplateBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected HtmlTemplateBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTemplateBase" /> class.
        /// </summary>
        protected HtmlTemplateBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TemplateBase.SetVar(IEnumerable{char}, object)" />
        public new HtmlTemplateBase SetVar(IEnumerable<char> name, object value)
        {
            return (HtmlTemplateBase)base.SetVar(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TemplateBase.SetVar{T}(IEnumerable{char}, T)" />
        public new HtmlTemplateBase SetVar<T>(IEnumerable<char> name, T value)
        {
            return (HtmlTemplateBase)base.SetVar<T>(name, value);
        }
        // Private Methods (2) 

        IHtmlTemplate IHtmlTemplate.SetVar(IEnumerable<char> name, object value)
        {
            return this.SetVar(name, value);
        }

        IHtmlTemplate IHtmlTemplate.SetVar<T>(IEnumerable<char> name, T value)
        {
            return this.SetVar<T>(name, value);
        }

        #endregion Methods
    }
}
