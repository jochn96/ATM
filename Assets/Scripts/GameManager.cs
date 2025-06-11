using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    
    
    [SerializeField]
    public UserData userData;

   
    private const string SAVE_FILE_NAME = "userdata.json";
    private string SavePath => Path.Combine(Application.dataPath, "Resources", SAVE_FILE_NAME);

    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadUserData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// 사용자 데이터를 JSON 형식으로 저장합니다.
    /// </summary>
    public void SaveUserData()
    {
        try
        {
            // Resources 폴더가 없으면 생성
            string resourcesPath = Path.Combine(Application.dataPath, "Resources");
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }
            
            string jsonData = JsonUtility.ToJson(userData, true);
            File.WriteAllText(SavePath, jsonData);
            Debug.Log($"데이터 저장 완료: {SavePath}");
            
            // 에디터에서 파일 변경 알림
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 저장 실패: {e.Message}");
        }
    }
    
    /// <summary>
    /// 저장된 사용자 데이터를 불러옵니다.
    /// 저장된 데이터가 없을 경우 기본값으로 초기화합니다.
    /// </summary>
    private void LoadUserData()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                string jsonData = File.ReadAllText(SavePath);
                userData = JsonUtility.FromJson<UserData>(jsonData);
                Debug.Log($"데이터 불러오기 완료: {SavePath}");
                
                // UI 업데이트
                UpdateAllUI();
            }
            else
            {
                // Resources 폴더에서 기본 데이터 로드 시도
                TextAsset jsonAsset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(SAVE_FILE_NAME));
                if (jsonAsset != null)
                {
                    userData = JsonUtility.FromJson<UserData>(jsonAsset.text);
                    Debug.Log("Resources에서 데이터 불러오기 완료");
                }
                else
                {
                    // 기본 데이터 생성
                    userData = new UserData("조성찬", 100000, 50000);
                    Debug.Log("기본 데이터 생성");
                    SaveUserData(); // 기본 데이터 저장
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 로드 실패: {e.Message}");
            // 오류 발생 시 기본 데이터 생성
            userData = new UserData("조성찬", 100000, 50000);
        }
    }
    
    /// <summary>
    /// 모든 UI를 업데이트합니다.
    /// </summary>
    public void UpdateAllUI()
    {
        // 모든 UIDisplay 컴포넌트 찾아서 업데이트
        UIDisplay[] displays = FindObjectsOfType<UIDisplay>();
        foreach (UIDisplay display in displays)
        {
            if (display != null)
            {
                display.SetUI();
            }
        }
        
        // PopupBank 컴포넌트 찾아서 업데이트
        PopupBank[] banks = FindObjectsOfType<PopupBank>();
        foreach (PopupBank bank in banks)
        {
            if (bank != null)
            {
                bank.Refresh();
            }
        }
    }
}
