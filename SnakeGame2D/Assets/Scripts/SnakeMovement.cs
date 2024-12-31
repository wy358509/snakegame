using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 direction = Vector2.right;
    public List<Transform> segments = new List<Transform>();
    public GameObject snakeSegmentPrefab;
    public Button restartButton; // 新增对重新玩按钮的引用
    public FoodSpawner foodSpawner; // 引用 FoodSpawner
    public Text scoreText; // 新增对用于显示积分的Text组件的引用
    private int score = 0; // 新增用于记录积分的变量
    const float segmentOffsetDistance = 0.3f; // 将常量定义在类的顶层，方便在类的方法中访问

    void Start()
    {
        Debug.Log("Start");
        segments.Add(this.transform);
        this.tag = "Head"; // 明确设置蛇头的标签
        Move(); // 手动调用一次 Move 方法以初始化位置
        InvokeRepeating("Move", 0.2f, 0.2f);
        if (restartButton != null)
        {
            Debug.Log("START =NULL");
            restartButton.gameObject.SetActive(false); // 初始隐藏按钮
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

    //更新蛇的每个段的位置
    void Move()
    {
        Debug.Log("Move");
        // 从尾部到头部更新每个段的位置
        for (int i = segments.Count - 1; i > 0; i--)
        {
            Debug.Log($"Move segments.Count Size: {i}");
            segments[i].position = segments[i - 1].position;
        }

        // 更新蛇头的位置
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
        // 优化位置计算逻辑，更灵活地根据蛇身情况确定新节段位置
        if (segments.Count > 0)
        {
            Vector3 prevSegmentPos = segments[segments.Count - 1].position;
            Vector3 newSegmentPos = prevSegmentPos - (Vector3)direction * segmentOffsetDistance;
            segment.position = newSegmentPos;
            Debug.Log("old position: " + segments[segments.Count - 1].position);
        }
        else
        {
            // 若还没有蛇身节段（理论上较少出现这种情况，但增加健壮性），默认放置在原点等合理位置
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
                foodSpawner.SpawnFood(); // 调用 FoodSpawner 生成新的食物
            }
            score++; // 吃到食物时积分加1
            scoreText.text = "Score: " + score; // 更新Text组件显示的积分内容
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("Game Over Wall");
            Time.timeScale = 0; // Pause the game
            if (restartButton != null)
            {
                Debug.Log("WALL !=NULL");
                restartButton.gameObject.SetActive(true); // 游戏失败时显示按钮
            }
        }
        else if(other.CompareTag("Tail"))
        { 
            Debug.Log("Game Over Tail");
            Time.timeScale = 0; // Pause the game
            if (restartButton != null)
            {
                Debug.Log("TAIL !=NULL");
                restartButton.gameObject.SetActive(true); // 游戏失败时显示按钮
            }
        }
    }

}



