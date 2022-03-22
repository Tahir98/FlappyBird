using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    public Text scoreText;
    void Start()
    {
        scoreText.text = "HIGHEST SCORE\n" + PlayerPrefs.GetInt("Highest Score");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitFromGame() {
        Application.Quit(0);
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }
}
