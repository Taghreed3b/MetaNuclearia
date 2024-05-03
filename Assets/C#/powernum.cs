using UnityEngine;
using UnityEngine.UI; // إضافة مكتبة واجهة المستخدم

public class powernum : MonoBehaviour
{
    public Text timerText; // مرجع لعنصر Text

    private float timer = 0.0f;
    private bool increasing = true;
    private const float MAX_TIME = 1800f;

    void Update()
    {
        if (increasing)
        {
            timer += Time.deltaTime;
            if (timer >= MAX_TIME)
            {
                increasing = false;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                increasing = true;
            }
        }

        // تحديث نص عنصر Text لعرض قيمة التايمر
        timerText.text = " " + Mathf.RoundToInt(timer).ToString();
    }
}
