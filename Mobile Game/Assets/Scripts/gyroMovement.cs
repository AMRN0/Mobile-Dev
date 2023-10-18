using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroMovement : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 20.0f;
    float dirX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX, 0f, 0f);
    }
}
