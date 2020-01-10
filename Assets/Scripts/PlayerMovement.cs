using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gameoverDelay = 1;

    private Vector3 startPos;

    

    Vector3 velocity;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
    
     void OnTriggerEnter(Collider other)
    {

         if (other.gameObject.CompareTag("Obstackle"))
        {
            Debug.Log("Game Over!");
            Invoke("restartgame", gameoverDelay);
        }

        if (other.gameObject.CompareTag("Winning"))
        {
            Debug.Log("You won!");
            nextgame();
        }

    }
    void restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void nextgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
