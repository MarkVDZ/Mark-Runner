using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CollisionManager : MonoBehaviour
{
    
    AABB player;
    static public List<AABB> groundTiles = new List<AABB>();
    static public List<AABB> powerups = new List<AABB>();
    static public List<AABB> walls = new List<AABB>();
    static public List<AABB> thowmps = new List<AABB>();
    static public List<AABB> lavas = new List<AABB>();
    static public List<AABB> mines = new List<AABB>();

    //public AudioClip move;
    //public AudioClip jump;
    //public AudioClip die;
     public AudioClip hurt;
     public AudioClip pickup;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<AABB>();
        
        //wall = GameObject.Find("Wall").GetComponent<AABB>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //print(wall);
        DoCollisionDetectionGround();

        if (PlayerController.isGod == false)
        {
            DoCollisionDetectionWall();
            DoCollisionDetectionThowmp();
            DoCollisionDetectionLava();
            DoCollisionDetectionMine();
            //DoCollisionDetectionPowerup();
        }
        DoCollisionDetectionPowerup();

    }

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
            /*else
            {
                player.GetComponent<PlayerController>().stopGravity = false;
                //player.GetComponent<MeshRenderer>().material.color = Color.blue;
            }*/
            foreach (AABB thowmp in thowmps)
            {
                bool resultThowmp = thowmp.checkOverlap(ground);

                if (resultThowmp == true)
                {
                    
                    Vector3 fix = thowmp.CalculateOverlapFix(ground);
                    //print(fix);
                    //player.GetComponent<PlayerController>().ApplyFix(fix);
                    thowmp.GetComponent<Osilate>().isMovingDown = false;                    
                }
            }
            /*bool resultThowmp = thump.checkOverlap(ground);

            if (resultThowmp == true)
            {
                print("COLLIDE!!!");
                Vector3 fix = thump.CalculateOverlapFix(ground);
                //print(fix);
                //player.GetComponent<PlayerController>().ApplyFix(fix);
                thump.GetComponent<Osilate>().isMovingDown = false;
            }*/

        }




    }
    void DoCollisionDetectionWall()
    {
        foreach (AABB wall in walls)
        {
            bool resultWall = player.checkOverlap(wall);
            if (resultWall == true)
            {
                //player.GetComponent<PlayerController>().velX = 0;
                PlayerController controller = player.GetComponent<PlayerController>();
                Vector3 fix = player.CalculateOverlapFix(wall);
                if(fix.y != 0)
                {
                    player.GetComponent<PlayerController>().ApplyFix(fix);
                    PlayerController.isGod = true;
                    controller.godTimer = .2f;
                }
                else
                {
                    AudioSource.PlayClipAtPoint(hurt, transform.position);
                    controller.life -= 1;
                    PlayerController.isGod = true;
                    controller.godTimer = .5f;
                }
                
            }
        }
        /*bool resultWall = player.checkOverlap(wall);
        //print(resultWall);
        if (resultWall == true)
        {
            player.GetComponent<PlayerController>().velX = 0;
        }*/
    }

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
                    //print("GSDGSDGASGWGASGAS");
                }
                else
                {
                    controller.life -= 1;
                    PlayerController.isGod = true;
                    controller.godTimer = .5f;
                }
                //player.GetComponent<PlayerController>().velX = 0;
            }
        }
        /*bool resultThowmp = player.checkOverlap(thump);
        //print(resultWall);
        if (resultThowmp == true)
        {
            player.GetComponent<PlayerController>().velX = 0;
        }*/


    }

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
                    player.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 2, player.GetComponent<Transform>().position.z);
                    //print("GSDGSDGASGWGASGAS");

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

    void DoCollisionDetectionPowerup()
    {
        foreach (AABB powerup in powerups)
        {
            bool resultPowerup = player.checkOverlap(powerup);
            //GetComponent<SoundManager>().playSFX(GetComponent<SoundManager>().pickup);
            if (resultPowerup == true)
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
        /*bool resultPowerup = player.checkOverlap(powerup);
        //print(resultWall);
        if (resultPowerup == true)
        {
            powerup.GetComponent<Powerup>().obtainPowerup();
        }*/
    }

}
