using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class MomentsQuiz : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager script
    public Console console;
    public TMPro.TMP_Text questionText;
    public Button[] answerButtons;
    public TMPro.TMP_Text feedbackText;

    private string[] userAnswers; // Store selected answers
    private int currentQuestionIndex = 0;

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


    private void Start()
    {
        userAnswers = new string[questions.Length];
        for (int i = 0; i < userAnswers.Length; i++)
        {
            userAnswers[i] = ""; // Initialize with an empty string
        }
        ShowQuestion(currentQuestionIndex);
    }

    public void AnswerSelected(int answerIndex)
    {
        string selectedAnswer = answerButtons[answerIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

        userAnswers[currentQuestionIndex] = selectedAnswer; // Update userAnswers

        if (selectedAnswer == correctAnswers[currentQuestionIndex])
        {
            gameManager.GainXP(2); // Gain 2 XP for a correct answer through GameManager
            Debug.Log("Gained 2 XP");
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
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed

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

        string[] allAnswers = wrongAnswers[index].Concat(new string[] { correctAnswers[index] }).ToArray();
        allAnswers = allAnswers.OrderBy(a => Random.value).ToArray(); // Shuffle the answers

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = allAnswers[i];
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

        // Load the main menu scene after a delay
        StartCoroutine(LoadMainMenuWithDelay());
    }

    private IEnumerator LoadMainMenuWithDelay()
    {
        yield return new WaitForSeconds(1.5f); // Delay before loading the main menu

        SceneManager.LoadScene(0); // Load the main menu scene
    }
}
