using BlackjackConsole.Card;
using NameGenerator.Generators;
using Action = BlackjackConsole.Player.Action;

namespace BlackjackConsole.Player;

public class Player : IDealable
{
    private static readonly RealNameGenerator NameGenerator = new();
    public Hand Hand { get; } = new();

    public Chipset Chipset { get; } = new();

    public string Name { get; } = NameGenerator.Generate();

    public Action GetAction()
    {
        int value = Hand.GetValue();
        return Hand.GetValue() switch
        {
            < 6 => Action.Double,
            >= 17 => Action.Stand,
            _ => Action.Hit
        };
    }

    public void AddCard(Card.Card card)
    {
        Hand.AddCard(card);
    }

    public override string ToString()
    {
        return $"{Name} - {Hand.GetValue()} ({Hand.ToString()})";
    }
}