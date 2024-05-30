using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase CharacterDB;
    public TextMeshProUGUI characterNameText;
    public Image characterImage;

    private int selectedOption = 0;
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 5;//change this number to set the default avatar//also need to change this in the game scene !!!
        }
        else
        {
            LoadCharacter();
        }

        UpdateCharacter(selectedOption);
    }
    public void NextOption()
    {
        selectedOption += 1;
        if(selectedOption >= CharacterDB.characterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        //SaveCharacter();
    }
    public void BackOption()
    {
        selectedOption -= 1;
        if(selectedOption < 0)
        {
            selectedOption = CharacterDB.characterCount - 1;
        }
        UpdateCharacter(selectedOption);
        //SaveCharacter();
    }
    private void UpdateCharacter(int selectedOption)
    {
        Character character = CharacterDB.GetCharacter(selectedOption);
        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;
    }
    private void LoadCharacter()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

}
