using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSceneController : MonoBehaviour
{
    public void GameExitButton()
    {
        Application.Quit();
    }


    public void GameStartButton()
    {
        SceneManager.LoadScene("Map");
    }
}
