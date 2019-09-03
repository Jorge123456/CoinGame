using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnjinSDK;
using CodingHelmet.Optional;
using UnityEngine.SceneManagement;


public class AccountManager : MonoBehaviour
{
    public static AccountManager instance;
    int score;

    public GameObject emailInput;
    public GameObject passwordInput;

    public EnjinIdentity userIdentity = null; 
    public EnjinAdminIdentity adminIdentity = null; 
    public string adminAddress = "";

    private readonly string PLATFORM_URL =  "https://kovan.cloud.enjin.io/";
    private readonly int APP_ID =  1157;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) {
            instance = this;
        }
        score = 0;
        EnjinConnector.Initialize(PLATFORM_URL);
        LoginAdmin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async public void LoginAdmin() {
        string email = "sd9l@protonmail.com";
        string password = "Pass";
        EnjinUser user = await EnjinUser.Login(email, password);
        Option<EnjinIdentity> adminIdentityOption = await user.GetIdentityForAppId(APP_ID);
        EnjinIdentity identity = adminIdentityOption.Reduce(() =>{
        throw new System.Exception("User ID not found");
    }); 
        adminAddress = identity.GetEthereumAddress().Reduce("");    
        adminIdentity = identity.asAdmin();
    }

    async public void Login(){
        string email = emailInput.GetComponent<InputField>().text;
        string password = passwordInput.GetComponent<InputField>().text;
        EnjinUser user = await EnjinUser.Login(email, password);
        Option<EnjinIdentity> userIdentityOption = await user.GetIdentityForAppId(APP_ID);
        EnjinIdentity userIdentity = userIdentityOption.Reduce(() =>{
            throw new System.Exception("User ID not found");
        });
        GoToGame();
    }
        private void GoToGame(){
            SceneManager.LoadScene("MainScene");
        }
    }
        
