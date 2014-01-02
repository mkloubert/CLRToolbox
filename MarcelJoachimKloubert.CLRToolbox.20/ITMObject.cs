// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

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

        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked when an error occured.
        /// </summary>
        event ErrorEventHandler Error;

        #endregion Delegates and Events
    }
}
