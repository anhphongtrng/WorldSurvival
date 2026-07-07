using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunShooting : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePos;
    protected float shootTime = 0f;
    protected float shootRate = 0.5f;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected AudioClip shootClip;
    [SerializeField] protected TextMeshProUGUI ammoText;

    protected bool isShooting = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Mouse.current.leftButton.isPressed && Time.time >= shootTime && currentAmmo > 0 && !isShooting)
        {
            StartCoroutine(Shoot());
        }
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

    public IEnumerator Shoot()
    {
        isShooting = true;

        AudioController.instance.PlaySFX(shootClip);

        for (int i = 0; i < StatsController.instance.bulletsPerShot; i++)
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            yield return new WaitForSeconds(0.2f);
        }

        currentAmmo--;
        UpdateAmmoUI();
        shootTime = Time.time + shootRate;

        isShooting = false;
    }

    public void UpdateAmmoUI()
    {
        if (currentAmmo > 0)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
        else
        {
            ammoText.text = "Press Right Mouse Button to Reload";
        }
    }
}