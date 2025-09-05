using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
}

public class SpriteFadeTransition : MonoBehaviour
{
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public float fadeDuration = 2f;

    private float timer = 0f;
    private bool fading = false;

    public void StartFade()
    {
        timer = 0f;
        fading = true;
    }

    void Update()
    {
        if (!fading) return;

        timer += Time.deltaTime;
        float alpha = Mathf.Clamp01(timer / fadeDuration);

        Color color1 = sprite1.color;
        Color color2 = sprite2.color;

        color1.a = alpha;
        color2.a = alpha;

        sprite1.color = color1;
        sprite2.color = color2;

        if (timer >= fadeDuration)
        {
            fading = false;
        }
    }
}
