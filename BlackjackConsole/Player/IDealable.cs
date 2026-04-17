using BlackjackConsole.Card;

namespace BlackjackConsole.Player;

public interface IDealable
{
    public void AddCard(Card.Card card);
    public string Name { get; }
}