using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int money = 0;
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI timerText;

    public float timerDuration = 300f;

    public void Start(){
        moneyText.text = "$" + money;
    }

    public void AddMoney(int moneyGained){
        money += moneyGained;
        moneyText.text = "$" + money;

    }
    public void SubtractMoney(int moneyLost){
        money -= moneyLost;
        moneyText.text = "$" + money;
    }
    public void Update(){
        timerDuration -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Floor(timerDuration / 60).ToString("00") + ":" + (timerDuration % 60).ToString("00");
        if(timerDuration <= 0){
            timerText.text = "Time: 00:00";
        }
    }
}
