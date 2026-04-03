using UnityEngine;

public class Descarted_Ideas : MonoBehaviour
{
    [Header("Arrow Instantiate properties")]
    [SerializeField] float shootCooldown;


    // I WANTED THE PLAYER TO SHOOT ARROWS; BUT I DIDNT ADD ANYTHING RELEVANT TO THE MAIN MECHANIC,
    // BUT A DID CREATED IT, AND CAN BE APPLIED, AND WORKS, AND ITS FUNCTIONAL
    #region SHOOT
    /*
    public void OnShoot(InputAction.CallbackContext context)
    {
        Quaternion Rotate;
        if (context.performed && Input.GetKey(KeyCode.DownArrow) && shootCooldown >= _PlyrStts.shootCooldownMax)
        {
            // CREATES GAME OBJECT PROJECTILE WITH PREFAB AND ROTATING EULER
            Rotate = Quaternion.Euler(0, 0, 180);

            GameObject newArrow = Instantiate(arrow, transform.position, Rotate);
            newArrow.GetComponent<Rigidbody2D>().AddForce(transform.up * -1 * _PlyrStts.ArrowSpeed, ForceMode2D.Impulse);
            Destroy(newArrow, 1f);
            shootCooldown = 0; // RESETS COOLDOWN
        }

        if (context.performed && shootCooldown >= _PlyrStts.shootCooldownMax)
        {
            // CREATES GAME OBJECT PROJECTILE WITH PREFAB AND ROTATING EULER
            if (LookingRight) Rotate = Quaternion.Euler(0, 0, -90);
            else Rotate = Quaternion.Euler(0, 0, 90);

            GameObject newArrow = Instantiate(arrow, transform.position, Rotate);
            newArrow.GetComponent<Rigidbody2D>().AddForce(transform.right * _PlyrStts.ArrowSpeed, ForceMode2D.Impulse);
            Destroy(newArrow, 1f);
            shootCooldown = 0; // RESETS COOLDOWN
        }
    }
    */
    #endregion

    #region Projectile
    /*
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [Header("Components")]
    [SerializeField] Collider2D _clldr;

    private void Awake()
    {   // INSTANTIATES COMPONENTS
        _clldr = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_clldr == null) return;

        if (collision.transform.CompareTag("Ground")) // IF TOUCHES THE GROUND
        {
            Destroy(gameObject); // DESTROY AFTER 1 SECOND
        }

    }
    */
    #endregion

}
