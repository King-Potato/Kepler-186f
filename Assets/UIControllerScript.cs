using UnityEngine;
using System.Collections;

public class UIControllerScript : MonoBehaviour
{
  public Canvas ReadyToPlayUI;

  public Canvas InGameUI;

  public Canvas ResultsUI;

  void Start()
  {
    UpdateUIs();
  }

  void Update()
  {
    if (GameState.CurrentState == GameState.State.Results)
    {
      if (Input.GetButtonDown("Fire0") ||
        Input.GetButtonDown("Fire1") ||
        Input.GetButtonDown("Fire2") ||
        Input.GetButtonDown("Fire3"))
      {
        GameState.CurrentState = GameState.State.ReadyToPlay;
        Application.LoadLevel(Application.loadedLevel);
      }
    }
  }

  public void UpdateUIs()
  {
    switch (GameState.CurrentState)
    {
      case GameState.State.ReadyToPlay:
        ReadyToPlayUI.enabled = true;
        InGameUI.enabled = false;
        ResultsUI.enabled = false;
        break;

      case GameState.State.Running:
        ReadyToPlayUI.enabled = false;
        InGameUI.enabled = true;
        ResultsUI.enabled = false;
        break;

      case GameState.State.Results:
        ReadyToPlayUI.enabled = false;
        InGameUI.enabled = false;
        ResultsUI.enabled = true;
        break;
    }
  }
}
