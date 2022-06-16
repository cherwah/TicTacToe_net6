namespace TicTacToe;

class Game
{
  private char human;
  private char computer;
  private char turn;
  private bool game_over;
  private GameBoard board;
  private FindWinner find_winner;

  public Game()
  {
    find_winner = new FindWinner();
    board = new GameBoard();
    game_over = false;
  }

  public void Start()
  {
    string winner;
    bool is_draw;

    ShowOpeningMenu();
    board.Show();

    while (!game_over)
    {
      if (turn == human)
      {
        HumanMove();
      }
      else if (turn == computer)
      {
        ComputerMove();
        InformLastMove(turn);
      }

      // display the updated game board
      board.Show();

      // check the status of the game
      (winner, is_draw) = Analyze();
      if (winner != null)
      {
        Console.WriteLine("\nWINNER: " + winner);
        game_over = true;
      }
      else if (is_draw)
      {
        Console.WriteLine("\nTHE GAME IS A DRAW.");
        game_over = true;
      }

      // alternative turns to move
      turn = (turn == human) ? computer : human;
    }
  }

  public void ShowOpeningMenu()
  {
    while (human != 'X' && human != 'O')
    {
      Console.WriteLine("\nWHICH SIDE DO YOU WANT?");
      Console.WriteLine("\n1. X");
      Console.WriteLine("2. O");
      Console.Write("\nPLEASE CHOOSE ONE: ");

      string input = Console.ReadLine();
      try
      {
        int option = int.Parse(input);

        if (option == 1)
        {
          human = 'X';
          computer = 'O';
        }
        else if (option == 2)
        {
          human = 'O';
          computer = 'X';
        }
      }
      catch (Exception) { }
    }

    // humans always make the first move
    turn = human;
  }

  public void HumanMove()
  {
    while (true)
    {
      Console.Write("\nHUMAN'S MOVE (e.g. 1,2): ");
      string input = Console.ReadLine();
      try
      {
        string[] pos = input.Split(',');
        int x = int.Parse(pos[0]);
        int y = int.Parse(pos[1]);

        if (!board.SetCell(x, y, human))
        {
          Console.WriteLine("INVALID MOVE.");
        }
        else
        {
          break;
        }
      }
      catch (Exception)
      {
        Console.WriteLine("INVALID INPUT.");
      }
    }
  }

  public void ComputerMove()
  {
    // check if we can win
    FindCloser computerCloser = new FindCloser(computer);
    board.Walk(computerCloser);

    (int, int)[] pos = computerCloser.GetCloser();
    if (pos != null)
    {
      MakeCloserMove(pos, computer);
      return;
    }

    // check if we need to block
    FindCloser humanCloser = new FindCloser(human);
    board.Walk(humanCloser);

    pos = humanCloser.GetCloser();
    if (pos != null)
    {
      MakeCloserMove(pos, computer);
      return;
    }

    if (!board.SetCell(1, 1, computer) &&
        !board.SetCell(0, 0, computer) &&
        !board.SetCell(0, 2, computer) &&
        !board.SetCell(2, 0, computer) &&
        !board.SetCell(2, 2, computer))
    {
      // not able to get center or one of the 4 corners,
      // make a random move
      board.SetRandomCell(computer);
    }
  }

  public void InformLastMove(char side)
  {
    int[] lastMove = board.GetLastMove();
    Console.WriteLine(String.Format("\n{0}'S MOVE: {1},{2}",
        (side == human) ? "HUMAN" : "COMPUTER",
        lastMove[0], lastMove[1]));
  }

  public void MakeCloserMove((int, int)[] pos, char side)
  {
    for (int i = 0; i < 3; i++)
    {
      int row = pos[i].Item1;
      int col = pos[i].Item2;

      if (board.GetCell(row, col) == GameBoard.DOT)
      {
        board.SetCell(row, col, side);
      }
    }
  }

  public (string, bool) Analyze()
  {
    board.Walk(find_winner);
    char? winSide = find_winner.GetWinSide();
    string winner = null;
    bool is_draw = false;

    if (winSide != null)
    {
      winner = SideToName(winSide);
    }
    else
    {
      is_draw = (board.GetTotalMoves() == 9);
    }

    return (winner, is_draw);
  }

  public string SideToName(char? side)
  {
    if (side == null)
    {
      return "";
    }

    return (side == human) ? "HUMAN" : "COMPUTER";
  }
}
