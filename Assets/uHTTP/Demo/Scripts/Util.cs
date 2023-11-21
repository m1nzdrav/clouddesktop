using System;
using System.Collections;
using UnityEngine;

public static class Util {
    private static IEnumerator DoDelayed(Action action, float delay) {
        yield return new WaitForSeconds(delay);
        action();
    }

    public static void DoDelayed(this MonoBehaviour monoBehaviour, Action action, float delay) {
        monoBehaviour.StartCoroutine(Util.DoDelayed(action, delay));
    }
}