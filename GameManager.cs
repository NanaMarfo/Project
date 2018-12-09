using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public Questions[] refQuestions;
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
    static int scorePoint = 0;
    [SerializeField]
    static Text scoreText;

    [SerializeField]
    Animator animator;

    void Start()
    {
        if (unanswerQList == null || unanswerQList.Count == 0)
        {
            unanswerQList = refQuestions.ToList<Questions>();
        }

        SetCurrentQuestion();
        UpdateScore();

    }
    void SetCurrentQuestion()
    {
        int unanswerQIndex = Random.Range(0, unanswerQList.Count);
        currentQuestion = unanswerQList[unanswerQIndex];

       QuestionText.text = currentQuestion.Question;

        if (currentQuestion.isTrue)
        {
            TruthAnswerText.text = "CORRECT";
            FalseAnswerText.text = "FALSE";
        }
        else
        {
            TruthAnswerText.text = "FALSE";
            FalseAnswerText.text = "CORRECT";
        }
    }

    public void AddScore(int newScore)
    {
        scorePoint += newScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + scorePoint;
    }

    IEnumerator QuestionTransition()
    {
        unanswerQList.Remove(currentQuestion);

        yield return new WaitForSeconds(questionBuffer);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Right!");
            AddScore(1);
        }
        else
        {
            Debug.Log("Wrong!");
        }

        StartCoroutine(QuestionTransition());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Right!");
            AddScore(1);
        }
        else
        {
            Debug.Log("Wrong!");
        }

        StartCoroutine(QuestionTransition());
    }
}
