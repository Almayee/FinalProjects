using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IWebHostEnvironment _env;

    public MovieService(IMovieRepository movieRepository, IWebHostEnvironment env)
    {
        _env = env;
        _movieRepository = movieRepository;
    }
    public async Task AddMovie(Movie movie)
    {
        if (movie.ImageFile == null)
            throw new Exceptions.FileNotFoundException("The file cannot be empty");

        movie.ImageUrl=Helper.SaveFile(_env.WebRootPath,@"uploads\movies" ,movie.ImageFile);

        await _movieRepository.AddAsync(movie);
        await _movieRepository.CommitAsync();
    }

    public void DeleteMovie(int id)
    {
       var existMovie=_movieRepository.Get(x=>x.Id==id);
        if (existMovie == null) 
            throw new EntityNotFoundException("File not found");

        Helper.DeleteFIle(_env.WebRootPath, @"uploads\movies", existMovie.ImageUrl);

        _movieRepository.Delete(existMovie);
        _movieRepository.Commit();
    }

    public List<Movie> GetAllProducts(Func<Movie, bool>? predicate = null)
    {
        return _movieRepository.GetAll(predicate);
    }

    public Movie GetProduct(Func<Movie, bool>? predicate = null)
    {
        return _movieRepository.Get(predicate);
    }

    public void UpdateMovie(Movie newMovie, int id)
    {
        var oldMovie=_movieRepository.Get(x => x.Id==id);

        if (oldMovie == null)
            throw new EntityNotFoundException("File not found!");

        if(newMovie.ImageFile != null)
        {
            Helper.DeleteFIle(_env.WebRootPath, @"uploads\movies", oldMovie.ImageUrl);
            oldMovie.ImageUrl=Helper.SaveFile(_env.WebRootPath,@"uploads\movies",newMovie.ImageFile);
        }

        _movieRepository.Commit();
    }
}
