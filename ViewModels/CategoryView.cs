using Flunt.Notifications;
using Flunt.Validations;

public class CategoryView : Notifiable<Notification>
{ 
    public string Name { get; set; }
    public string Color { get; set; }
    public int Amount { get; set; }
    public int Budget { get; set; }

    // Mapeia o objeto para verificar se todas os requerimentos foram atendidos e retorna uma resposta 
    public Category MapObject(){
        AddNotifications(new Contract<Notification>()
        .Requires()
        .IsNotNull(Name, "Informe o nome da categoria")
        .IsNotNull(Color, "Informe a cor da categoria")
        .IsNotNull(Amount, "Informe a quantidade")
        .IsNotNull(Budget, "Informe um orçamento para esta categoria"));
        return new Category();
    }
}