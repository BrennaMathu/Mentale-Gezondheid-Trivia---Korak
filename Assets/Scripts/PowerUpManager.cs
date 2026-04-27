using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private TextMeshProUGUI coinText;
    public int coinValue;
    public int coinRewardValue;
    [SerializeField]
    private List<AnswerButton> answerButtons;

    [Header("PowerUps")]
    [SerializeField] 
    private int halfChanceCost;
    [SerializeField]
    private TextMeshProUGUI halfChanceCostText;
    
    private void Start()
    {
        //updates the UI to display the correct cost for the HalfChance Power-up.
        halfChanceCostText.text = halfChanceCost.ToString();
    }
    
    //method called from the HalfChance button. Sets the buttons of 2 of the incorrect answers to un-interactable.
    public void HalfChancePowerUp()
    {
        //check if the player has enough coins
        if (coinValue >= halfChanceCost)
        {
            //list to add answers to.
            List<AnswerButton> answersToDisable = new List<AnswerButton>();

            //add the incorrect answers to the answersToDisable list.
            foreach (AnswerButton answerButton in answerButtons)
            {
                if (!answerButton.correctAnswer)
                {
                    answersToDisable.Add(answerButton);
                }
            }
            //delete a random answer from the answersToDisable list
            int answerToRemove = Random.Range(0, answersToDisable.Count);
            answersToDisable.RemoveAt(answerToRemove);
            
            //set the button component on the answerButtons in the answersToDisable list to un-interactable.
            foreach (AnswerButton answerToDisable in answersToDisable)
            {
                answerToDisable.GetComponent<Button>().interactable = false;
            }
            ChangeCoinValue(-halfChanceCost);
        }
    }

    //method that adds the given value to coinValue and updates the UI text.
    public void ChangeCoinValue(int value)
    {
        coinValue += value;
        coinText.text = coinValue.ToString();
    }
}
