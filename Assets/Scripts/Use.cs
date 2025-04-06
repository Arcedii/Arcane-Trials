using UnityEngine;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    
    public Button myButton;
    public Animator Animation;
    public BoxCollider2D triggerCollider;

    void Start()
    {
        myButton.gameObject.SetActive(false);
        myButton.onClick.AddListener(OnButtonClick);   
    }

    public void OnButtonClick()
    {
        Animation.SetTrigger("Play");
        triggerCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            myButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            myButton.gameObject.SetActive(false);
        }
    }
}
