using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    private Vector3 zVelocity;
    private bool go = true;
    private float distance= 0;
    public bool t = true;
    Vector3 offset1 = new Vector3(-7f, 0f, -12f);
    Vector3 offset2 = new Vector3(0f, 7f, -12f);
    Vector3 offset3 = new Vector3(7f, 0f, -12f);
    Vector3 offset4 = new Vector3(0f, 0f, -14f);
    Vector3 spin2 = new Vector3(20, 0, 0);
    int speed2 = 5;
    public float zValue = 0f;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
      
        GameObject player = GameObject.Find("Player");
        Movement movement = player.GetComponent<Movement>();
        Vector3 zVelocity = rb.velocity;
        //if (zVelocity.z > movement.Speed / 333.333)
        
            offset2 = new Vector3(0f, 7f, -12f);
            spin2 = new Vector3(20, 0, 0);
           // t = false;
        if (movement.onwall == true)
        {
            if (movement.RightHit == true)//player.transform.position.x > 9)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 20, 90), Time.deltaTime * 5);
                if ((transform.rotation.eulerAngles.z >= 45) && (transform.rotation.eulerAngles.z <= 100))
                    transform.position = player.transform.position + offset1;
                else
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset1, 100 * Time.deltaTime);
            }
            else if (movement.leftHit == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -20, -90), Time.deltaTime * 5);
                if (transform.rotation.eulerAngles.z <= -45)
                    transform.position = player.transform.position + offset3;
                else
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset3, 100 * Time.deltaTime);
            }
            
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(20, 0, 180), Time.deltaTime * 5);
                if (transform.rotation.eulerAngles.z <= -45)
                    transform.position = player.transform.position + offset4;
                else
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset4, 100 * Time.deltaTime);
            }
            
        }
        else //if (player.transform.position.y < -2.2) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(spin2), Time.deltaTime * 5);
            if (transform.rotation.eulerAngles.z <= 45)
                transform.position = player.transform.position + offset2;
            else
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset2, 100 * Time.deltaTime);
        }
        zValue = transform.rotation.eulerAngles.z;
    }
    /*
    void LateUpdate()
    {
    }
    */
}
