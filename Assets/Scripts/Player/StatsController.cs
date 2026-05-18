using System.Collections;
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

    [Header("Visuals")]
    public Vector3 originalScale; 
    public Transform playerVisuals;

    [Header("Drop Items Rate")]
    public float healItemDropRate = 0.01f;
    public float beamDamageBuffItemDropRate = 0.01f;
    public float bonusTimeItemDropRate = 0.01f;

    private Coroutine beamBuffCoroutine;
    

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

    private void Start()
    {
        originalScale = playerVisuals.localScale;
    }

    // UPDATE HEALTH
    public void UpdateCurrentHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void UpdateMaxHealth(float amount)
    {
        currentHealth += amount;
        maxHealth += amount;
    }

    // UPDATE DAMAGE
    public void UpdateBeamDamage(int amount)
    {
        beamWeaponDamage += amount;
    }

    public void UpdateGunDamage(int amount)
    {
        gunWeaponDamage += amount;
    }

    // UPDATE BEAM BUFF
    public void AddTemporaryBeamDamage(int amount, float duration)
    {
        if (beamBuffCoroutine != null)
        {
            StopCoroutine(beamBuffCoroutine);

            // remove old buff first
            beamWeaponDamage -= amount;

            playerVisuals.localScale = originalScale;
        }

        beamBuffCoroutine = StartCoroutine(BeamDamageBuff(amount, duration));
    }

    private IEnumerator BeamDamageBuff(int amount, float duration)
    {
        beamWeaponDamage += amount;

        playerVisuals.localScale = originalScale * 1.5f;

        yield return new WaitForSeconds(duration);

        beamWeaponDamage -= amount;

        playerVisuals.localScale = originalScale;

        beamBuffCoroutine = null;
    }

    // UPDATE MOVEMENT
    public void UpdateMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    // UPDATE ITEMS RATE
    public void UpdateHealItemDropRate(float amount)
    {
        healItemDropRate += amount;
    }

    public void UpdateBeamDamageBuffItemDropRate(float amount)
    {
        beamDamageBuffItemDropRate += amount;
    }

    public void UpdateBonusTimeItemDropRate(float amount)
    {
        bonusTimeItemDropRate += amount;
    }

}
