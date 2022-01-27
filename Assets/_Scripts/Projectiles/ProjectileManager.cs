using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private int damage, pierces = 1;
    private DamageTypes damageType;
    private float bulletSpeed = 0;

    public void Move(Vector2 _direction, float _bulletSpeed)
    {
        _direction.Normalize();
        transform.up = _direction.normalized;
        LevelManager.endLevel.AddListener(ForceStop);
        bulletSpeed = _bulletSpeed;
        StartCoroutine(Movement(_direction.normalized));
    }

    private void ForceStop() { StopAllCoroutines(); }

    IEnumerator Movement(Vector3 _direction)
    {
        float dieTime = Time.time + 2;
        while (Time.time < dieTime)
        {
            transform.position += _direction * Time.deltaTime * bulletSpeed;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damageType, damage);
            pierces--;
            if(pierces <=0)
                Destroy(gameObject);
        }

        if(collision.tag == "BaseWall")
        {
            collision.gameObject.GetComponent<DefenceManager>().TakeDamage(damageType, damage);
            Destroy(gameObject);
        }
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public void SetType(DamageTypes _damageType)
    {
        damageType = _damageType;
    }
    public void SetPierces(int _pierces)
    {
        pierces = _pierces;
    }
}
