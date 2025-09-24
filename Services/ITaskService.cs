using SimpleTaskApp.Models;

namespace SimpleTaskApp.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAllTasks();
        TaskItem GetTaskById(int id);
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public List<TaskItem> GetAllTasks() => _tasks;

        public TaskItem GetTaskById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public void AddTask(TaskItem task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
        }

        public void UpdateTask(TaskItem task)
        {
            var existingTask = GetTaskById(task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.IsCompleted = task.IsCompleted;
            }
        }

        public void DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task != null)
                _tasks.Remove(task);
        }
    }
}