using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    GameObject[] bomb;
    [SerializeField] float DelayTime;
    [SerializeField] GameObject Explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(DestroyEachBombs(DelayTime));


    }

    IEnumerator DestroyEachBombs(float DelayTime)
    {
        bomb = GameObject.FindGameObjectsWithTag("BombCollectible");

        foreach (GameObject i in bomb)
        {
            Vector2 BombLoc = new Vector2(transform.position.x, transform.position.y);
            yield return new WaitForSeconds(DelayTime);
            Destroy(i);
            Instantiate(Explosion, BombLoc, Quaternion.identity);
        }

    }

}
