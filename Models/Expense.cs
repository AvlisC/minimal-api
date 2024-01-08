using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Expense
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public double Value { get; set; }
    [ForeignKey("CategoryFK")]
    public Category Category { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? Description { get; set; }
    public double? PaymentVoucher { get; set; }
}