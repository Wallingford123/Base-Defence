using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase
{

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && LevelManager.started)
        {
            Vector2 ray = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0, mask);

            if(hit.point.x > -5)
            {
                ProjectileManager temp = Instantiate(projectile, gameObject.transform.position, Quaternion.identity).GetComponent<ProjectileManager>();
                temp.SetDamage(100);
                temp.SetType(DamageTypes.Physical);
                temp.SetPierces(2);
                temp.Move(new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y), projectileSpeed);
            }
        }
    }
}
