using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject PauseWin;
    public GameObject[] Hearts;

    public PlayerController player;


    // Start is called before the first frame update
    void Awake()
    { 
        Time.timeScale = 1;
    }


    private void Update()
    {
        PlayerLifeInfo();
    }


    public void PauseButton()
    {
        Debug.Log("Pause");
        PauseWin.SetActive(true);
        Time.timeScale = 0; // Game pause;
    }


    public void RestartButton()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("Map");
    }


    public void GameExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }


    public void WindowCloseButton()
    {
        Debug.Log("Restore");
        PauseWin.SetActive(false);
        Time.timeScale = 1;
    }


    public void PlayerLifeInfo()
    {
        if (player.life < 3)
        {
            int index = (Hearts.Length - player.life) - 1;
            Debug.Log(index);
            Hearts[index].SetActive(false);
        }
    }


    public void PlayerEnchantInfo()
    {

    }


    public void PlayerScoreInfo()
    {

    }


    public void PlayerAmmoInfo()
    {

    }


    public void PlayerWeaponInfo()
    {

    }

}
