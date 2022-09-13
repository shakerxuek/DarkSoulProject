using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public WeaponItem currentweapon;
    public bool isLeftHandSlot;
    public bool isRightHandSlot;
    public bool isBackSlot;

    public GameObject currentWeaponModel;

    public void Unloadweapon()
    {
        if(currentWeaponModel !=null)
        {
            currentWeaponModel.SetActive(false);
        }
    }

    public void UnloadweaponAndDestroy()
    {
        if(currentWeaponModel!=null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItem weaponItem)
    {   
        UnloadweaponAndDestroy();
        if(weaponItem == null)
        {   
            Unloadweapon();
            return;
        }

        GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
        if(model !=null)
        {
            if(parentOverride!=null)
            {
                model.transform.parent=parentOverride;
            }
            else
            {
                model.transform.parent=transform;
            }

            model.transform.localPosition=Vector3.zero;
            model.transform.localRotation=Quaternion.identity;
            model.transform.localScale=Vector3.one;
        }

        currentWeaponModel=model;
    }
}
