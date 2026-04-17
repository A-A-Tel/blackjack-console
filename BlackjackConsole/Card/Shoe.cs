namespace BlackjackConsole.Card;

public class Shoe
{
    private Queue<Card> _cards;

    public Shoe(int deckSize)
    {
        Queue<Card> cards = new();

        for (int i = 0; i < deckSize; i++)
            foreach (Suit suit in Enum.GetValuesAsUnderlyingType<Suit>())
            foreach (Rank rank in Enum.GetValuesAsUnderlyingType<Rank>())
                cards.Enqueue(new Card(suit, rank, true));

        _cards = cards;
    }

    public void Shuffle()
    {
        _cards = new Queue<Card>(_cards.Shuffle());
    }

    public Card Draw()
    {
        return _cards.Dequeue();
    }

    public void Append(Card card)
    {
        _cards.Enqueue(card);
    }
}