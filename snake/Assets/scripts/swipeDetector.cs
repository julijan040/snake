using UnityEngine;
using System.Collections;

public class swipeDetector : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public snakeManager snakeManager;

    public void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (!(secondPressPos == firstPressPos))
                {
                    if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                    {
                        if (currentSwipe.x < 0)
                        {
                            snakeManager.swipe = 2;
                            // Swipe Right          
                        }
                        else
                        {
                            snakeManager.swipe = 3;
                            //Swipe Left
                        }
                    }
                    else
                    {
                        if (currentSwipe.y < 0)
                        {
                            snakeManager.swipe = 0;
                            // Swipe Down
                        }
                        else
                        {
                            snakeManager.swipe = 1;
                            //Swipe Up
                        }
                    }
                }
            }
        }
    }
}
