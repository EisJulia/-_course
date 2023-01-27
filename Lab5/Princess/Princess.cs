using System.Net.Http.Json;
using System.Text;
using Lab5.request;

namespace Princess;

public class Princess
{
    private readonly HttpClient _httpClient;

    private readonly int _port = 5126;

    public Princess()
    {
        _httpClient = new HttpClient();
    }

    public void Choose(int contendersNum, int attemptId)
    {
        var t = (int)Math.Round(contendersNum / Math.E);
        SkipContenders(t, attemptId);
        while (true)
        {
            var contNum = GetNextContender(attemptId);
            if (contNum == -1) break;
            if (GetRate(contNum, attemptId) == 0)
            {
                SendChoice(contNum, GetHappiness(contNum), attemptId);
                return;
            }
        }

        if (GetRate(contendersNum - 1, attemptId) >= 50)
        {
            SendChoice(-1, GetHappiness(-1), attemptId);
            return;
        }

        SendChoice(contendersNum - 1, GetHappiness(contendersNum - 1), attemptId);
    }

    public void Generate100()
    {
        for (var i = 0; i < 100; i++)
        {
            var id = GenerateAttempt();
            Choose(100, id);
        }
    }

    public static int GetHappiness(int rate)
    {
        if (rate == -1) return 10;
        if (rate >= 50) return 0;
        return 100 - rate;
    }

    private void SendChoice(int contender, int happiness, int attemptId)
    {
        var dto = new PrincessChoseRequest() { ContenderNum = contender, Happiness = happiness };
        var content = JsonContent.Create(dto);
        var choiceResponse = _httpClient.PostAsync(
            "http://localhost:" + _port + "/simulator/" + attemptId + "/princess-chose",
            content).Result;
        Console.Out.WriteLine(choiceResponse.EnsureSuccessStatusCode());
    }

    private int GetNextContender(int attemptId)
    {
        var candidateNumResponse = _httpClient.PostAsync("http://localhost:" + _port + "/hall/" + attemptId +
                                                         "/get-next",
            new StringContent("", Encoding.UTF8, "application/json")).Result;
        candidateNumResponse.EnsureSuccessStatusCode();
        var candidate = candidateNumResponse.Content.ReadFromJsonAsync<int?>().Result;
        if (candidate == null) throw new Exception();
        return (int)candidate;
    }

    private void SkipContenders(int skipNum, int attemptId)
    {
        var dto = new SkipContendersRequest() { NumberToSkip = skipNum };
        var content = JsonContent.Create(dto);
        var candidateSkipResponse = _httpClient.PostAsync("http://localhost:" + _port + "/hall/" + attemptId + "/skip",
            content).Result;
        candidateSkipResponse.EnsureSuccessStatusCode();
    }

    private int? GetRate(int contenderNum, int attemptId)
    {
        var dto = new GetRateRequest() { ContenderNumber = contenderNum };
        var content = JsonContent.Create(dto);

        var candidateRateResponse = _httpClient.PostAsync(
            "http://localhost:" + _port + "/friend/" + attemptId + "/get-rate",
            content).Result;
        candidateRateResponse.EnsureSuccessStatusCode();
        var rate = candidateRateResponse.Content.ReadFromJsonAsync<int?>().Result;
        return rate;
    }

    public int GenerateAttempt()
    {
        var attemptResponse = _httpClient.PostAsync("http://localhost:" + _port + "/simulator/generate-attempt",
            new StringContent("", Encoding.UTF8, "application/json")).Result;
        attemptResponse.EnsureSuccessStatusCode();
        return attemptResponse.Content.ReadFromJsonAsync<int>().Result;
    }

    public int? CalcAvrHappiness()
    {
        var happinessResponse = _httpClient.GetAsync("http://localhost:" + _port + "/simulator/get-avr-happiness").Result;
        happinessResponse.EnsureSuccessStatusCode();
        return happinessResponse.Content.ReadFromJsonAsync<int?>().Result;
    }
}