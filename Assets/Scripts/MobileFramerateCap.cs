using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFramerateCap : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 90;
    }
}
