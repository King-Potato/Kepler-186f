using UnityEngine;
using System.Collections.Generic;

public class Asteroid : Damageable
{
  public List<GameObject> ExplosionFX;

  public AudioSource BreakSound;

  public List<GameObject> Contents;

  public float DropChance = 0.5f;

  public bool IsStartAsteroid = false;

  public UIControllerScript UIController;

  public MatchController MatchController;
  
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
    foreach(var fx in ExplosionFX) Instantiate(fx, transform.position, transform.rotation);
    BreakSound.Play();
    Destroy(gameObject, BreakSound.clip.length);
    AsteroidEmitter.DecreasePoolSize();

    if (Random.Range(0.0f, 1.0f) <= DropChance)
    {
      // Create a random item from Contents.
      int idx = Random.Range(0, Contents.Count);
      Instantiate(Contents[idx], transform.position, Quaternion.identity);
    }

    var shake = Camera.main.GetComponent<Shake>();
    shake.StartShake(0.4f, 0.5f);

    if(IsStartAsteroid)
    {
      GameState.CurrentState = GameState.State.Running;
      UIController.UpdateUIs();
      MatchController.Restart();
    }
  }
}