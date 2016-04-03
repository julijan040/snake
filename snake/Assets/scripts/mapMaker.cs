using UnityEngine;
using System.Collections;

public class mapMaker : MonoBehaviour
{
    Collider2D hit;
    public GameObject[] walls;
    public GameObject food;

    void Start()
    {
        foreach (GameObject wall in walls)
        {
            do
            {
                float width = 143 + Random.Range(0, 19) * 50;
                float height = 595 - Random.Range(1, 9) * 50;

                wall.GetComponent<RectTransform>().localPosition = new Vector2(width, height);

                hit = Physics2D.OverlapCircle(wall.transform.position, 1f);

            } while (hit != null);

            wall.layer = 5;
        }


        do
        {
            float width = 143 + Random.Range(0, 19) * 50;
            float height = 595 - Random.Range(1, 9) * 50;

            food.GetComponent<RectTransform>().localPosition = new Vector2(width, height);

            hit = Physics2D.OverlapCircle(food.transform.position, 1f);

        } while (hit != null);

    }
    
}
