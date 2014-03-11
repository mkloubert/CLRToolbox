// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.ServerAdmin.Classes.UI.General
{
    /// <summary>
    /// A basic page of the general section.
    /// </summary>
    public abstract class GeneralPageBase : ServerAdminPageBase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPageBase"/> class.
        /// </summary>
        /// <param name="sync">The value for the <see cref="ServerAdminPageBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected GeneralPageBase(object sync)
            : base(sync)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPageBase"/> class.
        /// </summary>
        protected GeneralPageBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <inheriteddoc />
        public override IEnumerable<dynamic> MenuItems
        {
            get
            {
                yield return new
                    {
                        Caption = "Overview",
                        Class = "generalOverview",
                        Link = "/" + typeof(Default).Name + ".aspx",
                    };

                yield return new
                    {
                        Caption = "Processes",
                        Class = "generalProcesses",
                        Link = "/" + typeof(processes).Name + ".aspx",
                    };
            }
        }

        #endregion Properties
    }
}
