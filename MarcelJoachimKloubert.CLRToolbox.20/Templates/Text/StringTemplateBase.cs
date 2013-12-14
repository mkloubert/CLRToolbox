// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Templates.Text
{
    /// <summary>
    /// A basic template that creates string output.
    /// </summary>
    public abstract class StringTemplateBase : TemplateBase, IStringTemplate
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTemplateBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected StringTemplateBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTemplateBase" /> class.
        /// </summary>
        protected StringTemplateBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (7)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IStringTemplate.Render()" />
        public string Render()
        {
            StringBuilder builder = new StringBuilder();
            this.OnRender(ref builder);

            return builder != null ? builder.ToString() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IStringTemplate.RenderTo(StringBuilder)" />
        public void RenderTo(StringBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            this.OnRender(ref builder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TemplateBase.SetVar(IEnumerable{char}, object)" />
        public new StringTemplateBase SetVar(IEnumerable<char> name, object value)
        {
            return (StringTemplateBase)base.SetVar(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TemplateBase.SetVar{T}(IEnumerable{char}, T)" />
        public new StringTemplateBase SetVar<T>(IEnumerable<char> name, T value)
        {
            return (StringTemplateBase)base.SetVar<T>(name, value);
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="StringTemplateBase.Render()" />
        /// and <see cref="StringTemplateBase.RenderTo(StringBuilder)" /> method.
        /// </summary>
        /// <param name="builder">
        /// The builder where to write the rendered content to. If <paramref name="builder" />
        /// is overwritten with <see langword="null" />, it indicates to return also a
        /// <see langword="null" /> reference.
        /// </param>
        protected abstract void OnRender(ref StringBuilder builder);
        // Private Methods (2) 

        IStringTemplate IStringTemplate.SetVar(IEnumerable<char> name, object value)
        {
            return this.SetVar(name, value);
        }

        IStringTemplate IStringTemplate.SetVar<T>(IEnumerable<char> name, T value)
        {
            return this.SetVar<T>(name, value);
        }

        #endregion Methods

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.Render()" />
        protected override sealed object RenderContent()
        {
            return this.Render();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override sealed string ToString()
        {
            return this.Render() ?? string.Empty;
        }
    }
}
