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
    public Text ammoText;
    

    // Start is called before the first frame update
    void Awake()
    { 
        Time.timeScale = 1;
    }


    private void Update()
    {
        PlayerLifeInfo();
        
    }

    private void LateUpdate()
    {
        PlayerAmmoInfo();
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
        Debug.Log(player.life);

        if (player.isHit)
        {
            Hearts[player.life].SetActive(false);
        }

        if (player.isHealGet && player.life <= 3)
        {
            Hearts[player.life-1].SetActive(true);
        }
        
        if (player.isShield)
        {
            for (int i = player.life; i < Hearts.Length; i++)
            {
                Hearts[i].SetActive(true);
            }
            player.life = 7;
            player.isShield = false;
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
        ammoText.text = player.weapon.ammo + " / " + player.weapon.ammo_Temp;
    }


    public void PlayerWeaponInfo()
    {

    }

}
