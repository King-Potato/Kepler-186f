using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{
  public GameObject Weapon;
	public Sprite Sprite;

  void OnTriggerEnter2D(Collider2D collider)
  {
    var ship = collider.GetComponent<Ship>();
    if (ship == null) return;
    
    var weaponObject = (GameObject)Instantiate(Weapon, Vector3.zero, Quaternion.identity);
    weaponObject.transform.parent = ship.transform;
    weaponObject.transform.localPosition = ship.Weapon.transform.localPosition;
    weaponObject.transform.localRotation = Quaternion.identity;

    var oldWeapon = ship.Weapon;
    ship.Weapon = weaponObject.GetComponent<IWeapon>();

	UIIconUpdater.SetImage(ship, Sprite);

    Destroy(oldWeapon.gameObject);
    Destroy(gameObject);
  }
}
