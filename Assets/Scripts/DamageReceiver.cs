using UnityEngine;
using UnityEngine.UI;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] protected float maxHP = 5f;
    [SerializeField] protected float currentHP;
    [SerializeField] protected Image hpBar;
    [SerializeField] protected GameObject bloodEffectPrefab;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (GamePauseController.instance.isPaused) return;
        currentHP -= dmg;
        UpdateHPBar();
        GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        Destroy(blood, 0.5f);
    }

    public virtual float GetCurrentHP()
    {
        return currentHP;
    }

    public virtual float GetMaxHP()
    {
        return maxHP;
    }

    public virtual void SetHP(float newHp)
    {
        currentHP = Mathf.Clamp(newHp, 0, maxHP);
    }

    public virtual void ChangeHP(float amount)
    {
        if (IsDead()) return;

        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public virtual bool IsDead()
    {
        return currentHP <= 0;
    }

    public void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}