using UnityEngine;

public class Ship : MonoBehaviour
{
  public IWeapon Weapon;

  public string FireInputName;

  void Update()
  {
    if(Input.GetButtonDown(FireInputName))
    {
      Weapon.Fire();
    }
  }
}
