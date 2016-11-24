# Tdf.CQRS
团队开发框架实战—CQRS架构，**命令查询职责分离模式(Command Query Responsibility Segregation，CQRS)**，该模式从业务上分离修改 (Command，增，删，改，会对系统状态进行修改)和查询（Query，查，不会对系统状态进行修改)的行为。从而使得逻辑更加清晰，便于对不同部分进行针对性的优化。
CQRS是一种思想很简单清晰的设计模式，他通过在业务上分离操作和查询来使得系统具有更好的可扩展性及性能，使得能够对系统的不同部分进行扩展和优化。在CQRS中，所有的涉及到对DB的操作都是通过发送Command，然后特定的Command触发对应事件来完成操作，这个过程是异步的，并且所有涉及到对系统的变更行为都包含在具体的事件中，结合Eventing Source模式，可以记录下所有的事件，而不是以往的某一点的数据信息，这些信息可以作为系统的操作日志，可以来对系统进行回退或者重放。

# Command方向的实现

## (1) Command对象
Command表示想要执行的命令，所以Command类的类名应当是动词的形式。例如RegisterCommand, ChangePasswordCommand等。不过Command后缀则是可选的，只要能保持一致即可。
Command对象的作用是用来封装命令数据，所以这类对象以属性为主，少量简单方法，但注意这些方法中不能包含业务逻辑。
举个用户注册的例子，用户注册是一个命令，所以我们需要一个RegisterCommand类，这个类定义如下：
```
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
```

## (2) CommandExecutor
**Command和CommandExecutor是一一对应的**。也就是说，一个Command只会对应一个CommandExecutor，这和后面的事件有区别，事件是一对多的，一个Event可以对应多个EventHandler。
CommandExecutor的作用是执行一个命令，对于注册的例子，我们会有一个RegisterCommandExecutor的类，它只有一个Execute方法，接受RegisterCommand参数：
```
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
```

在Execute方法中，我们需要先验证Command的正确性，但需要注意的是，这里的验证只是验证RegisterCommand中的数据是否合法，并非验证业务逻辑。例如，这里会验证邮箱是否为空且格式是否正确，但邮箱格式正确并不意味着就可以注册，因为系统可能要求18岁以上的成年人才能注册，而这属于业务逻辑，RegistrationService将会负责确保所有的业务规则不被破坏，**RegistrationService属于Domain Service**，存在于Domain Model中。

## (3) Command Bus
用于执行Command的是CommandExecutor，但CommandExecutor却并不用来在UI层调用，UI层中只会用到Command对象和即将提到的Command Bus。Command Bus的作用是将一个Command派发给相应的CommandExecutor去执行。在开发UI层时，我们不需要关心Command会被哪个Executor执行了，而只要知道，上帝赐予了我们一个CommandBus，我们只要创建好Command对象，扔给它，神奇的CommandBus就会帮我们把它执行完。这样一来，对于UI层的开发来说，所涉及的概念很简单，涉及的类也少，大部分的工作都是得到表单中的输入，封装成Command对象，扔给CommandBus。

这样我们就完成了CQRS中Command的一个基本实现。


# 更多资料和资源
- [团队开发框架实战—CQRS架构](http://www.jianshu.com/p/d4ca2133875c)
- [浅谈命令查询职责分离(CQRS)模式](http://www.cnblogs.com/yangecnu/p/Introduction-CQRS.html)
