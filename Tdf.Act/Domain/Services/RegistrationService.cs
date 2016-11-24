using System;
using System.Linq;
using Tdf.Act.Domain.Entities;
using Tdf.CQRS.Domain.Repositories;

namespace Tdf.Act.Domain.Services
{
    public class RegistrationService
    {
        private IRepository<User> _repository;

        public RegistrationService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Register(string email, string nickName, string password)
        {
            if (_repository.Query().Any(it => it.Email == email))
                throw new InvalidOperationException("Emails is used by other user. Please choose a new email.");

            var user = new User
            {
                Email = email,
                NickName = nickName,
                Password = password
            };

            _repository.Add(user);

            return user;
        }

    }
}
