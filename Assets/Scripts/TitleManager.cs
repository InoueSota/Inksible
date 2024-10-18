using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private InputManager inputManager;
    private bool isTriggerJump;

    // ���R���|�[�l���g�擾
    private Transition transition;
    private ColorData colorData;

    // �J�ڃV�[���於
    [SerializeField] private string nextScene;

    [Header("UI")]
    [SerializeField] private Image inkImage;
    [SerializeField] private Image titleImage;
    [SerializeField] private Image titleBackImage;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        transition = GameObject.FindWithTag("Transition").GetComponent<Transition>();

        // �F�f�[�^�̎擾
        colorData = new ColorData();
        colorData.Initialize();

        // �F���
        GlobalVariables.color1 = colorData.GetMainColor(GlobalVariables.colorNum);
        GlobalVariables.color2 = colorData.GetSubColor(GlobalVariables.colorNum);

        // UI�̐F
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
