using Microsoft.AspNetCore.Mvc;
using SimpleTaskApp.Models;
using SimpleTaskApp.Services;

namespace SimpleTaskApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        // ... остальные методы также нужно сделать асинхронными
        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.AddTaskAsync(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }
    }
}