using UnityEngine;
using UnityEngine.XR.Content.Interaction;

// تأكد من وجود هذا الكود في نفس namespace الذي يتواجد فيه XRGripButton
public class CoolantPumpIndicatorController : MonoBehaviour
{
    public XRGripButton gripButton; // اربط هذا بالزر XR في الـ Inspector
    public Transform indicatorNeedle;
    public float maxRotationDegrees = 135f;

    private bool isPumpActive = false;

    private void Awake()
    {
        // تأكد من إعداد الأحداث بشكل صحيح
        gripButton.onPress.AddListener(TurnOnPump);
        gripButton.onRelease.AddListener(TurnOffPump);
    }

    private void OnDestroy()
    {
        // نظف الأحداث عند تدمير الكائن
        gripButton.onPress.RemoveListener(TurnOnPump);
        gripButton.onRelease.RemoveListener(TurnOffPump);
    }

    private void TurnOnPump()
    {
        // تفعيل المضخة وتحديث المؤشر
        isPumpActive = true;
        UpdateIndicator();
    }

    private void TurnOffPump()
    {
        // إيقاف المضخة وتحديث المؤشر
        isPumpActive = false;
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        // تحديث دوران المؤشر بناءً على حالة المضخة
        var targetRotation = isPumpActive ? Quaternion.Euler(0f, 0f, -maxRotationDegrees) : Quaternion.identity;
        indicatorNeedle.localRotation = Quaternion.Slerp(indicatorNeedle.localRotation, targetRotation, Time.deltaTime * 10f);
    }
}
