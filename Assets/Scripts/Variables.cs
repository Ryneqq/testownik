﻿using UnityEngine;
using System.Collections;

public class Variables : MonoBehaviour {
    public static float studyTime;
    public static int correct;
    public static int anwsered;

    public static void Clear()
    {
        studyTime = 0.0f;
        correct = 0;
        anwsered = 0;
    }
}
