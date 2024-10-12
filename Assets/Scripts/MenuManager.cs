using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // 自コンポーネント取得
    private GameManager gameManager;
    private InputManager inputManager;
    private bool isTriggerUp;
    private bool isTriggerDown;
    private bool isTriggerJump;

    // 他コンポーネント取得
    private Transition transition;

    public enum MenuType
    {
        RETURN,
        RESTART,
        STAGESELECT
    }
    private MenuType menuType = MenuType.RETURN;

    // メニュー画面のアクティブフラグ
    private bool isMenuActive;

    [Header("初手専用")]
    [SerializeField] private float intervalTime;
    private float intervalTimer;
    private bool isFinishStart;

    [Header("メニュー画面背景")]
    [SerializeField] private Image menuBackImage;
    [SerializeField] private Color darkColor;
    private Color menuBackOriginColor;
    private Color menuBackTargetColor;

    [Header("メニュータブ")]
    [SerializeField] private RectTransform menuTabRect;
    private Vector3 menuTabOrigin;
    private Vector3 menuTabTarget;

    [Header("メニュー内のコンテンツ")]
    [SerializeField] private float menuContentsDiff;
    [SerializeField] private float chasePower;
    [SerializeField] private RectTransform returnRect;
    private Vector3 returnRectOrigin;
    private Vector3 returnRectTarget;
    [SerializeField] private RectTransform restartRect;
    private Vector3 restartRectOrigin;
    private Vector3 restartRectTarget;
    [SerializeField] private RectTransform stageSelectRect;
    private Vector3 stageSelectRectOrigin;
    private Vector3 stageSelectRectTarget;

    [Header("ステージ番号")]
    [SerializeField] private RectTransform stageNumberRect;
    [SerializeField] private Text stageNumber;
    private Vector3 stageNumberOrigin;
    private Vector3 stageNumberTarget;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        inputManager = GetComponent<InputManager>();

        intervalTimer = intervalTime;
        isFinishStart = false;

        menuBackOriginColor = new(darkColor.r, darkColor.g, darkColor.b, 0f);
        menuBackTargetColor = menuBackOriginColor;
        menuBackImage.color = menuBackTargetColor;

        menuTabOrigin = menuTabRect.localPosition;
        returnRectOrigin = returnRect.localPosition;
        restartRectOrigin = restartRect.localPosition;
        stageSelectRectOrigin = stageSelectRect.localPosition;

        menuTabTarget = menuTabOrigin;
        returnRectTarget = returnRectOrigin;
        restartRectTarget = restartRectOrigin;
        stageSelectRectTarget = stageSelectRectOrigin;

        stageNumberOrigin = stageNumberRect.localPosition;

        stageNumberTarget = stageNumberOrigin;

        stageNumber.text = GlobalVariables.retryStageName;

        transition = GameObject.FindWithTag("Transition").GetComponent<Transition>();
    }

    void Update()
    {
        GetInput();

        StartAnimation();
        if (isMenuActive)
        {
            ChangeMenuType();
        }
        Menu();
    }
    void StartAnimation()
    {
        if (!isFinishStart)
        {
            intervalTimer -= Time.deltaTime;
            if (intervalTimer <= 0f)
            {
                stageNumberTarget.x = -815f;
                isFinishStart = true;
            }
        }
    }
    void ChangeMenuType()
    {
        switch (menuType)
        {
            case MenuType.RETURN:

                returnRectTarget.x = returnRectOrigin.x - menuContentsDiff;
                restartRectTarget.x = restartRectOrigin.x;
                stageSelectRectTarget.x = stageSelectRectOrigin.x;

                if (isTriggerDown)
                {
                    menuType = MenuType.RESTART;
                }

                if (isTriggerJump)
                {
                    gameManager.SetPlayerAcitve(isMenuActive);
                    SetIsMenuActive();
                }

                break;
            case MenuType.RESTART:

                returnRectTarget.x = returnRectOrigin.x;
                restartRectTarget.x = restartRectOrigin.x - menuContentsDiff;
                stageSelectRectTarget.x = stageSelectRectOrigin.x;

                if (isTriggerUp)
                {
                    menuType = MenuType.RETURN;
                }
                else if (isTriggerDown)
                {
                    menuType = MenuType.STAGESELECT;
                }

                if (isTriggerJump)
                {
                    gameManager.SetPlayerAcitve(isMenuActive);
                    SetIsMenuActive();
                    gameManager.Restart();
                }

                break;
            case MenuType.STAGESELECT:

                returnRectTarget.x = returnRectOrigin.x;
                restartRectTarget.x = restartRectOrigin.x;
                stageSelectRectTarget.x = stageSelectRectOrigin.x - menuContentsDiff;

                if (isTriggerUp)
                {
                    menuType = MenuType.RESTART;
                }

                if (isTriggerJump && !transition.GetIsTransitionNow())
                {
                    SetIsMenuActive();
                    transition.SetTransition("SelectScene");
                }

                break;
        }
    }
    void Menu()
    {
        // デルタタイム対応
        float deltaChasePower = chasePower * Time.deltaTime;

        menuBackImage.color += (menuBackTargetColor - menuBackImage.color) * deltaChasePower;
        menuTabRect.localPosition += (menuTabTarget - menuTabRect.localPosition) * deltaChasePower;
        returnRect.localPosition += (returnRectTarget - returnRect.localPosition) * deltaChasePower;
        restartRect.localPosition += (restartRectTarget - restartRect.localPosition) * deltaChasePower;
        stageSelectRect.localPosition += (stageSelectRectTarget - stageSelectRect.localPosition) * deltaChasePower;
        stageNumberRect.localPosition += (stageNumberTarget - stageNumberRect.localPosition) * deltaChasePower;
    }

    // Setter
    public void SetIsMenuActive()
    {
        isMenuActive = !isMenuActive;
        if (isMenuActive)
        {
            menuBackTargetColor = darkColor;
            menuTabTarget.x = 440f;
            if (isFinishStart)
            {
                stageNumberTarget.x = stageNumberOrigin.x;
            }
            menuType = MenuType.RETURN;
        }
        else
        {
            menuBackTargetColor = menuBackOriginColor;
            menuTabTarget.x = menuTabOrigin.x;
            if (isFinishStart)
            {
                stageNumberTarget.x = -815f;
            }
        }
    }

    // Getter
    public bool GetIsMenuActive()
    {
        return isMenuActive;
    }
    void GetInput()
    {
        isTriggerUp = false;
        isTriggerDown = false;
        isTriggerJump = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.VERTICAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.VERTICAL) > 0f)
            {
                isTriggerUp = true;
            }
            else
            {
                isTriggerDown = true;
            }
        }
        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerJump = true;
        }
    }
}
