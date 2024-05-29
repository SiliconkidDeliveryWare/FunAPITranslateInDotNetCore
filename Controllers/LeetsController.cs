using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MSFunAPICRUDTest;

/* public class FunAPIHub : Hub
{
    public async Task TaskCompleted(int id)
    {
        await Clients.All.SendAsync("Completed", id);
    }
 */    /* public Task Send(string funapimessage)
    {
       return  Clients.Caller.SendAsync("Notify",funapimessage);
        //await Clients.Others.SendAsync("Receive", $"Добавлено: {product} в {DateTime.Now.ToShortTimeString()}");
    
    await HubContext.Clients.All.SendAsync("Notify", $"Home page loaded at: {DateTime.Now}");
        var uzer = HubContext.Clients.User;
        await HubContext.Clients.All.SendAsync("ReceiveMessage", "Updated!");
    }

    public override Task OnConnectedAsync()
    {
        var userName = Context!.User!.Identity!.Name;
        Clients.Caller.SendAsync("ReceiveMessage", $"Welcome, {userName}!");
        return base.OnConnectedAsync();
    } */
//}

public class LeetsController : Controller
{
    /*    private readonly IHubContext<FunAPIHub> HubContext;

       public LeetsController(IHubContext<FunAPIHub> hubcontext)
       {
           HubContext = hubcontext;
       }
    */
    static readonly HttpClient client = new HttpClient();
    ApplicationContext FunAPITranslateDb = new ApplicationContext();
    FunAPITranslateMultiModel ModelMultiFunAPITranslate = new FunAPITranslateMultiModel();

    public IActionResult Index()
    {
        //string html = "<h1>" + "Hello World!" + "<h1>";
        //return Content(html, "text/html");

        return View();
    }

    [HttpGet]
    [Route("/leets/welcome/{takename?}")]
    public IActionResult Welcome(string takename)
    {
        ViewData["Message"] = takename + ", Hello, Welcome to Fun API Translate";
        return View();
    }

    public string leetSpeakDynamic(String wordToTranslate, String wordTranslateTemplate)
    {
        //StringBuilder indexStr = new StringBuilder();
        for (int i = 0; i < wordToTranslate.Length; ++i)
        {
            char currentCharacter = wordToTranslate[i];
            int indexChar = wordTranslateTemplate.IndexOf(wordTranslateTemplate.Substring(i, 1));

            if (indexChar >= 0)
            {
                wordToTranslate.Replace(currentCharacter, wordTranslateTemplate[i]);
            }
        }
        if (wordToTranslate[(wordToTranslate.Length - 1)] == 's')
        {
            wordToTranslate.Replace('s', 'Z');
        }

        return wordToTranslate;
    }

    public static String leetsSpeakStatic(String wordToTranslate)
    {
        StringBuilder sbWordToTranslate = new StringBuilder(wordToTranslate);

        for (int i = 0; i < sbWordToTranslate.Length; ++i)
        {
            char currentCharacter = sbWordToTranslate[i];

            switch (currentCharacter)
            {
                case 'l':
                    sbWordToTranslate.Replace(currentCharacter, '0');
                    break;

                case 'i':
                    sbWordToTranslate.Replace(currentCharacter, '1');
                    break;

                case 'e':
                    sbWordToTranslate.Replace(currentCharacter, '2');
                    break;

                case 't':
                    sbWordToTranslate.Replace(currentCharacter, '3');
                    break;

                case 'z':
                    sbWordToTranslate.Replace(currentCharacter, '4');
                    break;
            }
            if (sbWordToTranslate[(sbWordToTranslate.Length - 1)] == 's')
            {
                sbWordToTranslate.Replace('s', 'Z');
            }

        }
        return sbWordToTranslate.ToString();
    }

    public async Task<IActionResult> DashboardFunAPI()
    {
        await FetchFunAPIData("", true, 0);
        return View(ModelMultiFunAPITranslate);
    }

    public async Task<IActionResult> UploadFunAPI(string? textSource, string? uploadTextAPI, Boolean isFunAPIDashboard)
    {
        if (textSource != null && textSource != "" && uploadTextAPI != null && uploadTextAPI != "")
            await LeetsSpeakFunAPI(textSource, uploadTextAPI);
        if (textSource != null && textSource != "" && uploadTextAPI != null && uploadTextAPI != "")
            await UploadFunAPIData(textSource!, uploadTextAPI!, ViewBag.uploadTextTarget);

        ViewBag.uploadTextSource = textSource;
        ViewBag.uploadTextAPI = uploadTextAPI;

        if (isFunAPIDashboard)
        {
            await DashboardFunAPI();
            return View("DashboardFunAPI", ModelMultiFunAPITranslate);
        }

        return View();
    }

