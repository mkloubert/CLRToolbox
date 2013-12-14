// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Templates.Text
{
    /// <summary>
    /// Describes a template that renders string content.
    /// </summary>
    public interface IStringTemplate : ITemplate
    {
        #region Operations (4)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.Render()" />
        new string Render();

        /// <summary>
        /// Renders string content and writes it to a <see cref="StringBuilder" />.
        /// </summary>
        /// <param name="builder">The target builder.</param>
        void RenderTo(StringBuilder builder);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.SetVar(IEnumerable{char}, object)" />
        new IStringTemplate SetVar(IEnumerable<char> name, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.SetVar{T}(IEnumerable{char}, T)" />
        new IStringTemplate SetVar<T>(IEnumerable<char> name, T value);

        #endregion Operations
    }
}
