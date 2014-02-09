// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes;
using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CloudNET.Classes.Sessions;
using System;
using System.Web;

namespace MarcelJoachimKloubert.CloudNET.Extensions
{
    static partial class CloudNetExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Returns the <see cref="ICloudApp" /> of a <see cref="HttpContext" /> object.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>
        /// The <see cref="ICloudApp" /> of <paramref name="context" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        public static ICloudApp GetCloudAppContext(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return (ICloudApp)context.Application[Global.APP_VAR_APPCONTEXT];
        }

        /// <summary>
        /// Returns the <see cref="ICloudSession" /> of a <see cref="HttpContext" /> object.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>
        /// The <see cref="ICloudSession" /> of <paramref name="context" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        public static ICloudSession GetCloudSession(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return (ICloudSession)context.Session[Global.SESSION_VAR_CLOUDSESSION];
        }

        /// <summary>
        /// Returns the <see cref="IPrincipalRepository" /> of a <see cref="HttpContext" /> object.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>
        /// The <see cref="IPrincipalRepository" /> of <paramref name="context" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        public static IPrincipalRepository GetPrincipalRepository(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return (IPrincipalRepository)context.Application[Global.APP_VAR_PRINCIPALS];
        }

        #endregion Methods
    }
}
