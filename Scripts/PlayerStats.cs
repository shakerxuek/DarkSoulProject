using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{   
    AnimatorHandler animatorHandler;
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth= 100;
    public float chipSpeed =2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadespeed;
    private float durationTimer;

    private void Awake() 
    {
        animatorHandler=GetComponentInChildren<AnimatorHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        overlay.color =new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health =Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if(overlay.color.a>0)
        {
            durationTimer +=Time.deltaTime;
            if(durationTimer>duration)
            {
                float tempAlpha=overlay.color.a;
                tempAlpha -=Time.deltaTime*fadespeed;
                overlay.color =new Color(overlay.color.r,overlay.color.g,overlay.color.b, tempAlpha);
            }
        }
    }
    public void UpdateHealthUI()
    {
        float fillF=frontHealthBar.fillAmount;
        float fillB=backHealthBar.fillAmount;
        float hFraction=health/maxHealth;
        if(fillB>hFraction)
        {
            frontHealthBar.fillAmount=hFraction;
            backHealthBar.color=Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete =lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            backHealthBar.fillAmount =Mathf.Lerp(fillB,hFraction,percentComplete);
        }
        if(fillF<hFraction)
        {
            backHealthBar.color =Color.green;
            backHealthBar.fillAmount=hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            frontHealthBar.fillAmount=Mathf.Lerp(fillF,backHealthBar.fillAmount,percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -=damage;
        lerpTimer=0f;
        durationTimer=0f;
        overlay.color =new Color(overlay.color.r,overlay.color.g,overlay.color.b, 1);
        animatorHandler.PlayTargetAnimation("smalldamage",true);

        if(health<=0)
        {
            health=0;
            animatorHandler.PlayTargetAnimation("death",true);
        }
    }
    public void RestoreHealth(float healAmount)
    {
        health+=healAmount;
        lerpTimer=0f;
    }
    public void increaseHealth(int level)
    {
        maxHealth+=(health*0.01f)*((100-level)*0.1f);
        health = maxHealth;
    }
}
