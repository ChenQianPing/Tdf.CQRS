using Tdf.CQRS.Commanding;

namespace Tdf.Act.Commands
{
    public class RegisterCommand : ICommand
    {
        public string Email { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public RegisterCommandResult ExecutionResult { get; set; }

        public RegisterCommand()
        {
        }
    }

    public class RegisterCommandResult
    {
        public string GeneratedUserId { get; set; }
    }
}
