using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles player input and movement
/// </summary>
public class PlayerController : MonoBehaviour
{

    //Static references
    /// <summary>
    /// Players score
    /// </summary>
    public static float score;
    /// <summary>
    /// Does the player have Iframes?
    /// </summary>
    public static bool isGod = false;
    /// <summary>
    /// Does the player have Iframes?
    /// </summary>
    public static bool hasIframes = false;
    /// <summary>
    /// Is the game stopped?
    /// </summary>
    public static bool isTimeStopped = false;
    /// <summary>
    /// Can the player power jump?
    /// </summary>
    public static bool canPowerJump = false;
    /// <summary>
    /// Can the player break walls?
    /// </summary>
    public static bool canBreakWalls = true;
    /// <summary>
    /// Is the player dead?
    /// </summary>
    public static bool isDead;

    /// <summary>
    /// Vector 3 used for changing the players position, but allowing access to change specific axis
    /// </summary>
    private Vector3 pos;

    /// <summary>
    /// Can the player move left?
    /// </summary>
    private bool moveLeft = false;
    /// <summary>
    /// Can the player move right?
    /// </summary>
    private bool moveRight = false;
    /// <summary>
    /// Is the player sliding?
    /// </summary>
    private bool isSliding = false;
    /// <summary>
    /// Is the player jumping?
    /// </summary>
    private bool isJumping = false;

    /// <summary>
    /// How high can the player jump
    /// </summary>
    private float jumpCap = 1.5f;
    /// <summary>
    /// Stop the scene change with this
    /// </summary>
    private float deadDelay;
    /// <summary>
    /// How long has the player been jumping?
    /// </summary>
    private float jumpTimer = 0;
    /// <summary>
    /// How long has the player been sliding?
    /// </summary>
    private float slideTimer = 0;
    /// <summary>
    /// How long should we prevent the player from moving
    /// </summary>
    private float moveDelay = 0;

    /// <summary>
    /// Sets the lane the player is in
    /// </summary>
    private int lane = 0;
    /// <summary>
    /// Controls the screen fade when the player loses
    /// </summary>
    private Color fade = new Color(0, 0, 0, 0);

    /// <summary>
    /// Speed the player is moving forward
    /// </summary>
    public float velX = 7;
    /// <summary>
    /// Speed gravity is pulling the player
    /// </summary>
    public float velY = 0;
    /// <summary>
    /// Gravity used for calculations downwards pull
    /// </summary>
    public float gravity;
    /// <summary>
    /// Additional move delay
    /// </summary>
    public float addedMoveDelay = 0;
    /// <summary>
    /// Number of lives the player has
    /// </summary>
    public float life;
    /// <summary>
    /// How long the player is invincible
    /// </summary>
    public float godTimer;
    /// <summary>
    /// How long the player is invincible
    /// </summary>
    public float iframeTimer;
    /// <summary>
    /// Score multiplier
    /// </summary>
    public float multiplier;
    /// <summary>
    /// UI display of the players remaining life
    /// </summary>
    public Text livesText;
    /// <summary>
    /// UI display of the player's score
    /// </summary>
    public Text scoreText;
    /// <summary>
    /// UI display of if the player is in God Mode
    /// </summary>
    public Text modeText;
    /// <summary>
    /// UI display of if the player can break walls
    /// </summary>
    public Text breakWallText;
    /// <summary>
    /// UI display of if the player can power jump
    /// </summary>
    public Text powerJumpText;

    /// <summary>
    /// Screen effect when the player is in God Mode
    /// </summary>
    public Image vision;
    /// <summary>
    /// Screen effect when the player has iFrames
    /// </summary>
    public Image visionTime;
    /// <summary>
    /// Screen displayed when the player dies
    /// </summary>
    public Image deathScreen;

    /// <summary>
    /// Particle system used when the player takes damage
    /// </summary>
    public ParticleSystem blood;
    /// <summary>
    /// Particle system used when the player can break walls
    /// </summary>
    public ParticleSystem power;

    /// <summary>
    /// Audio clip for the player moving
    /// </summary>
    public AudioClip move;
    /// <summary>
    /// Audio clip for the player jumping
    /// </summary>
    public AudioClip jump;
    /// <summary>
    /// Audio clip for the player dying
    /// </summary>
    public AudioClip die;

