using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public XPData xpData;
    public Slider sliderXP;
    public TMP_Text levelText;
    public Transform playerTransform;
    public Rigidbody boxRigidbody;

    public TMP_Text velocityText; // Reference to the TextMeshPro Text for velocity
    public TMP_Text gravityText;  // Reference to the TextMeshPro Text for gravity
    public TMP_Text massText;  // Reference to the TextMeshPro Text for mass

    private float timeElapsed = 0f;
    private Vector3 playerStartPosition;
    private bool increaseMassKeyPressed = false;
    private bool decreaseMassKeyPressed = false;

    private float massMultiplier = 1.0f;
    private float massIncrement = 0.01f; // Initial small increment

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playerTransform != null)
        {
            playerStartPosition = playerTransform.position;
        }

        SetBoxMass(1.0f);
    }

    void Update()
    {
        sliderXP.value = xpData.currentXP;
        levelText.text = xpData.currentLevel.ToString();

        // Check XP and level up
        if (xpData.currentXP >= 10)
        {
            LevelUp();
        }

        // Check if we are in Scene 2 or Scene 5
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 5f)
            {
                GainXP(1);
                timeElapsed -= 5f;
            }

            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                if (playerTransform != null && playerTransform.position.y < playerStartPosition.y - 10f)
                {
                    TeleportPlayerToStart();
                }

                // Check if "[" key is pressed
                if (Input.GetKeyDown(KeyCode.LeftBracket))
                {
                    decreaseMassKeyPressed = true;
                    massIncrement = 0.01f; // Initial small increment
                    Debug.Log("Decreased mass. Multiplier: " + massMultiplier);
                }

                // Check if "]" key is pressed
                if (Input.GetKeyDown(KeyCode.RightBracket))
                {
                    increaseMassKeyPressed = true;
                    massIncrement = 0.01f; // Initial small increment
                    Debug.Log("Increased mass. Multiplier: " + massMultiplier);
                }

                // Check if "[" key is released
                if (Input.GetKeyUp(KeyCode.LeftBracket))
                {
                    decreaseMassKeyPressed = false;
                }

                // Check if "]" key is released
                if (Input.GetKeyUp(KeyCode.RightBracket))
                {
                    increaseMassKeyPressed = false;
                }

                // Adjust mass logarithmically based on key presses
                if (decreaseMassKeyPressed)
                {
                    massMultiplier -= massIncrement; // Decrease mass incrementally
                    massIncrement *= 1.1f; // Increase the increment rate
                }

                if (increaseMassKeyPressed)
                {
                    massMultiplier += massIncrement; // Increase mass incrementally
                    massIncrement *= 1.1f; // Increase the increment rate
                }

                // Ensure massMultiplier stays within a reasonable range
                massMultiplier = Mathf.Clamp(massMultiplier, 0.01f, 100.0f);

                // Set box mass using the logarithmic multiplier
                SetBoxMass(1.0f * massMultiplier);

                // Calculate and display velocity
                if (boxRigidbody != null)
                {
                    // Calculate velocity
                    float velocity = boxRigidbody.velocity.magnitude;

                    // Get the current mass
                    float mass = boxRigidbody.mass;

                    // Update the velocity text
                    velocityText.text = "Velocity: " + velocity.ToString("F2");

                    // Update the mass text
                    massText.text = "Mass: " + mass.ToString("F2");
                }

                // Check for gravity change inputs
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    // Set gravity to Earth's gravity
                    Physics.gravity = new Vector3(0, -9.81f, 0);
                    Debug.Log("Gravity changed to Earth");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    // Set gravity to Mars' gravity
                    Physics.gravity = new Vector3(0, -3.71f, 0);
                    Debug.Log("Gravity changed to Mars");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    // Set gravity to Moon's gravity
                    Physics.gravity = new Vector3(0, -1.625f, 0);
                    Debug.Log("Gravity changed to Moon");
                }

                // Display the current planet's gravity
                UpdateGravityText();
            }

            
        }
    }

    // Function to update the gravity text based on the current gravity setting
    private void UpdateGravityText()
    {
        Vector3 currentGravity = Physics.gravity;
        string planetName = "Unknown";

        if (currentGravity == new Vector3(0, -9.81f, 0))
        {
            planetName = "Earth";
        }
        else if (currentGravity == new Vector3(0, -3.71f, 0))
        {
            planetName = "Mars";
        }
        else if (currentGravity == new Vector3(0, -1.625f, 0))
        {
            planetName = "Moon";
        }

        gravityText.text = "Gravity: " + planetName;
    }

    public void GainXP(int XP)
    {
        xpData.currentXP += XP;
    }

    public void LevelUp()
    {
        xpData.currentXP = 0;
        xpData.currentLevel += 1;
        FindObjectOfType<AudioManager>().Play("LevelUp");
    }

    private void TeleportPlayerToStart()
    {
        playerTransform.position = playerStartPosition;
    }

    private void SetBoxMass(float mass)
    {
        if (boxRigidbody != null)
        {
            boxRigidbody.mass = Mathf.Max(0.01f, mass);
        }
    }
}
