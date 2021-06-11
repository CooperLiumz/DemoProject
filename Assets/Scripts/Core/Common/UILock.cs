using UnityEngine;
using UnityEngine.UI;

public class MouseLock : MonoBehaviour
{
    public static MouseLock Instance;

    public GraphicRaycaster raycaster;

    void Awake ()
    {
        Instance = this;
    }

    public void Lock ()
    {
        SetDeactive ();
        CancelInvoke ( "SetActive" );

        Invoke ( "SetActive", 6f );
    }
    
    public void Unlock () {
        SetActive ();
    }

    private void SetDeactive ()
    {
        Debug.Log ( "Lock UI" );
        raycaster.enabled = false;
    }

    private void SetActive ()
    {
        Debug.Log ( "Unlock UI" );
        raycaster.enabled = true;
    }
}
