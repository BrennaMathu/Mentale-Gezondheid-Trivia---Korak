using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    //is told by the questionmanager if this answer is correct or wrong.
    public bool correctAnswer;
    //reference to the text that displays the answer.
    [SerializeField]
    private TextMeshProUGUI answerText;
    [SerializeField]
    public Image buttonImage;
    [SerializeField]
    private Button thisButton;
    [SerializeField]
    private GameManager gameManager;
    
    
    public static event Action CorrectAnswerAction;
    public static event Action IncorrectAnswerAction;
    
    //called by QuestionManager to set the answerText
    public void SetAnswerText(string newAnswerText)
    {
        answerText.text = newAnswerText;
    }

    //called by QuestionManager to set if this is the correct answer or not.
    public void SetCorrectAnswer(bool newCorrectAnswer)
    {
        correctAnswer = newCorrectAnswer;
    }

    public void SetActiveOrInactive(bool active)
    {
        if (active){thisButton.interactable = true;}
        else{thisButton.interactable = false;}
    }

    //method called when button is clicked. Checks if the answer ir correct or not and invokes the corresponding event.
    //Also sets the other buttons un-interactable and colours the clicked button.
    public void OnClick()
    {
        gameManager.SetOtherButtonsInactive(thisButton);
        //checks if this is the correct answer using the boolean set by QuestionManager.
        if (correctAnswer)
        {
            buttonImage.color = Color.green;
            CorrectAnswerAction?.Invoke();
        }
        else
        {
            buttonImage.color = Color.red;
            IncorrectAnswerAction?.Invoke();
        }
    }    
    

    
}
