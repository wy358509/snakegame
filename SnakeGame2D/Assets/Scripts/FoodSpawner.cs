using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // 食物预制体
    public int gridSizeX = 10;   // 场景网格的宽度
    public int gridSizeY = 10;   // 场景网格的高度

    void Start()
    {
        SpawnFood(); // 游戏开始时生成第一个食物
    }

    public void SpawnFood()
    {
        // 随机选择一个网格位置
        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);
        Vector2 position = new Vector2(x, y);

        // 实例化食物预制体并放置在选定的位置
        Instantiate(foodPrefab, position, Quaternion.identity);
        Debug.Log("New food spawned at: " + position);
    }
}



