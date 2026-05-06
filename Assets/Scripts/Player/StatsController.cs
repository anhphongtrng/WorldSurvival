using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController instance;

    [Header("Combat Stats")]
    public int beamWeaponDamage = 1;
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
