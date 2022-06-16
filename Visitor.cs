namespace TicTacToe;

interface Visitor
{
  void ReceiveRow(char[] row, (int, int)[] pos);
  bool IsDone();
}
