using UnityEngine;

public class MainCount : MonoBehaviour
{
    public byte TrainigLevel = 0;

    public GameObject TLStars1;
    public GameObject TLStars2;
    public GameObject TLStars3;

    public GameObject TLStars1Off;
    public GameObject TLStars2Off;
    public GameObject TLStars3Off;

    void Start()
    {
        TrainigLevel = ExitFromLevel.StrarsCount;
        
            
        // ������� ���������� ��� ������ ������
        TLStars1Off.SetActive(true);
        TLStars2Off.SetActive(true);
        TLStars3Off.SetActive(true);

        // �������� ��� "������" ������
        TLStars1.SetActive(false);
        TLStars2.SetActive(false);
        TLStars3.SetActive(false);

        // ���������� ������ � ����������� �� ������
        if (TrainigLevel >= 1)
        {
            TLStars1.SetActive(true);
            TLStars1Off.SetActive(false);
        }
        if (TrainigLevel >= 2)
        {
            TLStars2.SetActive(true);
            TLStars2Off.SetActive(false);
        }
        if (TrainigLevel == 3)
        {
            TLStars3.SetActive(true);
            TLStars3Off.SetActive(false);
        }
    }

}
