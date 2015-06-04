﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
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
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if (collider.gameObject.tag == "Projectile")
    {
      Debug.Log("Hit by projectile");

      var proj = collider.gameObject.GetComponent<Projectile>();
      m_health -= proj.Damage;
      Instantiate(proj.DestroyFX, collider.transform.position, collider.transform.rotation);

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
