using BlackjackConsole.Player;
using BlackjackConsole.TableSitters;

namespace BlackjackConsole.Table;

public class Table
{
    private readonly List<TableSitters.Player> _players = [];

    public Table(int playerCount, int rounds)
    {
        Rounds = rounds;
        _dealer = new Dealer();

        for (int i = 0; i < playerCount; i++)
            _players.Add(new TableSitters.Player());
    }

    public int Rounds { get; private set; }


    public IReadOnlyList<TableSitters.Player> Players => _players;

    private Dealer _dealer;

    public GameState State { get; private set; } = GameState.Deal;

    public void RenderTable()
        {
            foreach (TableSitters.Player player in _players)
            {
                Console.WriteLine(player.ToString());
            }
        }
    
    public void Advance()
    {
        State = (GameState)(((int)State + 1) % 4);
    }

    public void Play()
    {
        for (int i = 0; i < Rounds; i++)
        {
            StartNextSequence();
        }
    }

    private void Reset()
    {
        _players.Clear();
        for (int i = 0; i < _players.Count; i++)
            _players.Add(new TableSitters.Player());
        _dealer =  new Dealer();
    }

    public void StartNextSequence()
    {
        switch (State)
        {
            case GameState.Deal: StartDealSequence(); break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void StartDealSequence()
    {
        var participants = GetDealSequence();

        Console.WriteLine("\nTafel:");
        Console.WriteLine(string.Join(" - ", _players.Select(p => p.Name)));
        Console.WriteLine("\n\nDealer");

        for (int round = 0; round < 2; round++)
        {
            Console.WriteLine($"\n--- Ronde {round + 1} ---");

            foreach (var participant in participants)
            {
                while (true)
                {
                    Console.Write("Wie is er nu aan de beurt? ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Vul een geldige naam in");
                        continue;
                    }

                    string expectedName = participant is TableSitters.Player p
                        ? p.Name
                        : "Dealer";

                    if (input.Equals(expectedName, StringComparison.OrdinalIgnoreCase))
                    {
                        _dealer.Deal(participant);

                        Console.WriteLine($"✔ Juist — {expectedName} krijgt een kaart");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"✘ Onjuist. Verwachtte: {expectedName}");
                    }
                }
            }

            RenderTable();
        }

        Advance();
    }

    public List<IDealable> GetDealSequence()
    {
        return [..Players, _dealer];
    }
}