using System.Collections;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public bool isBlinking = false;

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
