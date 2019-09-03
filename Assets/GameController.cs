using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider timeSlider;
    public float timeLimit;
    
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
    }
}
