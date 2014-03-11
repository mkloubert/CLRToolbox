// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Web.UI;

namespace MarcelJoachimKloubert.ServerAdmin.Classes.UI
{
    /// <summary>
    /// A basic master page.
    /// </summary>
    public abstract partial class ServerAdminMasterPageBase : MasterPage
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAdminMasterPageBase"/> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="ServerAdminMasterPageBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ServerAdminMasterPageBase(object sync)
        {
            sync.ThrowIfNull(() => sync);

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAdminMasterPageBase"/> class.
        /// </summary>
        protected ServerAdminMasterPageBase()
            : this(sync: new object())
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        /// <summary>
        /// Is invoked when page is loaded.
        /// </summary>
        protected virtual void OnPageLoad()
        {
            // dummy
        }

        /// <summary>
        /// The event that invokes the <see cref="ServerAdminMasterPageBase.OnPageLoad()" /> method.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lock (this._SYNC)
            {
                this.OnPageLoad();
            }
        }

        #endregion Methods
    }
}
