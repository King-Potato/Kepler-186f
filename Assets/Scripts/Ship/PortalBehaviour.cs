using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour {
    public Collider2D colliderObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (transform.position.y > -50.0f)
            transform.position.Set(transform.position.x, transform.position.y - 100.0f, transform.position.z);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //if (contact.otherCollider.GetComponent<Collider2D>() == colliderObject)
            {
                contact.otherCollider.GetComponent<Transform>().position.Set(0.0f, 0.0f, 0.0f);
            }
        }
    }
}
