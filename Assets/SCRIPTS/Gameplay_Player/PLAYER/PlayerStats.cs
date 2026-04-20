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
    public float Bounce;
    public LayerMask mask;
    
    [Header("KeyCount")]
    public short KeyCount;

    [Header("ATTK STATS")]
    public float radiusSide;
    public float radiusDown;
    public LayerMask enemies;
    public float damage;

    /*
    [Header("Arrow Instantiate properties")]
    public float shootCooldownMax;
    public float ArrowSpeed;
    */



}
