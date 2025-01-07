using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] ballPrefabs;        // 生成するボールのプレファブ集
    public Transform ballParentTransform;   // 生成したボールの入れ先オブジェクト
    public float shotForce = 300;           // ショットパワー
    public float shotTorque = 30;           // 回転のパワー
    public int ballAmount = 100;            // 残数
    public int ballNumber;                  // ボールにつけたい識別番号

    public float baseWidth;                 // ボールを生成する位置の制限幅

    private GameObject nextBall;                   // 次のボール（投げられるボールがない場合はnullが入る）

    // Start is called before the first frame update
    void Start()
    {
        // 次のボールを準備する
        PrepareNextBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaanger.gameState == "timeover") return;

        // "Fire1"相当のキーが押されたら、SHotメソッドを発動
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    // ballPrefabsの中から任意のボールのプレハブを返す
    GameObject SampleBall()
    {
        // ランダムにballPrefabsの数に応じた数を取得
        int index = Random.Range(0, ballPrefabs.Length);
        // index番号目のballオブジェクトを配列から取得してメソッドの結果として返す
        return ballPrefabs[index];
    }

    Vector3 GetInstantiatePosition()
    {
        // 画面の横幅とInputのXの座標の割合からballの生成座標を計算して戻す
        // (InputのX座標が画面幅のどの割合の位置にいるか比をもとめ、
        //  ballを置く台座の幅に乗じて台座上の位置を求める。
        //  ただし台座の中心のX座標が0であるから、台座の幅の半分をオフセットとして差し引く
        float x =
            baseWidth *
            (Input.mousePosition.x / Screen.width) -
            (baseWidth / 2);

        return transform.position + new Vector3(x, 0, 0);
    }

    // 次のボールを準備
    // ボールオブジェクトを初期位置に生成して、そのGameObjectをnextBallメンバ変数にセットする
    // 残ボール数が0以下の場合はnullをセットする
    void PrepareNextBall()
    {

        if (ballAmount <= 0)
        {
            nextBall = null;
        }
        else 
        {
            nextBall =
                Instantiate(
                    SampleBall(),
                    ballParentTransform.position,
                    Quaternion.identity
            );

            //生成したボールの親オブジェクトにBallsオブジェクトを指名
            nextBall.transform.parent = ballParentTransform;

            //カメラに写るように重力をいったん無効化
            nextBall.GetComponent<Rigidbody>().useGravity = false;

            // 残ボールを減らす
            ballAmount--;
        }
    }

    // ボールを射出
    void Shot()
    {
        // 次のボールが準備できていなかったらなにもせず終了
        if (nextBall == null) return;

        // ボールを射出位置に移動
        GameObject ball = nextBall;
        ball.gameObject.transform.position = GetInstantiatePosition();

        //カメラに写るように無効にしていた重力を有効化
        ball.GetComponent<Rigidbody>().useGravity = true;

        // 次のボールを準備
        PrepareNextBall();

        // 生成した「ボールのballNumber」にballNumberの数字を付ける
        // ballNumberは更新
        BallController bc = ball.gameObject.GetComponent<BallController>();
        bc.ballNumber = ballNumber++;

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        //ボールのRigidbodyのAddForceで発射
        ballRigidbody.AddForce(transform.forward * shotForce);

        //ボールのRigidbodyのAddTorqueで回転
        ballRigidbody.AddTorque(new Vector3(0, shotTorque, 0));

    }

}
