using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour {

    List<AABB> currentOverlaps = new List<AABB>();

    public Vector3 halfSize;

    Vector3 min = Vector3.zero;
    Vector3 max = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
        calcEdges();
	}

    void calcEdges()
    {
        min = transform.position - halfSize;
        max = transform.position + halfSize;
    }

    public bool checkOverlap(AABB other)
    {
        if(other != null)
        {
            if (min.x > other.max.x) return false;
            if (max.x < other.min.x) return false;

            if (min.y > other.max.y) return false;
            if (max.y < other.min.y) return false;

            if (min.z > other.max.z) return false;
            if (max.z < other.min.z) return false;

            return true;
        }
        return true;
    }
}
