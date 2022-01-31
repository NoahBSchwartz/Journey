using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraScaler : MonoBehaviour
{ 
    // Set this to the in-world distance between the left & right edges of your scene.
    public float horizontalFoV = 90.0f;
    void Start()
    {
    }
            void Update()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;
        GetComponent<Camera>().fieldOfView = verticalFoV;
    }
}