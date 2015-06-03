using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
  public abstract bool Fire(out float knockback);
}