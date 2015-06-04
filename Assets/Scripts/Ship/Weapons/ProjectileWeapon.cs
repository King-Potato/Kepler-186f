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

    var proj = projectile.GetComponent<Projectile>();
    proj.Damage = ProjectileDamage;

    m_fireTimer = m_fireDelay;
  }
  
  void Update()
  {
    if(m_fireTimer > 0.0f) m_fireTimer -= Time.deltaTime;
  }
}