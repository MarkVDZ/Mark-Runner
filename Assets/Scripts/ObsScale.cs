using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the height of walls
/// </summary>
public class ObsScale : MonoBehaviour {

    /// <summary>
    /// Vector 3 used to manipulate certain axis of the object
    /// </summary>
    public Vector3 size;
    /// <summary>
    /// This is the max size the object can be
    /// </summary>
    [HideInInspector]
    static public float maxRange = 5;

	// Use this for initialization
    /// <summary>
    /// Sets a random Y size and sets the objects half size for AABB
    /// </summary>
	void Start () {
        size = transform.localScale;
        size.y = Random.Range(1, maxRange);
        transform.localScale = size;
        transform.GetComponentInParent<AABB>().halfSize.y = size.y / 2;
	}
}
