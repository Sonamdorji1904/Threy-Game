using UnityEngine;

public class FadeInSprite : MonoBehaviour
{
    public float delay = 1f;          // How long to wait before starting fade
    public float fadeDuration = 2f;   // How long the fade takes
    private SpriteRenderer sr;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();

        // Start with sprite invisible
        Color c = sr.color;
        c.a = 0f;
        sr.color = c;

        // Start fade after a delay
        Invoke(nameof(StartFade), delay);
    }

    void StartFade()
    {
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            Color c = sr.color;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            sr.color = c;

            yield return null;
        }
    }
}
