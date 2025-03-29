using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector cutscene; // ���������� ���� ���� Timeline
    public GameObject uiElements; // UI, ������� ����� ������ �� ����� ���-�����

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
            uiElements.SetActive(false); // ��������� UI

        cutscene.Play();
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (uiElements != null)
            uiElements.SetActive(true); // �������� UI �������

    }
}