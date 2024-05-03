using UnityEngine;

public class Speedometer2 : MonoBehaviour
{
    public Transform needle;
    public Speedometer1 speedometer1;
    private float currentAngle = 0.0f;
    public float rotationSpeed = 1.0f;

   

    // تحديث زاوية الإبرة بناءً على قيمة السلايدر
    public void UpdateNeedle(float sliderValue)
    {
        float previousAngle = currentAngle;
        // تحويل قيمة السلايدر من 0-1 إلى زاوية من 0 إلى -180 درجة
        float angle = Mathf.Lerp(0, -180, sliderValue);
        needle.eulerAngles = new Vector3(0, 0, angle);

        float angleDelta = currentAngle - previousAngle;
        speedometer1.UpdateNeedleAngle(angleDelta);
    }
}
