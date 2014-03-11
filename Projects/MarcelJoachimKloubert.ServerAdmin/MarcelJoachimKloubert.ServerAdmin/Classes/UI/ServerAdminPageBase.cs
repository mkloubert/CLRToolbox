// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MarcelJoachimKloubert.ServerAdmin.Classes.UI
{
    /// <summary>
    /// A basic page.
    /// </summary>
    public abstract class ServerAdminPageBase : Page
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAdminPageBase"/> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="ServerAdminPageBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ServerAdminPageBase(object sync)
        {
            sync.ThrowIfNull(() => sync);

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAdminPageBase"/> class.
        /// </summary>
        protected ServerAdminPageBase()
            : this(sync: new object())
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Returns the list of menu items.
        /// </summary>
        public virtual IEnumerable<dynamic> MenuItems
        {
            get { yield break; }
        }

        #endregion Properties

        #region Methods (4)

        // Protected Methods (4) 

        /// <summary>
        /// Is invoked when page is loaded.
        /// </summary>
        protected virtual void OnPageLoad()
        {
            // dummy
        }

        /// <summary>
        /// Is invoked before page starts to be initialized.
        /// </summary>
        protected virtual void OnPagePreInit()
        {
            // dummy
        }

        /// <summary>
        /// The event that invokes the <see cref="ServerAdminPageBase.OnPageLoad()" /> method.
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

        /// <summary>
        /// The event that invokes the <see cref="ServerAdminPageBase.OnPagePreInit()" /> method.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            lock (this._SYNC)
            {
                this.OnPagePreInit();
            }
        }

        #endregion Methods
    }
}
