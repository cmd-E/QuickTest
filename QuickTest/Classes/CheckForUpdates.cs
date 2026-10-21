using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QuickTest.Classes
{
    public enum UpdateStatus
    {
        Available,
        Current,
        Failed
    }
    public sealed class UpdateResult
    {
        public UpdateStatus Status;
        public string Tag;
        public UpdateResult(UpdateStatus status, string tag)
        {
            Status = status;
            Tag = tag;
        }
    }
    [DataContract]
    public sealed class Release
    {
        [DataMember(Name = "tag_name")]
        public string Tag { get; set; }
    }
    public static class CheckForUpdates
    {
        private static readonly HttpClient Client =
            new HttpClient { Timeout = TimeSpan.FromSeconds(15), MaxResponseContentBufferSize = 1024 * 1024 };
        private static readonly SemaphoreSlim Gate = new SemaphoreSlim(1, 1);
        public static bool TryVersion(string tag, out Version version)
        {
            version = null;
            if (string.IsNullOrWhiteSpace(tag))
                return false;
            tag = tag.Trim();
            if (tag.StartsWith("v", StringComparison.OrdinalIgnoreCase))
                tag = tag.Substring(1);
            if (!Version.TryParse(tag, out version) || version.Build < 0)
                return false;
            version = new Version(version.Major, version.Minor, version.Build, Math.Max(0, version.Revision));
            return true;
        }
        public static async Task<UpdateResult> Check()
        {
            return await CheckUsing(Client);
        }
        internal static async Task<UpdateResult> CheckUsing(HttpClient client)
        {
            await Gate.WaitAsync();
            try
            {
                using (var request = new HttpRequestMessage(
                           HttpMethod.Get, "https://api.github.com/repos/cmd-e/QuickTest/releases/latest"))
                {
                    request.Headers.UserAgent.ParseAdd("QuickTest/1.0");
                    request.Headers.Accept.ParseAdd("application/vnd.github+json");
                    using (var response = await client.SendAsync(request).ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode)
                            return new UpdateResult(UpdateStatus.Failed, null);
                        using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var release = (Release) new DataContractJsonSerializer(typeof(Release)).ReadObject(stream);
                            Version remote;
                            if (release == null || !TryVersion(release.Tag, out remote))
                                return new UpdateResult(UpdateStatus.Failed, null);
                            var v = Assembly.GetExecutingAssembly().GetName().Version;
                            var current = new Version(v.Major, v.Minor, Math.Max(0, v.Build), Math.Max(0, v.Revision));
                            return new UpdateResult(remote > current ? UpdateStatus.Available : UpdateStatus.Current,
                                                    release.Tag);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new UpdateResult(UpdateStatus.Failed, null);
            }
            finally
            {
                Gate.Release();
            }
        }
    }
}
