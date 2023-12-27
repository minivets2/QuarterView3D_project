using System;
using System.Collections;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    private bool isBlinking = false;

    private void OnEnable()
    {
        isBlinking = false;
        ChangeColorOnHit(Color.white);
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        
        ChangeColorOnHit(Color.red);
        yield return new WaitForSeconds(0.1f);
        ChangeColorOnHit(Color.white);
        
        yield return new WaitForSeconds(0.1f);
        
        ChangeColorOnHit(Color.red);
        yield return new WaitForSeconds(0.1f);
        ChangeColorOnHit(Color.white);
        
        
        isBlinking = false;
    }
    
    void ChangeColorOnHit(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        Material newMat = new Material(renderer.sharedMaterial);
        newMat.color = color;
        renderer.material = newMat;
    }
}
