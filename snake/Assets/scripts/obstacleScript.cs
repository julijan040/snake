using UnityEngine;
using System.Collections;

public class obstacleScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something has entered this zone.");
    }
   



}
