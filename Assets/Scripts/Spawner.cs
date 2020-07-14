using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject populatorObj;
    public GameObject zombie;

    MasterScript master;
    Populator populator;

    int countdown = 7200;

    void Start()
    {
        master = GameObject.FindGameObjectWithTag("MasterScript").GetComponent<MasterScript>();
        populator = populatorObj.GetComponent<Populator>();
        Spawnzz();
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Zombie").Length == 0)
        {
            countdown = 7200;
        }
        else
        {
            if (countdown > 0)
            {
                countdown--;
            }
            else {
                master.wave++;
                Spawnzz();
                populator.PopulateCrate();
            }
        }
    }

    void Spawnzz()
    {
        for (int i = 0; i < 100 * master.wave; i++)
        {
            GameObject zombe = Instantiate(zombie, new Vector2(Random.Range(-90, 90), Random.Range(-90, 90)), Quaternion.identity);
            ZombieControl zombieControl = zombe.GetComponent<ZombieControl>();
        }
    }

}
