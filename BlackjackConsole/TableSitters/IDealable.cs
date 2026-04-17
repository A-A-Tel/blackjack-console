namespace BlackjackConsole.TableSitters;

public interface IDealable
{
    public void AddCard(Card.Card card);
    public string Name { get; }
}