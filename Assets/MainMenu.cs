using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string newGameSceneName = "MainScene";
    [SerializeField] private string joinGameSceneName = "MainScene";

    [Header("Optional UI")]
    public TextMeshProUGUI myTextElement;
    public TMP_InputField usernameInput;
    public InputField legacyUsernameInput;

    private void Awake()
    {
        AutoBindInputs();
    }

    public void StartNewGame()
    {
        AutoBindInputs();
        ShowButtonMessage("Starting a new game...");
        string username = GetUsername();
        if (string.IsNullOrWhiteSpace(username))
        {
            username = "Player";
        }

        PlayerPrefs.SetString("username", username);
        PlayerPrefs.Save();
        LoadSceneByName(newGameSceneName);
    }

    public void JoinGame()
    {
        ShowButtonMessage("Joining game...");
        ShowButtonMessage("Loading scene: " + joinGameSceneName);
        LoadSceneByName(joinGameSceneName);
    }

    // Backward-compatible wrapper for existing OnClick bindings.
    public void PlayGame()
    {
        StartNewGame();
    }

    private void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name is empty in MainMenu.");
            ShowButtonMessage("Scene is not configured.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError("Scene not found in Build Settings: " + sceneName);
            ShowButtonMessage("Scene not found: " + sceneName);
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private string GetUsername()
    {
        string selectedInputText = GetSelectedInputText();
        if (!string.IsNullOrWhiteSpace(selectedInputText))
        {
            return selectedInputText;
        }

        if (usernameInput != null)
        {
            return usernameInput.text.Trim();
        }

        if (legacyUsernameInput != null)
        {
            return legacyUsernameInput.text.Trim();
        }

        return string.Empty;
    }

    private void AutoBindInputs()
    {
        if (usernameInput == null)
        {
            TMP_InputField[] tmpInputs = FindObjectsByType<TMP_InputField>(FindObjectsSortMode.None);
            foreach (TMP_InputField inputField in tmpInputs)
            {
                if (inputField != null)
                {
                    usernameInput = inputField;
                    break;
                }
            }
        }

        if (legacyUsernameInput == null)
        {
            InputField[] legacyInputs = FindObjectsByType<InputField>(FindObjectsSortMode.None);
            foreach (InputField inputField in legacyInputs)
            {
                if (inputField != null)
                {
                    legacyUsernameInput = inputField;
                    break;
                }
            }
        }
    }

    private string GetSelectedInputText()
    {
        if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null)
        {
            return string.Empty;
        }

        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        TMP_InputField selectedTmpInput = selectedObject.GetComponent<TMP_InputField>();
        if (selectedTmpInput != null)
        {
            return selectedTmpInput.text.Trim();
        }

        InputField selectedLegacyInput = selectedObject.GetComponent<InputField>();
        if (selectedLegacyInput != null)
        {
            return selectedLegacyInput.text.Trim();
        }

        return string.Empty;
    }

    public void ShowButtonMessage(string message)
    {
        if (myTextElement != null)
        {
            myTextElement.text = message;
        }
    }
}