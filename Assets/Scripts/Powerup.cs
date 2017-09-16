using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    PlayerController player;
    //public GameObject powerup;
    public bool isLife = false;
    public bool isGodPowerup = false;
    public bool isResetPowerup = false;
    public static bool isGod = false;

	// Use this for initialization
	void Start () {
        int powerPick = Random.Range(1, 10);
        print(powerPick);
        switch (powerPick)
        {
            case 1:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 2:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 3:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 4:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 5:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 6:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 7:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 8:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 9:
                isLife = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 10:
                isGodPowerup = true;
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }
        /*if(powerPick >= 9)
        {
            isLife = true;
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }*/
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
