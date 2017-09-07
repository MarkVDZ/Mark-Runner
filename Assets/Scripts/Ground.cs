using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public GameObject prefabFloor;
    private bool spawnNew = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x + .1f, transform.localPosition.y, transform.localPosition.z);

        if(transform.localPosition.x >= 5 && spawnNew == false)
        {
            Instantiate(prefabFloor, new Vector3(-10, -1, 0), Quaternion.identity);
            spawnNew = true;
        }
        if(transform.localPosition.x >= 10)
        {
            Destroy(this.gameObject);
        }
	}
}
