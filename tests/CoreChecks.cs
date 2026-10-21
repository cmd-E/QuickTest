using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using QuickTest.Classes;
class FakeResponse : HttpMessageHandler
{
    private readonly HttpStatusCode status;
    private readonly string body;
    public FakeResponse(HttpStatusCode status, string body)
    {
        this.status = status;
        this.body = body;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        return Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body) });
    }
}
class CoreChecks
{
    static void Check(bool condition, string message)
    {
        if (!condition)
            throw new Exception(message);
    }
    static void Reject(Action action)
    {
        try
        {
            action();
        }
        catch (FormatException)
        {
            return;
        }
        catch (ArgumentException)
        {
            return;
        }
        throw new Exception("Expected rejection");
    }
    static void Main()
    {
        var paragraphs =
            new[] { "<QUESTION>Question", "<VARIANT>same", "<VARIANT>same", "<VARIANT>C", "<VARIANT>D", "<VARIANT>E" };
        var bank = TaggedQuestionParser.Parse(paragraphs, "<question>", "<variant>");
        Check(bank.Count == 1, "parse uppercase");
        Reject(() => TaggedQuestionParser.Parse(new[] { "<variant>A" }, "<question>", "<variant>"));
        Reject(() => TaggedQuestionParser.Parse(new[] { "<question>Q", "<variant>A" }, "<question>", "<variant>"));
        Reject(() => TaggedQuestionParser.ValidateTags(" ", "<variant>"));
        var first = new TestSession(bank, 1, new Random(1));
        first.Questions[0].Selected = 1;
        var order = first.Questions[0].Order.ToArray();
        var second = new TestSession(bank, 1, new Random(2));
        Check(order.SequenceEqual(first.Questions[0].Order), "other session mutated order");
        Check(!first.Results()[0].IsCorrect, "equal text incorrectly accepted as answer identity");
        first.Questions[0].Selected = 0;
        Check(first.Complete && first.Results()[0].IsCorrect, "correct answer scoring");
        Version oldVersion, newVersion;
        Check(CheckForUpdates.TryVersion("v1.9.9", out oldVersion), "version parse");
        Check(CheckForUpdates.TryVersion("2.0.0", out newVersion) && newVersion > oldVersion,
              "ordered version comparison");
        Check(!CheckForUpdates.TryVersion("vbad", out oldVersion), "bad tag");
        foreach (var body in new[] { "bad JSON", "{}", "{\"tag_name\":\"vbad\"}" })
        {
            using (var client = new HttpClient(new FakeResponse(HttpStatusCode.OK, body)))
                Check(CheckForUpdates.CheckUsing(client).GetAwaiter().GetResult().Status == UpdateStatus.Failed,
                      "Malformed update response escaped");
        }
        using (var client = new HttpClient(new FakeResponse(HttpStatusCode.Forbidden, "{}")))
            Check(CheckForUpdates.CheckUsing(client).GetAwaiter().GetResult().Status == UpdateStatus.Failed,
                  "HTTP error escaped");
        using (var client = new HttpClient(new FakeResponse(HttpStatusCode.OK, "{\"tag_name\":\"v99.0.0\"}")))
            Check(CheckForUpdates.CheckUsing(client).GetAwaiter().GetResult().Status == UpdateStatus.Available,
                  "Valid update response rejected");
        Console.WriteLine("QuickTest parser, session isolation, answer identity and version checks passed");
    }
}