    /// <summary>
    /// Velocity of the player
    /// </summary>
    Vector3 velocity = new Vector3();
    /// <summary>
    /// Is the player on the ground?
    /// </summary>
    public bool isGrounded = false;
    /// <summary>
    /// How long the player can hold the jump button
    /// </summary>
    public float jumpTime = .75f;
    /// <summary>
    /// Strength of the jump
    /// </summary>
    float jumpImpulse;
    /// <summary>
    /// Base gravity multiplier
    /// </summary>
    public float baseGravityScale = .8f;
    /// <summary>
    /// Gravity wehn the player jumps
    /// </summary>
    float jumpGravityScale = .35f;
    /// <summary>
    /// Divider used to control more powerful jumps
    /// </summary>
    private float jumpDivider = 4;
    /// <summary>
    /// Light objects attached to the player
    /// </summary>
    public Light torch;
    /// <summary>
    /// How long is time frozen for?
    /// </summary>
    public float timeFreezeTimer;
    /// <summary>
    /// How long can the player power jump
    /// </summary>
    public float powerJumpTimer;
    /// <summary>
    /// Current game difficulty
    /// </summary>
    public float difficulty;
    /// <summary>
    /// Speed the player slides
    /// </summary>
    public float slideSpeed = 0;
    /// <summary>
    /// Players base move speed
    /// </summary>
    public float moveSpeed = 7;

    // Use this for initialization
    /// <summary>
    /// Resets certain variables for game reset
    /// </summary>
    void Start()
    {
        life = 1;
        score = 0;
        multiplier = 1;
        deadDelay = 3;
        isDead = false;
        difficulty = 1;
        baseGravityScale = .8f;
        moveSpeed = 7;
    }

    // Update is called once per frame
    /// <summary>
    /// Updates the player every frame. Handles movement calls
    /// </summary>
    void Update()
    {
        if (isTimeStopped)
        {
            timeFreezeTimer -= Time.deltaTime;
            if (timeFreezeTimer <= 0)
            {
                isTimeStopped = false;
            }
            return;
        }
        float jumping = Input.GetAxis("Jump");
        float movingHorizontal = Input.GetAxisRaw("Horizontal");
        float sliding = Input.GetAxis("Vertical");
        pos = transform.position;
        float gravityScale;
        gravityScale = baseGravityScale;
        //print("Grav: "+baseGravityScale);
       // print("SSSpeed: "+velX);

        gravity = ((jumpCap));
        jumpImpulse = gravity * jumpTime;

        // Move forward
        velocity.x = (velX + slideSpeed) * Time.deltaTime;
        //print(velocity.x);

        if (isGrounded == true)
        {
            velocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                if (isJumping == true && velocity.y > 0) gravityScale = jumpGravityScale;
            }
        }
        if(pos.y > 1 || !isGrounded || isSliding) velocity.y -= gravity * Time.deltaTime * gravityScale;
        //God Mode Timer
        if (isGod == true)
        {
            godTimer -= Time.deltaTime;
            if (godTimer <= 0)
            {
                isGod = false;
            }
        }
        if (hasIframes == true)
        {
            iframeTimer -= Time.deltaTime;
            if (iframeTimer <= 0)
            {
                hasIframes = false;
            }
        }
        if (canPowerJump)
        {
            powerJumpTimer -= Time.deltaTime;
            if(powerJumpTimer <= 0)
            {
                canPowerJump = false;
            }
        }

        //Call to switch lanes
        HandleInput(movingHorizontal, sliding);

        /*if (canBreakWalls)
        {
            power.Play();
        }
        else
        {
            power.Stop();
        }*/
        //Screen effect
        if (isGod == true)
        {
            vision.gameObject.SetActive(true);
            torch.range = 50;
        }
        else
        {
            vision.gameObject.SetActive(false);
            torch.range = 30;
        }

        //transform.position = pos;
        livesText.text = "Health: " + life.ToString();
        scoreText.text = "Score: " + score.ToString();
        modeText.text = "God Mode: " + isGod.ToString();
        breakWallText.text = "Break Walls: " + canBreakWalls.ToString();
        powerJumpText.text = "Power Jump: " + canPowerJump.ToString();

