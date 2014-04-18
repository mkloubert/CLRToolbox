// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.Reflection;
using System.Xml.Linq;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Documentation of an event.
    /// </summary>
    public sealed class EventDocumentation : MemberDocumentationBase<EventInfo>
    {
        #region Constructors (1)

        internal EventDocumentation(TypeDocumentation typeDoc, EventInfo @event, XElement xml)
            : base(typeDoc, @event, xml)
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override string ToString()
        {
            return string.Format("E:{0}.{1}",
                                 this.Type.ClrType.FullName,
                                 this.ClrMember.Name);
        }

        #endregion Methods
    }
}