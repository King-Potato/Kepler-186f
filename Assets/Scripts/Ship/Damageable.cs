using UnityEngine;

public class Damageable : MonoBehaviour
{
  public float StartingHealth = 100.0f;
  public float Health;

  public float ImpactDamageThreshold = 10.0f;
  public float ImpactDamageMultiplier = 2.0f;

  protected bool m_dead;

  protected virtual void Initialise()
  {
    Health = StartingHealth;
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    if (m_dead) return;

    if (collider.gameObject.tag == "Projectile")
    {
      var proj = collider.gameObject.GetComponent<Projectile>();
      Health -= proj.Damage;
      Instantiate(proj.DestroyFX, collider.transform.position, collider.transform.rotation);

      Destroy(collider.gameObject);
    }
    else
    {
      var impactMagnitude = collider.relativeVelocity.magnitude;

      if (impactMagnitude > ImpactDamageThreshold)
      {
        Health -= impactMagnitude * ImpactDamageMultiplier;
      }
    }
  }
}