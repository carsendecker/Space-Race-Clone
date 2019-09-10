using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController GC;
    
    public Slider timeSlider;
    public float timeLimit;
    public Vector2 playerSpawn;
    public float supplySpawnSpeed;
    public int startingCrates;
    public int bombChance;
    
    public int player1Score, player2Score;

    public TMP_Text p1ScoreText, p2ScoreText;
    public TMP_Text endText;
    public TMP_Text p1CargoText, p2CargoText;
    public GameObject supplyCrate;
    public GameObject bombPickup;
    public Vector2 supplySpawnBounds;

    private int p1Cargo, p2Cargo;

    // Start is called before the first frame update
    void Start()
    {
        GC = this;
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
        float randPosY = Random.Range(-supplySpawnBounds.y + 1f, supplySpawnBounds.y);

        Vector2 randPos = new Vector2(randPosX, randPosY);
        
        if (chance != bombChance - 1)
        {
            Instantiate(supplyCrate, randPos, Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickup, randPos, Quaternion.identity);
        }

        //Spawn for player 2
        randPosX = Random.Range(0.5f, supplySpawnBounds.x);
        randPosY = Random.Range(-supplySpawnBounds.y + 1f, supplySpawnBounds.y);

        randPos = new Vector2(randPosX, randPosY);
        
        if (chance != bombChance - 1)
        {
            Instantiate(supplyCrate, randPos, Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickup, randPos, Quaternion.identity);
        }
    }

    public void SupplyPickup(GameObject player)
    {
        if (player.CompareTag("Player 1"))
        {
            if (p1Cargo < 6)
                p1Cargo++;
            p1CargoText.text = p1Cargo + "/6";
        }
        else
        {
            if (p2Cargo < 6)
                p2Cargo++;
            p2CargoText.text = p2Cargo + "/6";
        }
    }

    public void SupplyRemove(GameObject player)
    {
        if (player.CompareTag("Player 1"))
        {
            p1Cargo = 0;
            p1CargoText.text = p1Cargo + "/6";
        }
        else
        {
            p2Cargo = 0;
            p2CargoText.text = p2Cargo + "/6";
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2"))
        {
            StartCoroutine(RespawnDelay(other));
        }
    }

    IEnumerator RespawnDelay(Collider2D other)
    {
         other.transform.rotation = Quaternion.identity;
            if (other.CompareTag("Player 1"))
            {
                player1Score += p1Cargo;
                p1Cargo = 0;
                p1CargoText.text = p1Cargo + "/6";
                p1ScoreText.text = player1Score.ToString();
                
                other.transform.position = new Vector3(999, 999, 0);
                yield return new WaitForSeconds(1);
                other.transform.position = new Vector3(-playerSpawn.x, playerSpawn.y, 0);
            }
            else
            {
                player2Score += p2Cargo;
                p2Cargo = 0;
                p2CargoText.text = p2Cargo + "/6";
                p2ScoreText.text = player2Score.ToString();
                
                other.transform.position = new Vector3(999, 999, 0);
                yield return new WaitForSeconds(1);
                other.transform.position = playerSpawn;
            }
    }
}
