using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI 표시를 담당하는 클래스
/// </summary>
public class UIDisplay : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balancemoneyText;
    public TextMeshProUGUI nameText;
    private int cash;
    private int balanceMoney;
    private string userName;

    /// <summary>
    /// 시작 시 UI를 설정합니다.
    /// </summary>
    private void Start()
    {
        SetUI();
    }

    /// <summary>
    /// UI를 업데이트합니다.
    /// </summary>
    public void SetUI()
    {
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            cash = GameManager.Instance.userData.cash;
            balanceMoney = GameManager.Instance.userData.balance;
            userName = GameManager.Instance.userData.userName;

            if (nameText != null)
                nameText.text = userName;   

            if (cashText != null)
                cashText.text = string.Format("{0:N0}", cash);
                
            if (balancemoneyText != null)
                balancemoneyText.text = string.Format("Balance {0:N0}", balanceMoney);
        }
    }
}
