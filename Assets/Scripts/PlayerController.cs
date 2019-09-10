using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber;
    public float MaxSpeed;
    public float TurnSpeed;

    public GameObject PickupParticles, StunnedParticles, DeathParticles;

    private Vector2 startPos;
    private Rigidbody2D rb;
    
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        
    }

    private void Move()
    {
        float forwardSpeed = 0;
        float rotateSpeed = 0;
        if (PlayerNumber == 1)
        {
            forwardSpeed = Input.GetAxis("Vertical") * MaxSpeed;
            rotateSpeed = Input.GetAxis("Horizontal") * TurnSpeed;
        }
        else if (PlayerNumber == 2)
        {
            forwardSpeed = Input.GetAxis("Vertical2") * MaxSpeed;
            rotateSpeed = Input.GetAxis("Horizontal2") * TurnSpeed;
        }

        rb.velocity = transform.up * (forwardSpeed * Time.deltaTime);
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            StartCoroutine(DeathDelay());
        }
        else if (other.CompareTag("Supply"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator DeathDelay()
    {
        Vector2 goAway = startPos;
        goAway.y -= 50;
        
        transform.position = goAway;
        yield return new WaitForSecondsRealtime(1);
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
    }
}
