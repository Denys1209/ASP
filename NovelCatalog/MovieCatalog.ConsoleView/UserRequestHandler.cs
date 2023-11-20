
using NovelCatalog.Application.Novelists;
using NovelCatalog.Domain.Models;
using NovelistCatalog.Domain.Models;
using System.Data;

namespace NovelCatalog.ConsoleView
{
    class UserRequestHandler
    {
        private readonly Client client;

        public UserRequestHandler(Client client)
        {
            this.client = client;
        }
        private async Task<List<Category>> GetCategoriesByIds()
        {
            var categoriesDiction = await client.GetCategorisWithIdsAsync();
            foreach (var category in categoriesDiction)
            {
                Console.WriteLine($"{category.Key} = {category.Value}");
            }
            Console.WriteLine("Enter categories ids(separated by spaces):");
            var ids = Console.ReadLine().Trim().Split(" ").Distinct().ToList();
            List<Category> categories = new List<Category>();
            foreach (var id in ids)
            {
                if (!(int.TryParse(id, out var parsedId) || (categoriesDiction.ContainsKey(parsedId))))
                {
                    Console.WriteLine($"Invalid id {id}");
                    throw new Exception($"Invalid id {id}");
                }
                categories.Add(await client.GetCategoryModelAsync(parsedId));
            }
            return categories;
        }

        private async Task<List<Novelist>> GetActorsByIds()
        {
            var actorsDiction = await client.GetActorsWithIdsAsync();
            foreach (var category in actorsDiction)
            {
                Console.WriteLine($"{category.Key} = {category.Value}");
            }
            Console.WriteLine("Enter categories ids(separated by spaces):");
            var ids = Console.ReadLine().Trim().Split(" ").Distinct().ToList();
            List<Novelist> actors = new List<Novelist>();
            foreach (var id in ids)
            {
                if (!(int.TryParse(id, out var parsedId) || (actorsDiction.ContainsKey(parsedId))))
                {
                    Console.WriteLine($"Invalid id {id}");
                    throw new Exception($"Invalid id {id}");
                }
                actors.Add(await client.GetActorModelAsync(parsedId));
            }
            return actors;
        }

        private async Task<List<Novel>> GetMoviesByIds()
        {
            var moviesDiction = await client.GetMoviesWithIdsAsync();
            foreach (var category in moviesDiction)
            {
                Console.WriteLine($"{category.Key} = {category.Value}");
            }
            Console.WriteLine("Enter categories ids(separated by spaces):");
            var ids = Console.ReadLine().Trim().Split(" ").Distinct().ToList();
            List<Novel> actors = new List<Novel>();
            foreach (var id in ids)
            {
                if (!(int.TryParse(id, out var parsedId) || (moviesDiction.ContainsKey(parsedId))))
                {
                    Console.WriteLine($"Invalid id {id}");
                    throw new Exception($"Invalid id {id}");
                }
                actors.Add(await client.GetMovieModelAsync(parsedId));
            }
            return actors;
        }



        public async Task<bool> HandleAsync(UserRequest request)
        {
            return request switch
            {
                UserRequest.CreateMovie => await CreateMovieAsync(),
                UserRequest.CreateCategory => await CreateCategoryAsync(),
                UserRequest.CreateActor => await CreateActorAsync(),
                UserRequest.UpdateMovie => await UpdateMovieAsync(),
                UserRequest.DeleteMovie => await DeleteMovieAsync(),
                UserRequest.GetMovie => await GetMovieAsync(),
                UserRequest.GetAllMovies => await GetAllMoviesAsync(),
                UserRequest.UpdateCategory => await UpdateCategoryAsync(),
                UserRequest.DeleteCategory => await DeleteCategoryAsync(),
                UserRequest.GetCategory => await GetCategoryAsync(),
                UserRequest.GetAllCategories => await GetAllCategoriesAsync(),
                UserRequest.GetAllActors => await GetAllActorsAsync(),
                UserRequest.UpdateActor => await UpdateActorAsync(),
                UserRequest.DeleteActor => await DeleteActorAsync(),
                UserRequest.GetActor => await GetActorAsync(),

                UserRequest.Exit => false,
                _ => throw new ArgumentOutOfRangeException(nameof(request), request, null)
            };
        }

