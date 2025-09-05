using UnityEngine;

public class DraggableWord : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;
    private QuizManager quizManager;

    void Start()
    {
        startPosition = transform.position;
        quizManager = FindObjectOfType<QuizManager>();
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if dropped on blank
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null && hit.CompareTag("Blank"))
        {
            quizManager.CheckAnswer(gameObject, hit.gameObject);

            // Disable dragging after correct drop
            if (!enabled) return; // Already disabled by QuizManager
        }
        else
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}
