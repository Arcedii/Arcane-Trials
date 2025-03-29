using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector cutscene; // Перетяните сюда вашу Timeline
    public GameObject uiElements; // UI, который можно скрыть на время кат-сцены

    void Awake()
    {
        if (cutscene != null)
        {
            cutscene.stopped += OnCutsceneEnd;
            PlayCutscene();
        }
    }

    void PlayCutscene()
    {
        if (uiElements != null)
            uiElements.SetActive(false); // Отключаем UI

        cutscene.Play();
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (uiElements != null)
            uiElements.SetActive(true); // Включаем UI обратно

    }
}