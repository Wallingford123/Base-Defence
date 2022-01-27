using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public static GameObject objRef;
    public static DefenceManager scriptRef;

    private float colorVal = 1;

    private void Start()
    {
        objRef = gameObject;
        scriptRef = this;
        currentHealth = maxHealth;
        StartCoroutine(ColorResetter());
    }

    public void TakeDamage(DamageTypes damageType, int damageTaken)
    {
        currentHealth -= damageTaken;
        colorVal = 0;
        if (currentHealth <= 0) LevelManager.endLevel.Invoke();
    }

    IEnumerator ColorResetter()
    {
        yield return new WaitForEndOfFrame();
        while (!LevelManager.levelComplete)
        {
            yield return new WaitForEndOfFrame();
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, colorVal);
            colorVal += Time.deltaTime*4;
        }
    }
}
