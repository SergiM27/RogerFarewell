﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressed : MonoBehaviour
{
    public void ImPressed()
    {
        if (Input.GetMouseButtonDown(0))
            this.gameObject.transform.position= this.gameObject.transform.position - new Vector3(0, 1, 0);
    }

    public void ImReleased()
    {
        if (Input.GetMouseButtonUp(0))
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 1, 0);
    }


}
