using UnityEngine;

public class Reposition : MonoBehaviour
{ 
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffZ = Mathf.Abs(playerPos.z - myPos.z);

        Vector3 playerDir = GameManager.instance.player._moveVec;

        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirZ = playerDir.z < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Floor" :

                if (diffX > diffZ)
                {
                    transform.localPosition += new Vector3(1, 0, 0) * dirX * 400;
                }
                else if (diffX < diffZ)
                {
                    transform.localPosition += new Vector3(0, 0, 1) * dirZ * 400;
                }
                break;
        }

    }
}
