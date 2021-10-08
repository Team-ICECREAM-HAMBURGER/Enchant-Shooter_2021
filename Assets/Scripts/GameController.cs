using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ParticleSystem[] bulletHitFX;
    public GameObject[] Items;

    [SerializeField] private float itemSpawnRate;


    private int index;


    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("ItemRandomSpawn", itemSpawnRate, itemSpawnRate);
    }


    // Hit FX Play
    public void HitFX(Vector3 hitPos, string bulletName)
    {
        switch(bulletName) {
            case "Bullet_Elec(Clone)":
                index = 0;
                break;
            case "Bullet_Fire(Clone)":
                index = 1;
                break;
            case "Bullet_Ice(Clone)":
                index = 2;
                break;
        }

        bulletHitFX[index].transform.position = hitPos;
        bulletHitFX[index].Play();
    }


    private void ItemRandomSpawn()
    {
        float ranX = Random.Range(-9, 42);
        float ranZ = Random.Range(-9, 29);

        int index = Random.Range(0, 3);

        Vector3 iSpawnPos = new Vector3(ranX, 0.6f, ranZ);

        //yield return new WaitForSeconds(itemSpawnRate);

        Instantiate(Items[index], iSpawnPos, Quaternion.identity);

        //yield return null;
    }
}
