using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EdgeCollider2D))]
public class BorderBehaviour : MonoBehaviour
{
	public Mesh mesh;
	public Material material;
	private Matrix4x4[] matrices;

	// Use this for initialization
	void Start()
	{
		EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
		matrices = new Matrix4x4[collider.pointCount];
		for (uint i = 0; i < matrices.Length; i++)
		{
			Vector3 translation = new Vector3(collider.points[i].x, collider.points[i].y, 0.0f);
			translation += collider.transform.position;
			matrices[i] = Matrix4x4.TRS(translation, Quaternion.identity, new Vector3(1.0f, 1.0f, 1.0f));
		}
	}

	void Update()
	{
		for (uint i = 0; i < matrices.Length; i++)
			Graphics.DrawMesh(mesh, matrices[i], material, 0);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Ship ship = collider.GetComponent<Ship>();
		if (!ship)
			return;

		ship.Kill();
	}
}
