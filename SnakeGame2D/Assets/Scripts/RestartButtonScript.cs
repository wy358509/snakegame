using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("RestartGame");
        // ����ʱ������
        Time.timeScale = 1;
        // ���¼��ص�ǰ������ʵ�����¿�ʼ��Ϸ��Ч��
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}