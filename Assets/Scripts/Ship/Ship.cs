using UnityEngine;

public class Ship : MonoBehaviour
{
  public IWeapon Weapon;
  public float StartingHealth = 100.0f;
  public float Health;
  public ushort Score;
  public string FireInputName;
  public Vector2 RespawnBoundaries;


  void Start()
  {
    Health = StartingHealth;
  }

  void Update()
  {
    if (Health <= 0.0f)
    {
      Respawn();
      return;
    }

    if (Input.GetButton(FireInputName))
    {
      Weapon.Fire();
    }
  }

  void Respawn()
  {
    GetComponent<Transform>().position = new Vector3(Random.Range(RespawnBoundaries.x, RespawnBoundaries.y), Random.Range(RespawnBoundaries.x, RespawnBoundaries.y), 0.0f);
    Health = StartingHealth;
  }
}
