using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RepasoParcial.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RepasoParcial.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {

        private CineDBContext _context;

        public PeliculaRepository(CineDBContext context)
        {
            _context = context;
        }

        public bool Create(Pelicula pelicula)
        {
            pelicula.Estreno = true; //toda película creada es estreno
            _context.Peliculas.Add(pelicula);
            return _context.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            {
                var peliculaDeleted = GetById(id);
                if (peliculaDeleted != null)
                {
                    _context.Peliculas.Remove(peliculaDeleted);

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public bool Delete(int id)   borrado por cambio de estado
        //{
        //    var envio = GetById(id);
        //    if (envio != null)
        //    {

        //        envio.Estado = "Eliminado"; 

        //        _context.T_Envio.Update(envio);
        //        _context.SaveChanges();

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        public List<Pelicula> GetAll()
        {
            //return _context.Peliculas.Where(p => p.EnEstreno).ToList(); devolver peliculas en estreno
            {
                return _context.Peliculas.ToList();
            }
        }

        public List<Pelicula> GetByDirectorTitle(string? director, string? titulo)
        {
           /* var query = _context.Peliculas.AsQueryable().Where(p => p.EnEstreno); */// Filtrar por películas en estreno
            {
                var query = _context.Peliculas.AsQueryable();


                if (!string.IsNullOrEmpty(director))
                {
                    query = query.Where(p => p.Director.Contains(director));
                }


                if (!string.IsNullOrEmpty(titulo))
                {
                    query = query.Where(p => p.Titulo.Contains(titulo));
                }

                //if (Anio.HasValue) // Validación si es INT
                //{
                //    query = query.Where(p => p.Anio == Anio.Value); 
                //}


                return query.ToList();
            }
        }

        //public async Task<List<Pelicula>> GetByDatesAsync(DateTime? fechaInicio, DateTime? fechaFin)   Filtrar por fecha de inicio y final
        //{
        //    var query = _context.Peliculas.AsQueryable();

        //    if (fechaInicio.HasValue)
        //    {
        //        query = query.Where(p => p.FechaEstreno >= fechaInicio.Value);
        //    }

        //    if (fechaFin.HasValue)
        //    {
        //        query = query.Where(p => p.FechaEstreno <= fechaFin.Value);
        //    }

        //    return await query.ToListAsync();
        //}


        public Pelicula? GetById(int id)
        {
            return _context.Peliculas.Find(id);
        }

        public bool Update(Pelicula pelicula)
        {
            if (pelicula != null && pelicula.Id != 0)
            {
                _context.Peliculas.Update(pelicula);

                _context.SaveChanges();
            }

            return false;
        }

    }

}
