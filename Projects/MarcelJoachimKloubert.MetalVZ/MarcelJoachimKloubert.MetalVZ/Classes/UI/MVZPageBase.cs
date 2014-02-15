// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Web.UI;

namespace MarcelJoachimKloubert.MetalVZ.Classes.UI
{
    /// <summary>
    /// A basic page.
    /// </summary>
    public abstract class MVZPageBase : Page
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="MVZPageBase"/> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="MVZPageBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected MVZPageBase(object sync)
        {
            sync.ThrowIfNull(() => sync);

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MVZPageBase"/> class.
        /// </summary>
        protected MVZPageBase()
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
        /// The event that invokes the <see cref="MVZPageBase.OnPageLoad()" /> method.
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
