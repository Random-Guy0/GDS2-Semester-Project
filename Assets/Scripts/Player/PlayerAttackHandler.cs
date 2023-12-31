using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttackHandler : AttackHandler
{ 
    [field: SerializeField] public List<PlayerWeapon> Weapons { get; private set; }
    private PlayerWeapon _selectedWeapon;
    public PlayerWeapon SelectedWeapon
    {
        get => _selectedWeapon;
        set
        {
            _selectedWeapon = value;
            foreach (PlayerWeapon weapon in Weapons)
            {
                weapon.Sprite.enabled = false;
            }

            value.Sprite.enabled = true;
        }
    }
    
    [SerializeField] private Animator animator;
    private bool doSecondAttack = false;
    private PlayerMovement playerMovement;
    private AmmoController ammoController;

    private bool bufferAttack = false;

    [SerializeField] private AnimationClip meleeAttack1Animation;
    [SerializeField] private AnimationClip meleeAttack2Animation;
    private float meleeAttack1Speed;
    private float meleeAttack2Speed;

    public bool CarryingBubble { get; private set; } = false;
    private BubbledEnemy grabbedBubble;
    [SerializeField] private float bubbleReleaseForce = 2f;
    
    public bool AttackButtonDown { get; private set; }

    // Weapon UI animator
    [SerializeField] private Animator weaponsUIanimator;
    [SerializeField] private float weaponSwitchDelay = 0.1f;

    [SerializeField] private Slider bathBombSlider;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ammoController = GetComponent<AmmoController>();

        meleeAttack1Speed = meleeAttack1Animation.length / MeleeAttacks[0].Duration;
        meleeAttack2Speed = meleeAttack2Animation.length / MeleeAttacks[0].Duration;
        animator.SetFloat("MeleeAttack1Speed", meleeAttack1Speed);
        animator.SetFloat("MeleeAttack2Speed", meleeAttack2Speed);

        SelectedWeapon = Weapons[0];

        bathBombSlider.maxValue = Weapons[2].Attack.Duration;

        foreach (PlayerWeapon weapon in Weapons)
        {
            weapon.weaponAttackSound.EventReference = weapon.Attack.AttackSound;
        }
    }

    private void Update()
    {
        if (SelectedWeapon.Attack is ContinuousRangedAttack attack)
        {
            if (AttackButtonDown && ammoController.CanUseAmmo(attack.AmmoCost))
            {
                if (!SelectedWeapon.weaponAttackSound.IsPlaying())
                {
                    SelectedWeapon.weaponAttackSound.Play();
                }
            }
            else
            {
                SelectedWeapon.weaponAttackSound.Stop();
            }
        }
    }

    public void DoAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            if (!AttackButtonDown)
            {
                AttackButtonDown = true;
                animator.ResetTrigger("InterruptAttack");
                
                if (SelectedWeapon.Attack is MeleeAttack meleeAttack)
                {
                    int index = Array.IndexOf(MeleeAttacks, meleeAttack);
                    DoMeleeAttack(index);
                }
                else if (SelectedWeapon.Attack is RangedAttack rangedAttack)
                {
                    int index = Array.IndexOf(RangedAttacks, rangedAttack);
                    DoRangedAttack(index);
                }
            }
            else
            {
                AttackButtonDown = false;
            }
        }
    }

    public override void DoMeleeAttack(int index = 0)
    {
        bufferAttack = CurrentlyAttacking;
        
        if (!bufferAttack)
        {
            if (!doSecondAttack)
            {
                animator.SetTrigger("DoMeleeAttack");
            }
            else
            {
                animator.SetTrigger("DoSecondMeleeAttack");
            }

            animator.SetBool("Attacking", true);

            base.DoMeleeAttack(index);
        }
    }

    public override void DoRangedAttack(int index = 0)
    {
        if (ammoController.CanUseAmmo(RangedAttacks[index].AmmoCost))
        {
            if (RangedAttacks[index] is ChargedRangedAttack)
            {
                StartCoroutine(BathBombCharge());
            }
            else if (RangedAttacks[index] is not ContinuousRangedAttack)
            {
                SelectedWeapon.weaponAttackSound.Play();
            }
            
            ammoController.UseAmmo(RangedAttacks[index].AmmoCost);
            base.DoRangedAttack(index);
        }
    }

    protected override IEnumerator WaitForAttack(float attackDuration)
    {
        if (SelectedWeapon.Attack is not RangedAttack)
        {
            SelectedWeapon.weaponAttackSound.Play();
            playerMovement.CanMove = false;
        }

        yield return base.WaitForAttack(attackDuration);
        playerMovement.CanMove = true;
        if (bufferAttack)
        {
            doSecondAttack = bufferAttack && !doSecondAttack;
            bufferAttack = false;
            DoMeleeAttack();
        }
        else
        {
            while (AttackButtonDown && SelectedWeapon == Weapons[3])
            {
                yield return null;
            }
            InterruptAttack();
        }

        if (!doSecondAttack)
        {
            animator.ResetTrigger("DoMeleeAttack");
        }
        else if(!bufferAttack)
        {
            animator.ResetTrigger("DoSecondMeleeAttack");
        }
    }

    public override Vector2 GetDirection()
    {
        Vector2 direction = playerMovement.Direction;
        if (direction.x == 0f)
        {
            direction.x = 1f;
        }
        return direction;
    }

    public override void InterruptAttack()
    {
        base.InterruptAttack();
        if (SelectedWeapon.Attack is MeleeAttack)
        {
            animator.SetTrigger("InterruptAttack");
            animator.SetBool("Attacking", false);
        }
    }

    public void GrabBubble(BubbledEnemy bubble)
    {
        if (!CarryingBubble)
        {
            CarryingBubble = true;
            bubble.Grab(this);
            grabbedBubble = bubble;
        }
        else
        {
            ReleaseBubble();
        }
    }

    public void ReleaseBubble()
    {
        CarryingBubble = false;
        grabbedBubble.Release(GetDirection() * bubbleReleaseForce);
        grabbedBubble = null;
    }

    public void HeldBubblePopped()
    {
        CarryingBubble = false;
        grabbedBubble = null;
    }

    protected override Vector2 GetAttackOrigin()
    {
        return SelectedWeapon.AttackOrigin.position;
    }

    public bool UseAmmo(int amount)
    {
        if (ammoController.CanUseAmmo(amount))
        {
            ammoController.UseAmmo(amount);
            return true;
        }

        return false;
    }

    private IEnumerator SelectWeapon(int index)
    {
        if ((AttackButtonDown && SelectedWeapon.Attack is ContinuousRangedAttack) || SelectedWeapon == Weapons[index] || CurrentlyAttacking)
        {
            yield break;
        }

        if (CarryingBubble)
        {
            ReleaseBubble();
        }

        yield return new WaitForSeconds(weaponSwitchDelay);
        
        SelectedWeapon = Weapons[index];
        weaponsUIanimator.SetTrigger("Weapon" + index);
    }

    public void SelectWeapon1(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            StartCoroutine(SelectWeapon(0));
        }
    }
    
    public void SelectWeapon2(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            StartCoroutine(SelectWeapon(1));
        }
    }
    
    public void SelectWeapon3(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            StartCoroutine(SelectWeapon(2));
        }
    }
    
    public void SelectWeapon4(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            StartCoroutine(SelectWeapon(3));
        }
    }

    public void CycleWeapon(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.Instance.Paused)
        {
            float direction = context.ReadValue<float>();
            int currentWeaponIndex = Weapons.IndexOf(SelectedWeapon);

            if (direction > 0f)
            {
                if (currentWeaponIndex == Weapons.Count - 1)
                {
                    StartCoroutine(SelectWeapon(0));
                }
                else
                {
                    StartCoroutine(SelectWeapon(currentWeaponIndex + 1));
                }
            }
            else if (direction < 0f)
            {
                if (currentWeaponIndex == 0)
                {
                    StartCoroutine(SelectWeapon(Weapons.Count - 1));
                }
                else
                {
                    StartCoroutine(SelectWeapon(currentWeaponIndex - 1));
                }
            }
        }
    }

    private IEnumerator BathBombCharge()
    {
        float chargeTime = 0f;
        bathBombSlider.gameObject.SetActive(true);
        
        while (AttackButtonDown && chargeTime <= SelectedWeapon.Attack.Duration)
        {
            chargeTime += Time.deltaTime;
            bathBombSlider.value = chargeTime;
            yield return null;
        }
        
        SelectedWeapon.weaponAttackSound.Play();
        bathBombSlider.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class PlayerWeapon
{
    [field: SerializeField] public Attack Attack { get; private set; }
    [field: SerializeField] public bool Unlocked { get; set; } = true;
    [field: SerializeField] public SpriteRenderer Sprite { get; private set; }
    [field: SerializeField] public Transform AttackOrigin { get; private set; }
    [field: SerializeField] public FMODUnity.StudioEventEmitter weaponAttackSound { get; private set; }

    public PlayerWeapon(Attack attack)
    {
        Attack = attack;
    }
}
