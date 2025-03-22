using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsMenuController : MonoBehaviour
{
    public Button StartBut;
    public Button Return;
    public Button OpenSettingsBut;
    public Button CloseSettingsBut;

    public GameObject MainMenu;
    public GameObject LevelMenu;
    public GameObject SettingsMenu;

    private void Start()
    {
        MainMenu.SetActive(true);
        LevelMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        StartBut.onClick.AddListener(StartGame);
        OpenSettingsBut.onClick.AddListener(OpenSettings);
        CloseSettingsBut.onClick.AddListener(CloseSettings);
        Return.onClick.AddListener(ReturnToMainMenu);
    }
    public void SceneLoader(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        LevelMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        LevelMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
