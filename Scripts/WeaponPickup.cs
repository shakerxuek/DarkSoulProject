using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : Interactable
{
    public WeaponItem weapon;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerLocamotion playerLocamotion;
        AnimatorHandler animatorHandler;

        playerInventory=playerManager.GetComponent<PlayerInventory>();
        playerLocamotion=playerManager.GetComponent<PlayerLocamotion>();
        animatorHandler=playerManager.GetComponentInChildren<AnimatorHandler>();

        playerLocamotion.rigidbody.velocity =Vector3.zero;
        animatorHandler.PlayTargetAnimation("Pick Item",true);
        playerInventory.weaponInventory.Add(weapon);
        playerManager.IteminteractableGameObject.GetComponentInChildren<Text>().text =weapon.itemName + " (press F again to close the window)";
        playerManager.IteminteractableGameObject.GetComponentInChildren<RawImage>().texture=weapon.itemIcon.texture;
        playerManager.IteminteractableGameObject.SetActive(true);
        playerInventory.weaponInRightHandSlots[0]=playerInventory.weaponInventory[0];
        Destroy(gameObject);
    }
}
