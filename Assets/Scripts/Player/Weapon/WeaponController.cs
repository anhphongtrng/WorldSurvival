using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject beamObject;
    [SerializeField] GameObject gunObject;

    GameObject currentWeapon;
    protected string worldName;

    private void Awake()
    {
        gunObject = GameObject.Find("Gun");
        gunObject.SetActive(false);
        beamObject = GameObject.Find("Beam");
        beamObject.SetActive(false);
        //currentWeapon = beamObject; 
        //currentWeapon.SetActive(true);
        SetWeaponByWorld();
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

    public void SetWeaponByWorld()
    {
        worldName = SceneManager.GetActiveScene().name;
        Debug.Log("Current World: " + worldName);
        switch (worldName)
        {
            case "DesertWorld":
                EquipWeapon(beamObject);
                Debug.Log("Equipped Beam Weapon for DesertWorld");
                break;
            case "SnowWorld":
                EquipWeapon(gunObject);
                Debug.Log("Equipped Gun Weapon for SnowWorld");
                break;
            default:
                EquipWeapon(beamObject);
                break;
        }
    }
}
