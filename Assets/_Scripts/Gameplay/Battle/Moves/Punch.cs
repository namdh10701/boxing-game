using static Punching;

public class Punch
{
    public float Dmg;
    public PunchingHand PunchHand { get; private set; }
    public PunchingDirection Direction { get; private set; }
    public Punch(float dmg, PunchingHand punchHand, PunchingDirection direction)
    {
        Dmg = dmg;
        PunchHand = punchHand;
        Direction = direction;
    }
}