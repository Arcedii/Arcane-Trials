using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFromLevel : MonoBehaviour
{
    public static byte StrarsCount = 0;
    public byte sceneToLoad; // �������� ����� ��� ��������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �������� ���� ������
        {
            StrarsCount = StarsController.StarsCount;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
