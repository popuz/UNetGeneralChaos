using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerChildTest : MonoBehaviour
{    
    private float _shakeAmplitude = 0.01f;
    
    void Update() =>  transform.position += Vector3.up * Mathf.Sin(Time.time) * _shakeAmplitude;    
}
