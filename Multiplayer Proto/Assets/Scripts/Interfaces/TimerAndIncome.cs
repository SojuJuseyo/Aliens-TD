using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimerAndIncome : MonoBehaviour {

	public Text textTimeMoney;
	public Text textTimeGame;
	public Text textMoney;
	public Text textIncomeValue;
	public int incomeMoneyPerRound;

	private DateTime start, end;
	private float incomeTime;
	private bool isStarted = false;
	private Player_Board playerBoard;
	private InGameInterface menu;
	private bool isAlreadySentRequestMoney = false;
	private float timeToIncome = 20.0f;

	void Start(){
		menu = GetComponent<InGameInterface> ();
	}

	void FixedUpdate(){
		if (isStarted){
			UpdateIncome();
			UpdateTime();
		}
	}

	public void StartCounters(){
		playerBoard = menu.getMyPlayerObject().GetComponent<Player_Board> ();
		textTimeGame.text = "";
		incomeTime = timeToIncome;
		start = end = DateTime.Now;
		isStarted = true;
	}

	public void ResetIncomeTime(){
		incomeTime = timeToIncome;
		isAlreadySentRequestMoney = false;
	}

	void UpdateIncome(){
		if (incomeTime > 0)
			incomeTime -= Time.deltaTime;
		if (incomeTime <= 0 && playerBoard.playerIdentity.playerTeam == Player_Board.e_player.PLAYER1 && !isAlreadySentRequestMoney) {
			playerBoard.wannaIncome (incomeMoneyPerRound);
			isAlreadySentRequestMoney = true;
		}
		textTimeMoney.text = ((int)incomeTime).ToString();
		textMoney.text = playerBoard.money.ToString ();
		textIncomeValue.text = incomeMoneyPerRound.ToString ();
	}

	void UpdateTime() {
		end = DateTime.Now;
		TimeSpan timeElapsed = end - start;
		textTimeGame.text = String.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsed.Hours, timeElapsed.Minutes, timeElapsed.Seconds);
	}
}
