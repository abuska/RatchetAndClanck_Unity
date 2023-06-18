using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // Prevent to destroy this object when loading new scene
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
