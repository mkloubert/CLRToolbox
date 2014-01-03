// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    static partial class EntityHelper
    {
        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Tries to return the table name and schema from a <see cref="TMTableAttribute" />.
        /// </summary>
        /// <typeparam name="E">The entity type from where to extract the data from.</typeparam>
        /// <param name="name">The variable where to write the table name to.</param>
        /// <param name="schema">The variable where to write the schema name to.</param>
        /// <returns>Attribute was found or not.</returns>
        public static bool TryGetTableNameAndSchema<E>(out string name,
                                                       out string schema) where E : global::MarcelJoachimKloubert.CLRToolbox.Data.Entities.IEntity
        {
            object[] allTableAttribs = typeof(E).GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Data.TMTableAttribute),
                                                                     false);

            IEnumerable<TMTableAttribute> castedTableAttribs = CollectionHelper.Cast<TMTableAttribute>(allTableAttribs);

            TMTableAttribute attrib = CollectionHelper.SingleOrDefault(castedTableAttribs);
            if (attrib != null)
            {
                name = attrib.Name;
                schema = attrib.Schema;

                return true;
            }

            name = typeof(E).Name;
            schema = null;

            return false;
        }

        #endregion Methods
    }
}
