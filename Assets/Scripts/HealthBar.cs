using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
  public Ship AttachedShip;

  public Vector3 Offset;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Camera.main.WorldToScreenPoint(AttachedShip.transform.position) + Offset;
    transform.localScale = new Vector3(AttachedShip.Health / 100.0f, 1.0f, 1.0f);
  }
}