namespace BlackjackConsole.Card;

public class Card
{
    private static readonly Dictionary<Rank, int> BlackjackValues = new()
    {
        { Rank.Two, 2 },
        { Rank.Three, 3 },
        { Rank.Four, 4 },
        { Rank.Five, 5 },
        { Rank.Six, 6 },
        { Rank.Seven, 7 },
        { Rank.Eight, 8 },
        { Rank.Nine, 9 },
        { Rank.Ten, 10 },
        { Rank.Jack, 10 },
        { Rank.Queen, 10 },
        { Rank.King, 10 },
        { Rank.Ace, 11 }
    };

    public Card(Suit suit, Rank rank, bool flipped)
    {
        Rank = rank;
        Suit = suit;
        Flipped = flipped;
        Value = BlackjackValues[rank];
    }

    public Rank Rank { get; init; }
    public Suit Suit { get; init; }
    public int Value { get; init; }
    public bool Flipped { get; private set; }

    public void Flip()
    {
        Flipped = !Flipped;
    }
}