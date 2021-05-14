using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseInfo : MonoBehaviour
{

    public Canvas mouseInfo;
    public GameObject mouseInfoLoc;
    public TMP_Text mouseInfoText;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {

        mouseInfo.enabled = false;
        mouseInfoLoc = GameObject.Find("MouseInfoLoc");

    }

    // Update is called once per frame
    void Update()
    {
        // transform main camera world location to screen canvas location to determine where
        // text should appear so it looks like it's on the object
        Vector3 infoPos = mainCamera.WorldToScreenPoint(mouseInfoLoc.transform.position);
        mouseInfoText.transform.position = infoPos;
    }
}
