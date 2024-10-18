using UnityEngine;
using UnityEngine.SceneManagement;

public class AllSceneManager : MonoBehaviour
{
    // ���͊Ǘ��I�u�W�F�N�g
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
        // �F�ς�
        if (GlobalVariables.colorNum > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            GlobalVariables.colorNum = 0;
        }
        else if (GlobalVariables.colorNum < 1 && Input.GetKeyDown(KeyCode.W))
        {
            GlobalVariables.colorNum++;
        }

        // P�L�[�̂݃V�[���̃��Z�b�g���s��
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // �E�B���h�E�����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
