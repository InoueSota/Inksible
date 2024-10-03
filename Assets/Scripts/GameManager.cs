using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private AllPlayerManager allPlayerManager;
    private MenuManager menuManager;
    private InputManager inputManager;
    private bool isTriggerReset;

    // ���R���|�[�l���g�擾
    private ColorData colorData;

    // �t���O��
    private bool isStart;
    private bool isClear;

    // ����
    private float readyTimer;

    // ���O
    [Header("���O")]
    [SerializeField] private string thisStageName;
    [SerializeField] private string nextStageName;

    // �F
    [Header("�F")]
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    // UI
    [Header("UI")]
    [SerializeField] private GameObject groupClearObj;
    [SerializeField] private Image[] clearInkColor1;
    [SerializeField] private Image[] clearInkColor2;

    // �v���C���[
    [Header("�v���C���[")]
    [SerializeField] private SoulManager soulManager;

    // �S�[��
    [Header("�S�[��")]
    [SerializeField] private SpriteRenderer color1SquareSpriteRenderer;
    [SerializeField] private SpriteRenderer color2SquareSpriteRenderer;
    [SerializeField] private ChangeLineManager changeLineManager;

    void Awake()
    {
        // �F�f�[�^�̎擾
        colorData = new ColorData();
        colorData = colorData.LoadColorData();

        // �F���
        color1 = colorData.GetMainColor(colorData, GlobalVariables.colorNum);
        color2 = colorData.GetSubColor(colorData, GlobalVariables.colorNum);
        GlobalVariables.color1 = color1;
        GlobalVariables.color2 = color2;
    }
    void Start()
    {
        allPlayerManager = GetComponent<AllPlayerManager>();
        menuManager = GetComponent<MenuManager>();
        inputManager = GetComponent<InputManager>();

        readyTimer = 2f;

        // ���O���
        GlobalVariables.retryStageName = thisStageName;
        GlobalVariables.nextStageName = nextStageName;

        // �O���[�o���ϐ��̏�����
        GlobalVariables.isClear = false;
        GlobalVariables.isGetItem1 = false;
        GlobalVariables.isGetItem2 = false;
    }

    void Update()
    {
        GetInput();

        Menu();
        Ready();
        CheckGoal();
    }

    void Menu()
    {
        // ���j���[���J�� / ����
        if (isTriggerReset && !isClear)
        {
            allPlayerManager.SetIsActive(menuManager.GetIsMenuActive());
            menuManager.SetIsMenuActive();
        }
    }
    void Ready()
    {
        if (!isStart)
        {
            readyTimer -= Time.deltaTime;
            if (readyTimer <= 0f)
            {
                allPlayerManager.StartInitialize(!menuManager.GetIsMenuActive());
                isStart = true;
            }
        }
    }
    void CheckGoal()
    {
        // �ǂ��炩�Е��ł��S�[�����Ă��邩
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

            // �ǂ�����S�[�����Ă��邩
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
    public void Restart()
    {
        // �v���C���[������
        allPlayerManager.PlayersInitialize();
        soulManager.Initialize();

        // �u���b�N������
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.COLOR1 ||
                block.GetComponent<AllObjectManager>().GetBlockType() == AllObjectManager.BlockType.COLOR2)
            {
                block.GetComponent<AllObjectManager>().SetBlockType(AllObjectManager.BlockType.WHITE);
                block.GetComponent<ObjectColorManager>().SetIsActive(true);
            }
        }

        // �A�C�e��������
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            item.GetComponent<ItemManager>().SetIsActive(true);
        }

        // �S�[��������
        changeLineManager.gameObject.SetActive(true);
        changeLineManager.Initialize();

        color1SquareSpriteRenderer.color = Color.white;
        color2SquareSpriteRenderer.color = Color.white;

        // �O���[�o���ϐ��̏�����
        GlobalVariables.isClear = false;
        GlobalVariables.isGetItem1 = false;
        GlobalVariables.isGetItem2 = false;
    }

    public void FinishChangeLine()
    {
        allPlayerManager.ChangePlayerActive();
    }

    // Setter
    public void SetPlayerAcitve(bool _isActive)
    {
        allPlayerManager.SetIsActive(_isActive);
    }

    // Getter
    void GetInput()
    {
        isTriggerReset = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.RESET))
        {
            isTriggerReset = true;
        }
    }
    public Color GetColor(int _num)
    {
        if (_num == 1)
        {
            return color1;
        }
        else if (_num == 2)
        {
            return color2;
        }
        return color1;
    }
}
