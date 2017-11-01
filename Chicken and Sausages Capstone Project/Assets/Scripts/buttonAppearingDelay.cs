using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonAppearingDelay : MonoBehaviour
{
    public Button buttons;
    public Image imageDisplay;
    public void Awake()
    {
        GameObject[] f1 = GameObject.FindGameObjectsWithTag("Buttons");
        foreach (GameObject f in f1)
        {
            f.SetActive(false);
            StartCoroutine(WaitForIt(3.0F));
            f.SetActive(true);
        }
        //buttons.enabled = false;
    }
    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}

