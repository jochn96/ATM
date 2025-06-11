using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용자 데이터를 저장하는 클래스
/// </summary>
[System.Serializable]
public class UserData 
{
    /// <summary>
    /// 사용자 이름
    /// </summary>
    public string userName;
    
    /// <summary>
    /// 현금 보유액
    /// </summary>
    public int cash;
    
    /// <summary>
    /// 계좌 잔액
    /// </summary>
    public int balance;

    /// <summary>
    /// UserData 생성자
    /// </summary>
    /// <param name="name">사용자 이름</param>
    /// <param name="initialCash">초기 현금 보유액</param>
    /// <param name="initialBalance">초기 계좌 잔액</param>
    public UserData(string name, int initialCash, int initialBalance)
    {
        this.userName = name;
        this.cash = initialCash;
        this.balance = initialBalance;
    }
    
   

}
