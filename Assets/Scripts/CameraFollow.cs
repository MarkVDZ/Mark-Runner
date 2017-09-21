using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //This is what the target will follow
    public Transform target;
    //Determines the offset at which to follow
    public Vector3 offset;
	
	// Update is called once per frame
    /// <summary>
    /// Moves the target every frame
    /// </summary>
	void LateUpdate () {
        transform.position = target.position + offset;
	}
}
