using BlackjackConsole.TableSitters;
using Action = BlackjackConsole.TableSitters.Action;

namespace BlackjackConsole.Table;

public class Table
{
    private readonly List<Player> _players = [];

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
        State = (GameState)(((int)State + 1) % 3);
    }

    public void Play()
    {
        while (true) StartNextSequence();
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
            case GameState.Play: StartPlaySequence(); break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void StartDealSequence()
    {
        var participants = GetDealSequence();

        Console.WriteLine("\nTafel:");
        Console.WriteLine(string.Join(" - ", _players.Select(p => p.Name)));
        Console.WriteLine("\n\nDealer");
        

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

            foreach (IDealable participant in participants)
                _dealer.Deal(participant);

            RenderTable();

        Advance();
    }
    
    public void StartPlaySequence()
    {
        Console.WriteLine("\n=== SPEELRONDE ===");

        foreach (var player in _players)
        {
            Console.WriteLine($"\nSpeler aan de beurt: {player.Name}");

            bool done = false;

            while (!done)
            {
                var expectedAction = player.GetAction();

                Console.WriteLine($"Speler {player.Name} {player.GetGestureText(expectedAction)}");

                while (true)
                {
                    Console.Write("Welke actie voer je uit? (hit/stand/double): ");
                    string? input = Console.ReadLine()?.ToLower();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Voer een geldige actie in.");
                        continue;
                    }

                    if (!TryParseAction(input, out Action chosenAction))
                    {
                        Console.WriteLine("Onbekende actie. Gebruik: hit, stand, double");
                        continue;
                    }

                    if (chosenAction == expectedAction)
                    {
                        Console.WriteLine($"✔ Juist — {player.Name} -> {expectedAction}");

                        ExecuteAction(player, chosenAction);

                        if (chosenAction is Action.Stand or Action.Double)
                            done = true;

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"✘ Onjuist. Verwachtte: {expectedAction}");
                    }
                }
            }
        }

        Advance();
    }

    public List<IDealable> GetDealSequence()
    {
        return [..Players, _dealer];
    }

    private static bool TryParseAction(string input, out Action action)
    {
        action = input switch
        {
            "hit" => Action.Hit,
            "stand" => Action.Stand,
            "double" => Action.Double,
            _ => default
        };

        return input is "hit" or "stand" or "double";
    }
    
    private void ExecuteAction(TableSitters.Player player, Action action)
    {
        switch (action)
        {
            case Action.Hit:
                _dealer.Deal(player);
                break;

            case Action.Double:
                _dealer.Deal(player);
                // optionally double bet here
                break;

            case Action.Stand:
                // do nothing
                break;
        }

        Console.WriteLine(player);
    }
}