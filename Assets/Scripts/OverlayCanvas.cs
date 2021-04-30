using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCanvas : MonoBehaviour
{
	// Make global
	public static OverlayCanvas Instance
	{
		get;
		set;
	}

	private void Awake()
	{
		Instance = this;
		int overlayCanvasCount = FindObjectsOfType<OverlayCanvas>().Length;
		if (overlayCanvasCount > 1)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
