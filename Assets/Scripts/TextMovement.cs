using TMPro;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;      // Reference to the TextMeshProUGUI component
    public float speed = 0.1f;              // Movement speed
    public float movementRange = 0.5f;      // Total movement range (up and down)
    public bool moveUpwards = true;         // Initial movement direction (up or down)

    private Vector2 startingPosition;     // Initial anchored position of the text

    void Start()
    {
        startingPosition = textMeshPro.rectTransform.anchoredPosition;
    }

    void Update()
    {
        float targetY;

        // Determine target position based on starting position, movement direction, and range
        if (moveUpwards)
        {
            targetY = startingPosition.y + movementRange / 2;
        }
        else
        {
            targetY = startingPosition.y - movementRange / 2;
        }

        // Move towards the target position in the Y direction using RectTransform
        textMeshPro.rectTransform.anchoredPosition = new Vector2(
            textMeshPro.rectTransform.anchoredPosition.x,
            Mathf.MoveTowards(textMeshPro.rectTransform.anchoredPosition.y, targetY, speed * Time.deltaTime)
        );

        // Reverse direction at boundaries based on the range
        if (Mathf.Abs(textMeshPro.rectTransform.anchoredPosition.y - targetY) < 0.01f)
        {
            moveUpwards = !moveUpwards;
        }
    }
}
