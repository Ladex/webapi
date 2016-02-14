using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WebApi2Book.Data.Entities;

namespace WebApiBook.Data.SqlServer.Mapping
{
    public abstract class VersionedClassMap<T>: ClassMap<T> where T :IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version).Column("ts")
                .CustomSqlType("Rowversion")
                .Generated.Always()
                .UnsavedValue("null");
        } 
    }
}
