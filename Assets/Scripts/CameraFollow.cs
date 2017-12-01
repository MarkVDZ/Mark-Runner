using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the camera follow the player
/// </summary>
public class CameraFollow : MonoBehaviour {

    /// <summary>
    /// This is what the target will follow
    /// </summary>
    public Transform target;
    /// <summary>
    /// Determines the offset at which to follow
    /// </summary>
    public Vector3 offset;
	
	// Update is called once per frame
    /// <summary>
    /// Moves the target every frame
    /// </summary>
	void LateUpdate () {
        transform.position = target.position + offset;
	}
}
