using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Tdf.Act.Domain.Entities;

namespace Tdf.Act.Nhb.Mapping
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("`User`");

            Id(c => c.Id, m => m.Generator(Generators.Assigned));

            Property(c => c.Email);
            Property(c => c.NickName);
            Property(c => c.Password);
        }
    }
}
