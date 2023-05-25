using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputValidation : MonoBehaviour
{ 
    public TMP_InputField nameField;
    public TMP_InputField passwordField;

    
    public TMP_Text errorText;
    public Button submitBtn;

    public void CheckInput()
    {
        if (nameField.text.Length > 16 || passwordField.text.Length > 32)
        {
            errorText.text = "You cannot enter more characters";
            submitBtn.interactable = false;

        }
        else
        {
            submitBtn.interactable = true;
            errorText.text = " ";
        }
    }
}
