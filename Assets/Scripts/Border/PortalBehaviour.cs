using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour
{
	public GameObject exitPoint;
	private Transform exitTransform;

	// Use this for initialization
	void Start()
	{
		exitTransform = exitPoint.GetComponent<Transform>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//ParticleSystem particleSystem = collider.transform.FindChild("Thruster Particles").GetComponent<ParticleSystem>();
		collider.GetComponent<Transform>().position = exitTransform.position;
	}
}
