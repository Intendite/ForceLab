using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using TMPro;

public class GravityQuiz : MonoBehaviour
{
    // References to GameManager and UI elements
    public GameManager gameManager;
    public Console console;
    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text feedbackText;

    // Arrays to store questions and answers
    private string[] userAnswers;
    private int currentQuestionIndex = 0;

    private string[] questions = {
        "What is the SI unit for mass?",
        "What is the SI unit for weight?",
        "What is mass?",
        "What is weight?",
        "Is mass constant?",
        "Is weight constant?",
        "What is a gravitational field?",
        "Define gravitational field strength.",
        "What is density?",
        "What are the units of density?"
        /* Add more questions here */
    };

    private string[] correctAnswers = {
        "kilogram (kg)",
        "newton (N)",
        "The amount of substance in a body.",
        "The gravitational force acting on an object due to the gravitational field.",
        "Yes, mass is constant.",
        "No, weight is not constant.",
        "A region of space where a body with mass experiences gravitational force due to gravitational attraction.",
        "Gravitational force per unit mass.",
        "Mass per unit volume.",
        "kilogram per cubic meter (kg/m�)"
        /* Add more correct answers here */
    };

    private string[][] wrongAnswers = {
        new string[] { "gram (g)", "meter (m)", "Density." },
        new string[] { "kilogram per cubic meter (kg/m�)", "meter per second squared (m/s�)", "The amount of matter in an object." },
        new string[] { "The gravitational force acting on an object.", "The force applied to lift an object.", "The volume of an object." },
        new string[] { "The amount of substance in a body.", "The force applied to lift an object.", "The volume of an object." },
        new string[] { "No, mass changes with location.", "No, mass changes with time.", "No, mass is constant." },
        new string[] { "No, weight changes with density.", "No, weight changes with location.", "Yes, weight is constant." },
        new string[] { "A region of space where there is no gravity.", "A region of space where electromagnetic fields exist.", "A region of space where light travels faster." },
        new string[] { "The force of gravity on an object.", "The force of air resistance on an object.", "The amount of mass in a region." },
        new string[] { "The amount of matter in a body.", "The gravitational force acting on an object.", "The mass per unit length." },
        new string[] { "newton per cubic meter (N/m�)", "gram per milliliter (g/mL)", "kilogram per liter (kg/L)" }
        /* Add more wrong answer arrays here */
    };

    private bool questionAnswered = false;

    private void Start()
    {
        // Initialize userAnswers array with empty strings
        userAnswers = new string[questions.Length].Select(x => "").ToArray();

        // Display the first question
        ShowQuestion(currentQuestionIndex);
    }

    public void AnswerSelected(int answerIndex)
    {
        if (questionAnswered)
        {
            return;
        }

        string selectedAnswer = answerButtons[answerIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

        userAnswers[currentQuestionIndex] = selectedAnswer;

        // Disable answer buttons after selection
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }

        if (selectedAnswer == correctAnswers[currentQuestionIndex])
        {
            gameManager.GainXP(2);
            Debug.Log("Gained 2 XP");
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Wrong!";
        }

        // Move to the next question after a delay
        StartCoroutine(NextQuestionWithDelay());

        questionAnswered = true;
    }

    private IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(1.0f);

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
        // Enable answer buttons
        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }

        questionText.text = questions[index];

        // Shuffle answers to display in random order
        string[] allAnswers = wrongAnswers[index].Concat(new string[] { correctAnswers[index] }).ToArray();
        allAnswers = allAnswers.OrderBy(a => Random.value).ToArray();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = allAnswers[i];
        }

        feedbackText.text = "";

        questionAnswered = false;
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
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(0);
    }
}
