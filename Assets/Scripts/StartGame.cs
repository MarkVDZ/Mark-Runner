using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class controls input for the Main Scene
/// </summary>
public class StartGame : MonoBehaviour {

	
	// Update is called once per frame
    /// <summary>
    /// Switches scenes when the "jump" button is pressed
    /// </summary>
	void Update () {
		if(Input.GetAxis("Jump") > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
	}
}
