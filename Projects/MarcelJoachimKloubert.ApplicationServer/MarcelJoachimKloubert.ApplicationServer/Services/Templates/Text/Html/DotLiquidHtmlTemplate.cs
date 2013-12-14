// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html;

namespace MarcelJoachimKloubert.ApplicationServer.Services.Templates.Text.Html
{
    internal sealed partial class DotLiquidHtmlTemplate : HtmlTemplateBase
    {
        #region Fields (1)

        private readonly string _SOURCE;

        #endregion Fields

        #region Constructors (1)

        internal DotLiquidHtmlTemplate(IEnumerable<char> src)
        {
            this.InitFilters();

            this._SOURCE = src.AsString();
        }

        #endregion Constructors

        #region Properties (1)

        internal ICollection<Type> Filters
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Protected Methods (1) 

        protected override void OnRender(ref StringBuilder builder)
        {
            if (this._SOURCE == null)
            {
                builder = null;
                return;
            }

            var @params = new RenderParameters();
            @params.Filters = this.Filters;

            var tpl = Template.Parse(this._SOURCE);
            foreach (var item in this.GetAllVars())
            {
                tpl.Assigns.Add(item.Key, ToLiquidObject(item.Value));
            }

            var renderedSrc = tpl.Render(@params);
            if (renderedSrc != null)
            {
                builder.Append(renderedSrc);
            }
            else
            {
                builder = null;
            }
        }
        // Private Methods (2) 

        private void InitFilters()
        {
            this.Filters = new SynchronizedCollection<Type>();

            this.Filters.Add(typeof(CommonFilters));
        }

        private static object ToLiquidObject(object obj)
        {
            var result = obj;

            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        #endregion Methods
    }
}
