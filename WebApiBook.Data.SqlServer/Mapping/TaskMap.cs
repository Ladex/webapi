using FluentNHibernate.Mapping;

namespace WebApiBook.Data.SqlServer.Mapping
{
    public class TaskMap : VersionedClassMap<WebApi2Book.Data.Entities.Task>
    {
        public TaskMap()
        {
            Id(x => x.TaskId);
            Map(x => x.Subject).Not.Nullable();
            Map(x => x.StartDate).Not.Nullable();
            Map(x => x.DueDate).Not.Nullable();
            Map(x => x.CompletedDate).Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();


            References(x => x.Status, "StatusId");
            References(x => x.CreatedBy, "CreatedUserId");

            HasManyToMany(x => x.Users)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Table("TaskUser")
                .ParentKeyColumn("TaskId")
                .ChildKeyColumn("UserId");
        }
    }
}