using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{

    [Header("Horizontal Movement")]
    public float Speed;

    [Header("Jumping")]
    public float JumpForce;
    public float JumpCancelSupresion;

    [Header("Grounded")]
    public float GroundedCheckSize;
    public float GroundedCheckRadius;
    public LayerMask mask;

    [Header("Arrow Instantiate properties")]
    public float shootCooldownMax;


}
