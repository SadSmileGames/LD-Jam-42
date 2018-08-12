﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    public float lifeTime = 5f;

	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(this.gameObject);
	}
}
