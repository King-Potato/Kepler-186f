using UnityEngine;

public class Shake : MonoBehaviour
{
  public bool Shaking = false;

  public float ShakeStrength = 0.0f;

  public float ShakeTimer = 0.0f;
  float m_lastDuration;

  public void Update()
  {
    if (!Shaking) return;

    float factor = ShakeTimer / m_lastDuration; 
    transform.position += Random.insideUnitSphere * ShakeStrength * factor;

    ShakeTimer -= Time.deltaTime;
    if(ShakeTimer <= 0.0f)
    {
      Shaking = false;
      ShakeTimer = 0.0f;
    }
  }

  public void StartShake(float strength, float duration)
  {
    Shaking = true;
    ShakeStrength = strength;
    ShakeTimer = duration;
    m_lastDuration = ShakeTimer;
  }
}
