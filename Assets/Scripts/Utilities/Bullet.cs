using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[HideInInspector]
	public float		bulletSpeed;
	[HideInInspector]
	public float		bulletLifetime;
	[HideInInspector]
	public int          bulletDamage;


	void Update()
	{
		if(TimeManager.Instance.gameplayState)
		{
            gameObject.transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);
			bulletLifetime -= Time.fixedDeltaTime;

			if(bulletLifetime <= 0f)
				gameObject.SetActive(false);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		Character character = other.gameObject.GetComponent<Character>();

		if(character != null)
		{
			character.Damage(bulletDamage);
			gameObject.SetActive(false);
		}
	}
}
