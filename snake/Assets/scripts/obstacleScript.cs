using UnityEngine;
using System.Collections;

public class obstacleScript : MonoBehaviour {

    public snakeManager snakeManagerScrpt;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        snakeManagerScrpt.makeEffectOfEndGame();
    }
   



}
