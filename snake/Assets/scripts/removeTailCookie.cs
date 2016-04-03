using UnityEngine;
using System.Collections;

public class removeTailCookie : MonoBehaviour {

    Collider2D hit;
    public snakeManager snakeManagerScrpt;
    public GameObject removeTailText;

    void Start()
    {
        StartCoroutine(startSpeedCookie());
    }

    IEnumerator startSpeedCookie()
    {
        yield return new WaitForSeconds(20f);
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
            removeTailText.SetActive(true);
            StartCoroutine(disableAfterSecond());


            snakeManagerScrpt.removeTail();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(-999f, -999f);
        }
    }

    IEnumerator disableAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        removeTailText.SetActive(false);
    }
}
