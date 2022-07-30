using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInstance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddPoint();
            Destroy(this.gameObject);

        }
    }
}
