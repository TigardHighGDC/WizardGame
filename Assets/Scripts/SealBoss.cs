using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealBoss : MonoBehaviour
{
    private bool CanBeHit = false;
    private bool IsDiving = false;
    public float sealSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if(IsDiving)
        {
            sealSpeed = 10;
            CanBeHit = false;
        }
        else
        {
            sealSpeed = 2;
            CanBeHit = true;
        }
    }


    public void Diving()
    {
        IsDiving = true;
    }


    /*
    apply vertical movement up wait then down changing animation have a random number thing from 1-3 for amount of times going up until splash down
    on splash down have damage radius and summon two small enemies
    slow movement while not diving but quick movement while diving along with not being able to be hit

    */

}