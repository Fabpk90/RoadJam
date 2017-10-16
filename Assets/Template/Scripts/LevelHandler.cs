using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour 
{

	public float lvl1Duration = 60f;
	public float lvl2Duration = 60f;
	public float anim1Duration = 10f;
	public float anim2Duration = 10f;
	public float tutoDuration = 10f;
	public float resultDuration = 10f;

	public GameObject lvl1;
	public GameObject lvl2;

	private State curstate;
	private State nextstate;
	private EnemySpawner enemySpawner;
	private enum State
	{
		Anim1,
		Tuto,
		Lvl1,
		Result1,
		Anim2,
		Lvl2,
		Result2,
		Fail
	};

	public GameObject Anim1Panel;
	public GameObject TutoPanel;
	public GameObject Anim2Panel;
	public GameObject ResultPanel;
	public GameObject FailPanel;

	public Text ResultScore;

	public GameObject HUD;

	void Start () 
	{
		curstate = State.Anim1;
		enemySpawner = GetComponent<EnemySpawner> ();

		Anim1Panel.SetActive (false);
		TutoPanel.SetActive (false);
		Anim2Panel.SetActive (false);
		ResultPanel.SetActive (false);
		FailPanel.SetActive (false);

		ApplyState ();
	}

	void ApplyState()
	{
		Debug.Log ("ApplyState state = " + curstate);

		enemySpawner.SetPaused (true);
		enemySpawner.DeleteAllEnemies ();
		HUD.SetActive (false);
		ResultScore.gameObject.SetActive (false);
		lvl1.SetActive (false);
		lvl2.SetActive (false);

		if (curstate == State.Anim1) 
		{
			Anim1Panel.SetActive (true);
			Debug.Log ("Anim1Panel !");
			nextstate = State.Tuto;
			Invoke ("ChangeState", anim1Duration);
		} 
		else if (curstate == State.Tuto) 
		{
			Anim1Panel.SetActive (false);
			TutoPanel.SetActive (true);
			//HUD.SetActive (true);
			nextstate = State.Lvl1;
			Invoke ("ChangeState", anim1Duration);
		} 
		else if (curstate == State.Lvl1) 
		{
			TutoPanel.SetActive (false);
			lvl1.SetActive (true);
			enemySpawner.isBossLvl = false;
			enemySpawner.SetPaused (false);
			HUD.SetActive (true);

			nextstate = State.Result1;
			Invoke ("ChangeState", lvl1Duration);
		} 
		else if (curstate == State.Result1) 
		{
			ResultPanel.SetActive (true);
			HUD.SetActive (true);
			ResultScore.gameObject.SetActive (true);
			ResultScore.text = ScoreManager.instance.getScore () + "pts";
			nextstate = State.Lvl2;
			Invoke ("ChangeState", resultDuration);
		} 
		else if (curstate == State.Lvl2) 
		{

			ResultPanel.SetActive (false);
			lvl2.SetActive (true);
			enemySpawner.isBossLvl = true;
			enemySpawner.SetPaused (false);
			HUD.SetActive (true);
			ResultScore.gameObject.SetActive (false);
			nextstate = State.Result2;
			Invoke ("ChangeState", lvl2Duration);
		} 
		else if (curstate == State.Result2) 
		{
			ResultScore.text = ScoreManager.instance.getScore () + "pts";
			HUD.SetActive (true);
			ResultPanel.SetActive (true);
			ResultScore.gameObject.SetActive (true);
		}
		// State == State.Fail
		else 
		{
			FailPanel.SetActive (true);
		}
			

	}

	public void PlayerIsDead()
	{
		nextstate = State.Fail;
		ChangeState ();
	}

	public void BossIsDead()
	{
		nextstate = State.Result2;
		ChangeState ();
	}

	void ChangeState()
	{
		curstate = nextstate;
		ApplyState ();
	}

	void Update () 
	{
	}
}
