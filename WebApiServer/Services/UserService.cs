using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ValueObjects;
using WebAPI.Server.Data;
using System.Drawing;
using Deadlindar.Repositories;


namespace WebAPI.Server.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public List<UserServer> GetAll()
        {
            return _userRepository.GetAll();
        }

        public UserServer? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public UserServer? GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }

        public void Add(UserServer userServer)
        {
            _userRepository.Add(userServer);
        }

        public UserServer? Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public bool Update(UserServer userServer)
        {
            return _userRepository.Update(userServer);
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}