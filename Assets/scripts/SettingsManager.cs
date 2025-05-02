using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Image brightnessOverlay; // Чёрный Image поверх всего экрана

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 1f);

        ApplyVolume(volumeSlider.value);
        ApplyBrightness(brightnessSlider.value);

        volumeSlider.onValueChanged.AddListener(ApplyVolume);
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
    }

    public void ApplyVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void ApplyBrightness(float value)
    {
        // Преобразуем яркость в затемнение: 1 (ярко) → 0 прозрачности
        if (brightnessOverlay != null)
        {
            Color c = brightnessOverlay.color;
            c.a = 1f - value; // Чем ниже значение — тем темнее экран
            brightnessOverlay.color = c;
            PlayerPrefs.SetFloat("brightness", value);
        }
    }
}
