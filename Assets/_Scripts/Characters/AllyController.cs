using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetingType { Closest, Strongest, RangedFocus }
public class AllyController : ControllerBase
{
    public TargetingType targetingType;
    private GameObject target;
    private void Start()
    {
        LevelManager.endLevel.AddListener(EndLevel);
        StartCoroutine(AttackCycle());
    }

    GameObject GetTarget(TargetingType _targetType)
    {
        EnemyBase t = LevelManager.enemies[0];
        float distance = 0;
        switch (_targetType) {
            case TargetingType.Closest:
                distance = Vector2.Distance(transform.position, t.transform.position);
                foreach (EnemyBase e in LevelManager.enemies)
                {
                    float tempDistance = Vector2.Distance(transform.position, e.transform.position);
                    if (tempDistance < distance)
                    {
                        t = e;
                        distance = tempDistance;
                    }
                }
                break;

            case TargetingType.Strongest:
                float strength = 0;
                distance = 10000;
                foreach (EnemyBase e in LevelManager.enemies)
                {
                    float tempDistance = Vector2.Distance(transform.position, e.transform.position);
                    float tempStrength = e.GetWeight();
                    if(tempStrength > strength)
                    {
                        t = e;
                        strength = tempStrength;
                        distance = tempDistance;
                    }
                    else if (tempStrength == strength)
                    {
                        if (tempDistance < distance)
                        {
                            t = e;
                            distance = tempDistance;
                        }
                    }
                }
                break;

            case TargetingType.RangedFocus:
                distance = Vector2.Distance(transform.position, t.transform.position);
                foreach (EnemyBase e in LevelManager.enemies)
                {
                    if (e.GetType() == typeof(RangedEnemy))
                    {
                        float tempDistance = Vector2.Distance(transform.position, e.transform.position);
                        if (tempDistance < distance)
                        {
                            t = e;
                            distance = tempDistance;
                        }
                    }   
                }
                break;
        }

        return t.gameObject;
    }
    IEnumerator AttackCycle()
    {
        Debug.Log(LevelManager.levelComplete);
        while (!LevelManager.levelComplete)
        {
            if (LevelManager.enemies.Count > 0)
            {
                if (!target)
                    target = GetTarget(targetingType);

                else
                {
                    ProjectileManager temp = Instantiate(projectile, gameObject.transform.position, Quaternion.identity).GetComponent<ProjectileManager>();
                    float xOffset = 0;
                    EnemyBase b = target.GetComponent<EnemyBase>();
                    if (b.GetType() != typeof(RangedEnemy) && !b.Attacking())
                    {
                        float dist = Vector2.Distance(transform.position, target.transform.position);
                        float speed = b.GetSpeed();
                        xOffset = speed * (dist / projectileSpeed / 10);
                        
                        for(int i = 0; i<5; i++)
                        {
                            dist = Vector2.Distance(transform.position, new Vector3(target.transform.position.x - xOffset, target.transform.position.y, target.transform.position.z));
                            xOffset = speed * (dist / projectileSpeed / 10);
                        }
                    }
                    temp.SetDamage(100);
                    temp.SetType(DamageTypes.Physical);
                    temp.SetPierces(20);
                    temp.Move(new Vector2(target.transform.position.x - transform.position.x - xOffset, target.transform.position.y - transform.position.y), projectileSpeed);
                    target = GetTarget(targetingType);
                    yield return new WaitForSeconds(1.0f / attacksPerSecond);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void EndLevel()
    {
        StopAllCoroutines();
    }
}
