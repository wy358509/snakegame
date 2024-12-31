using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // ʳ��Ԥ����
    public int gridSizeX = 10;   // ��������Ŀ��
    public int gridSizeY = 10;   // ��������ĸ߶�

    void Start()
    {
        SpawnFood(); // ��Ϸ��ʼʱ���ɵ�һ��ʳ��
    }

    public void SpawnFood()
    {
        // ���ѡ��һ������λ��
        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);
        Vector2 position = new Vector2(x, y);

        // ʵ����ʳ��Ԥ���岢������ѡ����λ��
        Instantiate(foodPrefab, position, Quaternion.identity);
        Debug.Log("New food spawned at: " + position);
    }
}



