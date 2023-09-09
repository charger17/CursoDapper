using BlogDapper.Models;
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
    }
}
