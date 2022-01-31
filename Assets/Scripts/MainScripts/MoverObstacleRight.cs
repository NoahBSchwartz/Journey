using UnityEngine;

public class MoverObstacleRight : MonoBehaviour
{
	public float levelStart = 0;
	public GameObject player;
	public float min = -20f;
	public float max = 10f;
	public float distance = 36;
	void Start()
	{
	}
	void Update()
	{
		if (player.transform.position.z > levelStart)
		{
			transform.position = new Vector3(Mathf.PingPong(Time.time * 20, max - min) + min, transform.position.y, transform.position.z);
		}
	}


}
