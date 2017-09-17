﻿using System.Collections;
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
    public float velX = 10;
    public float velY = 0;
    public float gravity = 60;
    public bool stopGravity = false;
    public float addedMoveDelay = 0;
    public float life = 1;
    float GRAVITY = 0;
    Vector3 pos;
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

        pos = transform.position;
        //pos.x += velX * Time.deltaTime;
        //transform.position = pos;
        if (stopGravity == false)
        {
            float accY = gravity * Time.deltaTime;
            GRAVITY = (gravity * Time.deltaTime) * 2;
            //print(GRAVITY);
            pos.y += velY - GRAVITY;// gravity * Time.deltaTime;
            transform.position = pos;
        }

        if (jumping > 0 && jumpTimer < .4f)
        {
            //print(jumping);
            jumpTimer += jumping * Time.deltaTime;
            //print(jumpTimer);
            Jump();
        }
        else
        {
            velY = 0;
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
            velX = 5;
            slideTimer = 0;
            
        }

        if(transform.position.y >= jumpCap)
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
        //print(velX);
        //print(moveDelay);
        //pos.x += velX * Time.deltaTime;
        //transform.position = pos;
        /*if (stopGravity == false)
        {
            float accY = gravity * Time.deltaTime;
            //GRAVITY = (gravity * Time.deltaTime) * 2;
            //print(GRAVITY);
            //pos.y += velY - GRAVITY;// gravity * Time.deltaTime;
            velY = accY;
            print(velY);
            pos.y = velY;
            print(velY);
            //transform.position = pos;
        }*/

        

        //transform.position = pos;
    }

    void Jump()
    {
        print("Jumping");
        //transform.Translate(new Vector3(0, (jumpTimer * 5), 0));
        velY += 10 * Time.deltaTime;
        pos.y = velY; 

    }

    void Slide()
    {
        print("Sliding");

        print(slideTimer);
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        if(velX > 3)
        {
            velX = 10;// - slideTimer;
            if(slideTimer >= 5)
            {
                velX -= (slideTimer * .5f);
            }
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

    //Apply a collision "fix" to prevent collision detection
    //param fix How far to move the player

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
                GRAVITY = 0;
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
