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
    public Image frontStaminaBar;
    public Image backStaminaBar;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadespeed;
    private float durationTimer;
    public float maxStamina;
    public int StaminaLevel=10;
    public float currentStamina;

    private void Awake() 
    {
        animatorHandler=GetComponentInChildren<AnimatorHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        maxStamina=SetMaxStamina();
        currentStamina=maxStamina;
        overlay.color =new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health =Mathf.Clamp(health,0,maxHealth);
        currentStamina =Mathf.Clamp(currentStamina,0,maxStamina);
        
        UpdateHealthUI();
        UpdateStaminaUI();
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

    public void UpdateStaminaUI()
    {
        float fillF=frontStaminaBar.fillAmount;
        float fillB=backStaminaBar.fillAmount;
        float hFraction=currentStamina/maxStamina;
        if(fillB>hFraction)
        {
            frontStaminaBar.fillAmount=hFraction;
            backStaminaBar.color=Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete =lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            backStaminaBar.fillAmount =Mathf.Lerp(fillB,hFraction,percentComplete);
        }
        if(fillF<hFraction)
        {
            backStaminaBar.color =Color.green;
            backStaminaBar.fillAmount=hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            frontStaminaBar.fillAmount=Mathf.Lerp(fillF,backStaminaBar.fillAmount,percentComplete);
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
    public void TakStaminaDamage(int damage)
    {
        currentStamina=currentStamina-damage;
        lerpTimer=0f;
    }

    public void RestoreStamina(float healAmount)
    {
        currentStamina+=healAmount;
        lerpTimer=0f;
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

    private float SetMaxStamina()
    {
        maxStamina=StaminaLevel*10;
        return maxStamina;
    }
}
