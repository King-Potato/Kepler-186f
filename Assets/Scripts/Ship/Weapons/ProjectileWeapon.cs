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

  AudioSource m_audio;

  int ownerID = -1;

  void Start()
  {
    m_fireDelay = 1.0f / FireRate;
    m_audio = GetComponent<AudioSource>();

    var ship = GetComponentInParent<Ship>();
    ownerID = ship.ID;
  }

  public override bool Fire(out float knockback)
  {
    if (m_fireTimer > 0.0f)
    {
      knockback = 0.0f;
      return false;
    }

    m_audio.Play();
    
    var projectile = (GameObject)Instantiate(Projectile, transform.position, transform.rotation);

    var body = projectile.GetComponent<Rigidbody2D>();
    body.velocity = transform.up * ProjectileSpeed;

    var proj = projectile.GetComponent<Projectile>();
    proj.Damage = ProjectileDamage;
    proj.FiredFrom = ownerID;

    m_fireTimer = m_fireDelay;
    
    knockback = 1000.0f * body.mass;

    return true;
  }
  
  void Update()
  {
    if(m_fireTimer > 0.0f) m_fireTimer -= Time.deltaTime;
  }
}