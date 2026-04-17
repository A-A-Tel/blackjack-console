using BlackjackConsole.Card;
using BlackjackConsole.Dealer;
using BlackjackConsole.Player;

namespace BlackjackConsole.Table;

public class Table
{
    private readonly List<Player.Player> _players = [];

    public Table(int playerCount, int rounds)
    {
        Rounds = rounds;
        Dealer = new Dealer.Dealer();

        for (int i = 0; i < playerCount; i++)
            _players.Add(new Player.Player());
    }

    public int Rounds { get; private set; }

    public IReadOnlyList<Player.Player> Players => _players;

    public Dealer.Dealer Dealer { get; }

    public GameState State { get; private set; } = GameState.Deal;

    public void Advance()
    {
        State = (GameState)(((int)State + 1) % 4);
    }

    public void DealToPlayer(Player.Player player, Card.Card card)
    {
        player.AddCard(card);
    }

    public void DealToDealer(Card.Card card)
    {
        Dealer.AddCard(card);
    }

    public IEnumerable<IDealable> GetDealSequence()
    {
        return [..Players, Dealer];
    }
}