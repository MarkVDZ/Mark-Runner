using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    PlayerController player;
    //public GameObject powerup;
    public bool isLife = false;
    public bool isGodPowerup = false;
    public bool isResetPowerup = false;
    public bool canRemove = false;

    // Use this for initialization
    void Start()
    {
        //player = GetComponent<PlayerController>();
        int powerPick = Random.Range(1, 11);
        //print(powerPick);
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
            default:
                break;
        }
        /*if(powerPick >= 9)
        {
            isLife = true;
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void obtainPowerup()
    {
        player =  FindObjectOfType<PlayerController>();
        //print("PICK A POWERUP");
        
        if (isLife == true)
        {
            extraLife();
        }
        else if (isGodPowerup == true)
        {
            godMode();
        }
        else if (isResetPowerup == true)
        {
            resetPlayerMovement();
        }


    }

    void resetPlayerMovement()
    {
        print("RESET MOVE!");
        if (player.addedMoveDelay > 0)
        {
            player.addedMoveDelay = 0;
            player.multiplier = 1;
        }
        canRemove = true;
    }

    void extraLife()
    {
        print("Gain LIFE!");
        if (player.life < 1)
        {
            player.life += 1;
        }
        canRemove = true;
    }

    void godMode()
    {
        print("ARE GOD!");
        PlayerController.isGod = true;
        player.godTimer = 6;
        canRemove = true;
    }
}
