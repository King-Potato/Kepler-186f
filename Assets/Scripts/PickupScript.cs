using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{
  public GameObject Weapon;

  void OnTriggerEnter2D(Collider2D collider)
  {
    var ship = collider.GetComponent<Ship>();
    if (ship == null) return;

    Debug.Log("Creating new weapon instance.");
    var weaponObject = (GameObject)Instantiate(Weapon, Vector3.zero, Quaternion.identity);
    weaponObject.transform.parent = ship.transform;
    weaponObject.transform.localPosition = ship.Weapon.transform.localPosition;
    weaponObject.transform.localRotation = Quaternion.identity;

    Debug.Log("Updating weapon reference.");
    var oldWeapon = ship.Weapon;
    ship.Weapon = weaponObject.GetComponent<IWeapon>();

    Debug.Log("Destroying old weapon.");
    Destroy(oldWeapon.gameObject);

    Debug.Log("Destroying pickup object.");
    Destroy(gameObject);
  }
}
