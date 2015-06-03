using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
  private class team
  {
    public static List<team> m_teams = new List<team>();
    public List<Ship> ships;
    private const int m_teamSize = 2;

    team()
    {
      ships = new List<Ship>();
    }

    team(List<Ship> ships)
    {
      this.ships = ships;
    }

    team(Ship ship)
    {
      this.ships = new List<Ship>();
      this.ships.Add(ship);
      ship.Setteam(this);
    }

    public static void AddPlayer(Ship ship)
    {
      if (m_teams.Count == 0)
        m_teams.Add(new team());
      team m_team = m_teams[m_teams.Count - 1];
      if (m_team.ships.Count >= m_teamSize)
      {
        m_team = new team(ship);
        m_teams.Add(m_team);
      }
      else
        m_team.ships.Add(ship);
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
  private team m_team;

  public float Health;
  ushort m_score;

  Rigidbody2D m_rigidBody;

  public AudioSource ExplosionAudioSource;

  void Start()
  {
    Health = StartingHealth;

    PlayerName = gameObject.name;
    m_rigidBody = GetComponent<Rigidbody2D>();
    team.AddPlayer(this);
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if (collider.gameObject.tag == "Projectile")
    {
      Debug.Log("Hit by projectile");
      var proj = collider.gameObject.GetComponent<Projectile>();
      Health -= proj.Damage;
      Instantiate(proj.DestroyFX, collider.transform.position, collider.transform.rotation);

      Ship ship = collider.transform.GetComponent<Ship>();
      bool isFriend = false;
      if (ship)
      {
        for (int i = 0; i < m_team.ships.Count; i++)
        {
          if (m_team.ships[i] == ship)
          {
            isFriend = true;
            break;
          }
        }
      }
      if (isFriend)
        Health -= collider.gameObject.GetComponent<Projectile>().Damage;

      Destroy(collider.gameObject);

      Debug.Log("New Health" + Health);
    }
    else
    {
      var impactMagnitude = collider.relativeVelocity.magnitude;

      Debug.Log("Impact Magnitude " + impactMagnitude);

      if (impactMagnitude > ImpactDamageThreshold)
      {
        Health -= impactMagnitude * ImpactDamageMultiplier;
        Debug.Log("New Health" + Health);
      }
    }
  }

  void Update()
  {
    if (Health <= 0.0f)
    {
      Kill();
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
    Health = StartingHealth;
  }

  public void Kill()
  {
    Debug.Log("Player been killed.");
    ExplosionAudioSource.Play();
    Respawn();
  }

  private void Setteam(team m_team)
  {
    this.m_team = m_team;
  }
}
