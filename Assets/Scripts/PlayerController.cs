using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool moveLeft = false;
    private bool moveRight = false;
    private float jumpCap = 3;
    private int lane = 0;
    private bool isSliding = false;
    private bool isJumping = false;
    private float jumpTimer = 0;
    private float slideTimer = 0;
    private float moveDelay = 0;
    public float speed = 10;
    public float gravity = 10;
    public bool stopGravity = false;
    private float addedMoveDelay = 0;
    //private Vector3


    // Use this for initialization
    void Start () {
        //jumping = Input.GetAxis("Jump");
        //movingHorizontal = Input.GetAxis("Horizontal");
        //timer = Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        //timer = 60 * Time.deltaTime;
        //print(timer);

        float jumping = Input.GetAxis("Jump"); 
        float movingHorizontal = Input.GetAxisRaw("Horizontal");
        float sliding = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if(stopGravity == false)
        {
            pos.y -= gravity * Time.deltaTime;
            transform.position = pos;
        }
        
        
        if(jumping > 0)
        {
            print(jumping);
            jumpTimer = jumping * Time.deltaTime;
            //print(jumpTimer);
            Jump();
        }

        if (movingHorizontal > 0 && moveLeft == false && moveRight == false && moveDelay <= 0) {
            //print(movingHorizontal);
            moveRight = true;

            //SwapLanes();
        }
        else if (movingHorizontal < 0 && moveRight == false && moveLeft == false && moveDelay <= 0)
        {
            moveLeft = true;
            //print(movingHorizontal);

           // SwapLanes();
        }
        //print(moveLeft);
        //print(moveRight);

        if (moveRight == true || moveLeft == true)
        {
            SwapLanes();
            moveDelay = .5f + addedMoveDelay;
        }

        if (sliding < 0)
        {
            isSliding = true;
            slideTimer += .2f;
            Slide();
        }
        else
        {
            isSliding = false;
            GetComponent<AABB>().halfSize.y = .5f;
            speed = 5;
            slideTimer = 0;
            
        }

        if(transform.localPosition.y >= jumpCap)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, jumpCap);
        }

        /*if(transform.localPosition.z >= 3.1)
        {
            //transform.localPosition = new Vector3(0, 0, rightLane);
            transform.Translate(0, 0, leftLane);
            print("LOCKED TO THE RIGHT!!!!!!!!!!!");
        }
        if(transform.localPosition.z <= -3.1)
        {
            //transform.localPosition = new Vector3(0, 0, leftLane);
            transform.Translate(0, 0, rightLane);
            print("LOCKED TO THE LEFT!!!!!!!!!!!!!!");
        }*/

        if(isSliding == false)
        {
            transform.localScale = Vector3.one;
        }

        //print(transform.localPosition);
        
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
        //print(speed);
        //print(moveDelay);
	}

    void Jump()
    {
        print("Jumping");
        transform.Translate(new Vector3(0, (jumpTimer * 5), 0));

    }

    void Slide()
    {
        print("Sliding");
        
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        if(speed > 3)
        {
            speed = 10 - slideTimer * 3 * Time.deltaTime;
        }
        
        GetComponent<AABB>().halfSize.y = .25f;

    }

    void SwapLanes()
    {

        if(moveRight == true)
        {
            lane--;
            addedMoveDelay += .1f;
            moveRight = false;
        }
        if(moveLeft == true)
        {
            lane++;
            addedMoveDelay += .1f;
            moveLeft = false;
        }

        //print(moveAmount);
    }

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

}
