using Autofac;
using NHibernate;
using System;
using System.Linq;
using System.Reflection;
using Tdf.Act.Application;
using Tdf.Act.Commands;
using Tdf.Act.Nhb;
using Tdf.Act.Nhb.Repositories;
using Tdf.CQRS.Commanding;
using Tdf.CQRS.Dependency;
using Tdf.CQRS.Domain.Repositories;
using Tdf.CQRS.Domain.Uow;

namespace Tdf.ActTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterDependencies();

            Console.Write(Register());
        }

        static string Register()
        {
            var command = new RegisterCommand();
            command.NickName = "Bobby";
            command.Password = "123";
            command.ConfirmPassword = "123";
            command.Email = "pingkeke@163.com";

            //new UserAppService(new DefaultCommandBus()).Register(command);
            new UserAppService(ObjectContainer.Resolve<ICommandBus>()).Register(command);

            var currentUserId = command.ExecutionResult.GeneratedUserId;
            return currentUserId;
        }

        static void RegisterDependencies()
        {
            ObjectContainer.Initialize(x =>
            {
                x.Register(c => SessionManager.OpenSession()).As<ISession>();

                x.RegisterType<NhbUnitOfWork>().Named<IUnitOfWork>("UnitOfWorkImpl");

                x.Register(c =>
                {
                    if (UnitOfWorkContext.CurrentUnitOfWork != null)
                    {
                        return UnitOfWorkContext.CurrentUnitOfWork;
                    }
                    return c.ResolveNamed<IUnitOfWork>("UnitOfWorkImpl");
                }).As<IUnitOfWork>();

                x.RegisterGeneric(typeof(NhbRepository<>)).As(typeof(IRepository<>));

                x.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();

                var asm = Assembly.Load("Tdf.Act");

                x.RegisterAssemblyTypes(asm).Where(it => !it.IsInterface && !it.IsAbstract).AsClosedTypesOf(typeof(ICommandExecutor<>));
            });
        }


    }
}
