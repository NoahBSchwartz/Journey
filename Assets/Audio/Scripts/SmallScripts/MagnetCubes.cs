using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCubes : MonoBehaviour
{
    bool inside;
    Transform magnet;
    float radius = 10f;
    float force = 1000f;
    Rigidbody m_Rigidbody;
    Rigidbody rb;
    void Start()
    {
        magnet = GameObject.Find("Obstacle").GetComponent<Transform>();
        inside = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            inside = true;
            Debug.Log("ye");
        }
    }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                inside = false;
            }

            if (inside)
            {
                Vector3 magnetField = magnet.position - transform.position;
                float index = (radius - magnetField.magnitude) / radius;
                rb.AddForce(force * magnetField * index);
            }
        }
    
}