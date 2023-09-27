using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBounce : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D powerUp;



    // Start is called before the first frame update
    void Start()
    {

        powerUp.velocity = new Vector2(speed, transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {

    }




}
