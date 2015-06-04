using UnityEngine;
using System.Collections;

public class UIFollowScript : MonoBehaviour
{
  public Transform Target;

  public void Update()
  {
    if (Target == null) return;
    transform.position = Camera.main.WorldToScreenPoint(Target.position);
  }
}
