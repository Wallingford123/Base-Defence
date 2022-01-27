using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0)) {
            Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, mask);
            if (hit.collider != null)
            {
                EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
                enemy.TakeDamage(DamageTypes.Physical, 1000);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, mask);
            if (hit.collider != null)
            {
                EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
                enemy.TakeDamage(DamageTypes.Magical, 1000);
            }
        }*/
    }
}
