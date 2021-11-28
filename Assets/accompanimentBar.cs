using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class accompanimentBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
    public void setInitialAccompaniment (float accompaniment) {
        slider.value = accompaniment;
        gradient.Evaluate(0f);
        fill.color = gradient.Evaluate(0f);
    }

    public void setMaxAccompaniment (float accompaniment) {
        slider.maxValue = accompaniment;
    }

    public void setAccompaniment (float accompaniment) {
        slider.value = accompaniment;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
