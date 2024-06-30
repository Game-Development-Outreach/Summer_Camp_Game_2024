using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedrunClock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minutesField;
    [SerializeField] private TextMeshProUGUI secondsField;

    private bool m_ticking;

    public void StartClock() => m_ticking = true;
    public void StopClock() => m_ticking = false;


    private void Update()
    {
        if(!m_ticking)
            return;

        secondsField.text = "";
        minutesField.text = "";

        float currentTime = Time.realtimeSinceStartup;
        float currentTimeMinutes = Mathf.FloorToInt(currentTime / 60.0f);
        float currentTimeSeconds = Mathf.RoundToInt(currentTime) - currentTimeMinutes * 60;

        if (currentTimeSeconds < 10)
            secondsField.text += "0";
        if (currentTimeMinutes < 10)
            minutesField.text += "0";

        secondsField.text += currentTimeSeconds.ToString();
        minutesField.text += currentTimeMinutes.ToString();

    }
}
