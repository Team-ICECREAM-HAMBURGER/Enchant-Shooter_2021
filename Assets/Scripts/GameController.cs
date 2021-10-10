using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ParticleSystem[] bulletHitFX;
    public GameObject[] items;
    public GameObject[] enemies;
    
    [SerializeField] private float itemSpawnRate;
    [SerializeField] private float enemySpawnRate;

    private int index;


    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("ItemRandomSpawn", itemSpawnRate, itemSpawnRate);
        InvokeRepeating("EnemySpawn", enemySpawnRate, enemySpawnRate);
    }


    // Hit FX Play
    public void HitFX(Vector3 hitPos, string bulletName)
    {
        switch(bulletName) {
            case "Bullet_Normal(Clone)":
                index = 0;
                break;
            case "Bullet_Elec(Clone)":
                index = 1;
                break;
            case "Bullet_Fire(Clone)":
                index = 2;
                break;
            case "Bullet_Ice(Clone)":
                index = 3;
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

        Instantiate(items[index], iSpawnPos, Quaternion.identity);

        //yield return null;
    }


    private void EnemySpawn()
    {
        float enRanX = Random.Range(-9, 42);
        float enRanZ = Random.Range(-9, 29);

        int index = Random.Range(0, 4);

        Vector3 enemySpawnPos = new Vector3(enRanX, 0.6f, enRanZ);

        Ray ray;
        RaycastHit hit;

        if (Physics.Raycast(enemySpawnPos, Vector3.down, out hit))
        {
            Debug.Log("pos: " + hit.transform.tag);
            if(hit.transform.tag != "Enemy")
            {
                Instantiate(enemies[index], enemySpawnPos, Quaternion.identity);
            }
        }

    }

}
