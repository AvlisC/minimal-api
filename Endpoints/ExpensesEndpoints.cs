using Carter;
using Microsoft.AspNetCore.Mvc;

// Modulo que descreve os verbos dos endpoints
public class ExpensesEndpoints : ICarterModule
{
    public readonly AppDbContext _context;

    public ExpensesEndpoints()
    {
        _context = new AppDbContext();
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder expensesMap = app.MapGroup("api/v1/").WithTags("Expenses").WithOpenApi();

        expensesMap.MapGet("all-expenses", () =>
        {
            List<Expense> expenses = _context.Expenses.ToList();

            return Results.Ok(expenses);
        })
        .WithSummary("Get all expenses")
        .Produces<Expense>()
        .Produces(StatusCodes.Status404NotFound);

        expensesMap.MapGet("expense/{id}", ([FromRoute] int id) =>
        {
            ICollection<Expense> expenses = _context.Expenses.Where(x => x.Id == id).ToList();

            return Results.Ok(expenses);
        })
        .WithSummary("Get expense by id")
        .Produces<Expense>()
        .Produces(StatusCodes.Status404NotFound);

        expensesMap.MapPost("create-expenses", (ExpenseView model) =>
        {
            Expense mappedObject = model.MapObject();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            _context.Expenses.Add(mappedObject);
            _context.SaveChanges();

            return Results.Created($"create-expenses/{mappedObject.Id}", mappedObject);
        })
        .WithSummary("Create a new expense")
        .Produces<Expense>()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        expensesMap.MapDelete("expense/{id}", ([FromRoute] int id) =>
        {
            ICollection<Expense> expenses = _context.Expenses.Where(x => x.Id == id).ToList();

            _context.Remove(expenses);
            _context.SaveChanges();

            return Results.NoContent();
        })
        .WithSummary("Delete an existing expense")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);
    }


}
