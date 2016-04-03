using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class snakeManager : MonoBehaviour {

    int lookingPosition; // position that tells us in witch direction will snake move
    int lookedPosition; // position that tells us in witch direction did snake move last turn

    public List<GameObject> snakeParts = new List<GameObject>(); // list of gameobjects of snake

    RectTransform headTransform; // head of snake
    RectTransform lastTailTransform; // last tail of snake

    public List<GameObject> poolOfSnakeParts = new List<GameObject>(); // pool of snake parts

    Vector2 positionOfLastTail; // position of last free position

    public float speed; // speed of snake

    public bool endGame; // if end game

    public endGameManager endGameManager;

    public int score;

    public AudioSource audioSource;
    public AudioSource audioLevelUp;
    public AudioClip deathSound;
    public AudioClip eatingSound;

    public Sprite deadPlayer; // sprite for dead player
    public Sprite happyPlayer; // sprite for dead player
    public Sprite normalPlayer;
    public Image spriteHead;

    public GameObject numberOne;
    public GameObject numberTwo;
    public GameObject numberThree;

    public GameObject food;

    public GameObject speedUpText;
    

    public GameObject headObject;

    public int swipe;

    void Start ()
    {
        swipe = -1;

        audioSource = GetComponent<AudioSource>(); 

        score = 0;

        endGame = false;

        speed = 0.4f;

        headTransform = snakeParts[0].GetComponent<RectTransform>();
        lastTailTransform = snakeParts[2].GetComponent<RectTransform>();

        lookingPosition = Random.Range(0, 3); // random looking position that cannot be right (becouse we start faced left)
        lookedPosition = 2  ;

        Vector2 lookingRotation;

        if(lookingPosition==0)
        {
            lookingRotation = new Vector2(0f,-1f);
        }
        else if(lookingPosition == 1)
        {
            lookingRotation = new Vector2(0f, 1f);
        }
        else if(lookingPosition == 2)
        {
            lookingRotation = new Vector2(1f, 0f);
        }
        else
        {
            lookingRotation = new Vector2(-1f, 0f);
        }

        float angle = Mathf.Atan2(lookingRotation.x,lookingRotation.y) * Mathf.Rad2Deg;
        headObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        spriteHead = headTransform.gameObject.GetComponent<Image>();

        StartCoroutine(threeSecondSpawn()); // delay for start        

    }

    IEnumerator threeSecondSpawn()
    {
        StartCoroutine(showStartNumbers()); // shows start numbers

        yield return new WaitForSeconds(3f);

        StartCoroutine(moveSnake()); // moves snake
        StartCoroutine(speedUpSnake()); // speeds up snake
    }

    IEnumerator showStartNumbers()
    {
        audioLevelUp.Play();
        numberThree.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioLevelUp.Play();
        numberThree.SetActive(false);
        numberTwo.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioLevelUp.Play();
        numberTwo.SetActive(false);
        numberOne.SetActive(true);
        yield return new WaitForSeconds(1f);
        numberOne.SetActive(false);
    }


    void Update ()
    {
        detectKeys();

        

    }

    void detectKeys()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || swipe == 0) && lookedPosition != 1)
        {
            lookingPosition = 0;
            float angle = Mathf.Atan2(0f, -1f) * Mathf.Rad2Deg;
            headObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || swipe == 1) && lookedPosition != 0)
        {
            lookingPosition = 1;
            float angle = Mathf.Atan2(0f, 1f) * Mathf.Rad2Deg;
            headObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || swipe == 2) && lookedPosition != 3)
        {
            lookingPosition = 2;
            float angle = Mathf.Atan2(1f, 0f) * Mathf.Rad2Deg;
            headObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || swipe == 3) && lookedPosition != 2)
        {
            lookingPosition = 3;
            float angle = Mathf.Atan2(-1f, 0f) * Mathf.Rad2Deg;
            headObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void removeTail()
    {
        spriteHead.sprite = happyPlayer;
        StopCoroutine(changeHeadToNormalSprite());
        StartCoroutine(changeHeadToNormalSprite());

        audioSource.clip = eatingSound;
        audioSource.Play();

        lastTailTransform.localPosition = new Vector2(-999f, -999f);
        lastTailTransform.gameObject.SetActive(false);
        lastTailTransform = snakeParts[snakeParts.Count-2].GetComponent<RectTransform>();        
        snakeParts.RemoveAt(snakeParts.Count - 1);
    }

    IEnumerator speedUpSnake() // speed up snake every n seconds
    {
        yield return new WaitForSeconds(20f);
        if(!endGame)
        {
            audioLevelUp.Play();
            speedUpText.SetActive(true);
            StartCoroutine(disableAfterSecond());
            if (!(speed < 0.08f)) speed -= 0.04f;
            StartCoroutine(speedUpSnake());
        }        
    }

    IEnumerator disableAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        speedUpText.SetActive(false);
    }

    IEnumerator moveSnake()
    {
        yield return new WaitForSeconds(speed); // wait for speed;
        

        if (!endGame)
        {
            moveBody(); // move snake
            lookedPosition = lookingPosition; // looked position becomes looking position
            StartCoroutine(moveSnake()); // we start all over again
        }        
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
        spriteHead.sprite = happyPlayer;
        StopCoroutine(changeHeadToNormalSprite());
        StartCoroutine(changeHeadToNormalSprite());
        audioSource.clip = eatingSound;
        audioSource.Play();
        score++;
        poolOfSnakeParts[0].SetActive(true);
        poolOfSnakeParts[0].GetComponent<RectTransform>().localPosition = new Vector2(positionOfLastTail.x, positionOfLastTail.y);
        lastTailTransform = poolOfSnakeParts[0].GetComponent<RectTransform>();
        snakeParts.Add(poolOfSnakeParts[0]);
        poolOfSnakeParts.RemoveAt(0);
    }

    public IEnumerator changeHeadToNormalSprite()
    {
        yield return new WaitForSeconds(1f);
        if (!endGame) spriteHead.sprite = normalPlayer;
    }

    public void makeEffectOfEndGame()
    {
        if(!endGame)
        {
            endGame = true;
            audioSource.clip = deathSound;
            audioSource.Play();
            endGameManager.endGame();
            spriteHead.sprite = deadPlayer;

            foreach (GameObject tail in snakeParts)
            {
                tail.GetComponent<Rigidbody2D>().gravityScale = 30f;
                tail.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150f, 150f), 200f), ForceMode2D.Impulse);
                
            }

            food.GetComponent<Rigidbody2D>().gravityScale = 30f;
            food.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150f, 150f), 200f), ForceMode2D.Impulse);

        }

    }




}
