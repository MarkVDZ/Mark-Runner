using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject ground;
    public GameObject wall;
    public GameObject thowmp;
    public GameObject lava;
    public GameObject powerup;
    public Transform player;

    List<GameObject> chunks = new List<GameObject>();
    List<GameObject> obsticles = new List<GameObject>();
    List<GameObject> powerups = new List<GameObject>();
    int allowSpawn;
    int obsSpawn;
    int powerSpawn;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if(chunks.Count > 0)
        {
            if(player.position.x - chunks[0].transform.position.x > 25)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
                CollisionManager.groundTiles.RemoveAt(0);
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

            //foreach(GameObject chunk in chunks)
            //{
                for(int i = 1; i < 9; i++)
                {
                    allowSpawn = Random.Range(1, 10);
                    //print("i" + i + " " + "Generated " + allowSpawn);
                    if(allowSpawn >= 3)
                    {
                        obsSpawn = Random.Range(1, 10);
                        //print("i" + i + " " + "Generated " + obsSpawn);
                        //print(obsSpawn);
                        if (obsSpawn == 5)
                        {
                            Vector3 wallPos = Vector3.zero;

                            wallPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            //wallPos = chunk.transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objWall = Instantiate(wall, wallPos, Quaternion.identity);
                            obsticles.Add(objWall);
                            AABB wallAABB = objWall.GetComponent<AABB>();
                            CollisionManager.walls.Add(wallAABB);
                        }
                        if (obsSpawn == 7)
                        {
                            Vector3 thowmpPos = Vector3.zero;

                            thowmpPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objThowmp = Instantiate(thowmp, thowmpPos, Quaternion.identity);
                            float speed = Random.Range(2, 10);
                            obsticles.Add(objThowmp);
                            AABB thowmpAABB = objThowmp.GetComponent<AABB>();
                            CollisionManager.thowmps.Add(thowmpAABB);
                        }
                        if (obsSpawn == 9)
                        {
                            Vector3 lavaPos = Vector3.zero;

                            lavaPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint" + i.ToString()).position;
                            GameObject objLava = Instantiate(lava, lavaPos, Quaternion.identity);
                            obsticles.Add(objLava);
                            AABB lavaAABB = objLava.GetComponent<AABB>();
                            CollisionManager.lavas.Add(lavaAABB);
                        }
                    }
                    
                }
                for(int j = 1; j <= 6; j++)
                {
                    powerSpawn = Random.Range(1, 10);
                    if(powerSpawn >= 7)
                    {
                        Vector3 powerupPos = Vector3.zero;

                        powerupPos = chunks[chunks.Count - 1].transform.Find("PowerupSpawn" + j.ToString()).position;
                        GameObject objPowerup = Instantiate(powerup, powerupPos, Quaternion.identity);
                        powerups.Add(objPowerup);
                        //AABB powerupAABB = objPowerup.GetComponent<AABB>();
                        //CollisionManager.powerups.Add(powerupAABB);
                    }
                }
            //}
            /*Vector3 wallPos = Vector3.zero;

            wallPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint").position;
            GameObject objWall = Instantiate(wall, wallPos, Quaternion.identity);
            walls.Add(objWall);*/


        }

        
	}
}
