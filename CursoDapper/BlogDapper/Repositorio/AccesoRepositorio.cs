﻿using BlogDapper.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using XSystem.Security.Cryptography;

namespace BlogDapper.Repositorio
{
    public class AccesoRepositorio : IAccesoRepositorio
    {
        private readonly IDbConnection _bd;
        public AccesoRepositorio(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public List<Usuario> UserLogin(Usuario user)
        {
            var sql = "SELECT * FROM Usuario WHERE Login=@Login AND Password =@Password";

            //Convertir password a md5 antes de enviar la consulta
            var Password = obtenerMd5(user.Password);

            var validar = _bd.Query<Usuario>(sql, new
            {
                user.Login,
                Password
            });

            return validar.ToList();
        }

        public bool ValidateUser(Usuario user)
        {
            var sql = "SELECT * FROM Usuario WHERE Login=@Login";

            var existeUsuario = _bd.Query<Usuario>(sql, new
            {
                user.Login
            });

            return existeUsuario.Count() > 0;
          
        }

        public void AddUser(Usuario user) 
        {
            var Password = obtenerMd5(user.Password);
            var UserId = Guid.NewGuid().ToString();

            var ingresarUsuarioSql = @"INSERT INTO Usuario(User_ID,Login, Password) VALUES (@User_ID,@Login, @Password);";

            _bd.Execute(ingresarUsuarioSql, new
            {
                User_ID = UserId,
                user.Login,
                Password
            });
        }

        private static string obtenerMd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
