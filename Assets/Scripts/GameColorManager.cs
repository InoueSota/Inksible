using UnityEngine;

public class GameColorManager : MonoBehaviour
{
    // 自コンポーネント取得
    private GameManager gameManager;

    [SerializeField] private Color darkColor;
    private Color color1;
    private Color color2;

    void Start()
    {
        gameManager = GetComponent<GameManager>();

        color1 = GlobalVariables.color1;
        color2 = GlobalVariables.color2;

        MenuColorManager menuColorManager = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuColorManager>();
        menuColorManager.SetColor(color2, color1, darkColor);
    }
}
