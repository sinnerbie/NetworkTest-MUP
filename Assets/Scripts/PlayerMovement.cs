using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

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
    }
}
