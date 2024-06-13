using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts;

public interface IMovieService
{
    Task AddMovie(Movie movie);
    void UpdateMovie(Movie newMovie, int id);
    void DeleteMovie(int id);
    Movie GetProduct(Func<Movie, bool>? predicate = null);
    List<Movie> GetAllProducts(Func<Movie, bool>? predicate = null);
}
