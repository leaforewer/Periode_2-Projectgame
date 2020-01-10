using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private int count;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 14;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public float speed = 10f;
    public Text countText;
    public Text winText;

    // Start is called before the first frame update
    void Start () 
    {
        playerRb = GetComponent<Rigidbody> ();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        count = 0;
        SetCountText ();
        winText.text = "";
    }

    // Update is called once per frame
    void Update () 
    {

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        transform.Translate(Vector3.forward * speed);

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(5*Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-5*Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstackle")){
            gameOver = true;
            Debug.Log("Game Over");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            speed = 0;
            Invoke ("restartgame", 2);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }

        if (other.gameObject.CompareTag("Complete"))
        {
            Invoke ("nextgame", 1);
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

    void SetCountText ()
    {
        countText.text = "Score: " + count.ToString (); 
        if (count >= 8)
        {
            winText.text = "Excellent!!";
        }
    }
}