        if (life < 0)
        {
            //game over
            AudioSource.PlayClipAtPoint(die, transform.position);
            deadDelay -= Time.deltaTime;
            velX = 0;
            velY = 0;
            isDead = true;
            deathScreen.gameObject.SetActive(true);
            deathScreen.GetComponent<Image>().color = fade;
            //deathScreen.GetComponent<MeshRenderer>().material.color = fade;
            fade.a += Time.deltaTime / 2;
            if (deadDelay <= 0)
            {
                //Change the sceen to game over
                SceneManager.LoadScene("GameOverScene");
            }
        }
        transform.position += velocity;
        pos = transform.position;
    }//end update

    /// <summary>
    /// This method handles when the player is sliding or changing lanes
    /// </summary>
    /// <param name="movingHorizontal">How much the left or right button is being pressed</param>
    /// <param name="sliding"></param>
    void HandleInput(float movingHorizontal, float sliding)
    {
        if (movingHorizontal > 0 && moveLeft == false && moveRight == false && moveDelay <= 0)
        {
            //print(movingHorizontal);
            moveRight = true;
        }
        else if (movingHorizontal < 0 && moveRight == false && moveLeft == false && moveDelay <= 0)
        {
            moveLeft = true;
        }

        //Switch lanes
        if (moveRight == true || moveLeft == true)
        {
            SwapLanes();
            score += multiplier * multiplier * difficulty;
            multiplier = addedMoveDelay * 10;
            moveDelay = .5f + addedMoveDelay;
        }

        //Move delay logic
        if (moveDelay > 0)
        {
            moveDelay = moveDelay - Time.deltaTime;
        }

        //Actual lane move
        if (lane > 1)
        {
            lane--;
        }
        if (lane < -1)
        {
            lane++;
        }
        moveLane();

        //Slide Logic
        if (sliding < 0 && isGrounded && moveRight == false && moveLeft == false)
        {
            isSliding = true;
            slideTimer += .2f;
            slideSpeed = 4;
            Slide();
            //isGrounded = false;
        }
        else
        {
            isSliding = false;
            transform.localScale = Vector3.one;
            GetComponent<AABB>().halfSize.y = .5f;
            velX = moveSpeed;
            slideSpeed = 0;
            slideTimer = 0;

        }
    }

    /// <summary>
    /// This function handles when the player is jumping
    /// </summary>
    void Jump()
    {
        if (canPowerJump)
        {
            jumpDivider = 3;
        }
        else
        {
            jumpDivider = 4;
        }
        velocity.y = jumpImpulse / jumpDivider;
        isJumping = true;
        isGrounded = false;
        /*//print("Jumping");
        velY += 10 * Time.deltaTime;
        pos.y = velY;*/

    }

    /// <summary>
    /// This function handles when the player is sliding
    /// initially speeds them up than slows them down
    /// </summary>
    void Slide()
    {
        //print("Sliding");
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        if (velX > 3)
        {
            //velX = 10;
            velX = moveSpeed + 4;
            if (slideTimer >= 0)
            {
                velX -= (slideTimer * 1.2f);
            }
        }
        /*if (slideSpeed >= 0)
        {
            
            if (slideTimer >= 3 && slideSpeed > -2)
            {
                if (slideSpeed < -2) return;
                print("Lower");
                slideSpeed -= (slideTimer * .5f);
            }
        }*/
        //print(slideSpeed);
        
        GetComponent<AABB>().halfSize.y = .25f;
    }

    /// <summary>
    /// This function sets the players lane and plays a sound effect for moving
    /// It also ticks up the delay timer
    /// </summary>
    void SwapLanes()
    {
        if (moveRight == true)
        {
            AudioSource.PlayClipAtPoint(move, transform.position);
            lane--;
            addedMoveDelay += .1f;
            moveRight = false;
        }
        if (moveLeft == true)
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
        if (lane == -1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
        }
        else if (lane == 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
        else
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

        if (fix.x != 0)
        {
            //zero x velocity
            //pos.x = 0;
        }
        if (fix.y != 0)
        {
            //zero y velocity
            if(isJumping == false)
            {
                isGrounded = true;
                velocity.y = 0;
            }
            
            if(pos.y < 1)
            {
                //print("NO!!!!!!!!!!!!");
                isGrounded = true;
                velocity.y = 0;
            }
            //velocity.y = 0;
        }
        if (fix.z != 0)
        {
            //zero z velocity

        }

    }

}
