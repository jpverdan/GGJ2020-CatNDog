using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideChange : MonoBehaviour
{

    private float timeRemaining;
    [SerializeField] private float timerMax = 5f;
    public Slider slider;
    private GameManager _gm;

    private void Start() {
        timeRemaining = timerMax;
        _gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        slider.value = CalculateSliderValue();

        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if(timeRemaining<=0)
        {
            _gm.TimeUp();          
        }
    }


    float CalculateSliderValue()
    {
        return (timeRemaining   / timerMax);
    }
}
