using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFloater : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    public float amplitude;          //Set in Inspector 
    public float speed;                  //Set in Inspector 
    private float tempVal;
    private Vector3 tempPos;
    int x = 0;
    void Start()
    {
        tempVal = transform.position.y;
    }

    void Update()
    {
        //tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
       // transform.position = tempPos;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, x, x), Time.deltaTime * 1.5f);
        x++;
        void OnTriggerEnter(Collider collision)
        {
            //If player collides with enemy, play sound and restart level
            if (collision.gameObject.tag == "range")
            {
                Debug.Log("ywe");
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
            }
        }
    }
}
