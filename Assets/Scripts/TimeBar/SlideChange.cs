using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideChange : MonoBehaviour
{

    private float timeRemaining;
    private const float timerMax = 5f;
    public Slider slider;

    private void Start() {
        timeRemaining = timerMax;
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
            print("Dever de casa concluído!");           
        }
    }


    float CalculateSliderValue()
    {
        return (timeRemaining   / timerMax);
    }
}
