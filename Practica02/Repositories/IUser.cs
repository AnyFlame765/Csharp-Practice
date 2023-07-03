using Microsoft.AspNetCore.Mvc;
using Practica02.Model;

namespace Practica02.Repositories;

public interface IUser
{
    Task<IEnumerable<UserModel>> GetAllUser();

    Task<UserModel> GetUser(int Id);

    Task<bool> CreateUser(UserModel userModel);

    Task<bool> UpdateUser(int id, UserModel userModel);

    Task<bool> DeleteUser(int id);
}