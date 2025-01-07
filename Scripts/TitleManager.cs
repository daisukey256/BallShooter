using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;      // スタートボタン
    public string firstSceneName;       // ゲーム可開始のシーン名
    public TextMeshProUGUI hiScoreText; // ハイスコアテキスト

    const float DefaultGravity = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        // ボールの落下速度を半分に設定
        Physics.gravity = new Vector3(0, - DefaultGravity / 2.0f, 0);

        // ハイスコア表示を更新
        UpdateHiScoreText();
    }

    // ハイスコア表示を更新
    void UpdateHiScoreText()
    {
        // ハイスコア表示を更新(PlayerPrefs.GetInt("Score")を利用
        hiScoreText.text = "Hi-Score : " + PlayerPrefs.GetInt("Score").ToString();
    }


    // スタートボタン押下
    public void StartButtonClicked()
    {
        // 次のシーンのためにボールの落下速度をデフォルトに戻す
        Physics.gravity = new Vector3(0, -DefaultGravity, 0);

        // 変数に指名したシーン名にジャンプ
        SceneManager.LoadScene(firstSceneName);
    }

    // スコアリセットボタン押下
    public void ScoreResetButtonClicked()
    {
        // PlayerPrefsのデータをクリア
        PlayerPrefs.DeleteAll();

        // ハイスコア表示を更新
        UpdateHiScoreText();
    }

}
