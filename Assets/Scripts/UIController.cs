using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject gameOverWin;
    public GameObject pauseWin;
    public GameObject[] hearts;
    public GameObject[] weaponsIcon;
    public GameObject[] enchantIcon;
    public PlayerController player;
    public Text ammoText;
    public Text scoreText;


    // Start is called before the first frame update
    void Awake()
    { 
        Time.timeScale = 1;
    }


    private void Update()
    {
        PlayerEnchantInfo();
        PlayerLifeInfo();
        GameOver();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }

    }
    

    private void LateUpdate()
    {
        PlayerAmmoInfo();
        PlayerWeaponInfo();
        PlayerScoreInfo();
    }


    public void PauseButton()
    {
        //Cursor.visible = true;
        Debug.Log("Pause");
        pauseWin.SetActive(true);
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
        pauseWin.SetActive(false);
        Time.timeScale = 1;
    }


    public void PlayerLifeInfo()
    {
        Debug.Log(player.life);

        if (player.isHit)
        {
            hearts[player.life].SetActive(false);
        }

        if (player.isHealGet && player.life <= 3)
        {
            hearts[player.life-1].SetActive(true);
        }
        
        if (player.isShield)
        {
            for (int i = player.life; i < hearts.Length; i++)
            {
                hearts[i].SetActive(true);
            }
            player.life = 7;
            player.isShield = false;
        }
    }


    public void PlayerEnchantInfo()
    {
        switch (player.enchantType)
        {
            case EnchantType.Normal:
                EnchantIconSet(0);
                break;
            case EnchantType.Elec:
                EnchantIconSet(1);
                break;
            case EnchantType.Fire:
                EnchantIconSet(2);
                break;
            case EnchantType.Ice:
                EnchantIconSet(3);
                break;
        }
    }


    private void EnchantIconSet(int index)
    {
        for (int i = 0; i < enchantIcon.Length; i++)
        {
            if (index != i)
            {
                enchantIcon[i].SetActive(false);
            }
            else
            {
                enchantIcon[i].SetActive(true);
            }
        }
    }



    public void PlayerWeaponInfo()
    {
        switch (player.weaponType)
        {
            case WeaponType.HG:
                WeaponsIconSet(0);
                break;
            case WeaponType.AR:
                WeaponsIconSet(1);
                break;
            case WeaponType.SG:
                WeaponsIconSet(2);
                break;
            case WeaponType.RPG:
                WeaponsIconSet(3);
                break;
        }
    }


    private void WeaponsIconSet(int index)
    {
        for (int i = 0; i < weaponsIcon.Length; i++)
        {
            if (index != i)
            {
                weaponsIcon[i].SetActive(false);
            }
            else
            {
                weaponsIcon[i].SetActive(true);
            }
        }
    }


    public void PlayerScoreInfo()
    {
        Debug.Log("!");
        scoreText.text = player.scoreAll + "P";
    }


    public void PlayerAmmoInfo()
    {
        ammoText.text = player.weapon.ammo + " / " + player.weapon.ammo_Temp;
    }


    public void GameOver()
    {
        if (!player.gameObject.activeSelf)
        {
            Time.timeScale = 0;
            gameOverWin.SetActive(true);
        }
    }
}
