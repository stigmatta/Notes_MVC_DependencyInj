using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Notes_MVC_DependencyInj.Models;
using Notes_MVC_DependencyInj.Services;

namespace Notes_MVC_DependencyInj.Controllers;

public class HomeController : Controller
{
    INoteHandler _saver;
    public HomeController(INoteHandler saver)
    {
        _saver = saver;
    }

    public async Task<IActionResult> Index()
    {
        List<Note> notes = await _saver.Load();
        ViewBag.Notes = notes;
        return View();
    }
    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Index(Note note)
    {
        if (!ModelState.IsValid)
            return View(note);

        if (!string.IsNullOrEmpty(note.Tags))
        {
            note.Tags = string.Join(", ", note.Tags.Split(',')
                .Select(tag => tag.Trim())
                .Where(tag => !string.IsNullOrEmpty(tag)));
        }

        note.CreatedAt = DateTime.Now;

        _saver.Save(note);
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
