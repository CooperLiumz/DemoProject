using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Image> ().canvasRenderer.cull = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
