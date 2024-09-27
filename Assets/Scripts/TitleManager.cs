using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ����
    private InputManager inputManager;
    private bool isTriggerJump;

    // �J�ڃV�[���於
    [SerializeField] private string nextScene;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        GetInput();

        ChangeScene();
    }

    void ChangeScene()
    {
        if (isTriggerJump)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void GetInput()
    {
        isTriggerJump = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerJump = true;
        }
    }
}
