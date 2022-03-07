using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
	private float _damage;

	private void Update()
	{
		transform.Translate(new Vector3(0.0f, 0.0f, 1.0f * _speed) * Time.deltaTime);
	}

    public void SetSpeed(float speed)
	{
        _speed = speed;
	}

    public void SetDamage(float damage)
	{
        _damage = damage;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
			Destroy(gameObject, 0.0f);
		}
		else if (collision.gameObject.tag == "bot")
		{
			collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
			Destroy(gameObject, 0.0f);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Zone")
		{
			Destroy(gameObject, 0.0f);
		}
	}
}
