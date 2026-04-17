using BlackjackConsole.Card;
using BlackjackConsole.Player;
using NameGenerator.Generators;
using Action = BlackjackConsole.Player.Action;

namespace BlackjackConsole.TableSitters;

public class Player : IDealable
{
    private static readonly RealNameGenerator NameGenerator = new();
    public Hand Hand { get; } = new();

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

    public void PlaceBet()
    {
        Bet = new Random().Next(500);
    }

    public int Bet { get; set; }

    public void AddCard(Card.Card card)
    {
        Hand.AddCard(card);
    }

    public override string ToString()
    {
        return $"{Name} - Inzet: {Bet} - Waarde: {Hand.GetValue()} ({Hand})";
    }
}