        private async Task<bool> GetActorAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.GetActorAsync(parsedId);
            Console.WriteLine(message);
            return true;

        }

        private async Task<bool> DeleteActorAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.DeleteActorAsync(parsedId);
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> UpdateActorAsync()
        {
            string id;
            string firstName;
            string lastName;
            DateOnly birthDate;
            List<Novel> movies;
            Console.Write("Enter id:");
            id = Console.ReadLine() ?? "0";
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            Console.Write("Enter first name:");
            firstName = Console.ReadLine() ?? "";
            Console.Write("Enter last name:");
            lastName = Console.ReadLine() ?? "";
            Console.Write("Enter date of birth:");
            birthDate = DateOnly.Parse(Console.ReadLine());

            try
            {
                movies = await GetMoviesByIds();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            string message = await client.CreateActorAsync(
                new Novelist()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = birthDate,
                    Movies = movies,
                    Id = parsedId
                }
                );
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> CreateMovieAsync()
        {
            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name");
                return false;
            }
            Console.WriteLine($"Description:");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Invalied name");
                return false;
            }

            List<Category> categories = null;

            try
            {
                categories = await GetCategoriesByIds() ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            List<Novelist> actors = null;
            try
            {
                actors = await GetActorsByIds() ?? new List<Novelist>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var message = await client.CreateMovieAsync(new Novel()
            {
                Title = name,
                Categories = categories,
                Description = description,
                novelists = actors

            });
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> UpdateMovieAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name");
                return false;
            }

            List<Category> categories = new List<Category>();

            try
            {
                categories = await GetCategoriesByIds() ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            List<Novelist> actors =  new List<Novelist>();
            try
            {
                actors = await GetActorsByIds() ?? new List<Novelist>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var message = await client.UpdateMovieAsync(new Novel()
            {
                Title = name,
                Categories = categories,
                Id = parsedId,
                novelists = actors
                
            });
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> DeleteMovieAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.DeleteMovieAsync(parsedId);
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> GetMovieAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.GetMovieAsync(parsedId);
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> GetAllMoviesAsync()
        {
            var message = await client.GetAllMoviesAsync();
            Console.WriteLine(message);
            return true;
        }
        private async Task<bool> CreateCategoryAsync()
        {
            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name");
                return false;
            }

            var message = await client.CreateCategoryAsync(new Category()
            {
                Name = name
            });
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> UpdateCategoryAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name");
                return false;
            }

            var message = await client.UpdateCategoryAsync(new Category()
            {
                Name = name,
                Id = parsedId
            });
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> DeleteCategoryAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.DeleteCategoryAsync(parsedId);
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> GetCategoryAsync()
        {
            Console.WriteLine("Enter id:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Invalid id");
                return false;
            }

            var message = await client.GetCategoryAsync(parsedId);
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> GetAllCategoriesAsync()
        {
            var message = await client.GetAllCategoriesAsync();
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> GetAllActorsAsync()
        {
            var message = await client.GetAllActorsAsync();
            Console.WriteLine(message);
            return true;
        }

        private async Task<bool> CreateActorAsync()
        {
            string firstName;
            string lastName;
            DateOnly birthDate;
            List<Novel> movies;

            Console.Write("Enter first name:");
            firstName = Console.ReadLine() ?? "";
            Console.Write("Enter last name:");
            lastName = Console.ReadLine() ?? "";
            Console.Write("Enter date of birth:");
            birthDate = DateOnly.Parse(Console.ReadLine());

            try
            {
                movies = await GetMoviesByIds();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            string message = await client.CreateActorAsync(
                new Novelist()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = birthDate,
                    Movies = movies
                }
                );
            Console.WriteLine(message);
            return true;
        }
    }
}
