using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Will Dickinson
/// All elements of the GUI are done in here (score, lives, game over, and restart buttons)
/// </summary>
public class Gui : MonoBehaviour {

    public GameObject powerBar;
    public int lives = 3;
    public int score = 0;
    public GUIStyle text;
    public GUIStyle smallText;

    private bool gameOver = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Signifies game over
    /// </summary>
    public void GameOver()
    {
        gameOver = true;
    }

    private void OnGUI()
    {

        //Lives and score
        GUI.Box(new Rect(100, -50, 200, 200), "Lives: " + lives + "         Score: " + score, text);

        //Extra controls
        if(powerBar.GetComponent<PowerUp>().ready)
            GUI.Box(new Rect(Screen.width - 400, Screen.height - 200, 400, 200), "Press the down arrow or the 'S' key to activate \n MARINARA MADNESS", smallText);

        if (gameOver)
        {
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 100, 600, 200), "GAME OVER", text);

            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 100, 120, 60), "RESTART"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 200, 120, 60), "QUIT"))
                Application.Quit();

        }
    }
}
