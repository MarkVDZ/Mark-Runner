﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject ground;
    public GameObject wall;
    public Transform player;

    List<GameObject> chunks = new List<GameObject>();
    List<GameObject> walls = new List<GameObject>();
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

            Vector3 wallPos = Vector3.zero;

            wallPos = chunks[chunks.Count - 1].transform.Find("ObsSpwanPoint").position;
            GameObject objWall = Instantiate(wall, wallPos, Quaternion.identity);
            walls.Add(objWall);

        }

        
	}
}
