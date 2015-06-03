using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
  public abstract void Fire();

  public abstract float GetDamage();
}