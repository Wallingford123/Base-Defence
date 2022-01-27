using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageTypes { Physical,Magical,Penetrating }

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected float speed, dropChance, attackSpeed, attackRange;

    [SerializeField]
    protected int maxHealth, damage, physReduction, magicReduction, spawnWeighting;

    [SerializeField]
    protected DamageTypes damageType;

    protected int currentHealth;

    protected bool attacking = false;

    public virtual void Move()
    {
        transform.position += Vector3.left/10 * Time.deltaTime * speed;
    }

    public virtual IEnumerator Action()
    {
        while (!LevelManager.levelComplete)
        {
            if (transform.position.x - DefenceManager.objRef.transform.position.x < attackRange)
            {
                attacking = true;
                Attack();
                yield return new WaitForSeconds(1f / attackSpeed);
            }
            else { attacking = false; Move(); }
            yield return new WaitForEndOfFrame();
        }
    }

    public virtual void Attack()
    {
        DefenceManager.scriptRef.TakeDamage(damageType, damage);
    }

    public void TakeDamage(DamageTypes _damageType, int _damage)
    {
        float damageTaken = _damage;
        switch (_damageType)
        {
            case DamageTypes.Physical:
                if (physReduction < 100)
                    damageTaken = Mathf.FloorToInt(damageTaken * (100 - physReduction) / 100);
                else damageTaken = 0;
                break;

            case DamageTypes.Magical:
                if (magicReduction < 100)
                    damageTaken = Mathf.FloorToInt(damageTaken * (100 - magicReduction) / 100);
                else damageTaken = 0;
                break;
        }
        currentHealth -= (int)damageTaken;
        //Debug.Log(damageTaken + " " + _damageType.ToString() + " damage taken; New Health: " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    public virtual void Start()
    {
        ApplyScaling();
        currentHealth = maxHealth;
        if (attackSpeed <= 0) attackSpeed = 1f;
        StartCoroutine(Action());
    }
    private void ApplyScaling()
    {
        float scaledValue = Mathf.Pow(LevelManager.perLevelScaling, LevelManager.currentLevel-1);

        maxHealth = Mathf.FloorToInt(maxHealth * scaledValue);
        damage = Mathf.FloorToInt(damage * scaledValue);
    }
    public void Die()
    {
        //Debug.Log("unit ded");
        LevelManager.enemyKilled.Invoke(this);
        Destroy(gameObject);
    }

    public int GetDamage(){ return damage; }
    public int GetWeight() { return spawnWeighting; }
    public float GetSpeed() { return speed; }
    public bool Attacking() { return attacking; }
    public DamageTypes GetDamageType() { return damageType; }
}
