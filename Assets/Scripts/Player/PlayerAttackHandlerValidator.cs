#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAttackHandler))]
public class PlayerAttackHandlerValidator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerAttackHandler playerAttackHandler = (PlayerAttackHandler)target;
        
        EditorGUI.BeginChangeCheck();
        
        /*if (playerAttackHandler.MeleeAttacksUnlocked.Count > playerAttackHandler.MeleeAttacks.Length)
        {
            int count = playerAttackHandler.MeleeAttacksUnlocked.Count - playerAttackHandler.MeleeAttacks.Length;
            playerAttackHandler.MeleeAttacksUnlocked.RemoveRange(playerAttackHandler.MeleeAttacks.Length, count);
        }
        else if (playerAttackHandler.MeleeAttacksUnlocked.Count < playerAttackHandler.MeleeAttacks.Length)
        {
            for (int i = playerAttackHandler.MeleeAttacksUnlocked.Count; i < playerAttackHandler.MeleeAttacks.Length; i++)
            {
                playerAttackHandler.MeleeAttacksUnlocked.Add(true);
            }
        }
        
        if (playerAttackHandler.RangedAttacksUnlocked.Count > playerAttackHandler.RangedAttacks.Length)
        {
            int count = playerAttackHandler.RangedAttacksUnlocked.Count - playerAttackHandler.RangedAttacks.Length;
            playerAttackHandler.RangedAttacksUnlocked.RemoveRange(playerAttackHandler.RangedAttacks.Length, count);
        }
        else if (playerAttackHandler.RangedAttacksUnlocked.Count < playerAttackHandler.RangedAttacks.Length)
        {
            for (int i = playerAttackHandler.RangedAttacksUnlocked.Count; i < playerAttackHandler.RangedAttacks.Length; i++)
            {
                playerAttackHandler.RangedAttacksUnlocked.Add(true);
            }
        }*/

        List<Attack> attacks = new List<Attack>();
        attacks.AddRange(playerAttackHandler.MeleeAttacks);
        attacks.AddRange(playerAttackHandler.RangedAttacks);
        foreach (Attack attack in attacks)
        {
            bool hasAttack = false;
            foreach (PlayerWeapon weapon in playerAttackHandler.Weapons)
            {
                if (weapon.Attack == attack)
                {
                    hasAttack = true;
                    break;
                }
            }

            if (!hasAttack)
            {
                PlayerWeapon weapon = new PlayerWeapon(attack);
                playerAttackHandler.Weapons.Add(weapon);
            }
        }

        for(int i = 0; i < playerAttackHandler.Weapons.Count; i++)
        {
            if (!attacks.Contains(playerAttackHandler.Weapons[i].Attack))
            {
                playerAttackHandler.Weapons.RemoveAt(i);
                i--;
            }
        }

        EditorGUI.EndChangeCheck();
    }
}
#endif