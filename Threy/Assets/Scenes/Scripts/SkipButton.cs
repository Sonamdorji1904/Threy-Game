using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public SceneController sceneController;

    void OnMouseDown()
    {
        if (sceneController != null)
        {
            sceneController.ShowLevel(0); // Go to Level 1 (index 0)
        }
    }
}
