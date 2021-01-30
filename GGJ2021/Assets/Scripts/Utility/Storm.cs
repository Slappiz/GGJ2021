using System;
using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;



public class Storm : MonoBehaviour
{
    public float StormDuration = 10f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameEvents.OnStormEnter(10f);   
    }
}
