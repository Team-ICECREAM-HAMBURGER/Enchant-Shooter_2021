using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float actTime;

    private GameObject player;
    private PlayerController playerController;
    private WeaponType randomWeapon;

    private string itemType;


    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = playerController.gameObject;

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
                    if (playerController.life < 5)
                    {
                        playerController.life += 1;
                    }
                    break;
                case "Items_Shield(Clone)":    // HP -> 7
                    playerController.life = 7;
                    break;
                case "Items_Gun(Clone)":       // Gun Random Pick-Up
                    // Gun Random Select //
                    randomWeapon = (WeaponType)Random.Range(1, 4);
                    playerController.weaponType = randomWeapon;

                    // UnSelected Gun UnActivate //
                    for (int i = 0; i < player.transform.childCount; i++)
                    {
                        if (player.transform.GetChild(i).gameObject.activeSelf)
                        {
                            player.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }

                    // Selected Gun Activate //
                    player.transform.GetChild((int)randomWeapon).gameObject.SetActive(true);
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