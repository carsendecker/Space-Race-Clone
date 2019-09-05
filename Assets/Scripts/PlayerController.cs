using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public KeyCode UpKey, DownKey;

    private Vector2 startPos;
    
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(UpKey))
        {
            Vector2 pos = transform.position;
            pos.y += MoveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(DownKey))
        {
            Vector2 pos = transform.position;
            pos.y -= MoveSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            StartCoroutine(DeathDelay());
        }
    }

    IEnumerator DeathDelay()
    {
        Vector2 goAway = startPos;
        goAway.y -= 50;
        
        transform.position = goAway;
        yield return new WaitForSecondsRealtime(1);
        transform.position = startPos;

    }
}
