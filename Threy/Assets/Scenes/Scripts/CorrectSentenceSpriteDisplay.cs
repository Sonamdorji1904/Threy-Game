using System.Collections;
using UnityEngine;

public class CorrectSentenceSpriteDisplay : MonoBehaviour
{
    public Transform sentenceSprite; // Assign your correct answer sprite GameObject
    public Vector3 centerPosition = Vector3.zero; // Center position in world space
    public float moveDuration = 1f;
    public GameObject questionsGroup; // Assign your parent GameObject for all questions/options
    public SceneController sceneController; // <-- Add this
    public FadeTransition fadeTransition; // Assign in Inspector

    private Vector3 startPosition;

    void Awake()
    {
        if (sentenceSprite != null)
            sentenceSprite.gameObject.SetActive(false);
    }

    public void ShowCorrectAnswer()
    {
        if (questionsGroup != null)
            questionsGroup.SetActive(false);

        if (sentenceSprite == null) {
            Debug.LogWarning("sentenceSprite is not assigned!");
            return;
        }

        // Do NOT reset position here; use the position set in the Scene!
        sentenceSprite.gameObject.SetActive(true);
        StartCoroutine(MoveToCenter());
    }

    private IEnumerator MoveToCenter()
    {
        float elapsed = 0f;
        Vector3 initialPos = sentenceSprite.position;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            sentenceSprite.position = Vector3.Lerp(initialPos, centerPosition, elapsed / moveDuration);
            yield return null;
        }
        sentenceSprite.position = centerPosition;

        // Fade out, then switch level
        if (fadeTransition != null)
            yield return StartCoroutine(fadeTransition.FadeOut());

        if (sceneController != null)
            sceneController.ShowNextLevel();

        // Optionally fade in after switching
        if (fadeTransition != null)
            yield return StartCoroutine(fadeTransition.FadeIn());
    }
}