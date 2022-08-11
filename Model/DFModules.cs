namespace Model;

public class DFModules {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public virtual List<DFCard> RequiredCards { get; set; } = new();
}
