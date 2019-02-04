using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBanner : MonoBehaviour
{
    void Start()
    {
        showBannerAd();
    }

    private void showBannerAd()
    {
        string adID = "ca-app-pub-1..6/9..0";

        //***For Testing in the Device***
        // AdRequest request = new AdRequest.Builder()
        //.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
        //.AddTestDevice("2..9b")  // My test device.
        //.Build();

        //***For Production When Submit App***
        AdRequest request = new AdRequest.Builder().Build();

        BannerView bannerAd = new BannerView(adID, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(request);
    }
}
