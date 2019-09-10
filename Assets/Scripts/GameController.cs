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
    public float supplySpawnSpeed;
    public int startingCrates;
    public int bombChance;

    public TMP_Text p1ScoreText, p2ScoreText;
    public TMP_Text endText;
    public GameObject supplyCrate;
    public GameObject bombPickup;
    public Vector2 supplySpawnBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        timeSlider.maxValue = timeLimit;
        timeSlider.value = timeLimit;

        for (int i = 0; i < startingCrates; i++)
        {
            SpawnSupplies();
        }

        StartCoroutine(SupplyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        timeSlider.value = timeLimit;

        if (timeLimit <= 0)
        {
            Time.timeScale = 0;
            endText.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }

    //Spawns supply crates for both player's at the same time. Loops until the game stops
    private IEnumerator SupplyTimer()
    {
        yield return new WaitForSeconds(supplySpawnSpeed);
        SpawnSupplies();
        StartCoroutine(SupplyTimer());
    }

    private void SpawnSupplies()
    {
        int chance = Random.Range(0, bombChance);
        
        //Spawn for player 1
        //Random position within the bounds of player's play area
        float randPosX = Random.Range(-supplySpawnBounds.x, -0.5f);
        float randPosY = Random.Range(-supplySpawnBounds.y, supplySpawnBounds.y);

        Vector2 randPos = new Vector2(randPosX, randPosY);
        
        if (chance != bombChance)
        {
            Instantiate(supplyCrate, randPos, Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickup, randPos, Quaternion.identity);
        }

        //Spawn for player 2
        randPosX = Random.Range(0.5f, supplySpawnBounds.x);
        randPosY = Random.Range(-supplySpawnBounds.y, supplySpawnBounds.y);

        randPos = new Vector2(randPosX, randPosY);
        
        if (chance != bombChance)
        {
            Instantiate(supplyCrate, randPos, Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickup, randPos, Quaternion.identity);
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
