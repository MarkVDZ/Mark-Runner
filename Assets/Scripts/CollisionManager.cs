using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    AABB player;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<AABB>();
	}
	
	// Update is called once per frame
	void lateUpdate () {
        DoCollisionDetection();
	}

    void DoCollisionDetection()
    {

    }
}
