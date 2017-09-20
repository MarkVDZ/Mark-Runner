using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsScale : MonoBehaviour {

    public Vector3 size;

	// Use this for initialization
	void Start () {
        size = transform.localScale;
        //Random.Range(1, 3);
        size.y = Random.Range(1, 5);
        //size.z = Random.Range(1, 3);
        print(size);
        transform.GetComponentInParent<AABB>().halfSize.y = size.y / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
