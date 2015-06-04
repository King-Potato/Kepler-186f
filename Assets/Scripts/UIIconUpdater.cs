using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIIconUpdater : MonoBehaviour {
	public Ship ship;
	public static Dictionary<Ship, UIIconUpdater> instance = new Dictionary<Ship, UIIconUpdater>();

	public void Start()
	{
		instance.Add(ship, this);
	}

	public static void SetImage(Ship ship, Sprite sprite)
	{
		UIIconUpdater instance = new UIIconUpdater();
		if (UIIconUpdater.instance.TryGetValue(ship, out instance))
		{
			Image image = instance.transform.FindChild("Image").GetComponent<Image>();
			if (image)
				image.sprite = sprite;
		}
	}

}
