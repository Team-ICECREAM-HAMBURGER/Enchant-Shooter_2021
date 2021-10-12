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
    public AudioSource[] playerAudio;
    public AudioSource[] gunAudio;
    public AudioSource[] itemAudio;
    public AudioSource[] enchantAudio;
    public AudioSource[] enemyAudio;
    public AudioSource[] BGM;
    public PlayerController player;
    public Text ammoText;
    public Text scoreText;
    public bool enemyKilledSwt;

    private bool playerKilledSwt;


    // Start is called before the first frame update
    void Awake()
    {
        int index = Random.Range(0, 3);
        BGM[index].Play();

        playerKilledSwt = false;
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
    

    public void ButtonSound()
    {
        enchantAudio[0].Play();
    }

    public void EnemyKilledAudio()
    {
        enemyAudio[0].Play();
        enemyKilledSwt = true;
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
        //Debug.Log("Pause");
        pauseWin.SetActive(true);
        Time.timeScale = 0; // Game pause;
    }


    public void RestartButton()
    {
        //Debug.Log("Restart");
        SceneManager.LoadScene("Map");
    }


    public void GameExitButton()
    {
        //Debug.Log("Exit");
        Application.Quit();
    }


    public void WindowCloseButton()
    {
        //Debug.Log("Restore");
        pauseWin.SetActive(false);
        Time.timeScale = 1;
    }


    public void PlayerLifeInfo()
    {
        //Debug.Log(player.life);

        if (player.gameObject.activeSelf)
        {
            if (player.isHit && player.life > 0)
            {
                if (!playerAudio[0].isPlaying)
                {
                    playerAudio[0].Play();
                    player.isHit = false;
                }
                hearts[player.life].SetActive(false);
                
            }

            if (player.isHealGet && player.life <= 3)
            {
                if (!itemAudio[1].isPlaying && player.isHealGet)
                {
                    itemAudio[1].Play();
                    player.item_HP.Play();
                    player.isHealGet = false;
                }
                hearts[player.life - 1].SetActive(true);
            }

            if (player.isShield)
            {
                for (int i = player.life; i < hearts.Length; i++)
                {
                    hearts[i].SetActive(true);
                }
                
                if (!itemAudio[1].isPlaying && player.isShield)
                {
                    player.life = 7;
                    itemAudio[1].Play();
                    player.item_Shield.Play();
                    player.isShield = false;
                }
            }
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
        if (!itemAudio[0].isPlaying && player.isGunGet)
        {
            itemAudio[0].Play();
            player.item_Gun.Play();
            player.isGunGet = false;
        }

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
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(false);
            }

            if (!playerAudio[0].isPlaying && !playerKilledSwt)
            {
                playerAudio[1].Play();
                playerKilledSwt = true;
            }
            Time.timeScale = 0;
            gameOverWin.SetActive(true);
        }
    }
}
