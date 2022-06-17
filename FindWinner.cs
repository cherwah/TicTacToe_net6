namespace TicTacToe;

public class FindWinner : Visitor
{
  private char[] sides = { 'X', 'O' };
  private char? win_side;
  private bool is_done;

  public FindWinner()
  {
    win_side = null;
    is_done = false;
  }

  public void ReceiveRow(char[] row, (int, int)[] pos)
  {
    for (int k = 0; k < 2; k++)
    {
      char side = sides[k];

      if (row[0] == side && row[1] == side && row[2] == side)
      {
        win_side = side;
        is_done = true;

        break;
      }
    }
  }

  public char? GetWinSide()
  {
    return win_side;
  }

  public bool IsDone()
  {
    return is_done;
  }
}
