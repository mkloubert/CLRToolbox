// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MarcelJoachimKloubert.AppServer.Services.AppServerDb
{
    internal sealed class AppServerEntityMapper<E> : ClassMapping<E> where E : class, global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity
    {
        #region Constructors (1)

        internal AppServerEntityMapper()
        {
            this.Lazy(false);

            string table;
            string schema;
            EntityHelper.TryGetTableNameAndSchema<E>(name: out table,
                                                     schema: out schema);

            if (!string.IsNullOrWhiteSpace(schema))
            {
                this.Schema(schema);
            }

            if (!string.IsNullOrWhiteSpace(table))
            {
                this.Table(table);
            }

            var allColumns = EntityHelper.GetColumns<E>();

            foreach (var attrib in allColumns.Where(a => a.IsKey))
            {
                this.Id(attrib.Name,
                        this.CreateIdMapperAction(attrib));
            }

            foreach (var attrib in allColumns.Where(a => !a.IsKey))
            {
                this.Property(attrib.Name,
                              this.CreatePropertyMapperAction(attrib));
            }
        }

        #endregion Constructors

        #region Methods (2)

        // Private Methods (2) 

        private Action<IIdMapper> CreateIdMapperAction(TMColumnAttribute attrib)
        {
            return new Action<IIdMapper>((mapper) =>
                {
                    mapper.Column(attrib.Name);
                });
        }

        private Action<IPropertyMapper> CreatePropertyMapperAction(TMColumnAttribute attrib)
        {
            return new Action<IPropertyMapper>((mapper) =>
                {
                    mapper.Column(attrib.Name);
                    mapper.NotNullable(!attrib.IsNullable);
                });
        }

        #endregion Methods
    }
}
