using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScoreUpdater : MonoBehaviour
{
  public int ShipIndex;

  void Update()
  {
    GetComponentInChildren<Text>().text = ScoreManager.GetScore(ShipIndex).ToString();
  }
}