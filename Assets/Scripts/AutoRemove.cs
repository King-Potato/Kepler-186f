using UnityEngine;
using System.Collections;

public class AutoRemove : MonoBehaviour
{
  public float TimeToLive;

  void Start()
  {
    Destroy(gameObject, TimeToLive);
  }
}
