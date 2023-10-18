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