using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnee;
    public int maxNumZombie = 5;
    private bool stopSpawning = false;
    private float spawnTime = 2.0f;
    private float spawnDelay = 4.0f;
    private float position_x;
    private float position_z;
    private float Timer;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("zombie").Length >= maxNumZombie)
        {
            CancelInvoke();
        }
       
    }
    public void SpawnObject()
    {
        position_x = Random.Range(-30, 30);
        position_z = Random.Range(-30, 30);
        Instantiate(spawnee, new Vector3(position_x, 0, position_z), transform.rotation);
    }
}
