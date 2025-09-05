using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public GameObject hintPanel;   // Assign your sprite-based hint GameObject here
    public GameObject hintButton;  // Assign your sprite-based button GameObject here

    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color pressedColor = Color.gray;

    private SpriteRenderer buttonRenderer;
    private bool isHovering = false;

    private bool hintOpen = false;

    void Start()
    {
        if (hintPanel != null)
            hintPanel.SetActive(false);

        if (hintButton != null)
            buttonRenderer = hintButton.GetComponent<SpriteRenderer>();
        SetButtonColor(normalColor);
    }

    void SetButtonColor(Color color)
    {
        if (buttonRenderer != null)
            buttonRenderer.color = color;
    }

    public void ShowHint()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(true);
            hintOpen = true;
        }
    }

    public void HideHint()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
            hintOpen = false;
        }
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Hover effect
        if (!hintOpen && hintButton != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            bool hovering = hit.collider != null && hit.collider.gameObject == hintButton;

            if (hovering && !isHovering)
            {
                SetButtonColor(hoverColor);
                isHovering = true;
            }
            else if (!hovering && isHovering)
            {
                SetButtonColor(normalColor);
                isHovering = false;
            }
        }
        else if (!isHovering && buttonRenderer != null)
        {
            SetButtonColor(normalColor);
        }

        // Press effect and click logic
        if (Input.GetMouseButtonDown(0))
        {
            if (!hintOpen)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == hintButton)
                {
                    SetButtonColor(pressedColor);
                    ShowHint();
                }
            }
            else
            {
                HideHint();
            }
        }
        // Reset to hover color after press
        if (Input.GetMouseButtonUp(0) && isHovering)
        {
            SetButtonColor(hoverColor);
        }
    }
}