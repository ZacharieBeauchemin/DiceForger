namespace Model;

public class DFCardEffect {
    public int Id { get; set; }
    public EffectType Type { get; set; }
    public string Description { get; set; } = "";

    public enum EffectType {
        Instant,
        Activated,
        Timed
    }
}