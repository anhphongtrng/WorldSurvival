using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }
}
