using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace sqlstone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Privacy()
    {
        return new JsonResult(new {message="it's done"});
    }

    [HttpPost]
    public ActionResult CheckJournalEntry2([FromForm] string uuid, [FromForm] JournalEntry jentry) {
        return new JsonResult (new { Message = $"FORM - ENTRY2 - You sent in uuid: {uuid} But your jentry.id failed : {jentry.Id}" });
    }

    

}