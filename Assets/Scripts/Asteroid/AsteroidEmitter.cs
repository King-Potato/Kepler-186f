using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]
public class AsteroidEmitter : MonoBehaviour {
	public List<Rigidbody2D> asteroids;
	private static int poolSize;
	private const int MAX_POOL_SIZE = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (poolSize < MAX_POOL_SIZE)
		{
			Bounds bounds = transform.GetComponent<BoxCollider2D>().bounds;
			Rigidbody2D asteroid = Instantiate<Rigidbody2D>(asteroids[Random.Range(0, asteroids.Count)]);
			asteroid.transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), 0.0f);
			asteroid.velocity = new Vector3(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f, 0.0f);
			asteroid.angularVelocity = Random.value * 80.0f - 40.0f;
			asteroid.angularDrag = 0.0f;
			poolSize++;
		}
	}
}
