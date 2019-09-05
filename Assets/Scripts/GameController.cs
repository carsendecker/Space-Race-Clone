using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider timeSlider;
    public float timeLimit;
    public float playerStartY;
    public int player1Score, player2Score;

    public TMP_Text p1ScoreText, p2ScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        timeSlider.maxValue = timeLimit;
        timeSlider.value = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        timeSlider.value = timeLimit;

        if (timeLimit <= 0)
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2"))
        {
            Vector2 pos = other.transform.position;
            pos.y = playerStartY;
            other.transform.position = pos;
            
            if (other.CompareTag("Player 1"))
            {
                player1Score++;
                p1ScoreText.text = player1Score.ToString();
            }
            else
            {
                player2Score++;
                p2ScoreText.text = player2Score.ToString();

            }
        }
    }
}
