public static class GameState
{
  public enum State
  {
    ReadyToPlay,
    Running,
    Results
  }

  public static State CurrentState;

  public static int Winner;

  static GameState()
  {
    CurrentState = State.ReadyToPlay;
  }
}