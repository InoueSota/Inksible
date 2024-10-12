using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // 自コンポーネント取得
    private InputManager inputManager;
    private bool isTriggerDecide;
    private bool isTriggerUp;
    private bool isTriggerDown;

    // 他コンポーネント取得
    private Transition transition;

    // フラグ類
    private bool isActiveSelect;

    // シーン名
    [Header("シーン名")]
    [SerializeField] private string retryString;
    [SerializeField] private string nextStageString;
    [SerializeField] private string stageSelectString;
    private string nextScene;

    // UI
    [Header("UI")]
    [SerializeField] private GameObject inkObj;
    [SerializeField] private Image inkClear1;
    [SerializeField] private Image inkClear2;
    [SerializeField] private Image inkItem1;
    [SerializeField] private Image inkItem2;
    [SerializeField] private GameObject selectObj;
    [SerializeField] private GameObject retryObj;
    [SerializeField] private GameObject nextStageObj;
    [SerializeField] private GameObject stageSelectObj;
    [SerializeField] private GameObject triangleObj;

    // 座標系
    private float triangleY;

    private enum SelectMode
    {
        RETRY,
        NEXTSTAGE,
        STAGESELECT
    }
    private SelectMode selectMode = SelectMode.NEXTSTAGE;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        isActiveSelect = false;
        retryString = GlobalVariables.retryStageName;
        nextScene = GlobalVariables.nextStageName;
        triangleY = nextStageObj.transform.position.y;

        transition = GameObject.FindWithTag("Transition").GetComponent<Transition>();
    }

    void Update()
    {
        GetInput();

        if (!isActiveSelect)
        {
            ClearAnimation();

            if (isTriggerDecide)
            {
                inkObj.SetActive(false);
                selectObj.SetActive(true);
                isActiveSelect = true;
            }
        }
        else
        {
            Select();
        }
    }

    void ClearAnimation()
    {
        if (GlobalVariables.isClear)
        {
            inkClear1.color = GlobalVariables.color1;
            inkClear2.color = GlobalVariables.color2;
        }
        if (GlobalVariables.isGetItem1)
        {
            inkItem1.color = GlobalVariables.color1;
        }
        if (GlobalVariables.isGetItem2)
        {
            inkItem2.color = GlobalVariables.color2;
        }
    }
    void Select()
    {
        switch (selectMode)
        {
            case SelectMode.RETRY:

                if (isTriggerUp)
                {
                    ToStageSelect();
                }
                else if (isTriggerDown)
                {
                    ToNextStage();
                }

                break;
            case SelectMode.NEXTSTAGE:

                if (isTriggerUp)
                {
                    ToRetry();
                }
                else if (isTriggerDown)
                {
                    ToStageSelect();
                }

                break;
            case SelectMode.STAGESELECT:

                if (isTriggerUp)
                {
                    ToNextStage();
                }
                else if (isTriggerDown)
                {
                    ToRetry();
                }

                break;
        }

        if (isTriggerDecide && !transition.GetIsTransitionNow())
        {
            transition.SetTransition(nextScene);
        }
    }
    void ToRetry()
    {
        triangleObj.transform.position = new(triangleObj.transform.position.x, retryObj.transform.position.y, triangleObj.transform.position.z);
        nextScene = retryString;
        selectMode = SelectMode.RETRY;
    }
    void ToNextStage()
    {
        triangleObj.transform.position = new(triangleObj.transform.position.x, nextStageObj.transform.position.y, triangleObj.transform.position.z);
        nextScene = nextStageString;
        selectMode = SelectMode.NEXTSTAGE;
    }
    void ToStageSelect()
    {
        triangleObj.transform.position = new(triangleObj.transform.position.x, stageSelectObj.transform.position.y, triangleObj.transform.position.z);
        nextScene = stageSelectString;
        selectMode = SelectMode.STAGESELECT;
    }

    void GetInput()
    {
        isTriggerDecide = false;
        isTriggerUp = false;
        isTriggerDown = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerDecide = true;
        }
        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.VERTICAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.VERTICAL) > 0)
            {
                isTriggerUp = true;
            }
            else
            {
                isTriggerDown = true;
            }
        }
    }
}
