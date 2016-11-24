using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Linq;
using System.Reflection;

namespace Tdf.Act.Nhb
{
    public static class SessionManager
    {
        public static readonly ISessionFactory SessionFactory;

        static SessionManager()
        {
            var config = new Configuration();
            config.DataBaseIntegration(x =>
            {
                // SQLServer配置方法
                x.Driver<NHibernate.Driver.SqlClientDriver>();
                x.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                x.ConnectionStringName = "DefaultDBConnection";
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.Load("Tdf.Act").GetTypes().Where(it => it.Name.EndsWith("Map")));

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            SessionFactory = config.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
