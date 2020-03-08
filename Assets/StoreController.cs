using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async public void PurchaseItem(){
        
        string coinTokenID = "";
        string itemTokenID = "";
        string adminAddress = AccountManager.instance.adminAddress;

        EnjinItemParameter[] SendItem = new EnjinItemParameter[] { 
        EnjinItemParameter.ConstructFungible(coinTokenID, 100)};
        EnjinItemParameter[] ReceiveItem = new EnjinItemParameter[] {
        EnjinItemParameter.ConstructFungible(itemTokenID, 100)};

        IRequestHandler tradeCreationHandler =
        AccountManager.instance.userIdentity.CreateTradeItemRequestHandler(SendItem, ReceiveItem, adminAddress);
        IRequestHandler tradeAcceptHandler = 
        AccountManager.instance.adminIdentity.CompleteTradeItemRequestHandler(tradeID);

        await tradeCreationHandler.RegisterCallback(RequestEventType.CreateTradePending, (requestEvent) => {
        Debug.Log("Pending Done");
        },
        await tradeCreationHandler.RegisterCallback(RequestEventType.CreateTradeBroadcast, (requestEvent) => {
        Debug.Log("Broadcasting Done");
        },
        await tradeCreationHandler.RegisterCallback(RequestEventType.CreateTradeExecuted,
        async (requestEvent) => {
        string tradeID = requestEvent.Data.param1;
        Debug.Log("Create Trade Request Executed Done");
        },

        await tradeAcceptHandler.RegisterCallback(RequestEventType.CompleteTradePending,
        (completeRequestEvent) => {
            Debug.Log("Complete Trade Request Pending Done");
        },

        await tradeAcceptHandler.RegisterCallback(RequestEventType.CompleteTradeBroadcast,
        (completeRequestEvent) => {
            Debug.Log("Complete Trade Request Broadcast Done");
        },
        
        await tradeAcceptHandler.RegisterCallback(RequestEventType.CompleteTradeExecuted, 
        async (completeRequestEvent) => {
            Debug.Log("Trade Done!!");
        },

        await tradeAcceptHandler.Execute();
        //  tradeAcceptHandler.Wait(1000),
        
        await tradeCreationHandler.Execute();
        // tradeCreationHandler.Wait(1000);    
    

    }
        
    
}
        

