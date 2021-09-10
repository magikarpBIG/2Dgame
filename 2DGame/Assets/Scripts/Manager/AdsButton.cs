using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsButton : MonoBehaviour,IUnityAdsListener
{
#if UNITY_ANDROID
  
     private string gameID="4212705";
#elif UNITY_IOS
    private string gameID = "4212704";
#endif

    Button adsButton;
   
    public string placementID = "Rewarded_Android";
    void Start()
    {
        adsButton = GetComponent<Button>();
      //  adsButton.interactable = Advertisement.IsReady(placementID);
        
        if (adsButton)
        { 
            adsButton.onClick.AddListener(ShowReawrdAds);            
        }
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
            
        
    }
    public void ShowReawrdAds()
    {
        Advertisement.Show(placementID);
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                FindObjectOfType<PlayerController>().health = 3;
                FindObjectOfType<PlayerController>().isDead = false;

                UIManager.instance.UpdateHealth(FindObjectOfType<PlayerController>().health);
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (Advertisement.IsReady(placementID))
        {
            Debug.Log("光gap好了");
        }
    }

}
