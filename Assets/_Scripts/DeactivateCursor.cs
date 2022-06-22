using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateCursor : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isCursor;

    void Start()
    {
        if (isCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
