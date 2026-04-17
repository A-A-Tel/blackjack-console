using BlackjackConsole.Card;

namespace BlackjackConsole.TableSitters;

public class Dealer : IDealable
{
    public Hand Hand { get; } = new();
    public Shoe Shoe { get; } = new(4);

    public void Deal(IDealable participant)
    {
        participant.AddCard(Shoe.Draw());
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Payout()
    {
        throw new NotImplementedException();
    }

    public void AddCard(Card.Card card)
    {
        Hand.AddCard(card);
    }

    public string Name => "Dealer";
}