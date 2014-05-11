// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Describes the mother of all objects.
    /// </summary>
    public partial interface ITMObject
    {
        #region Data Members (1)

        /// <summary>
        /// Gets or sets the object that should be linked with that instance.
        /// </summary>
        object Tag { get; set; }

        #endregion Data Members
    }
}