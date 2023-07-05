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
        //relacionar la tabla de usuarios con la tabla de Departamentos
        var innerjoinQuery = "SELECT * FROM users INNER JOIN department ON users.departmentid = departments.id";
        var result = await _db.User.FromSqlRaw(innerjoinQuery).ToListAsync();
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
        var query = "INSERT INTO users(name, lastname, departmentid) VALUES (@name, @lastname, @departmentid )";
        var result = await _db.Database.ExecuteSqlRawAsync(
            query,
            new Npgsql.NpgsqlParameter("@name", userModel.Name),
            new Npgsql.NpgsqlParameter("@lastname", userModel.Lastname),
            new Npgsql.NpgsqlParameter("@departmentid", userModel.departmentid)
        );
        return result > 0;
    }

    public async Task<bool> UpdateUser(int id, UserModel userModel)
    {
        //hacer un subquery para obtener el id del departamento
        var query = "";
        var result = await _db.Database.ExecuteSqlRawAsync(
            query,
            new Npgsql.NpgsqlParameter("@name", userModel.Name),
            new Npgsql.NpgsqlParameter("@lastname", userModel.Lastname),
            new Npgsql.NpgsqlParameter("@departmentid", userModel.departmentid),
            new Npgsql.NpgsqlParameter("@id", userModel.Id)
        );
        
        return result > 0;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var query = "DELETE FROM users WHERE id = @id";
        var result = await _db.Database.ExecuteSqlRawAsync(query, new Npgsql.NpgsqlParameter("@id", id));
        return result > 0;
    }
}