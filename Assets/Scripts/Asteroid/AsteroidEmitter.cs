using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class AsteroidEmitter : MonoBehaviour
{
	public List<Rigidbody2D> Asteroids;
	private static int poolSize;
	public int AsteroidCount = 10;
  
  public static void DecreasePoolSize()
  {
    poolSize -= 1;
  }

  void Start()
  {
    poolSize = 0;
  }

	void Update()
  {
		if (poolSize < AsteroidCount)
		{
			Bounds bounds = transform.GetComponent<BoxCollider2D>().bounds;
      Rigidbody2D asteroid = Instantiate(Asteroids[Random.Range(0, Asteroids.Count)]);
			asteroid.transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0.0f);

      float scale = Random.Range(3.0f, 4.0f);
      asteroid.transform.localScale = new Vector3(scale, scale, scale);
			asteroid.GetComponent<CircleCollider2D> ().radius = Mathf.Sqrt(asteroid.transform.localScale.x * asteroid.transform.localScale.z / 3.141f) * 0.5f;
			asteroid.velocity = new Vector3(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f, 0.0f);
			asteroid.angularVelocity = Random.value * 80.0f - 40.0f;
			asteroid.angularDrag = 0.0f;
      
			poolSize++;
		}
	}
}
