using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osilate : MonoBehaviour {

    public bool isMovingDown = true;
    float speed = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if(isMovingDown == true)
        {
            pos.y -= speed * Time.deltaTime;
        }
        else
        {
            pos.y += speed * Time.deltaTime;
        }

        transform.position = pos;

        if(pos.y >= 5)
        {
            isMovingDown = true;
        }
	}
}
