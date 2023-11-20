using NovelCatalog.Application.Novelists;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Movies;
using NovelCatalog.Domain;
using NovelCatalog.Domain.Models;
using NovelCatalog.MemoryPersistense.Repositories;
using NovelistCatalog.Domain.Models;
using System.Net.Http;
using System.Security;

namespace NovelCatalog.ConsoleView
{
    class Client
    {
        private NovelService novelService;
        private CategoryService categoryService;
        private NovelistsService novelistsService;

        public Client()
        {
            novelService = new NovelService(new NovelRepository());
            categoryService = new CategoryService(new CategoryRepository());
            novelistsService = new NovelistsService(new NovelistRepository());
        }
        public async Task<Dictionary<int, string>> GetCategorisWithIdsAsync()
        {
            var categorisDtos = await categoryService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            Dictionary<int, string> returnValue = new Dictionary<int, string>();
            foreach (var item in categorisDtos)
            {
                returnValue.Add(item.Id, item.Name);
            }
            return returnValue;
        }
        
        public async Task<Dictionary<int, string>> GetActorsWithIdsAsync()
        {
            var actorsDtos = await novelistsService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            Dictionary<int, string> returnValue = new Dictionary<int, string>();
            foreach (var item in actorsDtos)
            {
                returnValue.Add(item.Id, item.ToString());
            }
            return returnValue;
        }

        public async Task<Dictionary<int, string>> GetMoviesWithIdsAsync()
        {
            var moviesDtos = await novelService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            Dictionary<int, string> returnValue = new Dictionary<int, string>();
            foreach (var item in moviesDtos)
            {
                returnValue.Add(item.Id, item.ToString());
            }
            return returnValue;
        }



        public async Task<String> GetAllCategoriesAsync()
        {
            var categoriesDtos = await categoryService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            var result = categoriesDtos?.Aggregate("category list:", (current, categoryDto) =>
            current + $"\n{categoryDto}");
            return result;
        }

        public async Task<String> GetCategoryAsync(int id)
        {
            var categoryDto = await categoryService.GetAsync(id, CancellationToken.None);
            if (categoryDto == null)
            {
                throw new MyException($"Can't find category with {id}");
            }
            return categoryDto.ToString();
        }

        public async Task<Category?> GetCategoryModelAsync(int id) 
        {
            return await categoryService.GetAsync(id, CancellationToken.None);
        }

        public async Task<Novel?> GetMovieModelAsync(int id) 
        {
            return await novelService.GetAsync(id, CancellationToken.None);
        }


        public async Task<string> CreateCategoryAsync(Category createCategoryDto)
        {
            var response = await categoryService.CreateAsync(createCategoryDto, CancellationToken.None);
            return $"Category with id {createCategoryDto?.Id} was created";
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            await categoryService.DeleteAsync(id, CancellationToken.None);
            return $"Category with id {id} was deleted";
        }

        public async Task<string> UpdateCategoryAsync(Category category)
        {
            await categoryService.UpdateAsync(category, CancellationToken.None);
            return $"Category with id {category.Id} was updated";
        }

        public async Task<string> GetAllActorsAsync()
        {
            var actorDtos = await novelistsService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            var result = actorDtos?.Aggregate("Actor list:", (current, actorDto) =>
                current + $"\n{actorDto.ToStringWithMovies()}"
               );
            return result;
        }
        
        public async Task<Novelist?> GetActorModelAsync(int id) 
        {
            return await novelistsService.GetAsync(id, CancellationToken.None);
        }

        public async Task<string> GetActorAsync(int id)
        {
            var actorDto = await novelistsService.GetAsync(id, CancellationToken.None);
            if (actorDto == null)
            {
                throw new MyException($"Can't find actor with {id}");
            }
            return actorDto.ToStringWithMovies();
        }

        public async Task<string> CreateActorAsync(Novelist createActorDto)
        {
            var response = await novelistsService.CreateAsync(createActorDto, CancellationToken.None);
            return $"Actor with id {createActorDto?.Id} was created";
        }

        public async Task<string> UpdateActorAsync(Novelist actor)
        {
            await novelistsService.UpdateAsync(actor, CancellationToken.None);
            return $"Actor with id {actor.Id} was updated";
        }

        public async Task<string> DeleteActorAsync(int id)
        {
            await novelistsService.DeleteAsync(id, CancellationToken.None);
            return $"Actor with id {id} was deleted";
        }

        public async Task<String> GetAllMoviesAsync()
        {
            var movieDtos = await novelService.GetAllAsync(new FilterPaginationDto(""), CancellationToken.None);
            var result = movieDtos?.Aggregate("Movie list:", (current, movieDto) =>
                current + $"\n{movieDto.ToStringWithCategoryAndActors()}");
            return result;
        }

        public async Task<String> GetMovieAsync(int id)
        {
            var movieDto = await novelService.GetAsync(id, CancellationToken.None);
            if (movieDto == null)
            {
                throw new MyException($"Can't find movie with {id}");
            }
            return $"{movieDto.ToStringWithCategoryAndActors()}";
        }

        public async Task<string> CreateMovieAsync(Novel createMovieDto)
        {
            var response = await novelService.CreateAsync(createMovieDto, CancellationToken.None);
            return $"Movie with id {createMovieDto?.Id} was created";
        }

        public async Task<string> UpdateMovieAsync(Novel movie)
        {
            await novelService.UpdateAsync(movie, CancellationToken.None);
            return $"Movie with id {movie.Id} was updated";
        }

        public async Task<string> DeleteMovieAsync(int id)
        {
            await novelService.DeleteAsync(id, CancellationToken.None);
            return $"Movie with id {id} was deleted";
        }
    }
}
