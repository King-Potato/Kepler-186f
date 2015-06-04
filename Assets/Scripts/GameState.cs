public static class GameState
{
  public enum State
  {
    ReadyToPlay,
    Running,
    Results
  }

  public static State CurrentState;

  static GameState()
  {
    CurrentState = State.ReadyToPlay;
  }
}