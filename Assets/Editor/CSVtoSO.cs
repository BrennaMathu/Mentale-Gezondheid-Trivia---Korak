using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO
{
    private static string questionsPath = "/Editor/Questions.csv";
    private static int numberOfAnswers = 4;

    [MenuItem("Utilities/Generate Questions")]
    public static void GenerateQuestions()
    {
        Debug.Log("Attempted to Generate Questions at" + questionsPath);
        //Make each row a string
        string[] allLines = File.ReadAllLines(Application.dataPath + questionsPath);

        foreach (string s in allLines)
        {
            //separate the columns on each row by splitting at the comma's
            string[] splitData = s.Split(',');
            
            //QUESTION, CATEGORY, CORRECT ANSWER, WRONG ANSWER 1, WRONG ANSWER 2, WRONG ANSWER 3, EXPLANATION, REFERENCE.
            
            //Create the Scriptable object for this question.
            QuestionData questionData = ScriptableObject.CreateInstance<QuestionData>();
            
            //Set the singular values.
            questionData.question = splitData[0];
            questionData.category = splitData[1];
            questionData.explanation = splitData[numberOfAnswers + 2];
            questionData.reference = splitData[numberOfAnswers + 3];
            
            //Put the answers in the array.
            questionData.answers = new string[numberOfAnswers];
            for (int i = 0; i < numberOfAnswers; i++)
            {
                questionData.answers[i] = splitData[2 + i];
            }
            
            //Create the file name, remove the question mark if present to prevent errors.
            if (questionData.question.Contains("?"))
            {
                questionData.name = questionData.question.Remove(questionData.question.IndexOf("?"));
            }
            else
            {
                questionData.name = questionData.question;
            }
            
            //Save the question in the AssetDatabase.
            AssetDatabase.CreateAsset(questionData, $"Assets/Resources/Questions/{questionData.name}.asset");
        }
        //Remove the question called QUESTION from the list, because this question was made from the headings.
        AssetDatabase.DeleteAsset($"Assets/Resources/Questions/QUESTION.asset");
        //Save the Assets in the database.
        AssetDatabase.SaveAssets();
    }
}
