using UnityEngine;
using UnityEngine.SceneManagement;

public class AllSceneManager : MonoBehaviour
{
    // 入力管理オブジェクト
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        Scene();
    }

    void Scene()
    {
        // 色変え
        if (GlobalVariables.colorNum > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            GlobalVariables.colorNum = 0;
        }
        else if (GlobalVariables.colorNum < 1 && Input.GetKeyDown(KeyCode.W))
        {
            GlobalVariables.colorNum++;
        }

        // Pキーのみシーンのリセットを行う
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // ウィンドウを閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
