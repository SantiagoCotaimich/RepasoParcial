using RepasoParcial.Models;

namespace RepasoParcial.Repositories
{
    public interface IPeliculaRepository
    {
        List<Pelicula> GetAll();
        bool Create (Pelicula pelicula);


        bool Update(Pelicula pelicula);

        Pelicula? GetById(int id);

        List<Pelicula> GetByDirectorTitle(string? director, string? titulo); //List solo si pueden haber varios resultados - ojo a los signos de pregunta, al ponerlos quiere decir que acepta nulls
        //luego deberias ponerlos tambien en el repositorio y en el controller o te  va a dar error. Si estas buscando por dos variables, es fundamental para que una pueda quedar vacia
        //si es una sola variable, no haria falta a no ser que quieras que se pueda no cargar nada y te muestre todo, como un "getall" dentro de una consulta por parametros

        bool Delete(int id);
    }
}
