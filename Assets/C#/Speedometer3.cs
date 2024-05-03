using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Speedometer3 : MonoBehaviour
{
    public Transform needle;
    public Speedometer1 speedometer1;
    public float rotationSpeed = 1.0f;
    private float currentAngle = 0.0f;


    // تحديث زاوية الإبرة بناءً على قيمة السلايدر
    public void UpdateNeedle(float sliderValue)
    {
        float previousAngle = currentAngle;
        // تحويل قيمة السلايدر من 0-1 إلى زاوية من 0 إلى -180 درجة
        float angle = Mathf.Lerp(0, -180, sliderValue);
        needle.eulerAngles = new Vector3(0, 0, angle);
        // حساب الفرق في الزاوية وإرساله بشكل عكسي إلى Speedometer1
        float angleDelta = currentAngle - previousAngle;
        speedometer1.UpdateNeedleAngle(-angleDelta); // عكس الحركة
    }
}
