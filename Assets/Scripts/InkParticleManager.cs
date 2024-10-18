using UnityEngine;

public class InkParticleManager : MonoBehaviour
{
    // 自コンポーネント取得
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("表示時間")]
    [SerializeField] private float displayTimeMax;
    [SerializeField] private float displayTimeMin;
    private float displayTimer;
    private bool isStartDisplayTimer;

    void Update()
    {
        if (isStartDisplayTimer)
        {
            displayTimer -= Time.deltaTime;

            if (displayTimer <= 0f)
            {
                animator.SetTrigger("Start");
                isStartDisplayTimer = false;
            }
        }
    }

    // Setter
    public void Initialize(Color _color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        spriteRenderer.color = _color;
        displayTimer = Random.Range(displayTimeMin, displayTimeMax);
    }
    public void StartDisplayTimer()
    {
        isStartDisplayTimer = true;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
