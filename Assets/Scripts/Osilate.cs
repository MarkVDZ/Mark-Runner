using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the Thowmps movement up and down
/// </summary>
public class Osilate : MonoBehaviour {

    /// <summary>
    /// Is the object moving down or up?
    /// </summary>
    public bool isMovingDown = true;
    /// <summary>
    /// Determines how fast the object is moving
    /// </summary>
    public float speed;
    /// <summary>
    /// The max speed the object can move
    /// </summary>
    [HideInInspector]
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
