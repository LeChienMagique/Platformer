using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour {
    public TextMeshProUGUI timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // timer.text = $"Time \n {(Math.Max(0, 300 - Time.realtimeSinceStartup)):F0}";
    }
}
