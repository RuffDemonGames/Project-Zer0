﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
	[SerializeField] int lives = 2;
	int maxLives = 10;
	[SerializeField] int ammo = 0;
	int maxAmmo = 20;
	
	[SerializeField] int sparkCount = 0;
	int totalSparks = 10;
	[SerializeField] bool[] sparkiesCollected = {false, false, false, false, false};
	[SerializeField] int scrapCount = 0;
	int totalScrap = 100;
	[SerializeField] int goldCoinCount = 0;
	int totalGoldCoins = 5;
	[SerializeField] bool heartKeyOwned = false;
	[SerializeField] bool heartCageUnlocked = false;

	private Text coinAmount;
	private Text sparkAmount;
	private Text scrapAmount;
	private UIManager manager;

	void Awake()
	{
		manager = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
	}

	void Start()
	{
		//StartCoroutine ();
	}

	public bool GainLife(int amount) {
		if (lives == maxLives) {
			return false;
		} else {
			lives += amount;
			if (lives >= maxLives) {
				lives = maxLives;
			}
			//Do GUI stuff, play sound, etc actions
			return true;
		}
	}

	public void LoseLife() {
		lives--;
		//Die
		/*
		if (lives <= 0) {
			//Do Gameover actions
			lives = 2;
		}*/
		//Reset Stuff
	}

	public bool GainAmmo(int amount) {
		if (ammo == maxAmmo) {
			return false;
		} else {
			ammo += amount;
			if (ammo >= maxAmmo) {
				ammo = maxAmmo;
			}
			//Do GUI stuff, play sound, etc actions
			return true;
		}
	}
	
	public void LoseAmmo(int amount) {
		ammo -= amount;
		if (ammo < 0) {
			ammo = 0;
		}
	}
	
	public void CollectSpark() 
	{
		if(sparkCount == totalSparks)
		{
			sparkCount = totalSparks;
		}

		manager.ShowSpark ();
		sparkCount++;
		sparkAmount = GameObject.FindGameObjectWithTag ("Spark").transform.GetChild (1).GetComponent<Text> ();
		sparkAmount.text = sparkCount.ToString ();
		StartCoroutine (SparkTimer ());
	}

	public void CollectSparkie(int index) {
		sparkiesCollected[index] = true;
		bool sparkiesComplete = true;
		for(int i=0; i< 5; i++) {
			if(sparkiesCollected[i] == false) {
				sparkiesComplete = false;
				break;
			}
		}
		if (sparkiesComplete) {
			CollectSpark();
		}
	}
	
	public void CollectScrap() 
	{
		if(scrapCount == totalScrap)
		{
			scrapCount = totalScrap;
		}
		manager.ShowScrap();
		scrapCount++;
		scrapAmount = GameObject.FindGameObjectWithTag ("Scrap").transform.GetChild (1).GetComponent<Text> ();
		scrapAmount.text = scrapCount.ToString ();
		StartCoroutine (ScrapTimer ());

	}
	
	public void CollectGoldCoin() 
	{
		if(goldCoinCount == totalGoldCoins)
		{
			goldCoinCount = totalGoldCoins;
		}

		manager.ShowCoin ();
		goldCoinCount++;
		coinAmount = GameObject.FindGameObjectWithTag ("Coin").transform.GetChild (1).GetComponent<Text> ();
		coinAmount.text = goldCoinCount.ToString ();
		StartCoroutine (CoinTimer ());
	}
	
	public void CollectHeartKey() {
		heartKeyOwned = true;
		//Do GUI stuff, animation
	}
	
	public bool CollectHeartCage() {
		if (heartKeyOwned == true) {
			heartKeyOwned = false;
			heartCageUnlocked = true;

			return true;
		}
		return false;
	}

	IEnumerator CoinTimer()
	{
		yield return new WaitForSeconds(5.0f);
		manager.HideCoin ();
	}

	IEnumerator SparkTimer()
	{
		yield return new WaitForSeconds(5.0f);
		manager.HideSpark ();
	}

	IEnumerator ScrapTimer()
	{
		yield return new WaitForSeconds(5.0f);
		manager.HideScrap ();
	}
	
	//==========Getter Functions===========
	
	public int Lives {
		get {
			return lives;
		}
	}
	
	public int Ammo {
		get {
			return ammo;
		}
	}

	public int SparkCount {
		get {
			return sparkCount;
		}
	}
	
	public int ScrapCount {
		get {
			return scrapCount;
		}
	}
	
	public int GoldCoinCount {
		get {
			return goldCoinCount;
		}
	}
	
	public bool HeartKeyOwned {
		get {
			return heartKeyOwned;
		}
	}
	
	public bool HeartCageUnlocked {
		get {
			return heartCageUnlocked;
		}
	}
}
