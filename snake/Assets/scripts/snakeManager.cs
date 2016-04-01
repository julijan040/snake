using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snakeManager : MonoBehaviour {

    int lookingPosition;
    public List<GameObject> snakeParts = new List<GameObject>();
    RectTransform headTransform;
    RectTransform lastTailTransform;

    void Start ()
    {
        headTransform = snakeParts[0].GetComponent<RectTransform>();
        lastTailTransform = snakeParts[2].GetComponent<RectTransform>();

        lookingPosition = Random.Range(0, 3);

        StartCoroutine(moveSnake());
    }
	
	void Update ()
    {
        detectKeys();
    }

    void detectKeys()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && lookingPosition != 1)
        {
            lookingPosition = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && lookingPosition != 0)
        {
            lookingPosition = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lookingPosition != 3)
        {
            lookingPosition = 2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lookingPosition != 2)
        {
            lookingPosition = 3;
        }
    }

    IEnumerator moveSnake()
    {
        yield return new WaitForSeconds(1F);
        moveBody();
        StartCoroutine(moveSnake());
    }

    void moveBody()
    {
        moveTail();
        moveHead();
             
    }

    

    void moveTail()
    {
        lastTailTransform.localPosition = headTransform.localPosition; // change position of last tail with head of snake
        GameObject temp = lastTailTransform.gameObject;
        snakeParts.RemoveAt(snakeParts.Count - 1); // remove last tail from list
        snakeParts.Insert(1, temp); // insert last tail as first tail in snake
        lastTailTransform = snakeParts[snakeParts.Count - 1].GetComponent<RectTransform>(); // change reference to last tail

    }

    void moveHead()
    {
        if (lookingPosition == 0)
        {
            headTransform.localPosition = new Vector2(headTransform.localPosition.x, headTransform.localPosition.y - 50f);
        }
        else if (lookingPosition == 1)
        {
            headTransform.localPosition = new Vector2(headTransform.localPosition.x, headTransform.localPosition.y + 50f);
        }
        else if (lookingPosition == 2)
        {
            headTransform.localPosition = new Vector2(headTransform.localPosition.x - 50f, headTransform.localPosition.y);
        }
        else if (lookingPosition == 3)
        {
            headTransform.localPosition = new Vector2(headTransform.localPosition.x + 50f, headTransform.localPosition.y);
        }
    }

}
