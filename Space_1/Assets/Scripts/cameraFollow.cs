using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {
    public Transform target;

    private Vector3 targetPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.target != null)
        {
          this.targetPosition = this.target.position;
            this.targetPosition.z = -10;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime);
        }
        }
}

