namespace TicTacToe;

class GameBoard
{
  public const char DOT = '.';  // represent available slot
  private char[,] board;
  private int[,] history;
  private int history_len;

  public GameBoard()
  {
    // assign a DOT character to every position
    // in our 3x3 board to denote availability
    board = new char[3, 3];
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        board[i, j] = DOT;
      }
    }

    // only 9 possible moves; each move consists of a (i, j) pair
    history = new int[9, 2];

    history_len = 0;  // no history when game starts
  }

  public void Walk(Visitor visitor)
  {
    char[] row = { DOT, DOT, DOT };

    // walk the board horizontally
    for (int i = 0; i < 3; i++)
    {
      row[0] = board[i, 0];
      row[1] = board[i, 1];
      row[2] = board[i, 2];

      // (int, int)[] is an array that can store tuple of integers (e.g. (1,1)) 
      visitor.ReceiveRow(row, new (int, int)[] { (i, 0), (i, 1), (i, 2) });
      if (visitor.IsDone())
      {
        return;
      }
    }

    // walk the board vertically
    for (int i = 0; i < 3; i++)
    {
      row[0] = board[0, i];
      row[1] = board[1, i];
      row[2] = board[2, i];

      visitor.ReceiveRow(row, new (int, int)[] { (0, i), (1, i), (2, i) });
      if (visitor.IsDone())
      {
        return;
      }
    }

    // walk the board diagonally left
    row[0] = board[0, 0];
    row[1] = board[1, 1];
    row[2] = board[2, 2];
    visitor.ReceiveRow(row, new (int, int)[] { (0, 0), (1, 1), (2, 2) });
    if (visitor.IsDone())
    {
      return;
    }

    // walk the board diagonally right
    row[0] = board[0, 2];
    row[1] = board[1, 1];
    row[2] = board[2, 0];
    visitor.ReceiveRow(row, new (int, int)[] { (0, 2), (1, 1), (2, 0) });
    if (visitor.IsDone())
    {
      return;
    }
  }

  public bool SetCell(int row, int col, char ch)
  {
    if (row < 0 || row > 3)
    {
      return false;
    }

    if (col < 0 || col > 3)
    {
      return false;
    }

    // our board only recognizes 'X' or 'O'
    if (ch != 'X' && ch != 'O')
    {
      return false;
    }

    // check if position is available
    if (board[row, col] != DOT)
    {
      return false;
    }

    board[row, col] = ch;
    history[history_len, 0] = row;
    history[history_len, 1] = col;
    history_len++;
    return true;
  }

  public char? GetCell(int row, int col)
  {
    if (row < 0 || row > 3)
    {
      return null;
    }

    if (col < 0 || col > 3)
    {
      return null;
    }

    return board[row, col];
  }

  public bool SetRandomCell(char ch)
  {
    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        if (board[i, j] == DOT)
        {
          board[i, j] = ch;

          history[history_len, 0] = i;
          history[history_len, 1] = j;
          history_len++;
          return true;
        }
      }
    }

    return false;
  }

  public int[]? GetLastMove()
  {
    if (history_len == 0)
    {
      return null;
    }

    return new int[] { history[history_len - 1, 0], history[history_len - 1, 1] };
  }

  public int GetTotalMoves()
  {
    return history_len;
  }

  public void Show()
  {
    for (int i = 0; i < 3; i++)
    {
      Console.WriteLine(String.Format("{0} {1} {2}",
          board[i, 0], board[i, 1], board[i, 2]));
    }
  }
}
