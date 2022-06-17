namespace TicTacToe;

public class FindCloser : Visitor
{
  private char side;
  private bool is_done;
  private (int, int)[]? closer = null;

  public FindCloser(char side)
  {
    this.side = side;

    is_done = false;
  }

  public void ReceiveRow(char[] row, (int, int)[] pos)
  {
    int xCount = 0;
    int oCount = 0;

    for (int i = 0; i < 3; i++)
    {
      if (row[i] == 'X')
      {
        // check how many 'X' in that given row
        xCount++;
      }
      else if (row[i] == 'O')
      {
        // check how many 'O' in that given row
        oCount++;
      }
    }

    // possible to close ONLY when there are exactly
    // two markers (either 'X' or 'O') on a row
    if (xCount + oCount == 2)
    {
      if ((side == 'X' && xCount == 2) ||
        (side == 'O' && oCount == 2))
      {
        // this row can be closed with either 
        // one more 'X' or one more 'O'.
        closer = pos;
        is_done = true;
      }
    }
  }

  public (int, int)[]? GetCloser()
  {
    return closer;
  }

  public bool IsDone()
  {
    return is_done;
  }
}
