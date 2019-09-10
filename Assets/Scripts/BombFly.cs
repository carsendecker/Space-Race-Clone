using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFly : MonoBehaviour
{
    public float MoveSpeed;

    private CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        StartCoroutine(Reenable());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * MoveSpeed;
    }

    IEnumerator Reenable()
    {
        yield return new WaitForSeconds(0.4f);
        col.enabled = true;
    }
}
