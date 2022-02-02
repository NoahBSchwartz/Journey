using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
// Define all varaibles. Public allows edits in inspector
public float Speed;
    private Vector3 scaleChange, positionChange, movement;
    Rigidbody m_Rigidbody;
    private Vector3 initialVelocity;
    private float minVelocity = 10f;
    private Vector3 lastFrameVelocity;
    private Rigidbody rb;
    public bool isGrounded = true;
    private static bool t = true;
    private float Jump = 0f;
    private Vector3 teleport;
    public int obstacle = 0;
    float position = 1;
    public int scoreText;
    public bool onwall = false;
    int i = 0;
    public AudioSource keySource;
    public AudioSource enemySource;
    public AudioSource rollerSource;
    public AudioSource individualKey;
    public AudioSource enemyDeath;
    private float timer;
    public AudioSource bounceSource;
    public AudioSource Level1;
    private bool level = true;
    static float RespawnZ = -70.1f;
    static int realObstacle = 0;
    static bool meshDeactivator = true;
    static float playerPos = 0;
    private float H;
    private float J;
    private float V;
    public bool RightHit = false;
    public bool ceilingHit = false; 
    public bool leftHit = false; 
    static float lives = 2;
    private int spin = 1;
    public Vector3 zVelocity;
    private bool floor = true;
    private bool forward = true;
    public GameObject camera;
    private int counter = 0;
    void Awake()
    {
        //Get rigidbody for speed, set coordinates
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        GetComponent<Rigidbody>().transform.localScale = new Vector3(2, 2, 2);
        rollerSource.volume = 0f;
        rb.transform.position = new Vector3(-4.8f, -3.1f, RespawnZ);
        Vector3 zVelocity = rb.velocity;
        obstacle = realObstacle;
        Physics.gravity = new Vector3(0f, 0f, 0f);
    }
    void FixedUpdate()
    {
        if (lives == 1)
        {
            GameObject shield = GameObject.FindWithTag("shield");
            Destroy(shield);
            //shield.GetComponent<MeshRenderer>().enabled = false;
            // Debug.Log("ye");
        }
        GameObject camera = GameObject.Find("MainCam");
        CameraMovement camMovement = camera.GetComponent<CameraMovement>();
        Vector3 zVelocity = rb.velocity;
        movement = new Vector3(H, J, V);
        //Get player input for movement and add forces accordingly
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if ((onwall == true) && (leftHit == true))
        {
            H = Jump;
            J = -moveHorizontal;
            V = moveVertical;
            //if ((camMovement.zValue > 250) && (camMovement.zValue < 275))
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -2 * zVelocity.z, 2 * zVelocity.y), Time.deltaTime * 10);
            transform.Rotate(-.1f * zVelocity.z, 0, .1f * zVelocity.y);
        }
        else if ((onwall == true) && (RightHit == true))
        {
            H = Jump;
            J = moveHorizontal;
            V = moveVertical;
            // if ((camMovement.zValue > 80))
            transform.Rotate(.1f * zVelocity.z, 0, -.1f * zVelocity.y);
            //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 2 * zVelocity.z, -2 * zVelocity.y), Time.deltaTime * 10);
        }
        else if ((onwall == true) && (ceilingHit == true))
        {
            H = -moveHorizontal;
            J = Jump;
            V = moveVertical;
            transform.Rotate(-.1f * zVelocity.z, 0, .1f * zVelocity.x);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-2 * zVelocity.z, 0, 2 * zVelocity.x), Time.deltaTime * 10);
        }
        if (floor == true)
        {
            H = moveHorizontal;
            J = Jump;
            V = moveVertical;
            // if ((camMovement.zValue < 5) || (camMovement.zValue > 355));
            transform.Rotate(.1f * zVelocity.z, 0, -.1f * zVelocity.x);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(2 * zVelocity.z, 0, -2 * zVelocity.x), Time.deltaTime * 10);
        }
        
        if (zVelocity.z < -Speed / 666.333)
        {
            forward = false;
        }
        else if (zVelocity.z > Speed / 666.333)
        {
            forward = true;
        }
        counter++;
        //  else if (zVelocity.z > Speed / 333.333)
        //{
        // Vector3 movement = new Vector3(H, J, V);
        // }
        GetComponent<Rigidbody>().AddForce(movement * Speed * Time.deltaTime);
        Jump = 0f;
        var vel = GetComponent<Rigidbody>().velocity;
        var speed = vel.magnitude;
        m_Rigidbody = GetComponent<Rigidbody>();
        //Constrain player to no rotations 
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        lastFrameVelocity = rb.velocity;
        levelUnlocker();
      //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-2 * counter, 0, 2 * counter), Time.deltaTime * speed);
        //Initiate level voice when player starts moving 
        if ((speed > 2f) && (level == true))
        {
            Level1.Play();
            level = false;
        }
        //Turn up the rolling volume with player speed 
        if ((speed < 30f) && (speed > 0f))
            rollerSource.volume = speed / 82f;
        else if (speed > 30f)
            rollerSource.volume = .35f;
        else
            rollerSource.volume = 0f;
        //Open menu if escape is pressec
        if (Input.GetKey("escape"))
                SceneManager.LoadScene("Scene");
        if (playerPos < rb.transform.position.z)
        playerPos = rb.transform.position.z;
        // spin++;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        //Allow player to bounce
        Bounce(collision.contacts[0].normal);
        isGrounded = true;
    }
    private void Bounce(Vector3 collisionNormal)
    {
        //Alloew player to bounce
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
    */
    //Level logic
    void OnTriggerEnter(Collider collision)
    {
        //If player collides with enemy, play sound and restart level
        if (collision.gameObject.tag == "Enemy")
        {
            lives -= 1;
            if (lives == 1)
            {
               enemyDeath.Play();
            }
            if (lives == 0)
            {
                RestartScene();
             //   RespawnZ = -70.1f;
               // realObstacle = 0;
               // meshDeactivator = true;
                 lives = 2;
            }
            
        }
        //if obstacle is hit, destroy the obstacle, increment obstacle counter, and play sound
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            obstacle++;
           if ((obstacle != 3) || (obstacle != 9) || (obstacle != 12) || (obstacle != 15) || (obstacle != 18) || (obstacle != 19) || (obstacle != 21))
           {
                individualKey.Play();

            }
        }
        //if player collides with wall or barrier, play bounce sound
        if ((collision.gameObject.tag == "Wall") || (collision.gameObject.tag == "Barrier2") || (collision.gameObject.tag == "Barrier3") || (collision.gameObject.tag == "Barrier4") || (collision.gameObject.tag == "Barrier5") || (collision.gameObject.tag == "Barrier6") || (collision.gameObject.tag == "Barrier7") || (collision.gameObject.tag == "Barrier8"))
        {
            bounceSource.Play();
        }
        if ((collision.gameObject.tag == "Floor") && (floor == false))
        {
            onwall = false;
            leftHit = false;
            RightHit = false;
            ceilingHit = false;
            floor = true;
            Physics.gravity = new Vector3(0f, 0f, 0f);
        }
        if ((collision.gameObject.tag == "WallRight")  && (RightHit == false))
        {
            RightHit = true;
            //Debug.Log("ye");
            leftHit = false;
            ceilingHit = false;
            floor = false;
            Physics.gravity = new Vector3(30f, 0f, 0f);
            onwall = true; 
        }
        if ((collision.gameObject.tag == "Wall")  && (leftHit == false))
        {
            RightHit = false;
            leftHit = true;
            ceilingHit = false;
            floor = false;
            Physics.gravity = new Vector3(-30f, 0f, 0f);
            onwall = true;
        }
        if ((collision.gameObject.tag == "ceiling") && (ceilingHit == false))
        {
            ceilingHit = true;
            leftHit = false;
            //Debug.Log("ye");
            floor = false;
            RightHit = false;
            Physics.gravity = new Vector3(0f, 30f, 0f);
            onwall = true;
        }
        //if end obstacle is hit go back to menu
        if (collision.gameObject.tag == "End")
        {
            SceneManager.LoadScene("Scene");
             RespawnZ = -70.1f;
            realObstacle = 0;
             meshDeactivator = true;
        }
    }
   public void RestartScene()
    {
        //Restart the game method
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
    public void levelUnlocker()
    {
        //If enough obstacles are hit, unlock next door
        if (obstacle == 3) 
        {
            GameObject wall = GameObject.FindWithTag("Barrier2");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = false;
            }
            else if (meshDeactivator == true)
            {
                Destroy(wall);
                meshDeactivator = false;
            }
                realObstacle = 3;
                Speed = 10200;
                RespawnZ = 28f;
                keySource.Play();
                t = false;
        }
        else if (obstacle == 11)
        {
            GameObject wall = GameObject.FindWithTag("Barrier3");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = true;
            }
            else if (meshDeactivator == false)
            {
                Destroy(wall);
                meshDeactivator = true;
            }
            Speed = 10400;
            realObstacle = 11;
            keySource.Play();
            t = true;
            RespawnZ = 151f;
        }
       else if (obstacle == 17) 
        {
            GameObject wall = GameObject.FindWithTag("Barrier4");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = false;
            }
            else if (meshDeactivator == true)
            {
                Destroy(wall);
                meshDeactivator = false;
            }
            Speed = 10600;
            realObstacle = 17;
            keySource.Play();
            t = false;
            RespawnZ = 238.3f;
        }
        else if (obstacle == 21) 
        {
            GameObject wall = GameObject.FindWithTag("Barrier5");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = true;
            }
            else if (meshDeactivator == false)
            {
                Destroy(wall);
                meshDeactivator = true;
            }
            Speed = 10800;
            realObstacle = 21;
            keySource.Play();
            t = true;
            RespawnZ = 331f;
        }
       else if (obstacle == 28)
        {
            GameObject wall = GameObject.FindWithTag("Barrier6");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = false;
            }
            else if (meshDeactivator == true)
            {
                Destroy(wall);
                meshDeactivator = false;
            }
            Speed = 11000;
            realObstacle = 28;
            keySource.Play();
            t = false;
            RespawnZ = 445f;
        }
        else if (obstacle == 29) 
        {
            GameObject wall = GameObject.FindWithTag("Barrier7");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = true;
            }
            else if (meshDeactivator == false)
            {
                Destroy(wall);
                meshDeactivator = true;
            }
            Speed = 11200;
            realObstacle = 29;
            keySource.Play();
            t = true;
            RespawnZ = 534.5f;
        }
        else if (obstacle == 30) 
        {
            GameObject wall = GameObject.FindWithTag("Barrier8");
            if ((rb.transform.position.x == -4.8f))
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
                meshDeactivator = false;
            }
            else if (meshDeactivator == true)
            {
                Destroy(wall);
                meshDeactivator = false;
            }
            Speed = 11400;
            keySource.Play();
            t = false;
            RespawnZ = 534.5f;
        }
    }
}

