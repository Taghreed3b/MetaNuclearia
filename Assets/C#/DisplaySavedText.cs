using UnityEngine;
using TMPro; // تأكد من استيراد هذا إذا كنت تستخدم TextMeshPro

public class DisplaySavedText : MonoBehaviour
{
    public TMP_Text displayText; // يمكنك ربط هذا بعنصر Text أو TMP_Text في محرر Unity

    void Start()
    {
        // تحقق مما إذا كان هناك نص محفوظ في PlayerPrefs
        if (PlayerPrefs.HasKey("SavedText"))
        {
            // إذا كان هناك نص محفوظ، استخدمه
            displayText.text = PlayerPrefs.GetString("SavedText");
        }
        else
        {
            // إذا لم يكن هناك نص محفوظ، يمكنك عرض رسالة افتراضية
            displayText.text = "Guest";
        }
    }
}
