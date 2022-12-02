using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Events;

public class LoginRegister : MonoBehaviour
{
    [HideInInspector]
    public string playFabId;

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public TextMeshProUGUI displayText;

    public UnityEvent onLoggedIn;

    #region instance
    public static LoginRegister instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            gameObject.SetActive(false);
    }
    #endregion

    public void OnLoginButton ()
    {
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest
        {
            Username = usernameInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest,
            result => 
            {
                SetDisplayText("Logged in as: " + result.PlayFabId, Color.green);
                playFabId = result.PlayFabId;

                if (onLoggedIn != null)
                    onLoggedIn.Invoke();
            },
            error => SetDisplayText(error.ErrorMessage, Color.red)
        );
    }

    public void OnRegisterButton()
    {
        RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest
        {
            Username = usernameInput.text,
            DisplayName = usernameInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, 
            result => SetDisplayText("Registered new account a: " + result.PlayFabId, Color.green),
            error => SetDisplayText(error.ErrorMessage, Color.red)
        );
    }

    void SetDisplayText (string text, Color color)
    {
        displayText.text = text;
        displayText.color = color;
    }
}
