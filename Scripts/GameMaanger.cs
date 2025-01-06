using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMaanger : MonoBehaviour
{
    public static string gameState; // ゲームの状態をコントロールするstatic変数

    public TextMeshProUGUI hiScoreText;     // ハイスコア
    public TextMeshProUGUI scoreText;       // 現スコア
    public TextMeshProUGUI ballAmountText;  // ボール残数
    public TextMeshProUGUI timeText;        // タイム
    public GameObject statusText;         // 必要に応じて"GAME START"や"TIME UP"を表示、非表示させる
    public Shooter shooter;                 // Shooterスクリプト
    public float gameTime = 60.0f;          // カウントダウンの基準時間
    public string retrySceneName = "";      // リトライするシーン名

    public ScoreController scoreControl;    // 


    // Start is called before the first frame update
    void Start()
    {
        // 時間差でStatusTextを非表示
        Invoke("HiddenStatusText", 1.0f);

        // ゲームの状態を"playing"に変更
        gameState = "playing";

        // ハイスコアを更新(PlayerPrefs.GetInt("Score")を利用
        hiScoreText.text = "Hi-Score : " + PlayerPrefs.GetInt("Score").ToString();
    }

    // statesTextを非表示にするメソッド
    void HiddenStatusText()
    {
        statusText.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        bool isTimeUp = false;

        if (gameState != "playing") return;

        // gameTimeをカウントダウンして減らす
        gameTime -= Time.deltaTime;
        if (gameTime <= 0.0f)
        {
            gameTime = 0.0f;
            isTimeUp = true;
        }

        // timeTextに残タイムを更新
        timeText.text = "Time : " + ((int)gameTime).ToString();

        // scoreTextにスコアを更新
        scoreText.text = "Score : " + scoreControl.score.ToString();

        // ballAmountTextに残ボール数を更新
        ballAmountText.text = "Last : " + shooter.ballAmount.ToString();

        // もし、gameTimeが0以下ならTimeOberメソッドの発動
        if (isTimeUp) TimeOver();

    }

    void TimeOver()
    {
        // ハイスコア確認(PlayerPrefs.GetInt("Score"))と現スコアを比べて、
        // 必要ならハイスコア更新(PlayerPrefs.SetInt("Score",現スコア)
        int newScore = scoreControl.score;
        int hiScore = PlayerPrefs.GetInt("Score");
        if (newScore > hiScore)
        {
            PlayerPrefs.SetInt("Score", newScore);
        }

        // gameStateを更新
        gameState = "timeover";

        //statusTextを表示して、内容を”TIME UP”にすること
        statusText.GetComponent<TextMeshProUGUI>().text = "TIME UP";
        statusText.SetActive(true);

        //時間差で他シーン(Title、Title作っていない時は自分自身)に移る
        StartCoroutine(ChangeScene(5.0f, retrySceneName));
    }

    // 指定時間後に指名したシーンに移る
    IEnumerator ChangeScene(float delay, string sceneName)
    {
        // 指定時間待つ
        yield return new WaitForSeconds(delay);

        // 指定シーンに移る
        SceneManager.LoadScene(sceneName);
    }
}
