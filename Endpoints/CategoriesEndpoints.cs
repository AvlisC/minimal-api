using Carter;
using Microsoft.AspNetCore.Mvc;

public class CategoriesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder categoriesMap = app.MapGroup("api/v1/").WithTags("Categories").WithOpenApi();

        categoriesMap.MapGet("all-categories", (AppDbContext context) =>
        {
            List<Category> categories = context.Categories.ToList();

            return Results.Ok(categories);
        })
        .WithSummary("Get all categories")
        .Produces<Category>()
        .Produces(StatusCodes.Status404NotFound);

        categoriesMap.MapPost("create-category", (AppDbContext context, CategoryView model) =>
        {
            Category mappedObject = model.MapObject();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            context.Categories.Add(mappedObject);
            context.SaveChanges();

            return Results.Ok(mappedObject);
        })
        .WithSummary("Create a new category")
        .Produces<Category>()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        categoriesMap.MapDelete("category/{id}", ([FromRoute] int id, AppDbContext context) =>
        {
            ICollection<Category> categories = context.Categories.Where(x => x.Id == id).ToList();

            context.Remove(categories);
            context.SaveChanges();

            return Results.NoContent();
        })
        .WithSummary("Delete an existing category")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);
    }


}
