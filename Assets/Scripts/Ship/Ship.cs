using UnityEngine;

public class Ship : MonoBehaviour
{
  public IWeapon Weapon;
  public float StartingHealth = 100.0f;
  public string FireInputName;
  public Vector2 RespawnXBoundaries;
  public Vector2 RespawnYBoundaries;

  float m_health;
  ushort m_score;

  void Start()
  {
    m_health = StartingHealth;
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if(collider.gameObject.tag == "Projectile")
    {
      m_health -= collider.gameObject.GetComponent<Projectile>().Damage;
      Destroy(collider.gameObject);
    }
  }

  void Update()
  {
    if (m_health <= 0.0f)
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
    GetComponent<Transform>().position = new Vector3(Random.Range(RespawnXBoundaries.x, RespawnXBoundaries.y), Random.Range(RespawnYBoundaries.x, RespawnYBoundaries.y), 0.0f);
    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    GetComponent<Rigidbody2D>().rotation = 0.0f;
    m_health = StartingHealth;
  }

  public void Kill()
  {
    Debug.Log("Player been killed.");
    Respawn();
  }
}
