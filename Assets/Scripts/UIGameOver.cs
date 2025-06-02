using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
        ASM_MN = ASM_MN.Instance;
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();

        ASM_MN.YC1(scoreKeeper.GetID(),scoreKeeper.GetUserName(),scoreKeeper.GetScore(),ASM_MN.listRegion[scoreKeeper.GetIDregion()]);
        ASM_MN.YC2();
        ASM_MN.YC3();
        ASM_MN.YC4();
        ASM_MN.YC5();
        ASM_MN.YC6();
        ASM_MN.YC7();
    }
}
