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
        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User? GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User? Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }

        public bool IsLoginExist(string login)
        {
            return GetByLogin(login) is not null;
        }
    }
}