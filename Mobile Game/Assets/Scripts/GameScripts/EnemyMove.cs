using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody rb;

    private float force = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        force = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0, 0, -force));

        if (transform.position.z <= -10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
