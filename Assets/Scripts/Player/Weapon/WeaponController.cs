using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject beamObject;
    [SerializeField] GameObject gunObject;

    GameObject currentWeapon;

    private void Awake()
    {
        gunObject = GameObject.Find("Gun");
        gunObject.SetActive(false);
        beamObject = GameObject.Find("Beam");
        currentWeapon = beamObject; 
        currentWeapon.SetActive(true);
    }

    private void Update()
    {
        SwitchWeapon();
    }

    public void SwitchWeapon()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            EquipWeapon(gunObject);
        }
        else if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            EquipWeapon(beamObject);
        }
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
            currentWeapon.SetActive(false);

        currentWeapon = newWeapon;
        currentWeapon.SetActive(true);
    }
}
