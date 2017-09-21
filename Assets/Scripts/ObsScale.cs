﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsScale : MonoBehaviour {

    //Vector 3 used to manipulate certain axis of the object
    public Vector3 size;

	// Use this for initialization
    /// <summary>
    /// Sets a random Y size and sets the objects half size for AABB
    /// </summary>
	void Start () {
        size = transform.localScale;
        size.y = Random.Range(1, 5);
        transform.localScale = size;
        transform.GetComponentInParent<AABB>().halfSize.y = size.y / 2;
	}
}
