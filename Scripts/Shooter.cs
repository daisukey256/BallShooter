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

    // Start is called before the first frame update
    void Start()
    {

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


    // ボールを射出
    void Shot()
    {
        if (ballAmount <= 0) return;

        // ballオブジェクト生成
        GameObject ball =
            Instantiate(
                SampleBall(),
                GetInstantiatePosition(),
                Quaternion.identity
            );

        // 生成した「ボールのballNumber」にballNumberの数字を付ける
        // ballNumberは更新
        BallController bc = ball.gameObject.GetComponent<BallController>();
        bc.ballNumber = ballNumber++;

        // ballAmountを減らす
        ballAmount--;

        //生成したボールの親オブジェクトにBallsオブジェクトを指名
        ball.transform.parent = ballParentTransform;

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        //ボールのRigidbodyのAddForceで発射
        ballRigidbody.AddForce(transform.forward * shotForce);

        //ボールのRigidbodyのAddTorqueで回転
        ballRigidbody.AddTorque(new Vector3(0, shotTorque, 0));

    }

}
