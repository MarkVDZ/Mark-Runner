using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

    //Text used to displays the players final score
    public Text scoreText;

	// Use this for initialization
    /// <summary>
    /// Displays the players score
    /// </summary>
	void Start () {
        scoreText.text = "Score: " + PlayerController.score;
	}
	
	// Update is called once per frame
    /// <summary>
    /// Switches the scene based on player input
    /// Reset the game on "Submit" button press
    /// Go the the main menu on "Cancel" button press
    /// </summary>
	void Update () {
        if (Input.GetAxis("Cancel") > 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        if (Input.GetAxis("Submit") > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
