using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    // 入力
    private InputManager inputManager;
    private bool isTriggerJump;
    private bool isTriggerLeft;
    private bool isTriggerRight;

    // シーン名
    [SerializeField] private int stageMax;
    private int stageNumber;
    private string[] stageName;

    // UI
    [Header("UI")]
    [SerializeField] private Text stageNumberText;
    [SerializeField] private GameObject leftTriangleObj;
    [SerializeField] private GameObject rightTriangleObj;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        stageName = new string[stageMax];
        for (int i = 1; i < stageMax + 1; i++)
        {
            stageName[i - 1] = "Stage" + i.ToString();
        }
    }

    void Update()
    {
        GetInput();

        ChangeSelectStage();
        ChangeScene();
        UiManager();
    }

    void ChangeSelectStage()
    {
        // ステージ番号を減算する
        if (isTriggerLeft)
        {
            // すでに最小番号を選択していたら、最大番号にする
            if (stageNumber == 0)
            {
                stageNumber = stageMax - 1;
            }
            else
            {
                stageNumber--;
            }
        }
        // ステージ番号を加算する
        else if (isTriggerRight)
        {
            // すでに最大番号を選択していたら、最小番号にする
            if (stageNumber == stageMax - 1)
            {
                stageNumber = 0;
            }
            else
            {
                stageNumber++;
            }
        }
    }
    void ChangeScene()
    {
        if (isTriggerJump)
        {
            SceneManager.LoadScene(stageName[stageNumber]);
        }
    }
    void UiManager()
    {
        // 選択レベル番号数字の処理
        int addOneStageNumber = stageNumber + 1;
        stageNumberText.text = addOneStageNumber.ToString();

        // レベル選択三角形の処理
        if (stageNumber == 0)
        {
            if (leftTriangleObj.activeSelf)
            {
                leftTriangleObj.SetActive(false);
            }
            if (!rightTriangleObj.activeSelf)
            {
                rightTriangleObj.SetActive(true);
            }
        }
        else if (stageNumber == stageMax - 1)
        {
            if (!leftTriangleObj.activeSelf)
            {
                leftTriangleObj.SetActive(true);
            }
            if (rightTriangleObj.activeSelf)
            {
                rightTriangleObj.SetActive(false);
            }
        }
        else
        {
            if (!leftTriangleObj.activeSelf)
            {
                leftTriangleObj.SetActive(true);
            }
            if (!rightTriangleObj.activeSelf)
            {
                rightTriangleObj.SetActive(true);
            }
        }
    }

    void GetInput()
    {
        isTriggerJump = false;
        isTriggerLeft = false;
        isTriggerRight = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerJump = true;
        }
        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.HORIZONTAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.HORIZONTAL) < 0f)
            {
                isTriggerLeft = true;
            }
            else
            {
                isTriggerRight = true;
            }
        }
    }
}
