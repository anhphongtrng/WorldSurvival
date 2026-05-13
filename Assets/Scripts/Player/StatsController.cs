using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController instance;

    [Header("Combat Stats")]
    public int beamWeaponDamage = 10;
    public int gunWeaponDamage = 2;

    [Header("Movement Stats")]
    public float moveSpeed = 5f;

    [Header("Health Stats")]
    public float maxHealth = 5f;
    public float currentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMaxHealth(float amount)
    {
        maxHealth += amount;
    }

    public void UpdateBeamDamage(int amount)
    {
        beamWeaponDamage += amount;
    }

    public void UpdateGunDamage(int amount)
    {
        gunWeaponDamage += amount;
    }
    public void UpdateMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
