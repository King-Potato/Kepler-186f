using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : Damageable
{
  public IWeapon Weapon;
  public string PlayerName;
  public float StartingRespawnTimer = 10.0f;
  public int ScorePerKill = 1000;
  public string FireInputName;
  public Vector2 RespawnXBoundaries;
  public Vector2 RespawnYBoundaries;
  public GameObject ExplosionEffect;
  
  ushort m_score;
  float m_respawnTimer;

  Rigidbody2D m_rigidBody;

  public AudioSource ExplosionAudioSource;

  void Start()
  {
    Initialise();
    m_respawnTimer = StartingRespawnTimer;

    PlayerName = gameObject.name;
    m_rigidBody = GetComponent<Rigidbody2D>();
  }

  protected override void OnCollision(Collision2D collision)
  {
    var proj = collision.gameObject.GetComponent<Projectile>();
    if (proj == null) return;

    ScoreManager.ModifyScore(proj.FiredFrom, (int)proj.Damage);
    
    if (Health <= 0.0f)
    {
      ScoreManager.ModifyScore(proj.FiredFrom, ScorePerKill);
    }
  }

  void Update()
  {
    if (Health <= 0.0f && !m_dead)
    {
      Health = 0.0f;
      Kill();
      return;
    }

    if (m_dead)
    {
      if (GetComponent<ShipControl>().enabled)
      {
        GetComponent<ShipControl>().StopAll();
        GetComponent<ShipControl>().enabled = false;
      }

      m_respawnTimer -= Time.deltaTime;

      if (m_respawnTimer <= 0.0f)
      {
        Respawn();
      }

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

  void StartRespawn()
  {
    m_dead = true;
  }

  void Respawn()
  {
    GetComponent<Transform>().position = new Vector3(Random.Range(RespawnXBoundaries.x, RespawnXBoundaries.y), Random.Range(RespawnYBoundaries.x, RespawnYBoundaries.y), 0.0f);
    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    GetComponent<Rigidbody2D>().rotation = 0.0f;
    GetComponent<ShipControl>().enabled = true;
    GetComponent<ShipControl>().StartAll();

    var renderers = GetComponentsInChildren<Renderer>();
    foreach (var r in renderers)
    {
      r.material.color = Color.white;
    }

    Health = StartingHealth;
    m_respawnTimer = StartingRespawnTimer;
    m_dead = false;
  }

  public void Kill()
  {
    ExplosionAudioSource.Play();
    StartRespawn();

    var renderers = GetComponentsInChildren<Renderer>();
    foreach(var r in renderers)
    {
      r.material.color = new Color(0.2f, 0.2f, 0.2f);
    }

    Instantiate(ExplosionEffect, transform.position, transform.rotation);
  }
}
