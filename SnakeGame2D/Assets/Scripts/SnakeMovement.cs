using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 direction = Vector2.right;
    public List<Transform> segments = new List<Transform>();
    public GameObject snakeSegmentPrefab;
    public Button restartButton; // �����������水ť������
    public FoodSpawner foodSpawner; // ���� FoodSpawner
    public Text scoreText; // ������������ʾ���ֵ�Text���������
    private int score = 0; // �������ڼ�¼���ֵı���
    const float segmentOffsetDistance = 0.3f; // ��������������Ķ��㣬��������ķ����з���

    void Start()
    {
        Debug.Log("Start");
        segments.Add(this.transform);
        this.tag = "Head"; // ��ȷ������ͷ�ı�ǩ
        Move(); // �ֶ�����һ�� Move �����Գ�ʼ��λ��
        InvokeRepeating("Move", 0.2f, 0.2f);
        if (restartButton != null)
        {
            Debug.Log("START =NULL");
            restartButton.gameObject.SetActive(false); // ��ʼ���ذ�ť
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            ChangeDirection(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S))
            ChangeDirection(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A))
            ChangeDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D))
            ChangeDirection(Vector2.right);
    }

    //�����ߵ�ÿ���ε�λ��
    void Move()
    {
        Debug.Log("Move");
        // ��β����ͷ������ÿ���ε�λ��
        for (int i = segments.Count - 1; i > 0; i--)
        {
            Debug.Log($"Move segments.Count Size: {i}");
            segments[i].position = segments[i - 1].position;
        }

        // ������ͷ��λ��
        transform.position += (Vector3)(direction * speed);
        Debug.Log("Snake head position: " + transform.position);
    }

    void ChangeDirection(Vector2 newDirection)
    {
        Debug.Log("ChangeDirection");
        if (newDirection == -direction)
            return;
        direction = newDirection;
    }

    void Grow()
    {
        Debug.Log("Grow");
        Transform segment = Instantiate(snakeSegmentPrefab).transform;
        Debug.Log("count: " + segments.Count);
        // �Ż�λ�ü����߼��������ظ����������ȷ���½ڶ�λ��
        if (segments.Count > 0)
        {
            Vector3 prevSegmentPos = segments[segments.Count - 1].position;
            Vector3 newSegmentPos = prevSegmentPos - (Vector3)direction * segmentOffsetDistance;
            segment.position = newSegmentPos;
            Debug.Log("old position: " + segments[segments.Count - 1].position);
        }
        else
        {
            // ����û������ڶΣ������Ͻ��ٳ�����������������ӽ�׳�ԣ���Ĭ�Ϸ�����ԭ��Ⱥ���λ��
            segment.position = Vector3.zero;
        }
        segments.Add(segment);
        Debug.Log("segments1" + segments[1].position);
        segment.tag = "Tail";
        Debug.Log("New segment added. Total segments: " + segments.Count);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D with " + other.name + " tagged as " + other.tag);
        Debug.Log("Other object position: " + other.transform.position);
        if (other.CompareTag("Food"))
        {
            Debug.Log("Get Food");
            Destroy(other.gameObject);
            Grow();
            if (foodSpawner != null)
            {
                foodSpawner.SpawnFood(); // ���� FoodSpawner �����µ�ʳ��
            }
            score++; // �Ե�ʳ��ʱ���ּ�1
            scoreText.text = "Score: " + score; // ����Text�����ʾ�Ļ�������
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("Game Over Wall");
            Time.timeScale = 0; // Pause the game
            if (restartButton != null)
            {
                Debug.Log("WALL !=NULL");
                restartButton.gameObject.SetActive(true); // ��Ϸʧ��ʱ��ʾ��ť
            }
        }
        else if(other.CompareTag("Tail"))
        { 
            Debug.Log("Game Over Tail");
            Time.timeScale = 0; // Pause the game
            if (restartButton != null)
            {
                Debug.Log("TAIL !=NULL");
                restartButton.gameObject.SetActive(true); // ��Ϸʧ��ʱ��ʾ��ť
            }
        }
    }

}



