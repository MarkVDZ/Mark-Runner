﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This class handles collision responces between the player and other objects in the scene
/// </summary>
public class CollisionManager : MonoBehaviour
{
    /// <summary>
    /// A reference to the player's AABB
    /// </summary>
    AABB player;

    //Lists of all the different types of objects the player can interact with
    /// <summary>
    /// List of the ground tiles in the scene
    /// </summary>
    static public List<AABB> groundTiles = new List<AABB>();
    /// <summary>
    /// List of the powerups in the scene
    /// </summary>
    static public List<AABB> powerups = new List<AABB>();
    /// <summary>
    /// List of the walls in the scene
    /// </summary>
    static public List<AABB> walls = new List<AABB>();
    /// <summary>
    /// List of the thowmps in the scene
    /// </summary>
    static public List<AABB> thowmps = new List<AABB>();
    /// <summary>
    /// List of the lavapits in the scene
    /// </summary>
    static public List<AABB> lavas = new List<AABB>();
    /// <summary>
    /// List of the mines in the scene
    /// </summary>
    static public List<AABB> mines = new List<AABB>();

    /// <summary>
    /// Hurt sounds effect
    /// </summary>
     public AudioClip hurt;
    /// <summary>
    /// Pickup sound effect
    /// </summary>
     public AudioClip pickup;


    // Use this for initialization
    /// <summary>
    /// Sets the players AABB and clears the lists to avoid replay errors
    /// </summary>
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<AABB>();
        groundTiles.Clear();
        powerups.Clear();
        walls.Clear();
        thowmps.Clear();
        lavas.Clear();
    }

    // Update is called once per frame
    /// <summary>
    /// This calls all the different collision methods
    /// </summary>
    void LateUpdate()
    {
        //print(wall);
        DoCollisionDetectionGround();

        if (PlayerController.isGod == false && PlayerController.isDead == false && PlayerController.hasIframes == false)
        {
            DoCollisionDetectionWall();
            DoCollisionDetectionThowmp();
            DoCollisionDetectionLava();
            DoCollisionDetectionMine();
            //DoCollisionDetectionPowerup();
        }
        DoCollisionDetectionPowerup();

    }

    /// <summary>
    /// Handles collison of objects with the ground
    /// </summary>
    void DoCollisionDetectionGround()
    {

        foreach (AABB ground in groundTiles)
        {
            bool resultGround = player.checkOverlap(ground);
            //print(resultGround);
            if (resultGround == true)
            {
                Vector3 fix = player.CalculateOverlapFix(ground);
                //print(fix);
                player.GetComponent<PlayerController>().ApplyFix(fix);

            }

            foreach (AABB thowmp in thowmps)
            {
                bool resultThowmp = thowmp.checkOverlap(ground);

                if (resultThowmp == true && thowmp != null)
                {
                    
                    Vector3 fix = thowmp.CalculateOverlapFix(ground);
                    //print(fix);
                    //player.GetComponent<PlayerController>().ApplyFix(fix);
                    thowmp.GetComponent<Osilate>().isMovingDown = false;                    
                }
            }
        }
    }

    /// <summary>
    /// Handles player collision with the walls
    /// </summary>
    void DoCollisionDetectionWall()
    {
        foreach (AABB wall in walls)
        {
            bool resultWall = player.checkOverlap(wall);
            if (resultWall == true)
            {
                
                PlayerController controller = player.GetComponent<PlayerController>();
                Vector3 fix = player.CalculateOverlapFix(wall);
                if (fix.y != 0)
                {
                    /*player.GetComponent<PlayerController>().ApplyFix(fix);
                    PlayerController.hasIframes = true;
                    controller.iframeTimer = .3f;*/
                    //AudioSource.PlayClipAtPoint(hurt, transform.position);
                    //controller.blood.Play();
                    //controller.life -= 1;
                    //PlayerController.isGod = true;
                    PlayerController.hasIframes = true;
                    controller.iframeTimer = .5f;
                }
                else
                {
                    if (PlayerController.canBreakWalls)
                    {
                        Destroy(wall.gameObject);
                        walls.Remove(wall);
                        GetComponent<GameController>().walls.Remove(wall.gameObject);
                        PlayerController.canBreakWalls = false;
                        return;
                    }
                    AudioSource.PlayClipAtPoint(hurt, transform.position);
                    controller.blood.Play();
                    controller.life -= 1;
                    //PlayerController.isGod = true;
                    PlayerController.hasIframes = true;
                    controller.iframeTimer = .5f;

                }

            }
        }
    }

    /// <summary>
    /// Handles player collision with the thowmps
    /// </summary>
    void DoCollisionDetectionThowmp()
    {
        foreach (AABB thowmp in thowmps)
        {
            bool resultThowmp = player.checkOverlap(thowmp);
            //print(resultWall);
            if (resultThowmp == true)
            {
                AudioSource.PlayClipAtPoint(hurt, transform.position);
                PlayerController controller = player.GetComponent<PlayerController>();
                Vector3 fix = player.CalculateOverlapFix(thowmp);
                if(fix.y != 0)
                {
                    controller.life -= 2;
                    
                }
                else
                {
                    controller.life -= 1;
                    PlayerController.hasIframes = true;
                    controller.iframeTimer = .5f;
                }
                controller.blood.Play();
            }
        }
    }

    /// <summary>
    /// Handles player collision with the lava
    /// </summary>
    void DoCollisionDetectionLava()
    {
        foreach (AABB lava in lavas)
        {
            bool resultLava = player.checkOverlap(lava);

            if (resultLava == true)
            {
                AudioSource.PlayClipAtPoint(hurt, transform.position);
                PlayerController controller = player.GetComponent<PlayerController>();
                Vector3 fix = player.CalculateOverlapFix(lava);
                if (fix.y != 0)
                {
                    controller.life -= 1;
                    player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 3, player.GetComponent<Transform>().position.z);
                    //print("GSDGSDGASGWGASGAS");
                    PlayerController.hasIframes = true;
                    controller.iframeTimer = .8f;
                    player.GetComponent<PlayerController>().isGrounded = false;
                    controller.blood.Play();
                }
                else
                {
                    player.GetComponent<PlayerController>().ApplyFix(fix);
                }
            }
        }
    }

    void DoCollisionDetectionMine()
    {

    }

    /// <summary>
    /// Handles player collision with the powerups
    /// </summary>
    void DoCollisionDetectionPowerup()
    {
        foreach (AABB powerup in powerups)
        {
            bool resultPowerup = player.checkOverlap(powerup);
            if (resultPowerup == true && powerup != null)
            {
                //print("COLLIDE!!!");
                powerup.GetComponent<Powerup>().obtainPowerup();
                AudioSource.PlayClipAtPoint(pickup, transform.position);
                if(powerup.GetComponent<Powerup>().canRemove == true)
                {
                    Destroy(powerup.gameObject);
                    powerups.Remove(powerup);
                    GetComponent<GameController>().powerups.Remove(powerup.gameObject);
                    return;
                }
            }
        }
    }
}
