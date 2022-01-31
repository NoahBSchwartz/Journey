using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MoverEnemyScript : MonoBehaviour
{

	public float springForce = 20f;
	public float damping = 5f;
	public float force = 10f;
	public float levelStart = 0;
	public GameObject player;
	public GameObject enemy;
	public Rigidbody rb;
	public float thrust = 100f;
	Vector3 point;
	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;

	float uniformScale = 1f;
	BoxCollider m_Collider;
	float col = 2f;
	public float min = -20f;
	public float max = 10f;
	public float distance = 36;
	public float levelStop;
	public bool wall = true;
	void Start()
	{
		//min = transform.position.x;
		//max = transform.position.x + distance;
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++)
		{
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
		m_Collider = GetComponent<BoxCollider>();
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
			if (wall == true)
			{
				point = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
				AddDeformingForce(point, force);
				m_Collider.size = new Vector3(col, col, col);
				transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 20, max - min) + min, transform.position.z);
			}
			else
            {
				point = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
				AddDeformingForce(point, force);
				m_Collider.size = new Vector3(col, col, col);
				transform.position = new Vector3(Mathf.PingPong(Time.time * 20, max - min) + min, transform.position.y, transform.position.z);
			}
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
