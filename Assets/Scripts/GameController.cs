using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Items;

    [SerializeField] private float itemSpawnRate;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ItemRandomSpawn", itemSpawnRate, itemSpawnRate);
    }


    void ItemRandomSpawn()
    {
        float ranX = Random.RandomRange(-9, 42);
        float ranZ = Random.RandomRange(-9, 29);

        int index = Random.RandomRange(0, 3);

        Vector3 iSpawnPos = new Vector3(ranX, 0.6f, ranZ);

        //yield return new WaitForSeconds(itemSpawnRate);

        Instantiate(Items[index], iSpawnPos, Quaternion.identity);

        //yield return null;
    }
}
