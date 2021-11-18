using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace QuickTest.Classes
{
    public static class CheckForUpdates
    {
        private const string authorNick = "cmd-e";
        private const string repoName = "QuickTest";
        private const string acceptHeader = "application/vnd.github.v3+json";
        /// <summary>
        /// Sends request to github to check if there is a new version
        /// </summary>
        /// <returns>boolean, indicating if there is new version and string which contains version tag</returns>
        public static async Task<Tuple<bool, string>> Check()
        {
            var apiUrl = $"https://api.github.com/repos/{authorNick}/{repoName}/releases/latest";
            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(apiUrl),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:94.0) Gecko/20100101 Firefox/94.0");
            Task<HttpResponseMessage> response;
            HttpResponseMessage responseResult;
            string jsonString = string.Empty; 
            try
            {
                response = client.SendAsync(request);
                responseResult = response.Result;
                jsonString = await responseResult.Content.ReadAsStringAsync();
            }
            catch
            {
                return new Tuple<bool, string>(false, "-1");
            }
            
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            JsonElement root = jsonDocument.RootElement;
            var versionOnGithub = root.GetProperty("tag_name").GetString();
            return new Tuple<bool, string>(ThereIsNewVersion(versionOnGithub), versionOnGithub);
        }

        private static bool ThereIsNewVersion(string versionOnGithub)
        {
            var currentV = Assembly.GetExecutingAssembly().GetName().Version;
            if (versionOnGithub.StartsWith("v")) 
            {
                versionOnGithub = versionOnGithub.Substring(1);
            }
            var newVersionSplitted = versionOnGithub.Split('.');
            // in semantic versioning X.Y.Z are major, minor and patch
            var major = Convert.ToInt32(newVersionSplitted[0]);
            var minor = Convert.ToInt32(newVersionSplitted[1]);
            var patch = Convert.ToInt32(newVersionSplitted[2]);
            // if something is not the same as latest version from github it means there is new version
            return major > currentV.Major || minor > currentV.Minor || patch > currentV.Build;
        }
    }
}