    public async Task<IActionResult> FetchFunAPI(string? textAPI, bool isFunAPIDashboard, int page = 1)
    {
        ViewBag.fetchTextAPI = textAPI;
        await FetchFunAPIData(textAPI, isFunAPIDashboard, page);

        if (isFunAPIDashboard)
        {
            return View("DashboardFunAPI", ModelMultiFunAPITranslate);
        }

        return View(ModelMultiFunAPITranslate);
    }


    public async Task<IActionResult> ChangeFunAPI(string textSource, string changeTextAPI, int funTransID, bool isFunAPIDashboard, bool showChangeDetails, bool postChangeDetails)
    {
        if (showChangeDetails)
        {
            await ChangeFunAPIData(funTransID, isFunAPIDashboard);
            return View("ChangeFunAPIData", ModelMultiFunAPITranslate);
        }

        if (postChangeDetails)
            await ChangeFunAPIData(textSource, changeTextAPI, funTransID);

        await FetchFunAPIData(changeTextAPI, isFunAPIDashboard, 0);

        if (isFunAPIDashboard)
            return View("DashboardFunAPI", ModelMultiFunAPITranslate);

        return View(ModelMultiFunAPITranslate);
    }


    public async Task<IActionResult> RemoveFunAPI(string? textAPI, int funTransID, bool isFunAPIDashboard, bool showRemoveDetails, bool postRemoveDetails)
    {
        if (showRemoveDetails)
        {
            await RemoveFunAPIData(funTransID, isFunAPIDashboard);
            return View("RemoveFunAPIData", ModelMultiFunAPITranslate);
        }

        if (postRemoveDetails)
            await RemoveFunAPIData(funTransID);

        await FetchFunAPIData(textAPI, isFunAPIDashboard, 0);

        if (isFunAPIDashboard)
            return View("DashboardFunAPI", ModelMultiFunAPITranslate);

        return View(ModelMultiFunAPITranslate);
    }


    public async Task LeetsSpeakFunAPI(string textSource, string uploadTextAPI)
    {
        //if (textSource == null ){return BadRequest("Source Text Must Have A Value");};
        //if (uploadTextAPI == null ){return BadRequest("Source API Must Have A Value");};
        if (textSource != null && textSource != "" && uploadTextAPI != null && uploadTextAPI != "")
        {
            string url = "https://api.funtranslations.com/translate/" + uploadTextAPI + ".json/";

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("text", textSource),
        });

            using HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string funResult = await response.Content.ReadAsStringAsync();
            dynamic objFunResult = JsonConvert.DeserializeObject(funResult)!;

