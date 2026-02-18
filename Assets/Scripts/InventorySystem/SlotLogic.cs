using UnityEngine;

public class SlotLogic : MonoBehaviour
{
    public bool isEmpty = false;

    void Update()
    {
        if (transform.childCount == 0)
        {
            isEmpty = true;
        }
        else { isEmpty = false; }
    }
}
