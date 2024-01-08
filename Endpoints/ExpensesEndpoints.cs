using Carter;
using Microsoft.AspNetCore.Mvc;

public class ExpensesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var expensesMap = app.MapGroup("api/v1/").WithTags("Expenses").WithOpenApi();

        expensesMap.MapGet("all-expenses", (AppDbContext context) =>
        {
            List<Expense> expenses = context.Expenses.ToList();

            return Results.Ok(expenses);
        })
        .WithSummary("Get all expenses")
        .Produces<Expense>()
        .Produces(StatusCodes.Status404NotFound);

        expensesMap.MapGet("expense/{id}", ([FromRoute] int id, AppDbContext context) =>
        {
            ICollection<Expense> expenses = context.Expenses.Where(x => x.Id == id).ToList();

            return Results.Ok(expenses);
        })
        .WithSummary("Get expense by id")
        .WithOpenApi()
        .Produces<Expense>()
        .Produces(StatusCodes.Status404NotFound);

        expensesMap.MapPost("create-expenses", (AppDbContext context, ExpenseView model) =>
        {
            Expense mappedObject = model.MapObject();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            context.Expenses.Add(mappedObject);
            context.SaveChanges();

            return Results.Created($"create-expenses/{mappedObject.Id}", mappedObject);
        })
        .WithSummary("Create a new expense")
        .WithOpenApi()
        .Produces<Expense>()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        expensesMap.MapDelete("expense/{id}", ([FromRoute] int id, AppDbContext context) =>
        {
            ICollection<Expense> expenses = context.Expenses.Where(x => x.Id == id).ToList();

            context.Remove(expenses);
            context.SaveChanges();

            return Results.NoContent();
        })
        .WithSummary("Delete an existing expense")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);
    }


}
