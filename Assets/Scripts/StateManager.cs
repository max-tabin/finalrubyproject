using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
   /* public void ReloadCurrentScene()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    */

    public AudioSource musicSource;

    public static int scoreValue = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            scoreValue = 0;
            musicSource.Play();
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //scene.name
        }
    }
    
}
