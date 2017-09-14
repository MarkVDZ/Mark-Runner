using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    AABB player;
    static public List<AABB> groundTile = new List<AABB>();
    static public List<AABB> powerups = new List<AABB>();
    static public List<AABB> walls = new List<AABB>();
    public AABB wall;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<AABB>();
        //wall = GameObject.Find("Wall").GetComponent<AABB>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //print(wall);
        DoCollisionDetectionGround();
        DoCollisionDetectionWall();


    }

    void DoCollisionDetectionGround()
    {

        


        foreach (AABB ground in groundTile)
        {

            bool resultGround = player.checkOverlap(ground);
            //print(resultGround);
            if(resultGround == true)
            {
                //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5 * Time.deltaTime, player.transform.position.z);
                player.GetComponent<PlayerController>().stopGravity = true;
                //player.GetComponent<MeshRenderer>().material.color = Color.black;
                return;
            }
            else
            {
                player.GetComponent<PlayerController>().stopGravity = false;
                //player.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }



        
    }
    void DoCollisionDetectionWall()
    {
        bool resultWall = player.checkOverlap(wall);
        print(resultWall);
        if (resultWall == true)
        {
            player.GetComponent<PlayerController>().speed = 0;
        }
    }
}
