using UnityEngine;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    public SpriteRenderer fadeSprite; // Assign in Inspector
    public float fadeDuration = 1f;

    void Awake()
    {
        if (fadeSprite == null)
            fadeSprite = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
    }

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, elapsed / fadeDuration));
            yield return null;
        }
        SetAlpha(1f);
    }

    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, elapsed / fadeDuration));
            yield return null;
        }
        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        if (fadeSprite != null)
        {
            var c = fadeSprite.color;
            c.a = alpha;
            fadeSprite.color = c;
        }
    }
}