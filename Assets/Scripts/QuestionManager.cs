using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private AnswerButton[] answerButtons;
    [SerializeField]
    private TextMeshProUGUI explanationText;
    [SerializeField]
    private TextMeshProUGUI referenceText;

    [Header("Data")]
    [SerializeField]
    private List<QuestionData> questions;
    private QuestionData currentQuestion;
    [SerializeField] 
    private int correctAnswerChoice;

    private void Awake()
    {
        GetQuestions();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SelectNewQuestion();
        SetQuestionValues();
        SetAnswerValues();
    }

    private void GetQuestions()
    {
        //Put all the questions(QuestionData) from Resources/Questions in the questions list.
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }

    private void SelectNewQuestion()
    {
        //Get a random value to choose a question with.
        int randomQuestion = Random.Range(0, questions.Count);
        //Get a random question using the int.
        currentQuestion = questions[randomQuestion];
        //remove selected question so it can't be selected again.
        questions.RemoveAt(randomQuestion);
    }

    private void SetQuestionValues()
    {
        //set the category text to the current category.
        categoryText.text = currentQuestion.category;
        //set the question text to the current question.
        questionText.text = currentQuestion.question;
        //set the explanation text to the current explanation.
        explanationText.text = currentQuestion.explanation;
        //set the reference text to the current reference.
        referenceText.text = currentQuestion.reference;
    }

    private void SetAnswerValues()
    {
        //randomize the order the answers of the currentQuestion appear in.
        List<string> answers = RandomAnswers(new List<string>(currentQuestion.answers));

        for (int i = 0; i < answerButtons.Length; i++)
        {
            //boolean to pass along if the answer is correct or not.
            bool isCorrect = false;
            //set boolean to true if it's the correct answer.
            if (i == correctAnswerChoice) { isCorrect = true; }
            
            //pass the information along to the corresponding button.
            answerButtons[i].SetCorrectAnswer(isCorrect);
            answerButtons[i].SetAnswerText(answers[i]);
        }
    }

    private List<string> RandomAnswers(List<string> originalAnswers)
    {
        //answer is wrong unless found otherwise later.
        bool correctAnswerChoosen = false;
        //list that stores the answers in the new randomized order.
        List<string> newAnswers = new List<string>();

        //gets a random answer from the originalAnswers list and puts it in the newAnswers list.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            //Get a random int to choose a (remaining) answer with
            int random = Random.Range(0, originalAnswers.Count);
            
            //the first time the chosen answer is number 0, it's the correct answer.
            if (random == 0 && !correctAnswerChoosen)
            {
                //stores the number of the answer that's correct.
                correctAnswerChoice = i;
                //ensures the second answer in position 0 is stored as wrong.
                correctAnswerChoosen = true;
            }
            //add this answer to the new list.
            newAnswers.Add(originalAnswers[random]);
            //remove this answer from the original list.
            originalAnswers.RemoveAt(random);
        }
        //returns the randomized list to be used by the SetAnswerValues method.
        return newAnswers;
    }
}
