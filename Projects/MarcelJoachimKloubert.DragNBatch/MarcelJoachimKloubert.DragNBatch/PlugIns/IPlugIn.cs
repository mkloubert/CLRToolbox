// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox;
using System.Globalization;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns
{
    /// <summary>
    /// Describes a plugin.
    /// </summary>
    public interface IPlugIn : ITMDisposable, IIdentifiable, IHasName, IInitializable<IPlugInContext>
    {
        #region Methods (2)

        /// <summary>
        /// Returns the drop text for a specific culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The drop text.</returns>
        string GetDropText(CultureInfo culture);

        /// <summary>
        /// Handles files.
        /// </summary>
        /// <param name="context">The context.</param>
        void HandleFiles(IHandleFilesContext context);

        #endregion Methods
    }
}