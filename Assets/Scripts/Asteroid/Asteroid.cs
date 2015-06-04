using UnityEngine;

public class Asteroid : Damageable
{
  public GameObject ExplosionFX;

  public AudioSource BreakSound;

  void Start()
  {
    base.Initialise();
  }

  void Update()
  {
    if(Health < 0.0f && !m_dead)
    {
      Kill();
    }
  }

  void Kill()
  {
    m_dead = true;
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false;
    Instantiate(ExplosionFX, transform.position, transform.rotation);
    BreakSound.Play();
    Destroy(gameObject, BreakSound.clip.length);
  }
}