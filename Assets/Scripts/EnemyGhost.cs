using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    public float patrolDistance = 5f;  // ���������� ��������������
    public float speed = 2f;           // �������� ��������

    private Vector2 startPos;
    private bool movingRight = true;
    private Vector3 originalScale;

    void Start()
    {
        startPos = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= startPos.x + patrolDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= startPos.x - patrolDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1; // ����������� �� ��� X
        transform.localScale = flipped;
    }
}