            ViewBag.uploadTextTarget = (objFunResult.contents.translated).ToString();
        }
        //string html = "<h1>" + "Hello World!" + "<h1>";
        //return Content("<h1>" + "Hello World!" + "<h1>", "text/html");
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task UploadFunAPIData(string textSource, string uploadTextAPI, string uploadTextTarget)
    {
        if (textSource != null && textSource != "" && uploadTextAPI != null && uploadTextAPI != "" && uploadTextTarget != null && uploadTextTarget != "")
        {
            await Task.Delay(TimeSpan.FromMicroseconds(1));
            FunAPITranslate getToBeUploadedTranslatedFunAPI = new FunAPITranslate { textSource = textSource, textAPI = uploadTextAPI, textTarget = ViewBag.uploadTextTarget };
            FunAPITranslateDb.FunAPITranslates.Add(getToBeUploadedTranslatedFunAPI);
            FunAPITranslateDb.SaveChanges();
        }
    }

    public async Task FetchFunAPIData(string? textAPI, Boolean isFunAPIDashboard, int page)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1));
        ViewBag.fetchedData = "Yes";

        if (textAPI != null && textAPI != "" && !isFunAPIDashboard)
        {
            ModelMultiFunAPITranslate.QueryHasConditionData = FunAPITranslateDb.FunAPITranslates.Where(f => f.textAPI == textAPI).ToList();
        }
        else if (isFunAPIDashboard)
        {
            ModelMultiFunAPITranslate.QueryHasConditionData = FunAPITranslateDb.FunAPITranslates.Where(f => f.textAPI == textAPI).ToList();
            ModelMultiFunAPITranslate.QueryHasNoConditionData = FunAPITranslateDb.FunAPITranslates.ToList();
        }
        else
            ModelMultiFunAPITranslate.QueryHasConditionData = FunAPITranslateDb.FunAPITranslates.ToList();

        const int PageSize = 4;
        var count = ModelMultiFunAPITranslate.QueryHasConditionData.Count();
        ModelMultiFunAPITranslate.QueryHasConditionData = ModelMultiFunAPITranslate.QueryHasConditionData.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
        ViewBag.Page = page;
    }


    [HttpGet]
    //[ActionName("ChangeFunAPITranslates")]
    public async Task ChangeFunAPIData(int? funTransID, bool isFunAPIDashboard)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1));

        ViewBag.isFunAPIDashboard = isFunAPIDashboard;
        ModelMultiFunAPITranslate.QueryHasConditionData = FunAPITranslateDb.FunAPITranslates.Where(f => f.Id == funTransID).ToList();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task ChangeFunAPIData(string textSource, string changeTextAPI, int funTransID)
    {
        //send("Succes");
        var getToBeChangedTranslatedFunAPI = FunAPITranslateDb.FunAPITranslates.FirstOrDefault(x => x.Id == funTransID!);
        if (getToBeChangedTranslatedFunAPI != null)
        {
            await LeetsSpeakFunAPI(textSource, changeTextAPI);

            getToBeChangedTranslatedFunAPI!.textSource = textSource;
            getToBeChangedTranslatedFunAPI.textAPI = changeTextAPI;
            getToBeChangedTranslatedFunAPI.textTarget = ViewBag.uploadTextTarget;

            FunAPITranslateDb.FunAPITranslates.Update(getToBeChangedTranslatedFunAPI);
            FunAPITranslateDb.SaveChanges();
        }
    }


    [HttpGet]
    public async Task RemoveFunAPIData(int? funTransID, bool isFunAPIDashboard)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1));

        ViewBag.isFunAPIDashboard = isFunAPIDashboard;
        ModelMultiFunAPITranslate.QueryHasConditionData = FunAPITranslateDb.FunAPITranslates.Where(f => f.Id == funTransID).ToList();
    }


    [HttpPost]
    //[ActionName("RemoveFunAPIData")]
    [ValidateAntiForgeryToken]
    public async Task RemoveFunAPIData(int funTransID)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1));

        var getToBeRemovedTranslatedFunAPI = FunAPITranslateDb.FunAPITranslates.FirstOrDefault(x => x.Id == funTransID!);
        if (getToBeRemovedTranslatedFunAPI != null)
        {
            FunAPITranslateDb.FunAPITranslates.Remove(getToBeRemovedTranslatedFunAPI);
            FunAPITranslateDb.SaveChanges();
        }
    }


    public IResult FunAPIJsonData(int? funTransID, string? funTransTextAPI)
    {
        if (funTransID != null && funTransID != 0 && funTransTextAPI != null && funTransTextAPI != "")
        {
            var getJsonDataFunAPI = FunAPITranslateDb.FunAPITranslates.Where(x => x.Id == funTransID! && x.textAPI == funTransTextAPI!);
            var strGetJsonDataFunAPI = JsonConvert.SerializeObject(getJsonDataFunAPI);
            //return strGetJsonDataFunAPI;
            return Results.Json(getJsonDataFunAPI);
        }

        if (funTransID != null && funTransID != 0)
        {
            var getJsonDataFunAPI = FunAPITranslateDb.FunAPITranslates.FirstOrDefault(x => x.Id == funTransID);
            var strGetJsonDataFunAPI = JsonConvert.SerializeObject(getJsonDataFunAPI);
            return Results.Json(getJsonDataFunAPI);
        }

        if (funTransTextAPI != null && funTransTextAPI != "")
        {
            var getJsonDataFunAPI = FunAPITranslateDb.FunAPITranslates.Where(x => x.textAPI == funTransTextAPI!);
            var strGetJsonDataFunAPI = JsonConvert.SerializeObject(getJsonDataFunAPI);
            return Results.Json(getJsonDataFunAPI);
        }

        return Results.Json(new { ID = "No match Found", TextAPI = "No match Found" });
    }

    public string HumanJsonData(string humanName, string humanAge)
    {
        var strJsonData = "{\"Name\":\"" + humanName + "\", \"Age\":\"" + humanAge + "\" }";

        var varJsonData = new { Name = humanName, Age = humanAge };
        var strJson = JsonConvert.SerializeObject(varJsonData);
        var NewtJson = Json(varJsonData);

        dynamic objHumanSimple = JsonConvert.DeserializeObject(strJsonData)!;
        dynamic objHumanComplex = JsonConvert.DeserializeObject(strJson)!;

        ViewBag.Message = objHumanComplex.Name;
        return ViewBag.Message;
    }
}