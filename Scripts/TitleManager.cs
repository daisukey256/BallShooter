using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;      // スタートボタン
    public string firstSceneName;       // ゲーム可開始のシーン名

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // スタートボタン押下
    public void StartButtonClicked()
    {
        //// PlayerPrefsのデータをクリア
        //PlayerPrefs.DeleteAll();

        // 変数に指名したシーン名にジャンプ
        SceneManager.LoadScene(firstSceneName);
    }


}
