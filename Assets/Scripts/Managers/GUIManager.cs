using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : Singleton<GUIManager>
{
	private float				__maxHealthBarLength;

	private Canvas              __canvas;
	private Image               __overlay;
	private Image               __healthBarImage;
	private Image               __generalPanel;
	private Text                __generalText;
	private Text                __scoreCount;
	private Text                __killCount;

	private int                 __kills;
	private int                 __score;

	public int Kills
	{
		get
		{
			return __kills;
		}
		set
		{
			__kills = value;

			__killCount.text = "" + __kills;
		}
	}

	public int Score
	{
		get
		{
			return __score;
		}
		set
		{
			__score = value;

			__scoreCount.text = "" + __score;
		}
	}


	void Awake()
	{
		Application.LoadLevelAdditive("InGameUI");
	}

	void Start()
	{

	}

	void Update()
	{
		if(__canvas == null)
			Initialize();
	}

	public void Initialize()
	{
		__canvas = GameObject.FindObjectOfType<Canvas>();

		if(__canvas == null)
			return;

		__healthBarImage = __canvas.gameObject.transform.FindChild("HealthPanel/HPBar").GetComponent<Image>();
		__scoreCount = __canvas.gameObject.transform.FindChild("ScorePanel/Score").GetComponent<Text>();
		__killCount = __canvas.gameObject.transform.FindChild("ScorePanel/Kills").GetComponent<Text>();
		__generalPanel = __canvas.gameObject.transform.FindChild("GeneralPanel").GetComponent<Image>();
		__generalText = __generalPanel.transform.FindChild("Text").GetComponent<Text>();
		__overlay = __canvas.transform.FindChild("Overlay").GetComponent<Image>();

		__generalPanel.gameObject.SetActive(false);
		__overlay.color = new Color(0f, 0f, 0f, 0f);
		__overlay.gameObject.SetActive(false);

		__maxHealthBarLength = __healthBarImage.rectTransform.rect.width;

		ready = true;
	}

	public void PlayerHealthChanged()
	{
		float healthBarLength = (float)PlayerMovementManager.Instance.player.currentHealth / (float)PlayerMovementManager.Instance.player.startHealth;
		healthBarLength = healthBarLength *__maxHealthBarLength;
		__healthBarImage.rectTransform.sizeDelta = new Vector2(healthBarLength, __healthBarImage.rectTransform.rect.height);
	}

	public void ChangeOverlayTransparency(float newAlpha)
	{
		Debug.Log("> a: " + newAlpha);
		Color col = new Color(0f, 0f, 0f, newAlpha);
		__overlay.color = col;
	}

	public void EndGame(bool victory = false)
	{
		__generalPanel.gameObject.SetActive(true);
		__generalText.text = (victory ? "Victory!" : "Defeat");

		if(victory)
			__overlay.color = Color.blue;
		else
			__overlay.color = Color.red;

		__overlay.gameObject.SetActive(true);

		//Hashtable args = new Hashtable();
		//args.Add("from", __overlay.color.a);
		//args.Add("to", 255f);
		//args.Add("time", 2f);
		//args.Add("onupdate", "ChangeOverlayTransparency");
		//args.Add("onupdatetarget", gameObject);

		//iTween.ValueTo(gameObject, args);
	}
}
