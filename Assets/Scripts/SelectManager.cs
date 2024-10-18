using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    // ���R���|�[�l���g�擾
    private InputManager inputManager;
    private bool isTriggerJump;
    private bool isPushLeft;
    private bool isPushRight;
    private bool isPushUp;
    private bool isPushDown;

    // ���R���|�[�l���g�擾
    private Transition transition;

    // �����쐬
    private float initialIntervalTime = 0.1f;

    // �V�[����
    [SerializeField] private int stageMax;
    private int stageNumber;
    private string[] stageName;

    [Header("SelectUI")]
    [SerializeField] private Text stageNumberText;
    [SerializeField] private GameObject leftTriangleObj;
    [SerializeField] private GameObject rightTriangleObj;
    private Image leftTriangleImage;
    private Image rightTriangleImage;

    [Header("OtherUI")]
    [SerializeField] private Image headerBackImage;
    [SerializeField] private Image headerImage;

    [Header("�X�e�[�W�I���Ԋu�̎���")]
    [SerializeField] private float selectIntervalTime;
    private float selectIntervalTimer;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        stageName = new string[stageMax];
        for (int i = 1; i < stageMax + 1; i++)
        {
            stageName[i - 1] = "Stage" + i.ToString();
        }

        if (GameObject.FindWithTag("Transition"))
        {
            transition = GameObject.FindWithTag("Transition").GetComponent<Transition>();
        }

        headerBackImage.color = GlobalVariables.color1;
        headerImage.color = GlobalVariables.color2;

        leftTriangleImage = leftTriangleObj.GetComponent<Image>();
        rightTriangleImage = rightTriangleObj.GetComponent<Image>();
        leftTriangleImage.color = GlobalVariables.color1;
        rightTriangleImage.color = GlobalVariables.color1;
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
        selectIntervalTimer -= Time.deltaTime;

        if (selectIntervalTimer <= 0f && (isPushLeft || isPushRight || isPushUp || isPushDown))
        {
            // �X�e�[�W�ԍ������Z����
            if (isPushLeft)
            {
                // ���łɍŏ��ԍ���I�����Ă�����A�ő�ԍ��ɂ���
                if (stageNumber == 0)
                {
                    stageNumber = stageMax - 1;
                }
                else
                {
                    stageNumber--;
                }
                leftTriangleObj.transform.DORotate(Vector3.forward * 360f, 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
            }
            // �X�e�[�W�ԍ������Z����
            else if (isPushRight)
            {
                // ���łɍő�ԍ���I�����Ă�����A�ŏ��ԍ��ɂ���
                if (stageNumber == stageMax - 1)
                {
                    stageNumber = 0;
                }
                else
                {
                    stageNumber++;
                }
                rightTriangleObj.transform.DORotate(Vector3.forward * 360f, 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
            }
            // �X�e�[�W�ԍ��̍ő�l�ɂ���
            else if (isPushUp)
            {
                stageNumber = stageMax - 1;
            }
            // �X�e�[�W�ԍ����ŏ��l�ɂ���
            else if (isPushDown)
            {
                stageNumber = 0;
            }
            selectIntervalTimer = selectIntervalTime;
        }
    }
    void ChangeScene()
    {
        initialIntervalTime -= Time.deltaTime;

        if (initialIntervalTime <= 0f && isTriggerJump && !transition.GetIsTransitionNow())
        {
            transition.SetTransition(stageName[stageNumber]);
        }
    }
    void UiManager()
    {
        // �I�����x���ԍ������̏���
        int addOneStageNumber = stageNumber + 1;
        stageNumberText.text = "Stage" + addOneStageNumber.ToString();

        // ���x���I���O�p�`�̏���
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
        isPushLeft = false;
        isPushRight = false;
        isPushUp = false;
        isPushDown = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.JUMP))
        {
            isTriggerJump = true;
        }
        if (inputManager.IsPush(InputManager.INPUTPATTERN.HORIZONTAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.HORIZONTAL) < 0f)
            {
                isPushLeft = true;
            }
            else
            {
                isPushRight = true;
            }
        }
        if (inputManager.IsPush(InputManager.INPUTPATTERN.VERTICAL))
        {
            if (inputManager.ReturnInputValue(InputManager.INPUTPATTERN.VERTICAL) < 0f)
            {
                isPushDown= true;
            }
            else
            {
                isPushUp = true;
            }
        }
    }
}
