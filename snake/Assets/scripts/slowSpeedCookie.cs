using UnityEngine;
using System.Collections;

public class slowSpeedCookie : MonoBehaviour
{

    Collider2D hit;
    public snakeManager snakeManagerScrpt;
    public GameObject speedDownText;

    void Start()
    {
        StartCoroutine(startSpeedCookie());
    }

    IEnumerator startSpeedCookie()
    {
        yield return new WaitForSeconds(30f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        do
        {
            float width = 143 + Random.Range(0, 19) * 50;
            float height = 595 - Random.Range(1, 9) * 50;

            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(width, height);

            hit = Physics2D.OverlapCircle(transform.position, 1f);

        } while (hit != null);

        GetComponent<Animator>().Play("foodSpawn");

        yield return new WaitForSeconds(7f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-999f, -999f);

        StartCoroutine(startSpeedCookie());
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!snakeManagerScrpt.endGame)
        {
            speedDownText.SetActive(true);
            StartCoroutine(disableAfterSecond());

            snakeManagerScrpt.speed += 0.04f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-999f, -999f);

            snakeManagerScrpt.spriteHead.sprite = snakeManagerScrpt.happyPlayer;
            StopCoroutine(snakeManagerScrpt.changeHeadToNormalSprite());
            StartCoroutine(snakeManagerScrpt.changeHeadToNormalSprite());
            
            snakeManagerScrpt.audioLevelUp.Play();
        }
    }

    IEnumerator disableAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        speedDownText.SetActive(false);
    }
}
