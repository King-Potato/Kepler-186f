using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class AsteroidEmitter : MonoBehaviour {
	public List<Rigidbody2D> asteroids;
	private static int poolSize;
	public int asteroidCount = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (poolSize < asteroidCount)
		{
			Bounds bounds = transform.GetComponent<BoxCollider2D>().bounds;
			Rigidbody2D asteroid = Instantiate<Rigidbody2D>(asteroids[Random.Range(0, asteroids.Count)]);
			asteroid.transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0.0f);
			asteroid.transform.localScale = new Vector3(Random.Range(3.5f, 5.0f), 1.0f, Random.Range(3.5f, 5.0f));
			asteroid.GetComponent<CircleCollider2D> ().radius = Mathf.Sqrt(asteroid.transform.localScale.x * asteroid.transform.localScale.z / 3.141f) * 0.5f;
			asteroid.velocity = new Vector3(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f, 0.0f);
			asteroid.angularVelocity = Random.value * 80.0f - 40.0f;
			asteroid.angularDrag = 0.0f;
			poolSize++;
		}
	}
}
