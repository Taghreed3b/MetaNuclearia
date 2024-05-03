using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Transform targetTransform; // الهدف الذي يجب الوصول إليه
    public Text scoreText; // اربط هذا بمكون النص UI في المحرر
    public int pointsToAdd = 1; // عدد النقاط للإضافة في كل تحديث
    public int pointsToSubtract = 1; // عدد النقاط للخصم في كل تحديث
    public float distanceThreshold = 1.0f; // المسافة القصوى لاعتبار الـ GameObject في الهدف
    private static int score = 0; // متغير ثابت لتتبع النقاط الكلية

    public delegate void ScoreChanged();
    public static event ScoreChanged OnScoreChanged;

    private void Start()
    {
        UpdateScoreText();
        OnScoreChanged += UpdateScoreText;
    }

    private void OnDestroy()
    {
        OnScoreChanged -= UpdateScoreText;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);
        if (distance < distanceThreshold)
        {
            AddPoints();
        }
        else
        {
            SubtractPoints();
        }
    }

    private void AddPoints()
    {
        score += pointsToAdd;
        OnScoreChanged?.Invoke();
    }

    private void SubtractPoints()
    {
        score = Mathf.Max(0, score - pointsToSubtract); // تأكد من ألا تصبح النقاط سالبة
        OnScoreChanged?.Invoke();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public static int GetScore()
    {
        return score;
    }
}
