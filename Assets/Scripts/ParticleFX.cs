using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFX : MonoBehaviour
{
  void Update()
  {
    if (!GetComponent<ParticleSystem>().IsAlive())
    {
      Destroy(gameObject);
    }
  }
}