using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osilate : MonoBehaviour {

    public bool isMovingDown = true;
    public float speed;

	// Use this for initialization
	void Start () {
        speed = Random.Range(2, 7);
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
