using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchController : MonoBehaviour
{
  public float StartingTime;
  public bool Active;
  public Text TimeLeftText; 

  float m_TimeRemaining;

  public UIControllerScript UIController;

  void Start()
  {
    Restart();
  }

  public void Restart()
  {
    m_TimeRemaining = StartingTime;
    ScoreManager.Reset();
  }

  void Update()
  {
    if (Active && GameState.CurrentState == GameState.State.Running)
    {
      m_TimeRemaining -= Time.deltaTime;

      if (m_TimeRemaining <= 0.0f)
      {
        Active = false;

        int winner = -1;
        int winningScore = 0;
        for(int i = 0; i < 4; i++)
        {
          if (ScoreManager.GetScore(i) > winningScore) winner = i;
        }

        GameState.Winner = winner;
        GameState.CurrentState = GameState.State.Results;
        UIController.UpdateUIs();
      }

      TimeLeftText.text = "Time Left: " + m_TimeRemaining.ToString();
    }
  }


}