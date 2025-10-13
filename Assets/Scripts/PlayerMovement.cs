using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;

    bool jumping = false;
    bool canJump = true;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;
        if (!IsOwner)
        {
            enabled = false;
            rb.isKinematic = true;
            return;
        }

        rb.isKinematic = false;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0) 
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            else
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            else
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) jumping = true;

        if (rb.linearVelocity.y == 0)
            canJump = true;
        else 
            canJump = false;
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            jumping = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
