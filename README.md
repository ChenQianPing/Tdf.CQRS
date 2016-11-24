# Tdf.CQRS

Tdf.CQRS 
团队开发框架实战—CQRS架构，命令查询职责分离模式(Command Query Responsibility Segregation，CQRS)，该模式从业务上分离修改 (Command，增，删，改，会对系统状态进行修改)和查询（Query，查，不会对系统状态进行修改)的行为。从而使得逻辑更加清晰，便于对不同部分进行针对性的优化。
CQRS是一种思想很简单清晰的设计模式，他通过在业务上分离操作和查询来使得系统具有更好的可扩展性及性能，使得能够对系统的不同部分进行扩展和优化。在CQRS中，所有的涉及到对DB的操作都是通过发送Command，然后特定的Command触发对应事件来完成操作，这个过程是异步的，并且所有涉及到对系统的变更行为都包含在具体的事件中，结合Eventing Source模式，可以记录下所有的事件，而不是以往的某一点的数据信息，这些信息可以作为系统的操作日志，可以来对系统进行回退或者重放。

# 更多资料和资源
- [团队开发框架实战—CQRS架构](http://www.jianshu.com/p/d4ca2133875c)
- [浅谈命令查询职责分离(CQRS)模式](http://www.cnblogs.com/yangecnu/p/Introduction-CQRS.html)
