using UnityEngine;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [Header("Correct Answer")]
    public GameObject correctAnswerObject; // Assign the correct draggable word GameObject

    [Header("Sounds")]
    public AudioSource audioSource; // Assign a persistent AudioSource in the Inspector
    public AudioClip correctClip;
    public AudioClip wrongClip;

    [Header("Scene Control")]
    public SceneController sceneController; // Assign in Inspector

    [Header("Snap Offset")]
    public Vector3 snapOffset = Vector3.zero; // Set in Inspector for fine-tuning

    // This method should be called when a word is dropped onto a blank
    public void CheckAnswer(GameObject wordObject, GameObject blankObject)
    {
        if (wordObject == correctAnswerObject)
        {
            // Play correct sound and move to next level after sound
            StartCoroutine(CorrectAnswerRoutine(wordObject, blankObject));
        }
        else
        {
            // Play wrong sound
            if (audioSource && wrongClip)
                audioSource.PlayOneShot(wrongClip);

            // Reset the word to its original position
            var drag = wordObject.GetComponent<DraggableWord>();
            if (drag) drag.ResetPosition();
        }
    }

    private IEnumerator CorrectAnswerRoutine(GameObject wordObject, GameObject blankObject)
    {
        if (audioSource && correctClip)
            audioSource.PlayOneShot(correctClip);

        var blankSprite = blankObject.GetComponent<SpriteRenderer>();
        var wordSprite = wordObject.GetComponent<SpriteRenderer>();
        if (blankSprite != null && wordSprite != null)
        {
            Vector3 blankCenter = blankSprite.bounds.center;
            wordObject.transform.position = blankCenter + snapOffset;

            // Resize word to fit blank
            Vector3 blankSize = blankSprite.bounds.size;
            Vector3 wordSize = wordSprite.bounds.size;
            if (wordSize.x > 0 && wordSize.y > 0)
            {
                Vector3 scale = wordObject.transform.localScale;
                scale.x *= blankSize.x / wordSize.x;
                scale.y *= blankSize.y / wordSize.y;
                wordObject.transform.localScale = scale;
            }
        }
        else if (blankSprite != null)
        {
            wordObject.transform.position = blankSprite.bounds.center + snapOffset;
        }
        else
        {
            wordObject.transform.position = blankObject.transform.position + snapOffset;
        }

        // Optionally, disable dragging so it can't be moved again
        var drag = wordObject.GetComponent<DraggableWord>();
        if (drag) drag.enabled = false;

        // Wait for the correct sound to finish
        if (correctClip != null)
            yield return new WaitForSeconds(correctClip.length);

        // Move to next level (automatically)
        if (sceneController != null)
            sceneController.ShowNextLevel();
    }
}