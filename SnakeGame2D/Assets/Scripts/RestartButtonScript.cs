using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("RestartGame");
        // 重置时间缩放
        Time.timeScale = 1;
        // 重新加载当前场景，实现重新开始游戏的效果
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}