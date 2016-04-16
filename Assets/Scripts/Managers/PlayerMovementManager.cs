using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementManager : PlayerManager<PlayerMovementManager>
{
	public float				forwardSpeed				= 350f;
	public float				sideSpeed					= 275f;
	public float				backwardSpeed               = 225f;

	private bool                __enablePlayerControl                   = true;
	private GameObject			__mainCamera;


	public bool enablePlayerControl
	{
		get
		{
			return __enablePlayerControl;
		}
		set
		{
			__enablePlayerControl = value;

			PlayerWeaponsManager.Instance.allowFire = value;
		}
	}


	void Start()
	{
		__mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void FixedUpdate()
	{
		if(enablePlayerControl && TimeManager.Instance.gameplayState)
		{
			if(Input.GetAxis("Vertical") < 0)
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * Time.fixedDeltaTime * forwardSpeed, ForceMode.Acceleration);
			else if(Input.GetAxis("Vertical") > 0)
				gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.forward * Time.fixedDeltaTime * backwardSpeed, ForceMode.Acceleration);

			if(Input.GetAxis("Horizontal") > 0)
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * Time.fixedDeltaTime * sideSpeed, ForceMode.Acceleration);
			else if(Input.GetAxis("Horizontal") < 0)
				gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.right * Time.fixedDeltaTime * sideSpeed, ForceMode.Acceleration);

			//	TODO
			//	Study the code
			Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			transform.LookAt(mouse, transform.up);

			//gameObject.transform.LookAt(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
			//											Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f), Vector3.right);

			//Debug.Log("\t>gameObject.transform.right: " + gameObject.transform.right);

			//if(Input.GetButtonDown("Stop"))
		}
	}

	void LateUpdate()
	{
		Vector3 vec = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, __mainCamera.transform.position.z);
		__mainCamera.transform.position = vec;
	}

	public void ChangePlayerMovementState(bool state)
	{
		enablePlayerControl = state;
	}
}
