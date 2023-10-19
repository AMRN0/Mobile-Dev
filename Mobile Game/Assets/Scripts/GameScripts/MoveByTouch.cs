using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y, transform.position.z);
                rb.velocity = new Vector3(speed, 0f, 0f);
            }
            else if (touch.position.x < Screen.width / 2)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y, transform.position.z);
                rb.velocity = new Vector3(-speed, 0f, 0f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
