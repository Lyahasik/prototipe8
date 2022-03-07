using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, ICharacter
{
	public int Charge = 3;
	public GameObject Weapon;
	[Space]

	public float BulletDamage = 10.0f;
	public float BulletSpeed = 7.0f;
	public GameObject Bullet;
	[Space]

	[Range (1, 3)]
	public int BurstSize = 3;
	public float AngleScatter = 30.0f;
	[Space]

	public float LeveledDamage = 13.0f;

	private Vector3 _muzzleShift;

	private float _delayShot;
	private int _burstBullets;

	private float _halfAngleScatter;

	private void Start()
	{
		_halfAngleScatter = AngleScatter * 0.5f;
	}

	public void Update()
	{
		if (_burstBullets > 0)
		{
			if (_delayShot <= 0.0f)
			{
				Shot();
				_burstBullets--;
				_delayShot = 0.4f;
			}
		}

		ReduceDelay();
	}

	public int GetCharge()
	{
		return Charge;
	}

	public int GetBurstSize()
	{
		return BurstSize;
	}

	public void Shooting()
	{
		_burstBullets = BurstSize;
	}

	private void Shot()
	{
		Transform barrel = Weapon.transform.Find("Barrel");

		Quaternion _scatter = new Quaternion(0.0f, barrel.rotation.y, 0.0f, barrel.rotation.w) * Quaternion.Euler(0.0f, Random.Range(-_halfAngleScatter, _halfAngleScatter), 0.0f);
		GameObject _bullet = Instantiate(Bullet, barrel.position, _scatter);

		Bullet _componentBullet = _bullet.GetComponent<Bullet>();
		_componentBullet.SetDamage(BulletDamage);
		_componentBullet.SetSpeed(BulletSpeed);
	}


	private void ReduceDelay()
	{
		if (_delayShot > 0.0f)
		{
			_delayShot -= 1.0f * Time.deltaTime;
		}
	}

	public void Jerk(GameObject objectCollision)
	{
		objectCollision.GetComponent<Player>().TakeDamage(LeveledDamage);
	}
}
