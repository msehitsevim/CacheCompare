namespace CouchBaseProject.Models;

public partial class Errlog
{
    public int Guid { get; set; }

    public DateTime? Tarih { get; set; }

    public int? ErrNo { get; set; }

    public string? ErrProc { get; set; }

    public int? ErrLine { get; set; }

    public string? ErrMsg { get; set; }
}
