using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSceneController : MonoBehaviour
{
    public AudioSource[] BGM;
    public AudioSource startBtn;

    private void Awake()
    {
        int index = Random.Range(0, 3);
        BGM[index].Play();
    }


    public void GameExitButton()
    {
        Application.Quit();
    }


    public void GameStartButton()
    {
        startBtn.Play();
        SceneManager.LoadScene("Map");
    }



}
