using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

    private ParallaxSpeedFactor[] psfs;
    private float wBackground;
    private float wCam;
    public float scrollSpeed = 2f;

	// Use this for initialization
	void Start () {
        psfs = GetComponentsInChildren<ParallaxSpeedFactor>();
        wBackground = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        wCam = Camera.main.orthographicSize = Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        float xLeftCam = Camera.main.transform.position.x - wCam;
        foreach (ParallaxSpeedFactor psf in psfs)
        {
            float deltaX = -scrollSpeed * Time.deltaTime * psf.SpeedFactor;
            if ((psf.transform.position.x+wBackground)< xLeftCam)
            {
                psf.transform.Translate(new Vector3(2 * wBackground + deltaX, 0, 0));
            }
            else
            {
                psf.transform.Translate(new Vector3(deltaX, 0, 0));
            }
        }
	}
}
