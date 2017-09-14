using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    PlayerController player;
    public static bool isGod = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void resetPlayerMovement()
    {
        if(player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
        }
        
    }

    void extraLife()
    {
        if(player.life < 1)
        {
            player.life += 1;
        }
    }

    void godMode()
    {
        isGod = true;
    }
}
