using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GrowerEnemyScript : MonoBehaviour
{

	public float springForce = 20f;
	public float damping = 5f;
	public float force = 10f;
	public float levelStart = 0;
	public float levelStop = 100;
	public GameObject player;
	private Rigidbody rb;
	Vector3 point;
	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;
	float uniformScale = 1f;
	BoxCollider m_Collider;
	float col = 2f;
	void Start()
	{
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++)
		{
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
		m_Collider = GetComponent<BoxCollider>();
		transform.rotation = Quaternion.Euler(0, 20, -90);
	}

	void Update()
	{
		if ((player.transform.position.z > levelStart) && (player.transform.position.z < levelStop))
		{ 
			uniformScale = transform.localScale.x;
			for (int i = 0; i < displacedVertices.Length; i++)
			{
				UpdateVertex(i);
			}
			deformingMesh.vertices = displacedVertices;
			deformingMesh.RecalculateNormals();
			//Debug.Log(rb.tranform.position.x);
			point = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
			AddDeformingForce(point, force);
			m_Collider.size = new Vector3(col, col, col);
		}
	}

	void UpdateVertex(int i)
	{
		Vector3 velocity = vertexVelocities[i];
		Vector3 displacement = displacedVertices[i] - originalVertices[i];
		displacement *= uniformScale;
		velocity -= displacement * springForce * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity * (Time.deltaTime / uniformScale);
	}

	public void AddDeformingForce(Vector3 point, float force)
	{
		point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++)
		{
			AddForceToVertex(i, point, force);
		}
	}

	void AddForceToVertex(int i, Vector3 point, float force)
	{
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
}
