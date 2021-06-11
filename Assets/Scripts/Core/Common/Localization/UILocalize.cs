using UnityEngine;
using UnityEngine.UI; 
[ExecuteInEditMode]
[AddComponentMenu("UGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	public string key;

	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				Text lbl = GetComponent<Text>();
				if (lbl != null)
				{
					InputField input = ToolKit.FindInParents<InputField>(lbl.gameObject.transform);
					if (input != null && input.textComponent == lbl) input.text = value;
					else lbl.text = value;
#if UNITY_EDITOR
					if (!Application.isPlaying) ToolKit.SetDirty(lbl);
#endif
				}
			}
		}
	}

	bool mStarted = false;

	void OnEnable ()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying) return;
#endif
		if (mStarted) OnLocalize();
	}

	void Start ()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying) return;
#endif
		mStarted = true;
		OnLocalize();
	}

	void OnLocalize ()
	{
		if (string.IsNullOrEmpty(key))
		{
			Text lbl = GetComponent<Text>();
			if (lbl != null) key = lbl.text;
		}
		if (!string.IsNullOrEmpty(key)) value = Localization.Get(key);
	}
}
