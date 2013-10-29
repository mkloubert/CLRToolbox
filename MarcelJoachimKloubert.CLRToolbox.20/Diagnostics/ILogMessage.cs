// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// Describes an object that stores the data of a log message.
    /// </summary>
    public interface ILogMessage : IIdentifiable
    {
        #region Data Members (9)

        /// <summary>
        /// Gets the calling assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the list of underlying categories.
        /// </summary>
        IList<LoggerFacadeCategories> Categories { get; }

        /// <summary>
        /// Gets the underyling context.
        /// </summary>
        Context Context { get; }

        /// <summary>
        /// Gets the calling member.
        /// </summary>
        MemberInfo Member { get; }

        /// <summary>
        /// Gets the message (value).
        /// </summary>
        object Message { get; }

        /// <summary>
        /// Gets the underlying principal.
        /// </summary>
        IPrincipal Principal { get; }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// Gets the thread that has written that object.
        /// </summary>
        Thread Thread { get; }

        /// <summary>
        /// Gets the log time.
        /// </summary>
        DateTimeOffset Time { get; }

        #endregion Data Members
    }
}
