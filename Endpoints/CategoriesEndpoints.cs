using Carter;
using Microsoft.AspNetCore.Mvc;

// Modulo que descreve os verbos dos endpoints
public class CategoriesEndpoints : ICarterModule
{
    public readonly AppDbContext _context;

    public CategoriesEndpoints()
    {
        _context = new AppDbContext();
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder categoriesMap = app.MapGroup("api/v1/").WithTags("Categories").WithOpenApi();

        categoriesMap.MapGet("all-categories", () =>
        {
            List<Category> categories = _context.Categories.ToList();

            return Results.Ok(categories);
        })
        .WithSummary("Get all categories")
        .Produces<Category>()
        .Produces(StatusCodes.Status404NotFound);

        categoriesMap.MapPost("create-category", (CategoryView model) =>
        {
            Category mappedObject = model.MapObject();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            _context.Categories.Add(mappedObject);
            _context.SaveChanges();

            return Results.Ok(mappedObject);
        })
        .WithSummary("Create a new category")
        .Produces<Category>()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        categoriesMap.MapDelete("category/{id}", ([FromRoute] int id) =>
        {
            ICollection<Category> categories = _context.Categories.Where(x => x.Id == id).ToList();

            _context.Remove(categories);
            _context.SaveChanges();

            return Results.NoContent();
        })
        .WithSummary("Delete an existing category")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);
    }


}
