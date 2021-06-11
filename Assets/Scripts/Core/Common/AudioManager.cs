using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class QueueAudioSource : System.Object
{
    public Queue<GameObject> m_FreeLib = new Queue<GameObject>();
}

public class AudioManager : MonoBehaviour
{

    private static AudioManager mInstance;
    private static readonly object mStaticSyncRoot = new object();

    private Dictionary<string, AudioClip> mAudioPool = new Dictionary<string, AudioClip>();

    private Dictionary<string, QueueAudioSource> g_AudioSourcePool = new Dictionary<string, QueueAudioSource>();
    private List<GameObject> mAudioSourcePool = new List<GameObject>();
    private Dictionary<string, GameObject> mPrefabs = new Dictionary<string, GameObject>();

    private static float mVolumeSFX;
    private static float mVolumeBGM;

    private static float mVolumeSFXMax = 1.0f;
    private static float mVolumeBGMMax = 0.5f;

    private AudioSource mPlayingBGM;

    private AudioManager() { }

    public static AudioManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                lock (mStaticSyncRoot)
                {
                    if (mInstance == null)
                    {
                        GameObject singleton = GameObject.Find("UIManager");
                        if (singleton == null)
                        {
                            singleton = new GameObject("UIManager");
                        }
                        mInstance = singleton.AddComponent<AudioManager>();

                        if (PlayerPrefs.HasKey("VolumeSFX"))
                        {
                            mVolumeSFX = PlayerPrefs.GetFloat("VolumeSFX");
                        }
                        else
                        {
                            mVolumeSFX = mVolumeSFXMax;
                            PlayerPrefs.SetFloat("VolumeSFX", mVolumeSFX);
                        }
                        if (PlayerPrefs.HasKey("VolumeBGM"))
                        {
                            mVolumeBGM = PlayerPrefs.GetFloat("VolumeBGM");
                        }
                        else
                        {
                            mVolumeBGM = mVolumeBGMMax;
                            PlayerPrefs.SetFloat("VolumeBGM", mVolumeBGM);
                        }
                        mInstance.g_AudioSourcePool.Clear();
                    }
                }
            }
            return mInstance;
        }
    }

    public float VolumeSFX
    {
        get
        {
            return mVolumeSFX;
        }
        set
        {
            mVolumeSFX = value;
        }
    }

    public float VolumeBGM
    {
        get
        {
            return mVolumeBGM;
        }
        set
        {
            mVolumeBGM = value;
            mPlayingBGM.volume = value;
        }
    }

    #region 播放StreamingAssets目录下文件

    //public void PlayStreamBGM(string _filename, string _sourcename, bool _loop = true)
    //{
    //    StartCoroutine(SyncPlayStreamBGM(_filename, _sourcename, _loop));
    //}

    //private IEnumerator SyncPlayStreamBGM(string _filename, string _sourcename, bool _loop = true)
    //{
    //    yield return null;
    //    AudioSource audioSource = GetAudioSource(_sourcename).GetComponent<AudioSource>();
    //    audioSource.priority = 0;

    //    yield return LoadStreamAssetSounds(_filename);

    //    AudioClip clip = mAudioPool[_filename];
    //    audioSource.clip = clip;
    //    audioSource.volume = mVolumeBGM;
    //    audioSource.loop = _loop;
    //    audioSource.Play();
    //    mPlayingBGM = audioSource;
    //}

    //IEnumerator LoadStreamAssetSounds(string _filename)
    //{
    //    yield return null;

    //    AudioClip clip = null;
    //    if (mAudioPool.ContainsKey(_filename))
    //    {
    //        clip = mAudioPool[_filename];
    //    }
    //    else
    //    {
    //        WWW www = new WWW(string.Concat(Application.streamingAssetsPath, "/Sounds/", _filename));

    //        yield return www;

    //        if (!string.IsNullOrEmpty(www.error))
    //        {
    //            Debug.Log("Load Stream Asset Sounds Fail !  " + _filename);
    //        }
    //        else
    //        {
    //            Debug.Log("Load Stream Asset Sounds Success !  " + _filename);
    //            clip = www.GetAudioClip();
    //            clip.name = _filename;
    //            mAudioPool[_filename] = clip;
    //        }
    //    }
    //}

    #endregion
    
    #region 播放本地视频

    public void PlayLocalSFX(string _fileName, float _delay = 0)
    {
        PlayLocalSFX(_fileName, "AudioSourceBGM", _delay);
    }

    private void PlayLocalSFX(string _filename, string _sourcename, float _delay = 0)
    {
        AudioSource audioSource = GetAudioSource(_sourcename).GetComponent<AudioSource>();

        AudioClip clip = null;
        if (mAudioPool.ContainsKey(_filename))
        {
            clip = mAudioPool[_filename];
        }
        else
        {
            clip = Resources.Load(string.Concat("Sounds/",_filename), typeof(AudioClip)) as AudioClip;
            mAudioPool[_filename] = clip;
        }
        audioSource.volume = mVolumeSFX;
        if (_delay == 0)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = clip;
            audioSource.PlayDelayed(_delay);
        }
    }

    #endregion

    public float GetSFXTime(string _fileName)
    {
        if (mAudioPool.ContainsKey(_fileName))
        {
            return mAudioPool[_fileName].length;
        }
        else
        {
            return 0;
        }
    }

    public void StopAll()
    {
        foreach (GameObject audioSource in mAudioSourcePool)
        {
            AudioSource tempsource = audioSource.GetComponent<AudioSource>();
            if (tempsource.isPlaying)
            {
                tempsource.Stop();
            }
        }
    }

    public void Stop(string _clipname)
    {
        foreach (GameObject audioSource in mAudioSourcePool)
        {
            AudioSource tempsource = audioSource.GetComponent<AudioSource>();
            if (tempsource.clip != null && tempsource.clip.name.Equals(_clipname))
            {
                if (tempsource.isPlaying)
                {
                    tempsource.Stop();
                    Debug.Log("Stop Audio " + _clipname);
                }
                break;
            }
        }
    }

    public void ClearAll()
    {
        foreach (var obj in g_AudioSourcePool)
        {
            obj.Value.m_FreeLib.Clear();
        }
        mPrefabs.Clear();
        mAudioSourcePool.Clear();
        mAudioPool.Clear();
    }

    public void ForceClear()
    {
        foreach (GameObject audioSource in mAudioSourcePool)
        {
            if (audioSource != null)
            {
                AudioSource tempsource = audioSource.GetComponent<AudioSource>();
                if (tempsource.isPlaying)
                {
                    tempsource.Stop();
                }
                Destroy(tempsource);
            }
        }

        ClearAll();
    }

    //获得一个空闲的事件对象
    public GameObject GetAudioSource(string outName)
    {
        for (int i = 0; i < mAudioSourcePool.Count; i++)
        {
            GameObject audioSource = mAudioSourcePool[i];
            if (audioSource == null)
            {
                mAudioSourcePool.RemoveAt(i);
                i--;
            }
            else
            {
                AudioSource tempsource = audioSource.GetComponent<AudioSource>();
                if (!tempsource.isPlaying)
                {
                    pushFreeAudioSource(audioSource, audioSource.name);
                }
                else
                {
                }
            }
        }

        GameObject rv = null;
        QueueAudioSource _freeLib = null;
        if (g_AudioSourcePool.TryGetValue(outName, out _freeLib) == true)
        {
            if (_freeLib.m_FreeLib.Count > 0)
            {
                rv = _freeLib.m_FreeLib.Dequeue();
            }
        }
        if (rv == null)
        {
            if (mPrefabs.ContainsKey(outName))
            {
                rv = (GameObject)GameObject.Instantiate(mPrefabs[outName]);
            }
            else
            {
                UnityEngine.Object obj = Resources.Load(string.Concat("Sounds/", outName), typeof(GameObject));
                rv = Instantiate(obj) as GameObject;
                rv.name = rv.name.Substring(0, rv.name.IndexOf("(Clone)"));

                mPrefabs.Add(outName, rv);
            }
            rv.name = outName;
            mAudioSourcePool.Add(rv);
        }
        return rv;
    }

    //传入一个空闲的对象
    public void pushFreeAudioSource(GameObject inAudioSource, string inName)
    {
        QueueAudioSource _freeLib = null;
        if (g_AudioSourcePool.TryGetValue(inName, out _freeLib) == false)
        {
            _freeLib = new QueueAudioSource();
            g_AudioSourcePool.Add(inName, _freeLib);
        }
        if (!_freeLib.m_FreeLib.Contains(inAudioSource))
        {
            _freeLib.m_FreeLib.Enqueue(inAudioSource);
        }
    }
}
