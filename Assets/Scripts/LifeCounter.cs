using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{

    public Image[] lives;
    public int livesRemaining;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        livesRemaining--;
        lives[livesRemaining].enabled = false;

        if(livesRemaining == 0)
        {
            Debug.Log("You Lost");
        }
    }

}
