using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerDisplay : MonoBehaviour
{
    public Text timerText; // اربط هذا بعنصر النص UI في المحرر
    private TimeSpan timeSpan;
    private float elapsedTime = 0f;
    private float targetTime = 8 * 60; // 8 دقائق بالثواني

    void Update()
    {
        if (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(targetTime - elapsedTime);
            // تحديث تنسيق النص ليعرض الدقائق والثواني فقط
            timerText.text = string.Format("{0:D2}:{1:D2}",
                timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
