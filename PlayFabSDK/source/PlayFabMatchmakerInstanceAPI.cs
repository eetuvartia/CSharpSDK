using PlayFab.MatchmakerModels;
using PlayFab.Internal;
using PlayFab.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// Enables the use of an external match-making service in conjunction with PlayFab hosted Game Server instances
    /// </summary>
    public class PlayFabMatchmakerInstanceAPI
    {
	    private PlayFabApiSettings apiSettings = null;
        private PlayFabAuthenticationContext authenticationContext = null;
		
		public PlayFabMatchmakerInstanceAPI()
        {

        }

        public PlayFabMatchmakerInstanceAPI(PlayFabApiSettings settings = null)
        {
            apiSettings = settings;
        }

        public PlayFabMatchmakerInstanceAPI(PlayFabAuthenticationContext context = null)
        {
            authenticationContext = context;
        }
		
		public PlayFabMatchmakerInstanceAPI(PlayFabApiSettings settings = null, PlayFabAuthenticationContext context = null)
        {
            apiSettings = settings;
            authenticationContext = context;
        }
		
		public void SetSettings(PlayFabApiSettings settings)
        {
            apiSettings = settings;
        }

        public PlayFabApiSettings GetSettings()
        {
            return apiSettings;
        }

        public void SetAuthenticationContext(PlayFabAuthenticationContext context)
        {
            authenticationContext = context;
        }

        public PlayFabAuthenticationContext GetAuthenticationContext()
        {
            return authenticationContext;
        }
		
        /// <summary>
        /// Validates a user with the PlayFab service
        /// </summary>
        public async Task<PlayFabResult<AuthUserResponse>> AuthUserAsync(AuthUserRequest request, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            var developerSecretKey = request.AuthenticationContext?.DeveloperSecretKey ?? (authenticationContext?.DeveloperSecretKey ?? PlayFabSettings.DeveloperSecretKey);
            if (developerSecretKey == null) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "DeveloperSecretKey is not found in Request, Server Instance or PlayFabSettings");

            var httpResult = await PlayFabHttp.DoPost("/Matchmaker/AuthUser", request, "X-SecretKey", developerSecretKey, extraHeaders, apiSettings);
            if (httpResult is PlayFabError)
            {
                var error = (PlayFabError)httpResult;
                PlayFabSettings.GlobalErrorHandler?.Invoke(error);
                return new PlayFabResult<AuthUserResponse> { Error = error, CustomData = customData };
            }

            var resultRawJson = (string)httpResult;
            var resultData = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<PlayFabJsonSuccess<AuthUserResponse>>(resultRawJson);
            var result = resultData.data;

            return new PlayFabResult<AuthUserResponse> { Result = result, CustomData = customData };
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has joined the Game Server Instance specified
        /// </summary>
        public async Task<PlayFabResult<PlayerJoinedResponse>> PlayerJoinedAsync(PlayerJoinedRequest request, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            var developerSecretKey = request.AuthenticationContext?.DeveloperSecretKey ?? (authenticationContext?.DeveloperSecretKey ?? PlayFabSettings.DeveloperSecretKey);
            if (developerSecretKey == null) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "DeveloperSecretKey is not found in Request, Server Instance or PlayFabSettings");

            var httpResult = await PlayFabHttp.DoPost("/Matchmaker/PlayerJoined", request, "X-SecretKey", developerSecretKey, extraHeaders, apiSettings);
            if (httpResult is PlayFabError)
            {
                var error = (PlayFabError)httpResult;
                PlayFabSettings.GlobalErrorHandler?.Invoke(error);
                return new PlayFabResult<PlayerJoinedResponse> { Error = error, CustomData = customData };
            }

            var resultRawJson = (string)httpResult;
            var resultData = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<PlayFabJsonSuccess<PlayerJoinedResponse>>(resultRawJson);
            var result = resultData.data;

            return new PlayFabResult<PlayerJoinedResponse> { Result = result, CustomData = customData };
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has left the Game Server Instance specified
        /// </summary>
        public async Task<PlayFabResult<PlayerLeftResponse>> PlayerLeftAsync(PlayerLeftRequest request, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            var developerSecretKey = request.AuthenticationContext?.DeveloperSecretKey ?? (authenticationContext?.DeveloperSecretKey ?? PlayFabSettings.DeveloperSecretKey);
            if (developerSecretKey == null) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "DeveloperSecretKey is not found in Request, Server Instance or PlayFabSettings");

            var httpResult = await PlayFabHttp.DoPost("/Matchmaker/PlayerLeft", request, "X-SecretKey", developerSecretKey, extraHeaders, apiSettings);
            if (httpResult is PlayFabError)
            {
                var error = (PlayFabError)httpResult;
                PlayFabSettings.GlobalErrorHandler?.Invoke(error);
                return new PlayFabResult<PlayerLeftResponse> { Error = error, CustomData = customData };
            }

            var resultRawJson = (string)httpResult;
            var resultData = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<PlayFabJsonSuccess<PlayerLeftResponse>>(resultRawJson);
            var result = resultData.data;

            return new PlayFabResult<PlayerLeftResponse> { Result = result, CustomData = customData };
        }

        /// <summary>
        /// Instructs the PlayFab game server hosting service to instantiate a new Game Server Instance
        /// </summary>
        public async Task<PlayFabResult<StartGameResponse>> StartGameAsync(StartGameRequest request, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            var developerSecretKey = request.AuthenticationContext?.DeveloperSecretKey ?? (authenticationContext?.DeveloperSecretKey ?? PlayFabSettings.DeveloperSecretKey);
            if (developerSecretKey == null) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "DeveloperSecretKey is not found in Request, Server Instance or PlayFabSettings");

            var httpResult = await PlayFabHttp.DoPost("/Matchmaker/StartGame", request, "X-SecretKey", developerSecretKey, extraHeaders, apiSettings);
            if (httpResult is PlayFabError)
            {
                var error = (PlayFabError)httpResult;
                PlayFabSettings.GlobalErrorHandler?.Invoke(error);
                return new PlayFabResult<StartGameResponse> { Error = error, CustomData = customData };
            }

            var resultRawJson = (string)httpResult;
            var resultData = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<PlayFabJsonSuccess<StartGameResponse>>(resultRawJson);
            var result = resultData.data;

            return new PlayFabResult<StartGameResponse> { Result = result, CustomData = customData };
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user, which the external match-making service can then use to compute
        /// effective matches
        /// </summary>
        public async Task<PlayFabResult<UserInfoResponse>> UserInfoAsync(UserInfoRequest request, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            var developerSecretKey = request.AuthenticationContext?.DeveloperSecretKey ?? (authenticationContext?.DeveloperSecretKey ?? PlayFabSettings.DeveloperSecretKey);
            if (developerSecretKey == null) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "DeveloperSecretKey is not found in Request, Server Instance or PlayFabSettings");

            var httpResult = await PlayFabHttp.DoPost("/Matchmaker/UserInfo", request, "X-SecretKey", developerSecretKey, extraHeaders, apiSettings);
            if (httpResult is PlayFabError)
            {
                var error = (PlayFabError)httpResult;
                PlayFabSettings.GlobalErrorHandler?.Invoke(error);
                return new PlayFabResult<UserInfoResponse> { Error = error, CustomData = customData };
            }

            var resultRawJson = (string)httpResult;
            var resultData = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).DeserializeObject<PlayFabJsonSuccess<UserInfoResponse>>(resultRawJson);
            var result = resultData.data;

            return new PlayFabResult<UserInfoResponse> { Result = result, CustomData = customData };
        }

    }
}
