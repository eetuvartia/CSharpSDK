using System.Collections.Generic;
using System.Text;

namespace PlayFab
{
    public class PlayFabApiSettings
    {
        public readonly Dictionary<string, string> RequestGetParams = new Dictionary<string, string> {
            { "sdk", PlayFabSettings.SdkVersionString }
        };

        /// <summary> This is only for customers running a private cluster.  Generally you shouldn't touch this </summary>
        public string ProductionEnvironmentUrl = ".playfabapi.com";
        /// <summary> The name of a customer vertical. This is only for customers running a private cluster.  Generally you shouldn't touch this </summary>
        public string VerticalName = null;
        /// <summary> Session token for Entity API. Auto-Populated by GetEntityToken method. </summary>
        internal string EntityToken = null;
        /// <summary> Session ticket for Client API. Auto-Populated by any login or registration call. </summary>
        internal string ClientSessionTicket = null;
        /// <summary> You must set this value for PlayFabSdk to work properly (Found in the Game Manager for your title, at the PlayFab Website) </summary>
        public string DeveloperSecretKey = null;
        /// <summary> You must set this value for PlayFabSdk to work properly (Found in the Game Manager for your title, at the PlayFab Website) </summary>
        public string TitleId;
        public ErrorCallback GlobalErrorHandler;
        /// <summary> Set this to the appropriate AD_TYPE_X constant below </summary>
        public string AdvertisingIdType = null;
        /// <summary> Set this to corresponding device value </summary>
        public string AdvertisingIdValue = null;

        // DisableAdvertising is provided for completeness, but changing it is not suggested
        // Disabling this may prevent your advertising-related PlayFab marketplace partners from working correctly
        public bool DisableAdvertising = false;
        public readonly string AD_TYPE_IDFA = "Idfa";
        public readonly string AD_TYPE_ANDROID_ID = "Adid";

        public virtual string GetFullUrl(string apiCall, Dictionary<string, string> getParams)
        {
            return PlayFabSettings.GetFullUrl(apiCall, getParams, this);
        }
    }
}
