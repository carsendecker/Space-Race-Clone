using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber;
    public float MaxSpeed;
    public float TurnSpeed;
    public int SupplyCount;
    public bool HasBomb;
    public KeyCode ShootBomb;

    public GameObject PickupParticles, StunnedParticles, DeathParticles;
    public GameObject BombLive;
    public Image BombIndicator;

    public AudioClip pickupSound, hitSound, shootSound, bombHitSound;

    private Vector2 startPos;
    private Rigidbody2D rb;
    private bool canMove = true;
    private AudioSource aso;
    
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        aso = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if(canMove)
            Move();
    }

    void Update()
    {
        if (Input.GetKeyDown(ShootBomb) && HasBomb && canMove)
        {
            FireBomb();
        }   
        
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
            Instantiate(DeathParticles, transform.position, transform.rotation);
            StartCoroutine(DeathDelay());
            GameController.GC.SupplyRemove(gameObject);
            aso.PlayOneShot(hitSound);
        }
        else if (other.CompareTag("Supply"))
        {
            Instantiate(PickupParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameController.GC.SupplyPickup(gameObject);
            aso.PlayOneShot(pickupSound);
        }
        else if (other.CompareTag("BombPickup"))
        {
            Instantiate(PickupParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            HasBomb = true;
            BombIndicator.color = Color.white;
            aso.PlayOneShot(pickupSound);
        }
        else if (other.CompareTag("BombLive"))
        {
            Destroy(other.gameObject);
            Instantiate(StunnedParticles, transform.position, Quaternion.identity);
            StartCoroutine(StunnedDelay());
            aso.PlayOneShot(bombHitSound);
        }
    }

    private void FireBomb()
    {
        aso.PlayOneShot(shootSound);
        Instantiate(BombLive, transform.position, transform.rotation);
        HasBomb = false;
        BombIndicator.color = new Color(0.32f, 0.32f, 0.32f);
    }

    IEnumerator StunnedDelay()
    {
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.7f);
        rb.velocity = Vector2.zero;
        canMove = false;
        yield return new WaitForSeconds(2);
        canMove = true;
    }

    IEnumerator DeathDelay()
    {
        canMove = false;
        Vector2 goAway = startPos;
        goAway.y -= 50;
        
        transform.position = goAway;
        yield return new WaitForSecondsRealtime(1);
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        canMove = true;
    }
}
