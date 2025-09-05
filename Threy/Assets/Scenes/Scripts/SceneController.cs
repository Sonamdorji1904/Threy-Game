using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Scene Groups")]
    public GameObject mainSceneObjects;
    public GameObject skipSceneObjects;
    public GameObject[] levelScenes; // Array for all your level GameObjects

    private int currentLevel = 0;

    void Start()
    {
        ShowMainScene();
    }

    public void ShowMainScene()
    {
        mainSceneObjects.SetActive(true);
        skipSceneObjects.SetActive(false);
        HideAllLevels();
    }

    public void ShowSkipScene()
    {
        mainSceneObjects.SetActive(false);
        skipSceneObjects.SetActive(true);
        HideAllLevels();
    }

    public void ShowLevel(int levelIndex)
    {
        mainSceneObjects.SetActive(false);
        skipSceneObjects.SetActive(false);
        HideAllLevels();
        if (levelIndex >= 0 && levelIndex < levelScenes.Length)
        {
            levelScenes[levelIndex].SetActive(true);
            currentLevel = levelIndex;
        }
    }

    public void ShowNextLevel()
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel < levelScenes.Length)
        {
            ShowLevel(nextLevel);
        }
        else
        {
            Debug.Log("No more levels!");
            // Optionally, show a completion screen or loop back
        }
    }

    private void HideAllLevels()
    {
        foreach (var level in levelScenes)
        {
            if (level != null) level.SetActive(false);
        }
    }
}
