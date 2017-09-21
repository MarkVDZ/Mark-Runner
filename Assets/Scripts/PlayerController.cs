using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Static references
    //Players score
    public static float score;
    //Does the player have Iframes?
    public static bool isGod = false;
    //Is the player dead?
    public static bool isDead;

    //Vector 3 used for changing the players position, but allowing access to change specific axis
    private Vector3 pos;

    //Can the player move left?
    private bool moveLeft = false;
    //Can the player move right?
    private bool moveRight = false;
    //Is the player sliding?
    private bool isSliding = false;
    //Is the player jumping?
    private bool isJumping = false;

    //How high can the player jump
    private float jumpCap = 3;
    //Stop the scene change with this
    private float deadDelay;
    //How long has the player been jumping?
    private float jumpTimer = 0;
    //How long has the player been sliding?
    private float slideTimer = 0;
    //How long should we prevent the player from moving
    private float moveDelay = 0;
    //Used for gravity calculations
    private float Gravity = 0;

    //Sets the lane the player is in
    private int lane = 0;
    
    //Speed the player is moving forward
    public float velX = 10;
    //Speed gravity is pulling the player
    public float velY = 0;
    //Another gravity used for calculations for 1/meter per second
    public float gravity = 60;
    //Additional move delay
    public float addedMoveDelay = 0;
    //Number of lives the player has
    public float life;
    //How long the player is invincible
    public float godTimer;
    //Score multiplier
    public float multiplier;

    //Should gravity be used?
    public bool stopGravity = false;

    //Canvas text to display information to the player
    public Text livesText;
    public Text scoreText;
    public Text modeText;
    //Toggles a screen overlay on for certain effects
    public Image vision;
       
    //Particle system used when the player takes damage
    public ParticleSystem blood;

    //Audioclips used when the player does a certain action
    public AudioClip move;
    public AudioClip jump;
    public AudioClip die;


    // Use this for initialization
    /// <summary>
    /// Resets certain variables for game reset
    /// </summary>
    void Start () {
        //jumping = Input.GetAxis("Jump");
        //movingHorizontal = Input.GetAxis("Horizontal");
        //timer = Time.deltaTime;
        life = 1;
        score = 0;
        multiplier = 1;
        deadDelay = 3;
        isDead = false;
    }
	
	// Update is called once per frame
    /// <summary>
    /// Updates the player every frame. Handles movement calls
    /// </summary>
	void Update () {

        float jumping = Input.GetAxis("Jump"); 
        float movingHorizontal = Input.GetAxisRaw("Horizontal");
        float sliding = Input.GetAxis("Vertical");

        //print(life);

        if(isGod == true)
        {
            godTimer -= Time.deltaTime;
            if(godTimer <= 0)
            {
                isGod = false;
            }
        }

        pos = transform.position;
        pos.x += velX * Time.deltaTime;
        if (stopGravity == false)
        {
            Gravity = (gravity * Time.deltaTime) * 2;
            //print(GRAVITY);
            pos.y += velY - Gravity;
            transform.position = pos;
        }

        if (jumping > 0 && jumpTimer < .2f)
        {
            AudioSource.PlayClipAtPoint(jump, transform.position);
            //print(jumping);
            jumpTimer += jumping * Time.deltaTime;
            //print(jumpTimer);
            Jump();
        }
        else
        {
            velY = 0;
        }

        if (movingHorizontal > 0 && moveLeft == false && moveRight == false && moveDelay <= 0 && pos.y < 1.2)
        {
            //print(movingHorizontal);
            moveRight = true;
        }
        else if (movingHorizontal < 0 && moveRight == false && moveLeft == false && moveDelay <= 0 && pos.y < 1.2)
        {
            moveLeft = true;
        }

        if (moveRight == true || moveLeft == true)
        {
            SwapLanes();
            score += multiplier * multiplier;
            multiplier = addedMoveDelay * 10;
            moveDelay = .5f + addedMoveDelay;
        }

        if (sliding < 0 && pos.y < 1.2 && moveRight == false && moveLeft == false)
        {
            isSliding = true;
            slideTimer += .2f;
            Slide();
        }
        else
        {
            isSliding = false;
            GetComponent<AABB>().halfSize.y = .5f;
            velX = 5;
            slideTimer = 0;
            
        }

        if(transform.position.y >= jumpCap)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, jumpCap);
        }

        if(isSliding == false)
        {
            transform.localScale = Vector3.one;
        }
        
        if(moveDelay > 0)
        {
            moveDelay = moveDelay - Time.deltaTime;
        }

        if(lane > 1)
        {
            lane--;
        }
        if (lane < -1)
        {
            lane++;
        }
        moveLane();

        if(isGod == true)
        {
            vision.gameObject.SetActive(true);
        } else
        {
            vision.gameObject.SetActive(false);
        }
        //transform.position = pos;
        livesText.text = "Health: " + life.ToString();
        scoreText.text = "Score: " + score.ToString();
        modeText.text = "God Mode: " + isGod.ToString();

        if (life < 0)
        {
            //game over
            AudioSource.PlayClipAtPoint(die, transform.position);
            deadDelay -= Time.deltaTime;
            velX = 0;
            velY = 0;
            isDead = true;
            if(deadDelay <= 0)
            {
                //Change the sceen to game over
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
    /// <summary>
    /// This function handles when the player is jumping
    /// </summary>
    void Jump()
    {
        //print("Jumping");
        velY += 10 * Time.deltaTime;
        pos.y = velY; 
    }

    /// <summary>
    /// This function handles when the player is sliding
    /// initially speeds them up than slows them down
    /// </summary>
    void Slide()
    {
        //print("Sliding");
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        if(velX > 3)
        {
            velX = 10;
            if(slideTimer >= 5)
            {
                velX -= (slideTimer * .5f);
            }
        }     
        GetComponent<AABB>().halfSize.y = .25f;
    }

    /// <summary>
    /// This function sets the players lane and plays a sound effect for moving
    /// It also ticks up the delay timer
    /// </summary>
    void SwapLanes()
    {
        if(moveRight == true)
        {
            AudioSource.PlayClipAtPoint(move, transform.position);
            lane--;
            addedMoveDelay += .1f;
            moveRight = false;
        }
        if(moveLeft == true)
        {
            AudioSource.PlayClipAtPoint(move, transform.position);
            lane++;
            addedMoveDelay += .1f;
            moveLeft = false;
        }
    }

    /// <summary>
    /// This function moves the player physically based on the lane they are in
    /// </summary>
    void moveLane()
    {
        if(lane == -1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
        } else if(lane == 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        } else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 3);
        }
    }

    /// <summary>
    /// Apply a collision "fix" to prevent collision detection
    /// </summary>
    /// <param name="fix">How far to move the player</param>
    public void ApplyFix(Vector3 fix)
    {

        transform.position += fix;
        GetComponent<AABB>().calcEdges();

        if(fix.x != 0)
        {
            //zero x velocity
            //pos.x = 0;
        }
        if (fix.y != 0)
        {
            //zero y velocity
            if(isJumping != true)
            {
                Gravity = 0;
                //velY = 0;
                jumpTimer = 0;
            }
            
            //pos.y = 0;
        }
        if (fix.z != 0)
        {
            //zero z velocity

        }

    }

}
