using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/WeaponItem")]
public class WeaponItem : Item
{   
    public GameObject modelPrefab;
    public bool isUnarmed;
    public string OH_Light_Attack_1;
    public string OH_Light_Attack_2;
    public string OH_Heavy_Attack_1;

    public string Right_hand_idle;
    public string Left_hand_idle;
    public string twohand_idle;

    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;
}
