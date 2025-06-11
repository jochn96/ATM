using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 은행 팝업 UI 및 기능을 관리하는 클래스
/// </summary>
public class PopupBank : MonoBehaviour
{
    public GameObject deposit;
    public GameObject withdraw;
    public GameObject Button;
    public GameObject uiPopup;
    public GameObject saveCompletePopup; // 저장 완료 팝업

    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balancemoneyText;
    public TextMeshProUGUI nameText;

    public TMP_InputField directInput;
    public TMP_InputField WithdrawdirectInput;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        UserData data = GameManager.Instance.userData;
        nameText.text = data.userName;
        balancemoneyText.text = string.Format("{0:N0}", data.balance);
        cashText.text = string.Format("{0:N0}", data.cash);
    }

    public void OnClickDepositButton()
    {
        if (deposit != null)
        {
            Button.SetActive(false);
            deposit.SetActive(true);
            
        }     
    }

    public void OnClickWithdrawButton()
    {
        if (withdraw != null)
        {
            Button.SetActive(false);
            withdraw.SetActive(true);
            
        }
    }

    // 저장 버튼 클릭 이벤트
    public void OnClickSaveButton()
    {
        SaveData();
        
        // 저장 완료 팝업 표시 (팝업이 설정되어 있는 경우)
        if (saveCompletePopup != null)
        {
            saveCompletePopup.SetActive(true);
            // 2초 후 자동으로 팝업 닫기
            StartCoroutine(ClosePopupAfterDelay(2.0f));
        }
    }
    
    // 지정된 시간 후 팝업 닫기
    private IEnumerator ClosePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (saveCompletePopup != null)
        {
            saveCompletePopup.SetActive(false);
        }
    }

    public void Onclick10000Button()
    {
        if (GameManager.Instance.userData.cash >= 10000)
        {
            GameManager.Instance.userData.balance += 10000;
            GameManager.Instance.userData.cash -= 10000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }
    }

    public void OnclickWithdraw10000Button()
    {
        if (GameManager.Instance.userData.balance >= 10000)
        {
            GameManager.Instance.userData.balance -= 10000;
            GameManager.Instance.userData.cash += 10000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }

    }

    public void Onclick30000Button()
    {
        if (GameManager.Instance.userData.cash >= 30000)
        {
            GameManager.Instance.userData.balance += 30000;
            GameManager.Instance.userData.cash -= 30000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }
    }

    public void OnclickWithdraw30000Button()
    {
        if (GameManager.Instance.userData.balance >= 30000)
        {
            GameManager.Instance.userData.balance -= 30000;
            GameManager.Instance.userData.cash += 30000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }

    }

    public void Onclick50000Button()
    {
        if (GameManager.Instance.userData.cash >= 50000)
        {
            GameManager.Instance.userData.balance += 50000;
            GameManager.Instance.userData.cash -= 50000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }
    }

    public void OnclickWithdraw50000Button()
    {
        if (GameManager.Instance.userData.balance >= 50000)
        {
            GameManager.Instance.userData.balance -= 50000;
            GameManager.Instance.userData.cash += 50000;
            Refresh();
            SaveData();
        }
        else
        {
            UIPopup();
        }
    }

    public void DirectButton()
    {
        string input = directInput.text;

        
        if (string.IsNullOrEmpty(input))
            return;

        
        if (int.TryParse(input, out int amount))
        {
            
            if (amount <= 0)
            {
                
                return;
            }

            if (amount > GameManager.Instance.userData.cash)
            {                
                return;
            }

           
            GameManager.Instance.userData.balance += amount;
            GameManager.Instance.userData.cash -= amount;

            
            Refresh();
            SaveData();

            
            directInput.text = "";
        }
        
    }

    public void WithdrawDirectButton()
    {
        
        string input = WithdrawdirectInput.text;

        
        if (string.IsNullOrEmpty(input))
            return;

        
        if (int.TryParse(input, out int amount))
        {
            
            if (amount <= 0)
            {

                return;
            }

            if (amount > GameManager.Instance.userData.balance)
            {
                UIPopup();
                return;
            }
           
            //  ó
            GameManager.Instance.userData.balance -= amount;
            GameManager.Instance.userData.cash += amount;

            // UI 
            Refresh();
            SaveData();

            // Էâ ʱȭ
            directInput.text = "";
        }

    }

    public void BackButton()
    {
        Button.SetActive(true);
        deposit.SetActive(false);
        withdraw.SetActive(false);
    }

    public void UIPopup()
    {
        uiPopup.SetActive(true);
    }

    public void UiPopupOFF()
    {
        uiPopup.SetActive(false);
    }
    
    /// <summary>
    /// 데이터를 저장하고 필요한 경우 UI를 업데이트합니다.
    /// </summary>
    private void SaveData()
    {
        // GameManager를 통해 데이터 저장
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveUserData();
            Debug.Log("금액 변동으로 인한 자동 저장 완료");
        }
    }
}
