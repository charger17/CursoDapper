﻿using BlogDapper.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace BlogDapper.Repositorio
{
    public class SliderRepository : ISliderRepository
    {
        private readonly IDbConnection _bd;
        public SliderRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public List<Slider> GetSliders()
        {
            var sqlSlider = @"SELECT * FROM Slider";

            return _bd.Query<Slider>(sqlSlider).ToList();
        }

        public List<Articulo> GetArticulosForSlider()
        {
            var sqlSlider = @"SELECT * FROM Articulo ORDER BY IdArticulo DESC";

            return _bd.Query<Articulo>(sqlSlider).ToList();
        }

        public List<Categoria> GetCategoriasForSlider()
        {
            var sqlSlider = @"SELECT * FROM Categoria ORDER BY IdCategoria DESC";

            return _bd.Query<Categoria>(sqlSlider).ToList();
        }

        public Articulo GetArticuloForSlider(int id)
        {
            var sqlSlider = @"SELECT * FROM Articulo WHERE IdArticulo =@IdArticulo ORDER BY IdArticulo DESC";

            return _bd.Query<Articulo>(sqlSlider, new
            {
                IdArticulo = id
            }).Single();
        }

        public void InsertComentarioForSlider(Comentario comentario)
        {
            var sqlSlider = @"INSERT INTO Comentario(Titulo, Mensaje, ArticuloId, FechaCreacion) VALUES(@Titulo, @Mensaje, @ArticuloId, @FechaCreacion)";

            _bd.Execute(sqlSlider, new
            {
                comentario.Titulo,
                comentario.Mensaje,
                comentario.ArticuloId,
                FechaCreacion = DateTime.Now
            });
        }

        public List<Comentario> GetComentariosForSlider(int id)
        {
            var sqlSlider = @"SELECT * FROM Comentario WHERE ArticuloId =@ArticuloId ORDER BY IdComentario DESC";

            return _bd.Query<Comentario>(sqlSlider, new
            {
                ArticuloId = id
            }).ToList();
        }
    }
}
