using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public TextMeshProUGUI myTextElement;

    // This function will run when the button is clicked
    public void SendMessage(string message)
    {
        message= "Button Clicked";
        myTextElement.text = message;
    }

    public void CancelInvoke()
    {
        
    }
}