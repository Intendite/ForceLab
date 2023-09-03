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
        "Principle of Moments?",
        "Moment of a force?",
        "Units to measure Moment?",
        "How does Stability relate?",
        "Center of Gravity?",
        "Stable Equilibrium?",
        "Unstable Equilibrium?",
        "Neutral Equilibrium?"
        /* Add more questions here */
    };

    private string[] correctAnswers = {
        "Sum of clockwise and anticlockwise moments equals at equilibrium.",
        "Force's product and perpendicular distance to pivot.",
        "Measured in Newton-meter (Nm).",
        "Related by lowering center of gravity, increasing base area.",
        "Point where weight acts.",
        "Center raised when displaced but stays in base area.",
        "Center raised when displaced, falls outside base area.",
        "Center remains same level, line passes pivot."
        /* Add more correct answers here */
    };

    private string[][] wrongAnswers = {
        new string[] { "Sum of forces is zero.", "Product of distance and time.", "Measured in Joules (J)." },
        new string[] { "Same as weight.", "Result of adding two forces.", "Measured in kilograms (kg)." },
        new string[] { "Measured in meters (m).", "Measured in kilogram-meter (kgm).", "Measured in kilogram-meter per second (kgm/s)." },
        new string[] { "No relation to Moments.", "Related by increasing center of gravity.", "Related by decreasing base area." },
        new string[] { "Same as centroid.", "Point through which weight disappears.", "Always at the top." },
        new string[] { "Center lowered when displaced.", "Center raised but stays in base area.", "No specific conditions." },
        new string[] { "Same as neutral equilibrium.", "No relation to Moments.", "Center lowered when displaced." },
        new string[] { "Same as stable equilibrium.", "No specific conditions.", "Center remains same level, line passes pivot." }
        /* Add more wrong answer arrays here */
    };

    private bool questionAnswered = false;

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
        if (questionAnswered)
        {
            return;
        }

        string selectedAnswer = answerButtons[answerIndex].GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

        userAnswers[currentQuestionIndex] = selectedAnswer;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;
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
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = true;
        }

        questionText.text = questions[index];

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

        StartCoroutine(LoadMainMenuWithDelay());
    }

    private IEnumerator LoadMainMenuWithDelay()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(0);
    }
}
