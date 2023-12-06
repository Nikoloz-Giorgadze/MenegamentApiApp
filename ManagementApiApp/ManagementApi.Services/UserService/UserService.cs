using ManagementApi.Domain.Users;
using ManagementApi.Infrastructure.Repository.UserRepository;
using ManagementApi.Services.UserService.Request;
using Mapster;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ManagementApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> Register(UserRegisterModel user)
        {
            if (user is null)
                throw new ArgumentException("Wrong user");

            if (await _repo.Exist(user.Email))
                throw new Exception("This email already in use");

            if (user.Password != user.ConfirmPassword)
                throw new Exception("Incorrect password");

            var userToAdd = user.Adapt<User>();
            userToAdd.Password = CreatePasswordHash(user.Password);
            userToAdd.IsActive=true;

            return await _repo.Create(userToAdd);
        }

        public async Task<User> Login(UserLoginModel user)
        {
            string passwordHash = CreatePasswordHash(user.Password);
            var entity = await _repo.Get(user.Email, passwordHash);

            if (entity is null)
                throw new Exception("Email or password incorrect");

            return entity;
        }
        public async Task Delete(int id)
        {
            var userToRemove = await _repo.Get(id);

            if (userToRemove is null)
                throw new Exception("This user doesn't exists");

            userToRemove.IsActive = false;

            await _repo.SaveChanges();
        }
        private string CreatePasswordHash(string password)
        {
            using (SHA512 hmac = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = hmac.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (var ch in hashBytes)
                {
                    sb.Append(ch.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
