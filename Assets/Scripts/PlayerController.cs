using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public KeyCode UpKey, DownKey;
    
    void Start()
    {
        
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
}
