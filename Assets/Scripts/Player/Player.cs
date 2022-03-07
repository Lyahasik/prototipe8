using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IUpdate
{
    public Image ImageHealth;
    public float MaxHealth = 100.0f;
    public float CurrentHealth = 100.0f;
    [Space]

    public Image ImageEnergy;
    public float MaxEnergy = 100.0f;
    public float CurrentEnergy = 100.0f;
    [Space]

    public Image ImageCharge;
    public float EnergyRecoveryRate = 30.0f;

    private Animator _animatorCharacter;

    private Move _componentMove;

    public void LaunchUpdate()
    {
        RecoveryEnergy();
    }

    public void SetComponentMove(Move componentMove)
    {
        _componentMove = componentMove;
    }

    public void SetAnimatorCharacter(Animator animatorCharacter)
    {
        _animatorCharacter = animatorCharacter;
    }

    public void TakeDamage(float valueHealth)
    {
        _componentMove.Freeze(0.5f);

        CurrentHealth -= valueHealth;

        ImageHealth.fillAmount = CurrentHealth / MaxHealth;

        _animatorCharacter.SetBool("RunForward", false);
        _animatorCharacter.SetBool("RunBackward", false);
        _animatorCharacter.SetBool("RunLeft", false);
        _animatorCharacter.SetBool("RunRight", false);

        _animatorCharacter.SetTrigger("TakeDamage");

        if (CurrentHealth <= 0)
		{
            _animatorCharacter.SetTrigger("Death");
            _componentMove.Freeze(1000.0f);
        }
    }

    public void WasteOfEnergy(float valueEnergy)
	{
        if (valueEnergy <= CurrentEnergy)
        {
            CurrentEnergy -= valueEnergy;

            ImageEnergy.fillAmount = CurrentEnergy / MaxEnergy;
            UpdateAmountCharge();
        }
	}

    public void RecoveryEnergy()
	{
        if (CurrentEnergy < MaxEnergy)
		{
            CurrentEnergy += EnergyRecoveryRate * Time.deltaTime;

            if (CurrentEnergy > MaxEnergy)
			{
                CurrentEnergy = MaxEnergy;
            }

            ImageEnergy.fillAmount = CurrentEnergy / MaxEnergy;
            UpdateAmountCharge();
        }
	}

    private void UpdateAmountCharge()
    {
        if (ImageEnergy.fillAmount == 1.0f)
        {
            ImageCharge.fillAmount = ImageEnergy.fillAmount;
        }
        else if (ImageEnergy.fillAmount > 0.666f)
        {
            ImageCharge.fillAmount = 0.666f;
        }
        else if (ImageEnergy.fillAmount > 0.333f)
        {
            ImageCharge.fillAmount = 0.333f;
        }
        else
        {
            ImageCharge.fillAmount = 0.0f;
        }
    }
}
