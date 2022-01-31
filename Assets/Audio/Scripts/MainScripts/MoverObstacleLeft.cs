using UnityEngine;

public class MoverObstacleLeft : MonoBehaviour
{
	public float levelStart = 0;
	public GameObject player;
	public float min = -20f;
	public float max = 10f;
	public float distance = 36;
	public bool wall = false;
	void Start()
	{
	}
	void Update()
	{
		if (player.transform.position.z > levelStart)
		{
			if (wall == false)
				transform.position = new Vector3(-(Mathf.PingPong(Time.time * 20, max - min) + min), transform.position.y, transform.position.z);
			if (wall == true)
				transform.position = new Vector3(transform.position.x, (Mathf.PingPong(Time.time * 20, max - min) + min), transform.position.z);
		}
	}


}
