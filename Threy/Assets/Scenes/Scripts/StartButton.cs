using UnityEngine;

public class StartButton : MonoBehaviour
{
    public SceneController sceneController;

    void OnMouseDown()
    {
        if (sceneController != null)
        {
            sceneController.ShowSkipScene();
        }
    }
}
