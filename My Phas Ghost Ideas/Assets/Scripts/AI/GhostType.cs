using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ghost", menuName = "Ghost/New Ghost")]
public class GhostType : ScriptableObject
{
    [Header("Ghost Settings")]
    [Range(0, 5)] public float ghostSpeed;
    [Range(0, 100)] public float huntThreshhold;
    [Range(0, 100)] public float wonderChance;
    [Range(0, 100)] public float eventChance;
    [Range(0, 100)] public float interactChance;
    [Range(0, 100)] public float huntChance;

    [Header("Unique Ghost Settings")]
    //[Range(0, 50)] public float electronicsDisturbenceRange = 10f;
    [Range(0, 50)] public float hearingRange = 7.5f;
}
