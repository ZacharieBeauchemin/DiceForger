namespace Model;

public class DFCard {
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public int GloryPoints { get; set; }

    public List<DFCardCost> Costs { get; set; } = new();
    public List<DFCardEffect> Effects { get; set; } = new();

    public CardSet Set { get; set; }

    public override string ToString() => Name;

    public enum CardSet {
        Standard,
        Alternative,
        Goddess,
        Titans
    }
}