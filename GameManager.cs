using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Questions[][] refQuestions;
    private static List<Questions> unanswerQList;
    private Questions currentQuestion;

    [SerializeField]
    private Text QuestionText;

    [SerializeField]
    private Text TruthAnswerText;
    [SerializeField]
    private Text FalseAnswerText;

    [SerializeField]
    private float questionBuffer = 1f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    static int score;
    [SerializeField]
    public Text scoreText;

    void Start()
    {
        if (unanswerQList[0] == null || unanswerQList.Count == 0)
        {
            unanswerQList = refQuestions.ToList<Questions>();
        }

        SetCurrentQuestion();
        UpdateScore();
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    void SetCurrentQuestion()
    {
        int unanswerQIndex = Random.Range(0, unanswerQList.Count);
        currentQuestion = unanswerQList[unanswerQIndex];

       QuestionText.text = currentQuestion.Question;

        if (currentQuestion.isTrue)
        {
            TruthAnswerText.text = "FALSE";
            FalseAnswerText.text = "CORRECT";
        }
        else
        { 
            TruthAnswerText.text = "CORRECT";
            FalseAnswerText.text = "FALSE";
        }
    }


    IEnumerator QuestionTransition()
    {
        unanswerQList.Remove(currentQuestion);

        yield return new WaitForSeconds(questionBuffer);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Wrong!");
            AddScore(-1);
        }
        else
        {
            Debug.Log("Right!");
            AddScore(1);
        }

        StartCoroutine(QuestionTransition());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Wrong!");
            AddScore(-1);
        }
        else
        {
            Debug.Log("Right!");
            AddScore(1);
        }

        StartCoroutine(QuestionTransition());
    }
}
