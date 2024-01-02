using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMPro.TextMeshProUGUI level_text;

    public void UpdateExpSlider(int currnet, int target)
    {
        slider.maxValue = target;
        slider.value = currnet;
    }

    public void SetLevelText(int level)
    {
        level_text.text = "Level: " + level.ToString();
    }
}
