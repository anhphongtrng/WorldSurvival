using UnityEngine;
using UnityEngine.InputSystem;

public class GunReload : MonoBehaviour
{
    protected GunController gunController;
    [SerializeField] protected AudioClip reloadClip;

    private void Awake()
    {
        gunController = GetComponent<GunController>();
    }

    private void Update()
    {
        Reload();
    }

    public void Reload()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            AudioController.instance.PlaySFX(reloadClip);
            gunController.gunShooting.SetCurrentAmmo(gunController.gunShooting.GetMaxAmmo());
        }
    }
}
