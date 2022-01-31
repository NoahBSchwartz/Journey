using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockButton : MonoBehaviour
{
    public GameObject Instructions;
    // Start is called before the first frame update    
    void Start() { }

    // Update is called once per frame    
    void Update()
    {

        RaycastHit hit;
        // Cast a ray from the mouse position to the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //When the ray hits anything, print the world position of what the ray hits using hit.point
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            // if ((hit.point.x > -4.0)&& (hit.point.x < -1.6))
            GameObject wall = GameObject.Find("StartMenu");
            GameObject wal = GameObject.Find("tutorial-01/Text");
            if ((hit.transform.name) == "Start1")
            {
                GameObject.Destroy(wall);
                GameObject.Destroy(wal);
                Instructions.SetActive(true);
            }
            if ((hit.transform.name) == "Start2")
            SceneManager.LoadScene("GameScene");
            if ((hit.transform.name) == "Quit")
                Application.Quit();
            if (Input.GetKey("escape"))
                SceneManager.LoadScene("Scene");
        }

    }
}