using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weaponSheath;
    [SerializeField] GameObject weapon;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    public Targeter targeter;
    public Animator animator;

    private bool weaponOnhand;

    public bool isAttacking;


    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        targeter = GetComponentInChildren<Targeter>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckIfWeopanShouldBeDrawn();
    }
    public void WithdrawWeapon()
    {
 
            currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
            Destroy(currentWeaponInSheath);
            weaponOnhand = true;
        
        
    }

    public void SheathWeapon()
    {
        
        
            currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
            Destroy(currentWeaponInHand);
            weaponOnhand = false;
        

    }

    public void CheckIfWeopanShouldBeDrawn()
    {
        if (targeter.targets.Count != 0 && weaponOnhand == false) //enemies nearby and sword not in hand
        {
            animator.SetTrigger("DrawWeapon");
            weaponOnhand = true;
        }
        else if(targeter.targets.Count != 0 && weaponOnhand == true)
        {
            return;
        }
        else if (targeter.targets.Count == 0) //enemies are not nearby
        {
            if(isAttacking && weaponOnhand == false) //player is attacking 
            {
                animator.SetTrigger("DrawWeapon");
                weaponOnhand = true;
            }
            else if(!isAttacking && weaponOnhand == true)
            {
                animator.SetTrigger("UndrawWeapon");
                weaponOnhand = false;
            }
        }
    }

}
