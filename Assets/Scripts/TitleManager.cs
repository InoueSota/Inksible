using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // 自コンポーネント取得
    private InputManager inputManager;
    private bool isTriggerJump;

    // 他コンポーネント取得
    private Transition transition;
    private ColorData colorData;

    // 遷移シーン先名
    [SerializeField] private string nextScene;

    [Header("UI")]
    [SerializeField] private Image inkImage;
    [SerializeField] private Image titleImage;
    [SerializeField] private Image titleBackImage;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        transition = GameObject.FindWithTag("Transition").GetComponent<Transition>();

        // 色データの取得
        colorData = new ColorData();
        colorData.Initialize();

        // 色代入
        GlobalVariables.color1 = colorData.GetMainColor(GlobalVariables.colorNum);
        GlobalVariables.color2 = colorData.GetSubColor(GlobalVariables.colorNum);

        // UIの色
        inkImage.color = GlobalVariables.color1;
        inkImage.transform.DOLocalRotate(new(0f, 0f, 360f), 50f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
        titleImage.color = GlobalVariables.color1;
        titleBackImage.color = GlobalVariables.color2;
    }

    void Update()
    {
        GetInput();

        ChangeScene();
    }

    void ChangeScene()
    {
        if (isTriggerJump && !transition.GetIsTransitionNow())
        {
            transition.SetTransition(nextScene);
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
