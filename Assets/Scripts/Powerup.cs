using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //Reference to the player controller to activate powerups
    PlayerController player;

    //Determines what kind of powerup the powerup is
    public bool isLife = false;
    public bool isGodPowerup = false;
    public bool isResetPowerup = false;
    public bool canRemove = false;

    //Materials for the different types of powerups
    public Material lifeMat;
    public Material resetMat;
    public Material godMat;

    // Use this for initialization
    /// <summary>
    /// Generates a random integer and assigns the powerup ability and texture based on the number
    /// </summary>
    void Start()
    {
        int powerPick = Random.Range(1, 11);
        //print(powerPick);
        switch (powerPick)
        {
            case 1:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 2:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 3:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 4:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material = resetMat;
                break;
            case 5:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 6:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material = resetMat;
                break;
            case 7:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 8:
                isResetPowerup = true;
                GetComponent<MeshRenderer>().material = resetMat;
                break;
            case 9:
                isLife = true;
                GetComponent<MeshRenderer>().material = lifeMat;
                break;
            case 10:
                isGodPowerup = true;
                GetComponent<MeshRenderer>().material = godMat;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// This function is called from the CollisionManager class and determines what powerup the player collided with and what function to call
    /// </summary>
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
    /// <summary>
    /// This powerup resets the player's move timer, thus also reseting the score multiplier
    /// </summary>
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
    /// <summary>
    /// This powerup give the player an exrea life if they don't already have one
    /// </summary>
    void extraLife()
    {
        print("Gain LIFE!");
        if (player.life < 1)
        {
            player.life += 1;
        }
        canRemove = true;
    }
    /// <summary>
    /// This powerup makes the player invincible for a period of time
    /// </summary>
    void godMode()
    {
        print("ARE GOD!");
        PlayerController.isGod = true;
        player.godTimer = 6;
        canRemove = true;
    }
}
