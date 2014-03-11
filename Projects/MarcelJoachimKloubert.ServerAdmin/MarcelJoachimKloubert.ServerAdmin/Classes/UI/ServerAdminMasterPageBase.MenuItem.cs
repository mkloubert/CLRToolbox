// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.ServerAdmin.Classes.UI
{
    partial class ServerAdminMasterPageBase
    {
        #region Nested Classes (1)

        /// <summary>
        /// A menu item.
        /// </summary>
        protected sealed class MenuItem
        {
            #region Properties (3)

            /// <summary>
            /// Gets the caption of the item.
            /// </summary>
            public string Caption
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the CSS class(es) of the item.
            /// </summary>
            public string Class
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the link URL.
            /// </summary>
            public string Link
            {
                get;
                private set;
            }

            #endregion Properties

            #region Methods (1)

            // Public Methods (1) 

            /// <summary>
            /// Converts a dynamic item to an instance of that class.
            /// </summary>
            /// <param name="obj">The input object.</param>
            /// <returns>
            /// <paramref name="obj" /> as <see cref="MenuItem" /> or <see langword="null"/>
            /// if <paramref name="obj" /> is also <see langword="null"/>.
            /// </returns>
            public static MenuItem FromDynamic(dynamic obj)
            {
                if (obj == null)
                {
                    return null;
                }

                return new MenuItem
                    {
                        Caption = GetDynamicValue(obj, new Func<dynamic, string>(o => o.Caption)),
                        Class = GetDynamicValue(obj, new Func<dynamic, string>(o => o.Class)),
                        Link = GetDynamicValue(obj, new Func<dynamic, string>(o => o.Link)),
                    };
            }

            #endregion Methods

            private static T GetDynamicValue<T>(dynamic obj, Func<dynamic, T> valueProvider, T failValue = default(T))
            {
                try
                {
                    return valueProvider(obj);
                }
                catch
                {
                    return failValue;
                }
            }
        }

        #endregion Nested Classes
    }
}
