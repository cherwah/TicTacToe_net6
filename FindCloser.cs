namespace TicTacToe;

public class FindCloser : Visitor
{
  private char side;
  private bool is_done;
  private (int, int)[] closer;

  public FindCloser(char side)
  {
    this.side = side;

    is_done = false;
    closer = null;
  }

  public void ReceiveRow(char[] row, (int, int)[] pos)
  {
    int xCount = 0;
    int oCount = 0;

    for (int i = 0; i < 3; i++)
    {
      if (row[i] == 'X')
      {
        xCount++;
      }
      else if (row[i] == 'O')
      {
        oCount++;
      }
    }

    if (xCount + oCount == 2)
    {
      if ((this.side == 'X' && xCount == 2) ||
          (this.side == 'O' && oCount == 2))
      {
        closer = pos;
        is_done = true;
      }
    }
  }

  public (int, int)[] GetCloser()
  {
    return closer;
  }

  public bool IsDone()
  {
    return is_done;
  }
}
