using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    
    public void SceneLoader(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
