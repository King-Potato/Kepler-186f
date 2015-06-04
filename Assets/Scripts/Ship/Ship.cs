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

    Team(List<Ship> ships)
    {
      this.ships = ships;
    }

    Team(Ship ship)
    {
      this.ships = new List<Ship>();
      this.ships.Add(ship);
      ship.SetTeam(this);
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
  public float StartingHealth = 100.0f;
  public string FireInputName;
  public Vector2 RespawnXBoundaries;
  public Vector2 RespawnYBoundaries;
  private Team team;

  float m_health;
  ushort m_score;

  Rigidbody2D m_rigidBody;

  void Start()
  {
    m_health = StartingHealth;
    m_rigidBody = GetComponent<Rigidbody2D>();
    Team.AddPlayer(this);
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if (collider.gameObject.tag == "Projectile")
    {
      Debug.Log("Hit by projectile");
      var proj = collider.gameObject.GetComponent<Projectile>();
      m_health -= proj.Damage;
      Instantiate(proj.DestroyFX, collider.transform.position, collider.transform.rotation);

      Ship ship = collider.transform.GetComponent<Ship>();
      bool isFriend = false;
      if (ship)
      {
        for (int i = 0; i < team.ships.Count; i++)
        {
          if (team.ships[i] == ship)
          {
            isFriend = true;
            break;
          }
        }
      }
      if (isFriend)
        m_health -= collider.gameObject.GetComponent<Projectile>().Damage;

      Destroy(collider.gameObject);

      Debug.Log("New Health" + m_health);
    }
    else
    {
      var impactMagnitude = collider.relativeVelocity.magnitude;

      Debug.Log("Impact Magnitude " + impactMagnitude);

      if (impactMagnitude > 10.0f)
      {
        m_health -= impactMagnitude;
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
      if (Weapon.Fire(out knockback))
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

  private void SetTeam(Team team)
  {
    this.team = team;
  }
}
