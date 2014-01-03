// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class EntityHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Returns the list of columns of an entity type defined by <see cref="TMColumnAttribute" />.
        /// </summary>
        /// <typeparam name="E">The entity type from where to extract the data from.</typeparam>
        /// <returns>The list of found columns.</returns>
        public static IEnumerable<TMColumnAttribute> GetColumns<E>() where E : global::MarcelJoachimKloubert.CLRToolbox.Data.Entities.IEntity
        {
            List<MemberInfo> members = new List<MemberInfo>();

            members.AddRange(typeof(E).GetProperties(BindingFlags.Instance | BindingFlags.Public));
            members.AddRange(typeof(E).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic));
            members.AddRange(typeof(E).GetFields(BindingFlags.Instance | BindingFlags.Public));
            members.AddRange(typeof(E).GetFields(BindingFlags.Instance | BindingFlags.NonPublic));

            foreach (MemberInfo m in members)
            {
                foreach (TMColumnAttribute attrib in m.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumnAttribute), false))
                {
                    yield return attrib;
                }
            }
        }

        #endregion Methods
    }
}
