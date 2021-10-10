using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float actTime;
    
    private GameObject playerHand;
    private PlayerController playerController;
    private WeaponType randomWeapon;
    private string itemType;


    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHand = GameObject.Find("player_LeftHand.001");
        StartCoroutine("ActivatingTimer");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            itemType = gameObject.name;

            // Item Get //
            switch (itemType)
            {
                case "Items_Hill(Clone)":      // HP +1
                    if (playerController.life < 3)
                    {
                        playerController.life += 1;
                        playerController.isHealGet = true;
                    }
                    break;
                case "Items_Shield(Clone)":    // HP -> 7
                    playerController.life = 7;
                    playerController.isSheldGet = true;
                    break;
                case "Items_Gun(Clone)":       // Gun Random Pick-Up
                    // Gun Random Select //
                    randomWeapon = (WeaponType)Random.Range(1, 4);
                    playerController.weaponType = randomWeapon;

                    // UnSelected Gun UnActivate //
                    for (int i = 0; i < playerHand.transform.childCount; i++)
                    {
                        if (playerHand.transform.GetChild(i).gameObject.activeSelf)
                        {
                            playerHand.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }

                    // Selected Gun Activate //
                    playerHand.transform.GetChild((int)randomWeapon).gameObject.SetActive(true);
                    break;
            }
        }
    }


    IEnumerator ActivatingTimer()
    {
        yield return new WaitForSeconds(actTime);
        Destroy(gameObject);
    }
}