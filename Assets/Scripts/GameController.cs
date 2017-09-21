using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //References to the different GameObjects that can be generated
    public GameObject ground;
    public GameObject wall;
    public GameObject thowmp;
    public GameObject lava;
    public GameObject powerup;

    //Reference to the player's transform
    public Transform player;

    //Lists to store all the different GameObjects that can be generated
    List<GameObject> chunks = new List<GameObject>(); //Ground
    //List<GameObject> obsticles = new List<GameObject>);
    List<GameObject> walls = new List<GameObject>(); //Walls 
    List<GameObject> thowmps = new List<GameObject>(); //Thowmps
    List<GameObject> lavapits = new List<GameObject>(); //Lava
    List<GameObject> mines = new List<GameObject>(); 
    public List<GameObject> powerups = new List<GameObject>(); //Powerups

    //Integers used for random number generation that controls spawning
    int allowSpawn;
    int obsSpawn;
    int powerSpawn;

	// Use this for initialization
    /// <summary>
    /// Clears all the lists for game reset
    /// </summary>
	void Start () {
        chunks.Clear();
        walls.Clear();
        thowmps.Clear();
        lavapits.Clear();
        powerups.Clear();
	}
	
	// Update is called once per frame
    /// <summary>
    /// Generates new ground chunks for the player to move along and the obsticles/powerups on those chunks
    /// Also removes objects when they are no longer needed and also from the AABB lists in CollisionManager
    /// </summary>
	void Update () {
        
        if(chunks.Count > 0)
        {
            if(player.position.x - chunks[0].transform.position.x > 25)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
                CollisionManager.groundTiles.RemoveAt(0);

            }
            if(walls.Count > 0)
            {
                if (player.position.x - walls[0].transform.position.x > 25)
                {
                    Destroy(walls[0]);
                    walls.RemoveAt(0);
                    CollisionManager.walls.RemoveAt(0);
                }
            }
            if(thowmps.Count > 0)
            {
                if (player.position.x - thowmps[0].transform.position.x > 25)
                {
                    Destroy(thowmps[0]);
                    thowmps.RemoveAt(0);
                    CollisionManager.thowmps.RemoveAt(0);
                }
            }
            
            if(lavapits.Count > 0)
            {
                if (player.position.x - lavapits[0].transform.position.x > 25)
                {
                    Destroy(lavapits[0]);
                    lavapits.RemoveAt(0);
                    CollisionManager.lavas.RemoveAt(0);
                }
            }
            
            if(powerups.Count > 0)
            {
                if (player.position.x - powerups[0].transform.position.x > 25)
                {
                    Destroy(powerups[0]);
                    powerups.RemoveAt(0);
                    CollisionManager.powerups.RemoveAt(0);
                }
            }
            
        }
		while(chunks.Count < 5)
        {
            Vector3 position = Vector3.zero;
            if(chunks.Count > 0)
            {
                position = chunks[chunks.Count - 1].transform.Find("Connecter").position;
            }
            GameObject obj = Instantiate(ground, position, Quaternion.identity);
            chunks.Add(obj);
            AABB groundAABB = obj.GetComponent<AABB>();
            CollisionManager.groundTiles.Add(groundAABB);

            if(chunks.Count > 1)
            {
                for (int i = 1; i < 9; i++)
                {
                    allowSpawn = Random.Range(1, 10);
                    //print("i" + i + " " + "Generated " + allowSpawn);
                    if (allowSpawn >= 4)
                    {
                        obsSpawn = Random.Range(1, 11);
                        //print("i" + i + " " + "Generated " + obsSpawn);
                        //print(obsSpawn);
                        if (obsSpawn <= 3)
                        {
                            Vector3 wallPos = Vector3.zero;

                            wallPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            //wallPos = chunk.transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objWall = Instantiate(wall, wallPos, Quaternion.identity);
                            walls.Add(objWall);
                            AABB wallAABB = objWall.GetComponent<AABB>();
                            CollisionManager.walls.Add(wallAABB);
                        }
                        if (obsSpawn >= 4 && obsSpawn <= 7)
                        {
                            Vector3 thowmpPos = Vector3.zero;

                            thowmpPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objThowmp = Instantiate(thowmp, thowmpPos, Quaternion.Euler(0, -90, 0));
                            float speed = Random.Range(2, 10);
                            thowmps.Add(objThowmp);
                            AABB thowmpAABB = objThowmp.GetComponent<AABB>();
                            CollisionManager.thowmps.Add(thowmpAABB);
                        }
                        if (obsSpawn >= 8)
                        {
                            Vector3 lavaPos = Vector3.zero;

                            lavaPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objLava = Instantiate(lava, lavaPos, Quaternion.identity);
                            lavapits.Add(objLava);
                            AABB lavaAABB = objLava.GetComponent<AABB>();
                            CollisionManager.lavas.Add(lavaAABB);
                        }
                    }

                }
                for (int j = 1; j <= 6; j++)
                {
                    powerSpawn = Random.Range(1, 10);
                    if (powerSpawn >= 7)
                    {
                        Vector3 powerupPos = Vector3.zero;

                        powerupPos = chunks[chunks.Count - 1].transform.Find("PowerupSpawn" + j.ToString()).position;
                        GameObject objPowerup = Instantiate(powerup, powerupPos, Quaternion.Euler(0, 180, 0));
                        powerups.Add(objPowerup);
                        AABB powerupAABB = objPowerup.GetComponent<AABB>();
                        CollisionManager.powerups.Add(powerupAABB);
                    }
                }
            }
        }   
	}
}
