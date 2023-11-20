using NovelCatalog.ConsoleView;

var client = new Client();
var userRequestHandler = new UserRequestHandler(client);


var instructions = new Dictionary<UserRequest, string>
{
    { UserRequest.Exit, "Exit the application: exit" },
    { UserRequest.GetAllCategories, "Get all categories: getallcategories" },
    { UserRequest.GetCategory, "Get a specific category: getcategory <category_id>" },
    { UserRequest.CreateCategory, "Create a new category: createcategory <category_name>" },
    { UserRequest.DeleteCategory, "Delete a specific category: deletecategory <category_id>" },
    { UserRequest.UpdateCategory, "Update a specific category: updatecategory <category_id> <new_category_name>" },
    { UserRequest.GetAllActors, "Get all actors: getallactors" },
    { UserRequest.GetActor, "Get a specific actor: getactor <actor_id>" },
    { UserRequest.CreateActor, "Create a new actor: createactor <actor_name>" },
    { UserRequest.UpdateActor, "Update a specific actor: updateactor <actor_id> <new_actor_name>" },
    { UserRequest.DeleteActor, "Delete a specific actor: deleteactor <actor_id>" },
    { UserRequest.GetAllMovies, "Get all movies: getallmovies" },
    { UserRequest.GetMovie, "Get a specific movie: getmovie <movie_id>" },
    { UserRequest.CreateMovie, "Create a new movie: createmovie <movie_name>" },
    { UserRequest.UpdateMovie, "Update a specific movie: updatemovie <movie_id> <new_movie_name>" },
    { UserRequest.DeleteMovie, "Delete a specific movie: deletemovie <movie_id>" }
};


while (true)
{
    foreach (var request in instructions.Keys) 
    {
        Console.WriteLine($"{(int)request}){instructions[request]}");
    }

    Console.WriteLine("Enter what you need...");
    var input = Console.ReadLine();

    if (!int.TryParse(input, out var instruction) || !Enum.IsDefined(typeof(UserRequest), instruction))
    {
        Console.WriteLine("Invalid input");
        continue;
    }

    var result = await userRequestHandler.HandleAsync((UserRequest)instruction);
    if (!result)
        break;
}


