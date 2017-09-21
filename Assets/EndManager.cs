using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = "Score: " + PlayerController.score;
	}
	
	// Update is called once per frame
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
