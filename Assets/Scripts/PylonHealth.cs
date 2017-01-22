﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonHealth : MonoBehaviour {

	public int curHealth;
	public bool isDestroyed;
	Slider slider;

	// Use this for initialization
	void Start () {
		slider = gameObject.transform.FindChild("DamageCanvas").GetComponentInChildren<Slider> ();
		slider.interactable = false;
		slider.maxValue = curHealth;
		slider.value = curHealth;
		isDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = curHealth;
		Debug.Log (slider.value);
	}

	public void TakeDamage (int damage) {
		if (curHealth > 0) {
			curHealth -= damage;
		} else {
			isDestroyed = true;
		}
	}
}
