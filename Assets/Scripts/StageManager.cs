using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // CSVファイルを読み込む
    [SerializeField] private TextAsset stageCsvFile;
    private List<string[]> stageDatas = new List<string[]>();
    private int[,] stageTable;

    // 他コンポーネント取得
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AllPlayerManager allPlayerManager;
    [SerializeField] private Transform goalTransform;

    private enum BlockType
    {
        NONE,
        NORMAL,
        BLACK,
        PLAYER1,
        PLAYER2,
        DOUBLEPLAYER,
        ITEMCOLOR1,
        ITEMCOLOR2
    }

    // 生成対象
    [Header("生成対象")]
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject blackPrefab;
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private GameObject itemPrefab;

    // 親オブジェクト
    [Header("親オブジェクト")]
    [SerializeField] private Transform normalsTransform;
    [SerializeField] private Transform blacksTransform;
    [SerializeField] private Transform playersTransform;
    [SerializeField] private Transform itemsTransform;

    void Start()
    {
        LoadStageData();
        CreateStage();
    }

    void LoadStageData()
    {
        StringReader reader = new StringReader(stageCsvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            stageDatas.Add(line.Split(','));
        }

        string[] lines = stageCsvFile.text.Split('\n');
        int rows = lines.Length;
        int columns = lines[0].Split(new char[] { ',' }).Length;
        stageTable = new int[rows, columns];
    }
    void CreateStage()
    {
        GameObject player1 = null;
        GameObject player2 = null;

        for (int y = 0; y < stageTable.GetLength(0); y++)
        {
            for (int x = 0; x < stageTable.GetLength(1); x++)
            {
                // 生成座標
                Vector3 createPosition = new(x - (stageTable.GetLength(1) * 0.5f) + 0.5f, -y + (stageTable.GetLength(0) * 0.5f) - 0.5f, 0);

                if (int.Parse(stageDatas[y][x]) == (int)BlockType.NORMAL)
                {
                    GameObject normal = Instantiate(normalPrefab, createPosition, Quaternion.identity);
                    normal.transform.SetParent(normalsTransform, false);
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.BLACK)
                {
                    GameObject black = Instantiate(blackPrefab, createPosition, Quaternion.identity);
                    black.transform.SetParent(blacksTransform, false);
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.PLAYER1)
                {
                    player1 = Instantiate(player1Prefab, createPosition, Quaternion.identity);
                    player1.transform.SetParent(playersTransform, false);
                    player1.GetComponent<PlayerMoveManager>().CreateInitialize(allPlayerManager);
                    player1.GetComponent<PlayerGoalManager>().CreateInitialize(goalTransform);
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.PLAYER2)
                {
                    player2 = Instantiate(player2Prefab, createPosition, Quaternion.identity);
                    player2.transform.SetParent(playersTransform, false);
                    player2.GetComponent<PlayerMoveManager>().CreateInitialize(allPlayerManager);
                    player2.GetComponent<PlayerGoalManager>().CreateInitialize(goalTransform);
                    player2.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.COLOR2);
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.DOUBLEPLAYER)
                {
                    player1 = Instantiate(player1Prefab, createPosition, Quaternion.identity);
                    player2 = Instantiate(player2Prefab, createPosition, Quaternion.identity);
                    player1.transform.SetParent(playersTransform, false);
                    player2.transform.SetParent(playersTransform, false);
                    player1.GetComponent<PlayerMoveManager>().CreateInitialize(allPlayerManager);
                    player2.GetComponent<PlayerMoveManager>().CreateInitialize(allPlayerManager);
                    player1.GetComponent<PlayerGoalManager>().CreateInitialize(goalTransform);
                    player2.GetComponent<PlayerGoalManager>().CreateInitialize(goalTransform);
                    player2.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.COLOR2);
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.ITEMCOLOR1)
                {
                    GameObject item1 = Instantiate(itemPrefab, createPosition, Quaternion.identity);
                    item1.transform.SetParent(itemsTransform, false);
                    item1.GetComponent<ItemManager>().Initialize(ItemManager.ColorType.COLOR1, gameManager.GetColor1());
                }
                else if (int.Parse(stageDatas[y][x]) == (int)BlockType.ITEMCOLOR2)
                {
                    GameObject item2 = Instantiate(itemPrefab, createPosition, Quaternion.identity);
                    item2.transform.SetParent(itemsTransform, false);
                    item2.GetComponent<ItemManager>().Initialize(ItemManager.ColorType.COLOR2, gameManager.GetColor2());
                }
            }
        }

        allPlayerManager.Initialize(player1, player2);
    }
}
