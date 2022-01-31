using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(MeshFilter))]
public class FollowerEnemy : MonoBehaviour
{
	//Define all variables, making some open to the editor
	public Transform Player;
	public int MoveSpeed = 5;
	public int MaxDist = 30;
	int MinDist = 0;
	public float springForce = 20f;
	public float damping = 5f;
	public float force = 10f;
	public float levelStart = 0;
	public GameObject player;
	private Rigidbody rb;
	public Rigidbody play;
	public float levelStop;
	public float EnemyStart;
	Vector3 point;
	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;

	float uniformScale = 1f;
	BoxCollider m_Collider;
	float col = 2f;
	void Start()
	{
		//call mesh component and define affected vertices
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
		//if player gets within level coordinates, start adding a force to the enemy 
		    if ((player.transform.position.z > levelStart) && (player.transform.position.z < levelStop))
			{
			uniformScale = transform.localScale.x;
			for (int i = 0; i < displacedVertices.Length; i++)
			{
				UpdateVertex(i);
			}
			deformingMesh.vertices = displacedVertices;
			deformingMesh.RecalculateNormals();
			point = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
			AddDeformingForce(point, force);
			m_Collider.size = new Vector3(col, col, col);
			transform.LookAt(Player);
			//once players gets clost enough to enemy, start enemy following player
			if (player.transform.position.z > EnemyStart)
			{
				if (Vector3.Distance(transform.position, Player.position) >= MinDist)
				{
					var vel = play.velocity;
					var speed = vel.magnitude;
					if (speed > 5)
					{
						transform.position += transform.forward * MoveSpeed * Time.deltaTime;
					}
				}
			}
		}
	}

	void UpdateVertex(int i)
	{
		//Update each vertex of the mesh deformer as force is added
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
		//add points to each vertex of the enemy
		point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++)
		{
			AddForceToVertex(i, point, force);
		}
	}

	void AddForceToVertex(int i, Vector3 point, float force)
	{
		//add forces to each point of the enemy 
		Vector3 pointToVertex = displacedVertices[i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
}
