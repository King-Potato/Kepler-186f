using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchController : MonoBehaviour
{
  public float StartingTime;
  public bool Active;
  public Text TimeLeftText; 

  public float m_TimeRemaining;

  void Start()
  {
    Restart();
  }

  public void Restart()
  {
    m_TimeRemaining = StartingTime;
  }

  void Update()
  {
    if (Active)
    {
      m_TimeRemaining -= Time.deltaTime;

      if (m_TimeRemaining <= 0.0f)
      {
        Active = false;
        Restart();
      }

      TimeLeftText.text = "Time Left: " + m_TimeRemaining.ToString();
    }
  }


}