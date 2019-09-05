using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    public float MoveSpeed;
    public float WrapPosition;

    private float wrapTime;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += MoveSpeed * Time.deltaTime;
        transform.position = pos;

        if (Math.Abs(transform.position.x - WrapPosition) < 0.05) // if transform.position.x == WrapPosition
        {
            pos.x = -WrapPosition;
            transform.position = pos;
        }
    }
}
