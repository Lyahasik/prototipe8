using UnityEngine;

public interface ICharacter
{
	int GetCharge();
	int GetBurstSize();
	void Shooting();
	void Jerk(GameObject objectCollision);
}