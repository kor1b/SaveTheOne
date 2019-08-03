using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

    void FixedUpdate()
    {
        rb.AddForce(transform.right * 0.1f, ForceMode2D.Impulse);
    }
}
