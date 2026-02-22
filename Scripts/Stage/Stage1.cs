using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clip;
    public Button buttonSettings,buttonNewGame;
    void Start()
    {
        buttonSettings.onClick.AddListener(StartAudio);
        buttonNewGame.onClick.AddListener(StartGameplay);
    }

    void StartAudio()
    {
        audioSource.Play();
    }

    void StartGameplay()
    {
        SceneManager.LoadScene("GameplayScene");
    }


    void Update()
    {
        
    }
}
