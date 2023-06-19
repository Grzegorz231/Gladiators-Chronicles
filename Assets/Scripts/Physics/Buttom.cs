using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject door;
    internal bool interactable;
    private int colliderCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (colliderCount == 0)
        {
            door.SetActive(false);
            transform.localPosition += new Vector3(0, 0, 0.3f);
        }
        colliderCount++;
    }

    void OnTriggerExit(Collider other)
    {
        colliderCount--;
        if (colliderCount == 0)
        {
            door.SetActive(true);
            transform.localPosition -= new Vector3(0, 0, 0.3f);
        }
    }
}