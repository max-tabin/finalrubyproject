using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    
    public Text winText;

    public AudioSource musicSource;
    public AudioClip winSong;
    //private bool counting = false;

    public static int scoreValue = 0;
    Text score;



    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Fixed Robots: " + scoreValue;
        if (scoreValue >= 4)
        {
            GameWin();
            musicSource.clip = winSong;
            musicSource.Play();
            //winText.text = "You Win! Game Created by Max Tabin. Press R to Restart.";
        }
    }
    private void GameWin()
    {
        LevelManager.instance.WinGame();
        gameObject.SetActive(false);
    }
}
