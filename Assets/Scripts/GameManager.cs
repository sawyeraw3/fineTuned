﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject[] enemyTypes;
	public Transform[] spawnPoints;
	public int maxEnemies = 10;
	public int secBetweenSpawn = 5;
	public int waveEnemyIncrease = 4;
	public float timeBetweenWaves = 8;
	public float difficultySpread = 10;
	public float upgradeSpeed = 3;
	public AudioClip newWave;

	GameOverManager gmo;

	int totalEnemies = 0;
	int enemiesSpawned = 0;
	int whichSpawn = 0;
	int whichEnemy = 0;
	float difficulty = 1;
	float timer;
	float waveTimer;

	public readonly Color Blue = new Color((56f/255f),(63f/255f),(188f/255f), 1);
	public readonly Color Cyan = new Color((1f/255f),1,1, 1);
	public readonly Color Green = new Color((103f/255f),1,(100f/255f), 1);
	public readonly Color Orange = new Color(1,(173f/255f),(53f/255f), 1);
	public readonly Color Red = new Color(1,(49f/255f),(58f/255f), 1);
	public readonly Color Pink = new Color(1,(36f/255f),(239f/255f), 1);

	public Color[] cols;

	// Use this for initialization
	void Awake () {
		gmo = GetComponent<GameOverManager> ();
		cols = new Color[]{Blue, Cyan, Green, Orange, Red, Pink};
		totalEnemies += maxEnemies;
		EnemiesLeftManager.totalEnemies = totalEnemies;
		timer = 0;
		waveTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gmo.gameOver) {
			timer += Time.deltaTime;
			if (enemiesSpawned != maxEnemies && timer > secBetweenSpawn) {
				SpawnEnemies ();
				timer = 0;
			} else if (EnemiesLeftManager.totalEnemies == 0) {
				waveTimer += Time.deltaTime;
				if (waveTimer > timeBetweenWaves) {
					Debug.Log ("newWave");
					enemiesSpawned = 0;
					maxEnemies += waveEnemyIncrease;
					totalEnemies += maxEnemies;
					EnemiesLeftManager.totalEnemies = maxEnemies;
					WaveManager.wave++;
					AudioSource sound = GetComponentInChildren<AudioSource> ();
					sound.clip = newWave;
					sound.Play ();
					difficulty++;
					waveTimer = 0f;
				}
			}
		}
	}

	void SpawnEnemies() {
		enemiesSpawned ++;
		int e = 0;
		if (Random.Range (0f, difficultySpread) <= difficulty)
			e = 1;
		whichSpawn = Random.Range (0, spawnPoints.Length);
		GameObject newEnemy = Instantiate (enemyTypes[e], spawnPoints [whichSpawn].transform.position, Quaternion.identity) as GameObject;
		int i = Random.Range (0, 6);
		Color c = cols [i];
		Renderer[] rends = newEnemy.transform.FindChild ("Colored").GetComponentsInChildren<Renderer>();
		foreach(Renderer rend in rends)
			rend.material.color = c;
		Light[] lights = newEnemy.GetComponentsInChildren<Light> ();
		foreach (Light l in lights) {
			l.color = c;
		}
		Slider s = newEnemy.GetComponentInChildren<Slider> ();
		s.transform.FindChild ("Fill Area").GetComponentInChildren<UnityEngine.UI.Image> ().color = c;
	}
}