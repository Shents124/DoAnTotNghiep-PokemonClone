using UnityEngine;

[CreateAssetMenu(fileName = "Player Position", menuName = "Save System/Player Position")]
public class PlayerPositionSO : ScriptableObject
{
    public float xPos;
    public float yPos;

    public void SetPos(float x, float y)
    {
        xPos = x;
        yPos = y;
    }
    
    public Vector2 GetPos() => new Vector2(xPos, yPos);
}
