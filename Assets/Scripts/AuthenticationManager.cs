using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Authentication;
using TMPro;

public class AuthenticationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private TMP_Text _desctitionText;

    // Start is called before the first frame update
    async void Start()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("development");

        await UnityServices.InitializeAsync(options);
        UpdateUI();

        AuthenticationService.Instance.SignedIn += delegate
        {
            Debug.Log("Signed in...");
            UpdateUI();
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        UpdateUI();
    }

    void UpdateUI()
    {
        _statusText.text = UnityServices.State.ToString();
        _playerNameText.text = $"Guest: {AuthenticationService.Instance.PlayerId}";
        _desctitionText.text = $"Player ID: {AuthenticationService.Instance.PlayerId}\nAccess token: {AuthenticationService.Instance.AccessToken}";
    }


    // Butons
    public void OnClickBtnEconomy()
    {
        Debug.Log("Btn Economy Cliked!!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
