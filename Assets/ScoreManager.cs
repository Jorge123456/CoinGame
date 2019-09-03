using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnjinSDK;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    

    public TextMeshProUGUI textMesh;
    public static ScoreManager instance;
    int score;

    public void increaseScore() {
        score++;
        instance.textMesh.text = "Altcoin bag: " + score.ToString();
        if(score > 0 && score % 150 == 0) {
            Debug.Log("Time to mint"+ score);
            Mint();
        }
    }

    async private void Mint(){
        EnjinIdentity userIdentity = AccountManager.instance.userIdentity;
        EnjinAdminIdentity adminIdentity = AccountManager.instance.adminIdentity; 
        Debug.Log(receiverAddress);

        string receiverAddress = userIdentity.GetEthereumAddress().Reduce(() => {
        throw new System.Exception("Can't find any ETH address");
        });
        Debug.Log(receiverAddress);
        
        string tokenID = "";
        string[] receivers = new string[] { receiverAddress };
        int[] amounts = new int[] { 1 };
        
        IRequestHandler mintRequestHandler = 
        adminIdentity.CreateMintItemRequestHandler(tokenID, receivers, amounts);

        await mintRequestHandler.RegisterCallback(RequestEventType.MintPending, (requestEvent) => {
            Debug.Log("Completed Mint Pending");
        });

        await mintRequestHandler.RegisterCallback(RequestEventType.MintBroadcast, (requestEvent) => {
            Debug.Log("Completed Mint Broadcast");
        });

        await mintRequestHandler.RegisterCallback(RequestEventType.MintExecuted, (requestEvent) => {
            Debug.Log("Completed Mint Executed");
    });
    }

        await mintRequestHandler.Execute();
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) {
            instance = this;
        }
        score = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
