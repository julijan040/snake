using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class foodScript : MonoBehaviour {

    public snakeManager snakeManagerScrpt;
    Collider2D hit;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!snakeManagerScrpt.endGame)
        {

            snakeManagerScrpt.addSnakeTail();

            do
            {
                float width = 143 + Random.Range(0, 19) * 50;
                float height = 595 - Random.Range(1, 9) * 50;

                gameObject.GetComponent<RectTransform>().localPosition = new Vector2(width, height);

                hit = Physics2D.OverlapCircle(transform.position, 1f);

            } while (hit != null);

        }
        
    }
}
