using System;
using UnityEngine;

class ProjectileWeapon : IWeapon
{
  public GameObject Projectile;

  public float ProjectileSpeed;

  public float ProjectileDamage;

  public float FireRate;
  float m_fireDelay;
  float m_fireTimer = 0.0f;

  void Start()
  {
    m_fireDelay = 1.0f / FireRate;
  }

  public override void Fire()
  {
    if (m_fireTimer > 0.0f) return;
    
    var projectile = (GameObject)Instantiate(Projectile, transform.position, transform.rotation);
    var body = projectile.GetComponent<Rigidbody2D>();
    body.velocity = transform.up * ProjectileSpeed;

    m_fireTimer = m_fireDelay;
  }

  public override float GetDamage()
  {
    return ProjectileDamage;
  }

  void Update()
  {
    if(m_fireTimer > 0.0f) m_fireTimer -= Time.deltaTime;
  }
}