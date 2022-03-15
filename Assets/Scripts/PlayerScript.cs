using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    private Rigidbody2D rd2d;
    
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float speed;

    public Text score;

    private int scoreValue = 0;
    private int lives;
    Animator anim;
    bool rhe = true;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    bool music = true;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        anim = GetComponent<Animator>();
        lives = 3;

        SetLivesText();
        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        if (music == true)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
          
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (collision.collider.tag == "Enemy")
        {
           
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue>=18 && rhe==true)
        {
            transform.position = new Vector4(80.0f,3.0f, 0.0f);
            lives = 3;
            SetLivesText();
            rhe = false;
        }
        if (scoreValue == 33)
        {
            winTextObject.SetActive(true);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Debug.Log("collision.collider.tag");
            if (Input.GetKey(KeyCode.W))
            {

                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                
            }

        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }

    }


}