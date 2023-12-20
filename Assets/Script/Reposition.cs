using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    private Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 playerDir = GameManager.instance.player._moveVec;
        Vector3 myPos = transform.position;

        float dirX = playerPos.x - myPos.x;
        float dirZ = playerPos.z - myPos.z;

        float diffx = Mathf.Abs(dirX);
        float diffz = Mathf.Abs(dirZ);

        dirX = dirX > 0 ? 1 : -1;
        dirZ = dirZ > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Floor" :

                if (diffx > diffz)
                {
                    transform.Translate(new Vector3(1, 0, 0) * dirX * 200);
                }
                else if (diffx < diffz)
                {
                    transform.Translate(new Vector3(0, 0, 1) * dirZ * 200);
                }
                
                break;
            
            case "Enemy" :

                if (coll.enabled)
                {
                    transform.Translate(playerDir * 50 + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f) ));
                }

                break;
        }

    }
}
