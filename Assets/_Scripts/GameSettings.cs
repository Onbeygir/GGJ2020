using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool MultiTouchEnabled = false;

    private void Awake()
    {
        Input.multiTouchEnabled = MultiTouchEnabled;
    }
}
