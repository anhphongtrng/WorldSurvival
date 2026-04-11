using UnityEngine;
using UnityEngine.InputSystem;

public class GunReload : MonoBehaviour
{
    protected GunController gunController;

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
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            gunController.gunShooting.SetCurrentAmmo(gunController.gunShooting.GetMaxAmmo());
        }
    }
}
