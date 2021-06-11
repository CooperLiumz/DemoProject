using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("UGUI/Internal/Localization")]
public class Localization : System.Object
{
	private static Localization mInstance;
	private static readonly object mStaticSyncRoot = new object();
	public static Localization instance
	{
		get
		{
			if (mInstance == null)
			{
				lock (mStaticSyncRoot) {
					mInstance = new Localization();
				}
			}
			return mInstance;
		}
	}  
	static public bool localizationHasBeenSet = false;
	static Dictionary<string, string> MyDictionary = new Dictionary<string, string>();
	static string mLanguage;
	
	static public void Set (string languageName, Dictionary<string, string> dictionary)
	{
		mLanguage = languageName;
		PlayerPrefs.SetString("Language", mLanguage);
		MyDictionary = dictionary;
		localizationHasBeenSet = true;
	}

	static public string Get (string key)
	{
		if (!localizationHasBeenSet) 
		{
			mLanguage = PlayerPrefs.GetString("Language", "translate_zh_CN");
			localizationHasBeenSet = true;
		}
		string val;
		if (MyDictionary.TryGetValue(key, out val)) return val;
#if UNITY_EDITOR
		Debug.LogWarning("Localization key not found: '" + key + "'");
#endif
		return key;
	}
}
