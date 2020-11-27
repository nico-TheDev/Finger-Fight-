using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    public string heroName;

    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public float critRate;
    public float critDamage;

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

    }

    public int GetDamage(float percentage = 1f)
    {

        float critDmg = attackDamage * critRate;
        float normalDmg = attackDamage * (1 - critRate);

        return (int)((critDmg + normalDmg) * percentage);
    }
}
