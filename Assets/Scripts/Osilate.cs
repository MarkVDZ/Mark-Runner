using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osilate : MonoBehaviour {

    //Is the object moving down or up?
    public bool isMovingDown = true;
    //Determines how fast the object is moving
    public float speed;
    static public float maxRange = 7;

	// Use this for initialization
    /// <summary>
    /// Sets a random speed for the objects to move at
    /// </summary>
	void Start () {
        speed = Random.Range(2, maxRange);
	}
	
	// Update is called once per frame
    /// <summary>
    /// Move the object either up or down every frame
    /// </summary>
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
