using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 入力
    private InputManager inputManager;
    private bool isTriggerReset;

    // フラグ類
    private bool isStart;
    private bool isClear;

    // 時間
    private float readyTimer;

    // 名前
    [Header("名前")]
    [SerializeField] private string thisStageName;
    [SerializeField] private string nextStageName;

    // 色
    [Header("色")]
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    // UI
    [Header("UI")]
    [SerializeField] private Image inkColor1;
    [SerializeField] private Image inkColor2;
    [SerializeField] private GameObject groupClearObj;
    [SerializeField] private Image[] clearInkColor1;
    [SerializeField] private Image[] clearInkColor2;

    // プレイヤー
    [Header("プレイヤー")]
    [SerializeField] private AllPlayerManager allPlayerManager;
    [SerializeField] private SoulManager soulManager;

    // ゴール
    [Header("ゴール")]
    [SerializeField] private SpriteRenderer color1SquareSpriteRenderer;
    [SerializeField] private SpriteRenderer color2SquareSpriteRenderer;
    [SerializeField] private ChangeLineManager changeLineManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        readyTimer = 2f;

        inkColor1.color = color1;
        inkColor2.color = color2;

        // 名前代入
        GlobalVariables.retryStageName = thisStageName;
        GlobalVariables.nextStageName = nextStageName;

        // 色代入
        GlobalVariables.color1 = color1;
        GlobalVariables.color2 = color2;

        // グローバル変数の初期化
        GlobalVariables.isClear = false;
        GlobalVariables.isGetItem1 = false;
        GlobalVariables.isGetItem2 = false;
    }

    void Update()
    {
        GetInput();

        Ready();
        CheckGoal();

        Restart();
    }

    void Ready()
    {
        if (!isStart)
        {
            readyTimer -= Time.deltaTime;
            if (readyTimer <= 0f)
            {
                allPlayerManager.StartInitialize();
                isStart = true;
            }
        }
    }
    void CheckGoal()
    {
        // どちらか片方でもゴールしているか
        if (allPlayerManager.GetIsPlayerGoal(1) || allPlayerManager.GetIsPlayerGoal(2))
        {
            if (allPlayerManager.GetIsPlayerGoal(1))
            {
                if (changeLineManager)
                {
                    changeLineManager.SetStart(ChangeLineManager.ColorMode.COLOR1, color1);
                }
                color1SquareSpriteRenderer.color = color1;
            }
            if (allPlayerManager.GetIsPlayerGoal(2))
            {
                if (changeLineManager)
                {
                    changeLineManager.SetStart(ChangeLineManager.ColorMode.COLOR2, color2);
                }
                color2SquareSpriteRenderer.color = color2;
            }

            // どちらもゴールしているか
            if (!isClear && allPlayerManager.GetIsPlayerGoal(1) && allPlayerManager.GetIsPlayerGoal(2))
            {
                for (int i = 0; i < clearInkColor1.Length; i++)
                {
                    clearInkColor1[i].color = color1;
                }
                for (int i = 0; i < clearInkColor2.Length; i++)
                {
                    clearInkColor2[i].color = color2;
                }
                groupClearObj.SetActive(true);

                GlobalVariables.isClear = true;
                isClear = true;
            }
        }
    }
    void Restart()
    {
        if (isTriggerReset && isStart && !isClear)
        {
            // プレイヤー初期化
            allPlayerManager.PlayersInitialize();
            soulManager.Initialize();

            // ブロック初期化
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.COLOR1 ||
                    block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.COLOR2)
                {
                    block.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.WHITE);
                    block.GetComponent<ObjectColorManager>().SetIsActive(true);
                }
            }

            // アイテム初期化
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            {
                item.GetComponent<ItemManager>().SetIsActive(true);
            }

            // ゴール初期化
            changeLineManager.gameObject.SetActive(true);
            changeLineManager.Initialize();

            color1SquareSpriteRenderer.color = Color.white;
            color2SquareSpriteRenderer.color = Color.white;

            // グローバル変数の初期化
            GlobalVariables.isClear = false;
            GlobalVariables.isGetItem1 = false;
            GlobalVariables.isGetItem2 = false;
        }
    }

    public void FinishChangeLine()
    {
        allPlayerManager.ChangePlayerActive();
    }

    void GetInput()
    {
        isTriggerReset = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.RESET))
        {
            isTriggerReset = true;
        }
    }
    public Color GetColor1()
    {
        return color1;
    }
    public Color GetColor2()
    {
        return color2;
    }
}
