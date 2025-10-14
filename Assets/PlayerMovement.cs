using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private float moveInput;
    private bool isGrounded;


    //fal mászás
    private bool isTouchingWall = false;
    private Vector3 wallNormal; // az irány, amerre a fal néz
    public float wallJumpForceX = 5f;
    public float wallJumpForceY = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Bal/Jobb input (A/D vagy ←/→)
        moveInput = Input.GetAxisRaw("Horizontal");

        

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                isGrounded = false;
            }
            else if (isTouchingWall)
            {
                // Falról oldalra ugrás
                Vector3 jumpDir = new Vector3(-wallNormal.x * wallJumpForceX, wallJumpForceY, 0f);
                rb.linearVelocity = jumpDir;
                isTouchingWall = false; // már nem vagyunk a falhoz ragadva
            }
        }
    }

    void FixedUpdate()
    {
        // Vízszintes mozgás
        Vector3 v = rb.linearVelocity;
        v.x = moveInput * moveSpeed;
        rb.linearVelocity = new Vector3(v.x, v.y, rb.linearVelocity.z);
    }

    // Talaj érzékelés
    void OnCollisionStay(Collision collision)
    {
        // Talaj
        foreach (ContactPoint cp in collision.contacts)
        {
            if (collision.gameObject.CompareTag("Ground") && Vector3.Dot(cp.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
            }
            // Fal
            else if (collision.gameObject.CompareTag("Wall") && Mathf.Abs(Vector3.Dot(cp.normal, Vector3.up)) < 0.1f)
            {
                isTouchingWall = true;
                wallNormal = cp.normal; // oldalirány
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
        if (collision.gameObject.CompareTag("Wall"))
            isTouchingWall = false;
    }
}
