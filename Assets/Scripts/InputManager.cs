using UnityEngine;

public class InputManager : MonoBehaviour
{
    // 入力パターン
    public enum INPUTPATTERN
    {
        HORIZONTAL,
        VERTICAL,
        JUMP,
        SPECIAL,
        RESET,
        CANCEL
    }
    
    // 入力を受け取る
    private float inputHorizontal = 0;
    private float inputVertical = 0;
    private int inputJump = 0;
    private int inputSpecial = 0;
    private int inputReset = 0;
    private int inputCancel = 0;

    // 前回の入力
    private float preInputHorizontal = 0;
    private float preInputVertical = 0;
    private int preInputJump = 0;
    private int preInputSpecial = 0;
    private int preInputReset = 0;
    private int preInputCancel = 0;

    void Update()
    {
        preInputHorizontal = inputHorizontal;
        preInputVertical = inputVertical;
        preInputJump = inputJump;
        preInputSpecial = inputSpecial;
        preInputReset = inputReset;
        preInputCancel = inputCancel;

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputJump = (int)Input.GetAxisRaw("Jump");
        inputSpecial = (int)Input.GetAxisRaw("Special");
        inputReset = (int)Input.GetAxisRaw("Reset");
        inputCancel = (int)Input.GetAxisRaw("Cancel");
    }

    public bool IsTrgger(INPUTPATTERN inputPattern_)
    {
        switch (inputPattern_)
        {
            case INPUTPATTERN.HORIZONTAL:

                if (inputHorizontal != 0 && preInputHorizontal == 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.VERTICAL:

                if (inputVertical != 0 && preInputVertical == 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.JUMP:

                if (inputJump != 0 && preInputJump == 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.SPECIAL:

                if (inputSpecial != 0 && preInputSpecial == 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.RESET:

                if (inputReset != 0 && preInputReset == 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.CANCEL:

                if (inputCancel != 0 && preInputCancel == 0)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool IsPush(INPUTPATTERN inputPattern_)
    {
        switch (inputPattern_)
        {
            case INPUTPATTERN.HORIZONTAL:

                if (inputHorizontal != 0f)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.VERTICAL:

                if (inputVertical != 0f)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.JUMP:

                if (inputJump != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.SPECIAL:

                if (inputSpecial != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.RESET:

                if (inputReset != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.CANCEL:

                if (inputCancel != 0)
                {
                    return true;
                }
                return false;
        }
        return false;
    }

    public bool IsRelease(INPUTPATTERN inputPattern_)
    {
        switch (inputPattern_)
        {
            case INPUTPATTERN.HORIZONTAL:

                if (inputHorizontal == 0 && preInputHorizontal != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.VERTICAL:

                if (inputVertical == 0 && preInputVertical != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.JUMP:

                if (inputJump == 0 && preInputJump != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.SPECIAL:

                if (inputSpecial == 0 && preInputSpecial != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.RESET:

                if (inputReset == 0 && preInputReset != 0)
                {
                    return true;
                }
                return false;

            case INPUTPATTERN.CANCEL:

                if (inputCancel == 0 && preInputCancel != 0)
                {
                    return true;
                }
                return false;

        }
        return false;
    }

    public float ReturnInputValue(INPUTPATTERN inputPattern_)
    {
        switch (inputPattern_)
        {
            case INPUTPATTERN.HORIZONTAL:

                return inputHorizontal;

            case INPUTPATTERN.VERTICAL:

                return inputVertical;

            case INPUTPATTERN.JUMP:

                return inputJump;

            case INPUTPATTERN.SPECIAL:

                return inputSpecial;

            case INPUTPATTERN.RESET:

                return inputReset;

            case INPUTPATTERN.CANCEL:

                return inputCancel;
        }
        return 0;
    }
}
