using UnityEngine;

public class StarsController : MonoBehaviour
{
    public static byte StarsCount = 0;

    public GameObject StarOff1;
    public GameObject StarOff2;
    public GameObject StarOff3;

    public GameObject StarOn1;
    public GameObject StarOn2;
    public GameObject StarOn3;

    private void Start()
    {
        StarOff1.gameObject.SetActive(true);
        StarOff2.gameObject.SetActive(true);
        StarOff3.gameObject.SetActive(true);

        StarOn1.gameObject.SetActive(false);
        StarOn2.gameObject.SetActive(false);
        StarOn3.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (StarsCount == 1)
        {
            StarOff1.gameObject.SetActive(false);
            StarOn1.gameObject.SetActive(true);
        }
        else if (StarsCount == 2)
        {
            StarOff2.gameObject.SetActive(false);
            StarOn2.gameObject.SetActive(true);
        } else if (StarsCount == 3)
        {
            StarOff3.gameObject.SetActive(false);
            StarOn3.gameObject.SetActive(true);
        }
    }

}
