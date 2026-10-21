using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using QuickTest.Classes;
using Xunit;

namespace QuickTest.Tests
{
    public class UpdateTests
    {
        [Theory]
        [InlineData("1.2.3", "1.2.3.0")]
        [InlineData("v1.2.3", "1.2.3.0")]
        [InlineData(" V1.2.3.4 ", "1.2.3.4")]
        [InlineData("0.0.0", "0.0.0.0")]
        public void NormalizesSupportedReleaseTags(string tag, string expected)
        {
            Assert.True(CheckForUpdates.TryVersion(tag, out var version));
            Assert.Equal(new Version(expected), version);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("v")]
        [InlineData("1.2")]
        [InlineData("vbad")]
        [InlineData("1.2.3-beta")]
        [InlineData("-1.2.3")]
        [InlineData("2147483648.0.0")]
        public void RejectsMalformedReleaseTags(string tag)
        {
            Assert.False(CheckForUpdates.TryVersion(tag, out _));
        }

        [Theory]
        [InlineData("99.0.0", UpdateStatus.Available)]
        [InlineData("0.0.0", UpdateStatus.Current)]
        public async Task ComparesTheWholeVersionAndPreservesTheTag(string tag, UpdateStatus expected)
        {
            using (var client = Client(HttpStatusCode.OK, "{\"tag_name\":\"v" + tag + "\"}"))
            {
                var result = await CheckForUpdates.CheckUsing(client);

                Assert.Equal(expected, result.Status);
                Assert.Equal("v" + tag, result.Tag);
            }
        }

        [Fact]
        public async Task EqualVersionIsCurrent()
        {
            string version = typeof(CheckForUpdates).Assembly.GetName().Version.ToString();
            using (var client = Client(HttpStatusCode.OK, "{\"tag_name\":\"" + version + "\"}"))
                Assert.Equal(UpdateStatus.Current, (await CheckForUpdates.CheckUsing(client)).Status);
        }

        [Theory]
        [InlineData("not JSON")]
        [InlineData("{}")]
        [InlineData("null")]
        [InlineData("{\"tag_name\":null}")]
        [InlineData("{\"tag_name\":\"vbad\"}")]
        public async Task InvalidResponsesReturnRecoverableFailure(string body)
        {
            using (var client = Client(HttpStatusCode.OK, body))
            {
                var result = await CheckForUpdates.CheckUsing(client);

                Assert.Equal(UpdateStatus.Failed, result.Status);
                Assert.Null(result.Tag);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task HttpErrorsAreRecoverable(HttpStatusCode status)
        {
            using (var client = Client(status, "{}"))
                Assert.Equal(UpdateStatus.Failed, (await CheckForUpdates.CheckUsing(client)).Status);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NetworkFailureAndTimeoutDoNotPreventTheNextCheck(bool timeout)
        {
            using (var client = new HttpClient(new Handler((request, token) =>
                Task.FromException<HttpResponseMessage>(timeout ? (Exception)new TaskCanceledException() : new HttpRequestException()))))
                Assert.Equal(UpdateStatus.Failed, (await CheckForUpdates.CheckUsing(client)).Status);
            using (var client = Client(HttpStatusCode.OK, "{\"tag_name\":\"v99.0.0\"}"))
                Assert.Equal(UpdateStatus.Available, (await CheckForUpdates.CheckUsing(client)).Status);
        }

        [Fact]
        public async Task UsesHttpsGitHubHeadersAndDisposesResponseContent()
        {
            Uri uri = null;
            string agent = null, accept = null;
            var content = new TrackingContent("{\"tag_name\":\"v99.0.0\"}");
            using (var client = new HttpClient(new Handler((request, token) =>
            {
                uri = request.RequestUri;
                agent = request.Headers.UserAgent.ToString();
                accept = request.Headers.Accept.ToString();
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
            })))
            {
                await CheckForUpdates.CheckUsing(client);

                Assert.Equal("https://api.github.com/repos/cmd-e/QuickTest/releases/latest", uri.AbsoluteUri);
                Assert.Contains("QuickTest/", agent);
                Assert.Equal("application/vnd.github+json", accept);
                Assert.True(content.Disposed);
            }
        }

        [Fact(Timeout = 5000)]
        public async Task ConcurrentChecksWaitForTheActiveRequest()
        {
            int calls = 0;
            var firstResponse = new TaskCompletionSource<HttpResponseMessage>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (var client = new HttpClient(new Handler((request, token) =>
            {
                if (Interlocked.Increment(ref calls) == 1) return firstResponse.Task;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{\"tag_name\":\"v99.0.0\"}") });
            })))
            {
                var first = CheckForUpdates.CheckUsing(client);
                var second = CheckForUpdates.CheckUsing(client);
                Assert.Equal(1, calls);
                firstResponse.SetResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{\"tag_name\":\"v99.0.0\"}") });

                var results = await Task.WhenAll(first, second);

                Assert.Equal(2, calls);
                Assert.All(results, result => Assert.Equal(UpdateStatus.Available, result.Status));
            }
        }

        private static HttpClient Client(HttpStatusCode status, string body)
        {
            return new HttpClient(new Handler((request, token) =>
                Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body) })));
        }

        private sealed class Handler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send;
            internal Handler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send) { this.send = send; }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token) { return send(request, token); }
        }

        private sealed class TrackingContent : StringContent
        {
            internal bool Disposed;
            internal TrackingContent(string content) : base(content) { }
            protected override void Dispose(bool disposing) { Disposed = true; base.Dispose(disposing); }
        }
    }
}
