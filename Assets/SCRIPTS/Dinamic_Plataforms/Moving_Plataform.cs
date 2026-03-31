using UnityEngine;

public class Moving_Plataform : MonoBehaviour
{
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] Vector3 NextPoint;
    [SerializeField] float PlatSpeed;
    private void Awake()
    {

        NextPoint = PointA.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextPoint, PlatSpeed * Time.deltaTime);

        if(transform.position == NextPoint)
        {
            NextPoint = (NextPoint == PointA.position) ? PointB.position : PointA.position;
        }


    }


}
