using UnityEngine;
using System.Collections;

public class Hooverer : MonoBehaviour
{
    public float		hooverDistanceX;
    public float		hooverDistanceY;
    public float		hooverSpeed;

	[HideInInspector]
	public bool hooverState
	{
		get
		{
			return __hooverState;
		}
		set
		{
			__hooverState = value;

			if(value)
				iTween.Resume("hooveringTween" + gameObject.GetInstanceID());
			else
				iTween.Pause("hooveringTween" + gameObject.GetInstanceID());
		}
	}

	private bool        __hooverState           = true;

	private Vector3		__startingPosition;
	
    void Start()
    {
		__startingPosition = gameObject.transform.position;

		Hashtable args = new Hashtable();

		if(hooverDistanceX != 0 || hooverDistanceY != 0)
		{
			Vector3 posX = new Vector3(__startingPosition.x + hooverDistanceX, __startingPosition.y + hooverDistanceY, __startingPosition.z);
			args.Add("name", "hooveringTween" + gameObject.GetInstanceID());
			args.Add("position", posX);
			args.Add("easetype", iTween.EaseType.easeInOutCubic);
			args.Add("speed", hooverSpeed);
			args.Add("oncomplete", "Hoover");
			args.Add("oncompletetarget", gameObject);
			iTween.MoveTo(gameObject, args);
		}
	}

	public void Hoover()
	{
		Vector3 pos = Vector3.zero;

		if((__startingPosition.x - hooverDistanceX) == gameObject.transform.position.x && (__startingPosition.y - hooverDistanceY) == gameObject.transform.position.y)
			pos = new Vector3(__startingPosition.x + hooverDistanceX, __startingPosition.y + hooverDistanceY, __startingPosition.z);
		else if((__startingPosition.x + hooverDistanceX) == gameObject.transform.position.x && (__startingPosition.y + hooverDistanceY) == gameObject.transform.position.y)
			pos = new Vector3(__startingPosition.x - hooverDistanceX, __startingPosition.y - hooverDistanceY, __startingPosition.z);

		if(pos != Vector3.zero)
		{
			Hashtable args = new Hashtable();
			args.Add("name", "hooveringTween");
			args.Add("position", pos);
			args.Add("easetype", iTween.EaseType.easeInOutCubic);
			args.Add("speed", hooverSpeed);
			args.Add("oncomplete", "Hoover");
			args.Add("oncompletetarget", gameObject);
			iTween.MoveTo(gameObject, args);
		}
	}
}
