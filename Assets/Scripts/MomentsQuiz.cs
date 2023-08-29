using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MomentsQuiz : MonoBehaviour
{
    public GameObject QuizMenuUI;
    public TMPro.TMP_Text questionText;
    public Button[] answerButtons;
    public TMPro.TMP_Text feedbackText;
    public static bool GameIsPaused = false;

    private string[] questions = {
        "What is the moment of a force?",
        "Which of the following factors affects the moment of a force?",
        "In which unit is the moment of a force measured?",
        /* Add more questions here */
    };

    private string[] correctAnswers = {
        "The turning effect of a force about a point.",
        "The magnitude of the force and the perpendicular distance from the pivot.",
        "Newton-meter (Nm).",
        /* Add more correct answers here */
    };

    private string[][] wrongAnswers = {
        new string[] { "The magnitude of a force.", "The direction of a force.", "The result of adding two forces." },
        new string[] { "The direction of the force.", "The point of application of the force.", "The color of the force." },
        new string[] { "Kilogram (kg).", "Meter (m).", "Joule (J)." },
        /* Add more wrong answer arrays here */
    };

    private string[] userAnswers;
    private int currentQuestionIndex = 0;

    private void Start()
    {
        userAnswers = new string[questions.Length];
        ShowQuestion(currentQuestionIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameIsPaused)
            {
                QuizEnd();
            }

            else
            {
                QuizTime();
            }
        }
    }

    private void QuizEnd()
    {
        QuizMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    private void QuizTime()
    {
        QuizMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void AnswerSelected(int answerIndex)
    {
        string selectedAnswer = answerButtons[answerIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

        if (selectedAnswer == correctAnswers[currentQuestionIndex])
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Wrong!";
        }

        StartCoroutine(NextQuestionWithDelay());
    }

    private IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // Adjust the delay time as needed

        currentQuestionIndex++;
        feedbackText.text = "";

        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            CalculateScore();
        }
    }

    private void ShowQuestion(int index)
    {
        questionText.text = questions[index];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerIndex = i; // Store the value of i for the lambda function
            // answerButtons[i].onClick.RemoveAllListeners(); // Remove any existing listeners
            // answerButtons[i].onClick.AddListener(() => AnswerSelected(answerIndex)); // Add the listener

            if (i == 0)
            {
                answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = correctAnswers[index];
            }
            else
            {
                answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = wrongAnswers[index][i - 1];
            }
        }
        feedbackText.text = "";
    }

    private void CalculateScore()
    {
        int score = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            if (userAnswers[i] == correctAnswers[i])
            {
                score++;
            }
        }
        feedbackText.text = "Quiz completed! Score: " + score + "/" + questions.Length;
    }


}

