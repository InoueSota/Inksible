using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // 基本情報
    private static Transition instance;

    // 遷移先
    private string nextSceneName;

    // フラグ類
    private bool isTransition;
    private bool isTransitionNow;

    [Header("自オブジェクト取得")]
    [SerializeField] private Camera secondaryCamera;

    [Header("上幕")]
    [SerializeField] private GameObject topObj;
    [SerializeField] private SpriteRenderer topSpriteRenderer;
    [SerializeField] private Vector3 topFirstPosition;
    [SerializeField] private Vector3 topSecondPosition;
    [SerializeField] private Ease topEase;

    [Header("下幕")]
    [SerializeField] private GameObject downObj;
    [SerializeField] private SpriteRenderer downSpriteRenderer;
    [SerializeField] private Vector3 downFirstPosition;
    [SerializeField] private Vector3 downSecondPosition;
    [SerializeField] private Ease downEase;

    [Header("中央帯")]
    [SerializeField] private GameObject centerObj;
    [SerializeField] private Vector3 centerFirstScale;
    [SerializeField] private Vector3 centerSecondScale;
    [SerializeField] private Ease centerEase;

    [Header("帯テキスト")]
    [SerializeField] private SpriteRenderer centerText;
    [SerializeField] private Color textFirstColor;
    [SerializeField] private Color textSecondColor;
    [SerializeField] private Ease textEase;

    [Header("イージング")]
    [SerializeField] private float easeTime;

    void Awake()
    {
        // シーンが切り替わってもオブジェクトを保持するプログラム
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        isTransition = false;

        if (GameObject.FindWithTag("Transition"))
        {
            if (GameObject.FindWithTag("Transition") != gameObject)
            {
                Destroy(this);
            }
        }
    }

    void OnEnable()
    {
        // シーン読み込み
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        // シーン読み込み
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンがロードされた後の動作を再開
        if (isTransition)
        {
            DoTransition();
        }
    }

    void Update()
    {
        if (isTransition)
        {
            DoTransition();
        }
        if (Camera.main != null && Camera.main.GetUniversalAdditionalCameraData().cameraStack.Count == 0)
        {
            // カメラ追加処理
            Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(secondaryCamera);
        }
    }
    void DoTransition()
    {
        isTransitionNow = true;

        // 色変え
        topSpriteRenderer.color = GlobalVariables.color1;
        downSpriteRenderer.color = GlobalVariables.color2;

        centerText.DOColor(textFirstColor, easeTime).SetEase(textEase);
        centerObj.transform.DOScale(centerFirstScale, easeTime).SetEase(centerEase);
        downObj.transform.DOLocalMove(downFirstPosition, easeTime).SetEase(downEase);
        topObj.transform.DOLocalMove(topFirstPosition, easeTime).SetEase(topEase).OnComplete(() =>
        {
            // ここで画面切り替わる処理(画面遷移で画面が見えてない場所)
            SceneManager.LoadScene(nextSceneName);
            centerText.DOColor(textSecondColor, easeTime).SetEase(textEase);
            centerObj.transform.DOScale(centerSecondScale, easeTime).SetEase(centerEase);
            downObj.transform.DOLocalMove(downSecondPosition, easeTime).SetEase(downEase);
            topObj.transform.DOLocalMove(topSecondPosition, easeTime).SetEase(topEase).OnComplete(() =>
            {
                // 画面遷移が閉じる
                isTransitionNow = false;
            });
        });

        isTransition = false;
    }

    // Setter
    public void SetTransition(string name)
    {
        isTransition = true;
        nextSceneName = name;
    }

    // Getter
    public bool GetIsTransitionNow()
    {
        return isTransitionNow;
    }
}
