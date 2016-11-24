using System;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Services;
using Tdf.CQRS.Commanding;
using Tdf.CQRS.Domain.Repositories;

namespace Tdf.Act.Commands.Executors
{
    public class RegisterCommandExecutor : ICommandExecutor<RegisterCommand>
    {
        public IRepository<User> _repository;

        public RegisterCommandExecutor(IRepository<User> repository)
        {
            _repository = repository;
        }

        public void Execute(RegisterCommand cmd)
        {
            #region 验证传入的Command对象是否合法
            if (String.IsNullOrEmpty(cmd.Email))
                throw new ArgumentException("Email is required.");

            if (cmd.Password != cmd.ConfirmPassword)
                throw new ArgumentException("Password not match.");

            // other command validation logics

            #endregion

            #region 调用领域模型完成操作
            var service = new RegistrationService(_repository);
            var user = service.Register(cmd.Email, cmd.NickName, cmd.Password);

            cmd.ExecutionResult = new RegisterCommandResult
            {
                GeneratedUserId = user.Id
            };
            #endregion
        }
    }
}
