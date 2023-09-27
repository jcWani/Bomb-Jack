using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] float spawnRate = 10f;
    Vector2 wheretoSpawn;
    float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            wheretoSpawn = new Vector2(Random.Range(-8.4f, 8.4f), transform.position.y);
            Instantiate(enemy1, wheretoSpawn, Quaternion.identity);
        }

    }
}
