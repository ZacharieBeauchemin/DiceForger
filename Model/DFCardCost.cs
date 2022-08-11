namespace Model;

public class DFCardCost {
    public int Id { get; set; }
    public ShardType Type { get; set; }
    public int Value { get; set; }

    public enum ShardType {
        Sun,
        Moon
    }
}