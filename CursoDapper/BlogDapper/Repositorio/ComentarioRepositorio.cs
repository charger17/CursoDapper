using BlogDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace BlogDapper.Repositorio
{
    public class ComentarioRepositorio : IComentarioRepositorio
    {
        private readonly IDbConnection _bd;

        public ComentarioRepositorio(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Comentario ActualizarComentario(Comentario comentario)
        {
            var sql = @"UPDATE Comentario 
                        SET Titulo = @Titulo, Mensaje = @Mensaje
                        WHERE IdComentario = @IdComentario";
            _bd.Execute(sql, comentario);

            return comentario;
        }

        public void BorrarComentario(int id)
        {
            var sql = "DELETE FROM Comentario WHERE IdComentario = @IdComentario";
            _bd.Execute(sql, new { IdComentario = id });
        }

        public Comentario CrearComentario(Comentario comentario)
        {
            var sql = "INSERT INTO Comentario (Titulo, Mensaje, ArticuloId, FechaCreacion) VALUES (@Titulo, @Mensaje, @ArticuloId, @FechaCreacion)";
            _bd.Execute(sql, new 
            {
                comentario.Titulo,
                comentario.Mensaje,
                comentario.Articulo,
                FechaCreacion = DateTime.Now,
            });

            return comentario;
        }

        public Comentario GetComentario(int id)
        {
            var sql = "SELECT * FROM Comentario WHERE IdComentario = @IdComentario";
            return _bd.Query<Comentario>(sql, new { IdComentario = id }).SingleOrDefault();
        }


        public List<Comentario> GetComentarios()
        {
            var sql = "SELECT * FROM Comentario";
            return _bd.Query<Comentario>(sql).ToList();
        }
        public List<Comentario> GetComentarioArticulo()
        {
            var sql = @"SELECT C.*, A.Titulo FROM Comentario C
                        INNER JOIN Articulo A ON A.IdArticulo = C.ArticuloId
                        ORDER BY C.IdComentario;";
            var comentario = _bd.Query<Comentario, Articulo, Comentario>(sql, (C,A) =>
            {
                C.Articulo = A;
                return C;
            }, splitOn: "ArticuloId");

            return comentario.Distinct().ToList();
        }

    }
}
