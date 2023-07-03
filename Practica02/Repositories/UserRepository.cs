using System.Collections.Immutable;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Practica02.Datos;
using Practica02.Model;

namespace Practica02.Repositories;

public class UserRepository : IUser
{
    private ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserModel>>  GetAllUser()
    {
        var query = "SELECT * FROM users";
        var result = await _db.User.FromSqlRaw(query).ToListAsync();
        return result;
    }

    public async Task<UserModel> GetUser(int Id)
    {
        var query = "SELECT * FROM users WHERE id=@id";
        var result = await _db.User.FromSqlRaw(
            query,
            new Npgsql.NpgsqlParameter("@id", Id)
        ).ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<bool> CreateUser(UserModel userModel)
    {
        var query = "INSERT INTO users(name, lastname, departmentid) VALUES (@name, @lastname, @departmentId )";
        var result = await _db.Database.ExecuteSqlRawAsync(
            query,
            new Npgsql.NpgsqlParameter("@name", userModel.Name),
            new Npgsql.NpgsqlParameter("@lastname", userModel.Lastname),
            new Npgsql.NpgsqlParameter("@departmentId", userModel.departmentId)
        );
        return result > 0;
    }

    public Task<bool> UpdateUser(int id, UserModel userModel)
    {
        return null;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var query = "DELETE FROM users WHERE id = @id";
        var result = await _db.Database.ExecuteSqlRawAsync(query, new Npgsql.NpgsqlParameter("@id", id));
        return result > 0;
    }
}