using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endGameManager : MonoBehaviour {
    
    public Text timeSurvived;
    public Text ate;
    public snakeManager snakeManager;
    public GameObject endGamePanel;
	
    public void endGame()
    {
        endGamePanel.SetActive(true);
        timeSurvived.text = "TIME SURVIVED: " + System.Math.Round(Time.timeSinceLevelLoad, 2);
        ate.text = "YOU ATE: "  + snakeManager.score;
    }

    public void restart()
    {
        SceneManager.LoadScene("testScene1");
    }
}
