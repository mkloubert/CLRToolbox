// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Templates.Text.Html
{
    /// <summary>
    /// Describes a template that renders HTML content.
    /// </summary>
    public interface IHtmlTemplate : IStringTemplate
    {
        #region Operations (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IStringTemplate.SetVar(IEnumerable{char}, object)" />
        new IHtmlTemplate SetVar(IEnumerable<char> name, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IStringTemplate.SetVar{T}(IEnumerable{char}, T)" />
        new IHtmlTemplate SetVar<T>(IEnumerable<char> name, T value);

        #endregion Operations
    }
}
