using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnScreenControls : MonoBehaviour
{
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            transform.Find("LeftStick").gameObject.SetActive(true);
            transform.Find("Button").gameObject.SetActive(true);
        }
    }

}
