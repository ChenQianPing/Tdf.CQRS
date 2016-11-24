using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.Act.Commands;
using Tdf.CQRS.Commanding;

namespace Tdf.Act.Application
{
    public class UserAppService
    {
        public ICommandBus _commandBus { get; private set; }

        public UserAppService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public void Register(RegisterCommand command)
        {
            _commandBus.Send(command);

            var email = command.Email;
            var currentUserId = command.ExecutionResult.GeneratedUserId;
        }
    }
}
