using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldMover : MonoBehaviour
{
    private Vector3 zVelocity;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Vector3 zVelocity = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 zVelocity = rb.velocity;
       // Debug.Log(rb.velocity);
        GameObject player = GameObject.Find("Player");
        Movement movement = player.GetComponent<Movement>();
        if ((movement.onwall == true) && (movement.leftHit == true))
        {
            // Debug.Log(movement.zVelocity.z);
            //transform.Rotate((0f, -2 * zVelocity.z, 2 * zVelocity.y));
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -2 * zVelocity.z, 2 * zVelocity.y), Time.deltaTime * 100);
        }
        else if ((movement.onwall == true) && (movement.RightHit == true))
        {
            //transform.Rotate(0f, 2 * zVelocity.z, -2 * zVelocity.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 2 * zVelocity.z, -2 * zVelocity.y), Time.deltaTime * 100);
        }
        else if ((movement.onwall == true) && (movement.ceilingHit == true))
        {
            //tranform.Rotate(-2 * zVelocity.z, 0f, 2 * zVelocity.x);
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-2 * zVelocity.z, 0, 2 * zVelocity.x), Time.deltaTime * 100);
        }
        else
        {
            //  public void Rotate(Vector3 eulers, Space relativeTo = Space.Self);
          //  transform.Rotate(-2 * zVelocity.z, 0, 2 * zVelocity.x);//, Space.Self);
            //transform.Rotate(-2 * zVelocity.z eulers, 0,  2 * zVelocity.x eulers);
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(2 * zVelocity.z, 0, -2 * zVelocity.x), Time.deltaTime * 100);
        }
       
    }
}
