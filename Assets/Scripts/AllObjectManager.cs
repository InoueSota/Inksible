using UnityEngine;

public class AllObjectManager : MonoBehaviour
{
    // ÉuÉçÉbÉNÇÃèÛë‘
    public enum BlockType
    {
        COLOR1,
        COLOR2,
        WHITE,
        BLACK
    }
    [SerializeField] private BlockType blockType;

    // Setter
    public void SetBlockType(BlockType _blockType)
    {
        blockType = _blockType;
    }

    // Getter
    public BlockType GetBlockType()
    {
        return blockType;
    }
    public BlockType GetDifferentBlockType()
    {
        if (blockType == BlockType.COLOR1)
        {
            return BlockType.COLOR2;
        }
        else if (blockType == BlockType.COLOR2)
        {
            return BlockType.COLOR1;
        }
        return 0;
    }
}
