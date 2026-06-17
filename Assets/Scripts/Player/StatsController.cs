using System.Collections;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController instance;

    [Header("Combat Stats")]
    public int beamWeaponDamage = 10;
    public int gunWeaponDamage = 2;
    public float beamRangeAttack = 5;
    public int beamLineCount = 1;

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

    [Header("Prefabs")]
    public GameObject auraPrefab;
    

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

    // UPDATE COMBAT STATS
    public void UpdateBeamDamage(int amount)
    {
        beamWeaponDamage += amount;
    }

    public void UpdateGunDamage(int amount)
    {
        gunWeaponDamage += amount;
    }

    public void UpdateBeamAttackRange(float amount)
    {
        beamRangeAttack += amount;
    }

    public void UpdateBeamLineCount(int amount)
    {
        beamLineCount += amount;
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
        GameObject aura = Instantiate(auraPrefab, playerVisuals.position, Quaternion.identity, playerVisuals);
        aura.transform.SetParent(playerVisuals);
        playerVisuals.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(duration);
        beamWeaponDamage -= amount;
        playerVisuals.localScale = originalScale;
        Destroy(aura);
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
