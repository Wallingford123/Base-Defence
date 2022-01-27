using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour
{
    [SerializeField]
    protected LayerMask mask;
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected int projectileSpeed;
    [SerializeField]
    protected float attacksPerSecond;
}
