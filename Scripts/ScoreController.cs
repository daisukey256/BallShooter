using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score;   // そのゲームにおける加算されていくべきスコア

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // 得点が格納されているBallControllerを得る
            BallController bc = other.gameObject.GetComponent<BallController>();
            // スコアに得点を加算
            score += bc.ballScore;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // 得点が格納されているBallControllerを得る
            BallController bc = other.gameObject.GetComponent<BallController>();
            // スコアから減算
            score -= bc.ballScore;
        }
    }

}
