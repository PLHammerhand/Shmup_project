using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float rotationSpeedX;
    public float rotationSpeedY;
    public float rotationSpeedZ;

    void Update()
    {
        __Rotate();
	}

    private void __Rotate()
    {
		gameObject.GetComponent<Rigidbody>().AddTorque(rotationSpeedX, rotationSpeedY, rotationSpeedZ, ForceMode.Acceleration);
    }
}
