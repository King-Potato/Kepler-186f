using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
  private class Team
	{
		public static List<Team> teams = new List<Team>();
		public List<Ship> ships;
		private const int teamSize = 2;

		Team()
		{
			ships = new List<Ship>();
		}

		Team (List<Ship> ships)
		{
			this.ships = ships;
		}

		Team (Ship ship)
		{
			this.ships = new List<Ship>();
			this.ships.Add(ship);
		}

		public static void AddPlayer(Ship ship)
		{
			if (teams.Count == 0)
				teams.Add(new Team());
			Team team = teams[teams.Count - 1];
			if (team.ships.Count >= teamSize)
			{
				team = new Team(ship);
				teams.Add(team);
			}
			else
				team.ships.Add(ship);
		}
	}
  public IWeapon Weapon;
  public string PlayerName;
  public float StartingHealth = 100.0f;
  public float ImpactDamageThreshold = 10.0f;
  public float ImpactDamageMultiplier = 2.0f;
  public string FireInputName;
  public Vector2 RespawnXBoundaries;
  public Vector2 RespawnYBoundaries;

  float m_health;
  ushort m_score;

  Rigidbody2D m_rigidBody;

  void Start()
  {
    m_health = StartingHealth;
    PlayerName = gameObject.name;
    m_rigidBody = GetComponent<Rigidbody2D>();
	Team.AddPlayer(this);
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if (collider.gameObject.tag == "Projectile")
    {
      Debug.Log("Hit by projectile");
      m_health -= collider.gameObject.GetComponent<Projectile>().Damage;
      Destroy(collider.gameObject);
      Debug.Log("New Health" + m_health);
    }
    else
    {
      var impactMagnitude = collider.relativeVelocity.magnitude;

      Debug.Log("Impact Magnitude " + impactMagnitude);

      if (impactMagnitude > ImpactDamageThreshold)
      {
        m_health -= impactMagnitude * ImpactDamageMultiplier;
        Debug.Log("New Health" + m_health);
      }
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
      float knockback = 0.0f;
      if(Weapon.Fire(out knockback))
      {
        m_rigidBody.AddForce(-transform.up * knockback);
      }
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
