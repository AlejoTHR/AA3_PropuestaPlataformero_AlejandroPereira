using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PatrollMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;

    [Header("Atributes")]
    [SerializeField] Vector3 NextPoint;
    [SerializeField] Vector3 Rotation = new Vector3(0,180,0); //  180 DEGREES
    [SerializeField] float PlatSpeed;
    private void Awake()
    {
        NextPoint = PointA.position; // INSTANCIATES DESTINATION POSITION
    }

    private void Update()
    {   // MOVES TOWARDS NEXT DESTINATION POSITION
        transform.position = Vector3.MoveTowards(transform.position, NextPoint, PlatSpeed * Time.deltaTime); 

        if (transform.position == NextPoint) // IF ARRIVES
        {
            if(NextPoint == PointA.position) // ON ARRIVAK POINT A
            {
                NextPoint = PointB.position; // NEXT POSITION IS POINT B
                transform.Rotate(Rotation); // ROTATES TO FACE POINT B

            }
            else if(NextPoint == PointB.position) // NEXT POSOTION IS POINT A
            {
                NextPoint = PointA.position; // NEXT POSITION IS POINT A
                transform.Rotate(Rotation); // ROTATES TO FACE POINT A
            }
        }
    }

}
