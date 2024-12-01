namespace Orders.Models;

public record CustomerField()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; } // "Textbox" or "Dropdown"
}