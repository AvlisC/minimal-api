using Flunt.Notifications;
using Flunt.Validations;

public class ExpenseView : Notifiable<Notification>
{
    public string Title { get; set; }
    public double Value { get; set; }
    public Category Category { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Description { get; set; }
    public string? PaymentVoucher { get; set; }

    // Mapeia o objeto para verificar se todas os requerimentos foram atendidos e retorna uma resposta 
    public Expense MapObject()
    {
        AddNotifications(new Contract<Notification>()
        .Requires()
        .IsNotNull(Title, "Informe o título do gasto")
        .IsNotNull(Value, "Informe o valor do gasto")
        .IsNotNull(Category, "Informe a categoria do gasto")
        .IsNotNull(PaymentDate, "Informe uma data de pagamento"));
        return new Expense();
    }

}