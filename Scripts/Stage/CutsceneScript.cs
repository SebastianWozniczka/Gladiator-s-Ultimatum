using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    public string sceneName;
    public const float TIME_LIMIT = 5F;
    private float timer = 0F;

    void Start()
    {

    }

    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= TIME_LIMIT)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
