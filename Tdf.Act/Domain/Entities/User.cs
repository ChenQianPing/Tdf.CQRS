using System;

namespace Tdf.Act.Domain.Entities
{
    public class User
    {
        public virtual string Id { get; protected set; }

        public virtual string Email { get; set; }

        public virtual string NickName { get; set; }

        public virtual string Password { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
