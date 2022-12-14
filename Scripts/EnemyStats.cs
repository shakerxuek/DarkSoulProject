using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthlevel=10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;

    private void Awake() 
    {
        animator=GetComponentInChildren<Animator>();
    }

    private void Start() 
    {
        maxHealth=SetMaxHealthFromHealthLevel();
        currentHealth=maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth=healthlevel*10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth=currentHealth-damage;

        animator.Play("smalldamage");

        if(currentHealth<=0)
        {
            currentHealth=0;
            animator.Play("death");
        }
    }
}
