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

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskService.AddTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskService.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Delete(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleComplete(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _taskService.UpdateTask(task);
            }
            return RedirectToAction("Index");
        }
    }
}