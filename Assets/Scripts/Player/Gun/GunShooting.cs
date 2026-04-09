using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooting : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePos;
    protected float shootTime = 0f;
    protected float shootRate = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= shootTime)
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            shootTime = Time.time + shootRate;
        }
    }
}
