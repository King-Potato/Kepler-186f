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

		LineRenderer lines = gameObject.AddComponent<LineRenderer>();
		lines.material = new Material(Shader.Find("Particles/Additive"));
		lines.SetColors(Color.red, Color.red);
		lines.SetWidth(0.4f, 0.4f);
		lines.SetVertexCount(collider.pointCount);

        for (uint i = 0; i < matrices.Length; i++)
		{
			Vector3 translation = new Vector3(collider.points[i].x, collider.points[i].y, 0.0f);
			translation += collider.transform.position;
			matrices[i] = Matrix4x4.TRS(translation, Quaternion.identity, new Vector3(1.0f, 1.0f, 1.0f));
			lines.SetPosition((int) i, collider.points[i] + new Vector2(collider.transform.position.x, collider.transform.position.y));
		}
	}

	void Update()
	{
		for (uint i = 0; i < matrices.Length; i++)
		{
			if (i < matrices.Length - 1)
			{
				Vector4 firstPoint, secondPoint;
				firstPoint = secondPoint = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
				firstPoint = matrices[i] * firstPoint;
				secondPoint = matrices[i + 1] * secondPoint;
				//Debug.DrawLine(firstPoint, secondPoint);
            }
			Graphics.DrawMesh(mesh, matrices[i], material, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Ship ship = collider.GetComponent<Ship>();
		if (!ship)
			return;

		ship.Kill();
	}
}
