namespace Entities;


public partial class ErrorLog
{
    public int Id { get; set; }
    public DateTime? ErrorDate { get; set; }
    public int? ErrorNo { get; set; }
    public int? ErrorLine { get; set; }
    public string? ErrorMessage { get; set; }

}