using UnityEngine;
using System.Collections;

public static class ScoreManager
{
  private static int[] m_scores = new int[4];

  static ScoreManager()
  {
    Reset();
  }

  public static void ModifyScore(int shipIndex, int amount)
  {
    m_scores[shipIndex] += amount;
  }

  public static void Reset()
  {
    for (int i = 0; i < 4; i++) m_scores[i] = 0;
  }

  public static int GetScore(int shipIndex)
  {
    return m_scores[shipIndex];
  }
}