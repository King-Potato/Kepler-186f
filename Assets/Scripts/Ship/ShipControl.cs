﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl : MonoBehaviour
{
  public string ThrustInput = "Thrust0";

  public string SteerInput = "Steer0";

  public ShipConfigObject ShipConfig;

  Rigidbody2D m_rigidBody;

  public AudioSource EngineAudioSource;
  float m_engineSpool = 0.0f;

  ParticleSystem m_particleSystem;
  float m_particleNormalSpeed;

  void Start()
  {
    m_rigidBody = GetComponent<Rigidbody2D>();

    m_particleSystem = transform.FindChild("Thruster Particles").GetComponent<ParticleSystem>();
    if (m_particleSystem) m_particleNormalSpeed = m_particleSystem.startSpeed;
  }
  
  public void StopAll()
  {
    EngineAudioSource.volume = 0;
    m_engineSpool = 0;
    m_particleSystem.Stop();
  }

  public void StartAll()
  {
    m_particleSystem.Play();
  }

  void FixedUpdate()
  {
    float thrust = Mathf.Clamp01(Input.GetAxis(ThrustInput));
    float steer = Input.GetAxis(SteerInput);
    
    m_rigidBody.AddForce(transform.up * thrust * ShipConfig.ThrusterPower);
    m_rigidBody.AddTorque(steer * ShipConfig.TurningStrength);

    m_engineSpool += thrust * Time.fixedDeltaTime * 0.2f;
    if (thrust < 0.1f) m_engineSpool -= Time.fixedDeltaTime * 0.5f;
    m_engineSpool = Mathf.Clamp01(m_engineSpool);
    
    EngineAudioSource.volume = m_engineSpool;
    EngineAudioSource.pitch = m_engineSpool * 1.5f;

    if(m_particleSystem)
    {
      var colour = m_particleSystem.startColor;
      colour.a = m_engineSpool;
      m_particleSystem.startColor = colour;
      m_particleSystem.startSpeed = m_engineSpool * m_particleNormalSpeed;
    }
  }
}