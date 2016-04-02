using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class snakeManager : MonoBehaviour {

    int lookingPosition; // position that tells us in witch direction will snake move
    int lookedPosition; // position that tells us in witch direction did snake move last turn

    public List<GameObject> snakeParts = new List<GameObject>(); // list of gameobjects of snake

    RectTransform headTransform; // head of snake
    RectTransform lastTailTransform; // last tail of snake

    public List<GameObject> poolOfSnakeParts = new List<GameObject>(); // pool of snake parts

    Vector2 positionOfLastTail; // position of last free position

    float speed;

    void Start ()
    {
        speed = 0.2f;

        headTransform = snakeParts[0].GetComponent<RectTransform>();
        lastTailTransform = snakeParts[2].GetComponent<RectTransform>();

        lookingPosition = Random.Range(0, 3); // random looking position that cannot be right (becouse we start faced left)
        lookedPosition = lookingPosition; // looked position is the same as looking position at start

        StartCoroutine(moveSnake());
    }
	
	void Update ()
    {
        detectKeys();

        

    }

    void detectKeys()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && lookedPosition != 1)
        {
            lookingPosition = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && lookedPosition != 0)
        {
            lookingPosition = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lookedPosition != 3)
        {
            lookingPosition = 2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lookedPosition != 2)
        {
            lookingPosition = 3;
        }
    }

    IEnumerator moveSnake()
    {
        yield return new WaitForSeconds(speed); // wait for speed;
        moveBody(); // move snake
        lookedPosition = lookingPosition; // looked position becomes looking position
        StartCoroutine(moveSnake()); // we start all over again
    }

    void moveBody()
    {
        moveTail();
        moveHead();
             
    }

    

    void moveTail()
    {
        positionOfLastTail = lastTailTransform.localPosition;
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

    public void addSnakeTail()
    {
        poolOfSnakeParts[0].GetComponent<RectTransform>().localPosition = new Vector2(positionOfLastTail.x, positionOfLastTail.y);
        lastTailTransform = poolOfSnakeParts[0].GetComponent<RectTransform>();
        snakeParts.Add(poolOfSnakeParts[0]);
        poolOfSnakeParts.RemoveAt(0);
    }

    

}
