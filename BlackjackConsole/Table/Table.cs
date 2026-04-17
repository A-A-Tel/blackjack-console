using BlackjackConsole.Player;
using BlackjackConsole.TableSitters;

namespace BlackjackConsole.Table;

public class Table
{
    private readonly List<Player.Player> _players = [];

    public Table(int playerCount, int rounds)
    {
        Rounds = rounds;
        Dealer = new Dealer();

        for (int i = 0; i < playerCount; i++)
            _players.Add(new Player.Player());
    }

    public int Rounds { get; private set; }

    public void RenderTable()
    {
        foreach (Player.Player player in _players)
        {
            Console.WriteLine(player.ToString());
        }
    }

    public IReadOnlyList<Player.Player> Players => _players;

    public Dealer Dealer { get; }

    public GameState State { get; private set; } = GameState.Deal;

    public void Advance()
    {
        State = (GameState)(((int)State + 1) % 4);
    }

    public IEnumerable<IDealable> GetDealSequence()
    {
        return [..Players, Dealer];
    }
}