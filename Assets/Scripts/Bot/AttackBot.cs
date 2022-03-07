using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBot : MonoBehaviour, IUpdate
{
	private Player _componentPlayer;

	private Animator _animatorCharacter;
	private ICharacter _componentICharacter;

	private Move _componentMove;

	private float _energyShot;
	private float _burstSize;

	public void LaunchUpdate()
	{
		if (_timeFreeze > 0.0f)
		{
			Defrost();
		}
		else
		{
			AttackActivation();
		}
	}

	public void SetAnimatorCharacter(Animator animatorCharacter)
	{
		_animatorCharacter = animatorCharacter;
	}

	public void SetICharacter(ICharacter componentICharacter)
	{
		_componentICharacter = componentICharacter;
		_energyShot = _componentPlayer.MaxEnergy / _componentICharacter.GetCharge();
		_burstSize = _componentICharacter.GetBurstSize();
	}

	public void SetComponentPlayer(Player componentPlayer)
	{
		_componentPlayer = componentPlayer;
	}
	public void SetComponentMove(Move componentMove)
	{
		_componentMove = componentMove;
	}

	#region attack

	private void AttackActivation()
	{
		if (_componentPlayer.CurrentEnergy >= _energyShot)
		{
			Freeze(1.3f);
			_componentMove.Freeze(1.3f);

			ShotAnimation();
			_componentICharacter.Shooting();

			_componentPlayer.WasteOfEnergy(_energyShot);
		}
	}

	private void ShotAnimation()
	{
		switch (_burstSize)
		{
			case 1:
				_animatorCharacter.SetTrigger("Shot01");
				break;
			case 2:
				_animatorCharacter.SetTrigger("Shot02");
				break;
			case 3:
				_animatorCharacter.SetTrigger("Shot03");
				break;
			default:
				Debug.Log("Attack : Not found AttackAnimation");
				break;
		}
	}

#endregion attack

	#region freeze

	private float _timeFreeze = 0.0f;

	public void Freeze(float timeSeconds)
	{
		_timeFreeze = timeSeconds;
	}

	private void Defrost()
	{
		if (_timeFreeze > 0.0f)
		{
			_timeFreeze -= 1.0f * Time.deltaTime;
		}
	}

	#endregion freeze
}
