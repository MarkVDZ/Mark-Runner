using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles powerups for the player and setting them up
/// </summary>
public class Powerup : MonoBehaviour
{
    
    /// <summary>
    /// Reference to the player controller to activate powerups
    /// </summary>
    PlayerController player;

    /// <summary>
    /// Is the powerup a life powerup?
    /// </summary>
    public bool isLife = false;
    /// <summary>
    /// Is the powerup a God Mode powerup?
    /// </summary>
    public bool isGodPowerup = false;
    /// <summary>
    /// Is the powerup a move timer reset powerup?
    /// </summary>
    public bool isResetPowerup = false;
    /// <summary>
    /// Is the powerup a power jump powerup?
    /// </summary>
    public bool isPowerJump = false;
    /// <summary>
    /// Is the powerup a player can break walls powerup?
    /// </summary>
    public bool isWallBreaker = false;
    /// <summary>
    /// Is the powerup a frezee the game powerup?
    /// </summary>
    public bool isFreezeTime = false;
    /// <summary>
    /// Is the powerup ready to be removed from the game?
    /// </summary>
    public bool canRemove = false;

    /// <summary>
    /// Life powerup material
    /// </summary>
    public Material lifeMat;
    /// <summary>
    /// Move reset powerup material
    /// </summary>
    public Material resetMat;
    /// <summary>
    /// God Mode powerup material
    /// </summary>
    public Material godMat;
    /// <summary>
    /// Life powerup material
    /// </summary>
    public Material breakMat;
    /// <summary>
    /// Move reset powerup material
    /// </summary>
    public Material jumpMat;
    /// <summary>
    /// God Mode powerup material
    /// </summary>
    public Material freezeMat;

    // Use this for initialization
    /// <summary>
    /// Generates a random integer and assigns the powerup ability and texture based on the number
    /// </summary>
    void Start()
    {
        int powerPick = Random.Range(1, 16);
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
                //isGodPowerup = true;
                isGodPowerup = true;
                GetComponent<MeshRenderer>().material = godMat;
                break;
            case 11:
                isPowerJump = true;
                GetComponent<MeshRenderer>().material = jumpMat;
                break;
            case 12:
                isWallBreaker = true;
                GetComponent<MeshRenderer>().material = breakMat;
                break;
            case 13:
                isFreezeTime = true;
                GetComponent<MeshRenderer>().material = freezeMat;
                break;
            case 14:
                isWallBreaker = true;
                GetComponent<MeshRenderer>().material = breakMat;
                break;
            case 15:
                isPowerJump = true;
                GetComponent<MeshRenderer>().material = jumpMat;
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
        else if (isPowerJump)
        {
            PowerJump();
        }
        else if (isWallBreaker)
        {
            WallBreaker();
        }
        else if (isFreezeTime)
        {
            FreezeTime();
        }


    }
    /// <summary>
    /// This powerup resets the player's move timer, thus also reseting the score multiplier
    /// </summary>
    void resetPlayerMovement()
    {
        //print("RESET MOVE!");
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
        //print("Gain LIFE!");
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
        //print("ARE GOD!");
        PlayerController.isGod = true;
        player.godTimer = 6;
        canRemove = true;
    }
    /// <summary>
    /// This powerup gives the player the ability to power jump
    /// </summary>
    void PowerJump()
    {
        //print("Power Jump!");
        PlayerController.canPowerJump = true;
        player.powerJumpTimer = 6;
        canRemove = true;
    }
    /// <summary>
    /// This powerup gives the player the ability to break walls
    /// </summary>
    void WallBreaker()
    {
        //print("Wall Break active!");
        PlayerController.canBreakWalls = true;
        canRemove = true;
    }
    /// <summary>
    /// This powerup freezes time for a few seconds
    /// </summary>
    void FreezeTime()
    {
        //print("STOP TIME!");
        PlayerController.isTimeStopped = true;
        player.timeFreezeTimer = 2;
        PlayerController.isGod = true;
        player.godTimer = 2;
        canRemove = true;
    }
}
