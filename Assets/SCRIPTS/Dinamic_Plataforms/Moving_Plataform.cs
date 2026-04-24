using UnityEngine;

public class Moving_Plataform : MonoBehaviour
{
    [Header("DESTINY POINTS")]
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;

    [Header("ATRIBUTES")]
    [SerializeField] Vector3 NextPoint;
    [SerializeField] float PlatSpeed;

    private void Awake()
    {
        NextPoint = PointA.position; // NEXT POSITION ALWAYS STARTS IN A
    }
    private void Update()
    {
        // MOVES TOWARDS DESTINY POINT
        transform.position = Vector3.MoveTowards(transform.position, NextPoint, PlatSpeed * Time.deltaTime); 

        if(transform.position == NextPoint)
        { // IF NEXT POSITION IS OR IS NOT A
            NextPoint = (NextPoint == PointA.position) ? PointB.position : PointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided == null) return;

            if (collided.CompareTag("Player"))
            {
                gameObject.transform.SetParent(collided.transform);// PLAYER MOVES WITH PLATFORM
            }
        
    }
    private void OnTriggerExit2D(Collider2D collided)
    {
        if (collided == null) return;

            if (collided.CompareTag("Player"))
            {
                gameObject.transform.SetParent(null); // PLAYER MOVES WITHOUT PLATFORM
            }
        
    }
}
