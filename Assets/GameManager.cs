using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text FactText;

    [SerializeField]
    private Text TrueAnswerText;

    [SerializeField]
    private Text FalseAnswerText;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    private float TimeBetweenQuestions = 1f;

    public Question[] Questions { get => questions; set => questions = value; }

    void start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        int RandomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[RandomQuestionIndex];

        FactText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            TrueAnswerText.text = "CORRECT";
            FalseAnswerText.text = "WRONG";
        }
        else
        {
            TrueAnswerText.text = "WRONG";
            FalseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(TimeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }
}