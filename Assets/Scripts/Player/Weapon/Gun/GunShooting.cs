using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooting : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePos;
    protected float shootTime = 0f;
    protected float shootRate = 0.5f;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        Shoot();
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo = ammo;
    }

    public void Shoot()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= shootTime && currentAmmo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            bullet.transform.SetParent(transform);
            shootTime = Time.time + shootRate;
            currentAmmo--;
        }
    }
}
