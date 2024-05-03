using UnityEngine;

public class Speedometer1 : MonoBehaviour
{
    public Transform needle;
    private float currentAngle = 0.0f;
    public float controlFactor = 0.5f; // معامل تحكم لتنظيم الاستجابة
    public bool isAutoMoving = true; // هل المؤشر يتحرك تلقائيًا
    private float targetAutoMoveAngle = -90.0f; // الزاوية المستهدفة في الحركة التلقائية
    public float autoMoveSpeed = 50.0f; // سرعة الحركة التلقائية



    private void Update()
    {
        if (isAutoMoving)
        {
            // تحريك المؤشر تلقائيًا نحو الزاوية المستهدفة
            currentAngle = Mathf.MoveTowards(currentAngle, targetAutoMoveAngle, autoMoveSpeed * Time.deltaTime);
            if (currentAngle == targetAutoMoveAngle)
            {
                isAutoMoving = false;
            }
        }

        needle.eulerAngles = new Vector3(0, 0, currentAngle);
    }

    public void UpdateNeedleAngle(float angleChange)
    {
        if (!isAutoMoving)
        {
            // تطبيق معامل التحكم على التغيير
            currentAngle += angleChange * controlFactor;
            currentAngle = Mathf.Clamp(currentAngle, -180, 0);
        }
    }

    public float GetCurrentAngle()
    {
        return currentAngle;
    }
}
