using UnityEngine;

public class AllPlayerManager : MonoBehaviour
{
    // 自コンポーネント取得
    private InputManager inputManager;
    private bool isTriggerSpecial;

    // 他コンポーネント取得
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoulManager soulManager;

    // 管理対象
    [SerializeField] private GameObject player1Obj;
    [SerializeField] private GameObject player2Obj;
    private PlayerMoveManager player1MoveManager;
    private PlayerMoveManager player2MoveManager;
    private PlayerGoalManager player1GoalManager;
    private PlayerGoalManager player2GoalManager;
    private AllObjectManager player1AllObjectManager;
    private AllObjectManager player2AllObjectManager;
    private ObjectColorManager player1ObjectColorManager;
    private ObjectColorManager player2ObjectColorManager;

    // 管理モード
    private enum ActivePlayer
    {
        PLAYER1,
        PLAYER2
    }
    private ActivePlayer activePlayer = ActivePlayer.PLAYER1;

    // フラグ
    private bool isActive;
    private bool isPlayer1Active;
    private bool isPlayer2Active;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        Initialize();

        isActive = false;
        isPlayer1Active = false;
        isPlayer2Active = false;
    }
    public void Initialize()
    {
        player1MoveManager = player1Obj.GetComponent<PlayerMoveManager>();
        player2MoveManager = player2Obj.GetComponent<PlayerMoveManager>();
        player1GoalManager = player1Obj.GetComponent<PlayerGoalManager>();
        player2GoalManager = player2Obj.GetComponent<PlayerGoalManager>();
        player1AllObjectManager = player1Obj.GetComponent<AllObjectManager>();
        player2AllObjectManager = player2Obj.GetComponent<AllObjectManager>();
        player1ObjectColorManager = player1Obj.GetComponent<ObjectColorManager>();
        player2ObjectColorManager = player2Obj.GetComponent<ObjectColorManager>();
        player2ObjectColorManager.SetIsActive(isPlayer2Active);
    }

    void Update()
    {
        GetInput();

        ChangePlayer();
    }

    void ChangePlayer()
    {
        switch (activePlayer)
        {
            case ActivePlayer.PLAYER1:

                if (isPlayer1Active && isTriggerSpecial && !player2GoalManager.GetIsGoal())
                {
                    isPlayer1Active = false;
                    soulManager.SetStart(player1AllObjectManager.GetBlockType(), player1Obj.transform.position, player2Obj.transform.position, gameManager.GetColor(1));
                }

                break;
            case ActivePlayer.PLAYER2:

                if (isPlayer2Active && isTriggerSpecial && !player1GoalManager.GetIsGoal())
                {
                    isPlayer2Active = false;
                    soulManager.SetStart(player2AllObjectManager.GetBlockType(), player2Obj.transform.position, player1Obj.transform.position, gameManager.GetColor(2));
                }

                break;
        }
    }
    void AllObjectChangeAlpha()
    {
        player1ObjectColorManager.SetIsActive(isPlayer1Active);
        player2ObjectColorManager.SetIsActive(isPlayer2Active);

        switch (activePlayer)
        {
            case ActivePlayer.PLAYER1:

                // BLOCK
                foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
                {
                    if (block.GetComponent<AllObjectManager>().GetBlockType() == player1AllObjectManager.GetBlockType())
                    {
                        block.GetComponent<ObjectColorManager>().SetIsActive(false);
                    }
                    else if (block.GetComponent<AllObjectManager>().GetBlockType() == player2AllObjectManager.GetBlockType())
                    {
                        block.GetComponent<ObjectColorManager>().SetIsActive(true);
                    }
                }

                break;
            case ActivePlayer.PLAYER2:

                // BLOCK
                foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
                {
                    if (block.GetComponent<AllObjectManager>().GetBlockType() == player1AllObjectManager.GetBlockType())
                    {
                        block.GetComponent<ObjectColorManager>().SetIsActive(true);
                    }
                    else if (block.GetComponent<AllObjectManager>().GetBlockType() == player2AllObjectManager.GetBlockType())
                    {
                        block.GetComponent<ObjectColorManager>().SetIsActive(false);
                    }
                }

                break;
        }
    }

    public void ChangePlayerActive()
    {
        if (isActive)
        {
            switch (activePlayer)
            {
                case ActivePlayer.PLAYER1:

                    isPlayer2Active = true;
                    AllObjectChangeAlpha();
                    activePlayer = ActivePlayer.PLAYER2;

                    break;
                case ActivePlayer.PLAYER2:

                    isPlayer1Active = true;
                    AllObjectChangeAlpha();
                    activePlayer = ActivePlayer.PLAYER1;

                    break;
            }
        }
    }
    public void StartInitialize(bool _isActive)
    {
        isActive = _isActive;
        isPlayer1Active = true;
        isPlayer2Active = false;
    }
    public void SetIsActive(bool _isActive)
    {
        isActive = _isActive;
    }

    void GetInput()
    {
        isTriggerSpecial = false;

        if (inputManager.IsTrgger(InputManager.INPUTPATTERN.SPECIAL))
        {
            isTriggerSpecial = true;
        }
    }
    public bool GetIsPlayerActive(PlayerMoveManager _playerMoveManager)
    {
        if (isActive)
        {
            if (_playerMoveManager == player1MoveManager)
            {
                return isPlayer1Active;
            }
            else if (_playerMoveManager == player2MoveManager)
            {
                return isPlayer2Active;
            }
        }
        return false;
    }
    public bool GetIsPlayerGoal(int _playerNumber)
    {
        if (_playerNumber == 1)
        {
            return player1GoalManager.GetIsGoal();
        }
        else if (_playerNumber == 2)
        {
            return player2GoalManager.GetIsGoal();
        }
        return false;
    }
    public void PlayersInitialize()
    {
        isPlayer1Active = true;
        isPlayer2Active = false;
        activePlayer = ActivePlayer.PLAYER1;

        player1Obj.SetActive(true);
        player2Obj.SetActive(true);
        player1MoveManager.Initialize();
        player2MoveManager.Initialize();
        player1GoalManager.Initialize();
        player2GoalManager.Initialize();
        player1ObjectColorManager.SetIsActive(isPlayer1Active);
        player2ObjectColorManager.SetIsActive(isPlayer2Active);
    }
}
