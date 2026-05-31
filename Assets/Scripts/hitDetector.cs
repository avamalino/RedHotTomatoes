using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class hitDetector : MonoBehaviour
{
    public static List<Tomato> activeTomatoes = new List<Tomato>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        
    }

    void checkHit()
    {
        float currentTime = Time.time; //change to current time the song is on

        Tomato closest = null;
        float smallestDifference = Mathf.Infinity;

        //for (Tomato tomato in Tomato.activeTomatoes)
        {

        }
    }
}
