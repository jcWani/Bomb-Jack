using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform character;
    [SerializeField] float speed;
    [SerializeField] float agroRange;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, character.position);

        if(disToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            stopChasingPlayer();
        }

    }

    private void stopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);

    }


    private void ChasePlayer()
    {
        if (transform.position.x < character.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }




}
