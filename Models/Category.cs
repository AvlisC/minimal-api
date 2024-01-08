using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public double Amount { get; set; }
    public double Budget { get; set; }
}