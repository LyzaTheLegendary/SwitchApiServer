using System.Collections.Concurrent;

namespace Common.Threading {
    /// <summary>
    /// This class handles the pooling of async execution of methods and functions
    /// </summary>
    /// <remarks>
    /// Important note, When using this class if it is disposed OR stopped it will stop ALL executing tasks using a Cancellation token!
    /// </remarks>
    public class TaskPool {
        private readonly List<Task> _pool;
        private readonly List<CancellationTokenSource> tokenList;
        private readonly BlockingCollection<Action> _tasks;
        public TaskPool(int poolSize) {
            _pool       = new List<Task>(poolSize);
            tokenList   = new List<CancellationTokenSource>(poolSize);
            _tasks      = new BlockingCollection<Action>(poolSize);

            for (int i = 0; i < poolSize; i++) {
                CancellationTokenSource token = new();
                _pool.Add(new Task(TaskLoop,token.Token, TaskCreationOptions.LongRunning));
            }
        }
        ~TaskPool() {
            Stop();
            tokenList.Clear();
            _pool.Clear();
            // Clearing these makes sure that all the tasks die and their cycle is over.
            // Meaning Tasks will not be in scope and not be used for 3 cpu cycles marking it for garbage collection
        }
        public void Stop() {
            foreach(CancellationTokenSource token in tokenList) 
                token.Cancel(false);
        }
        public bool IsRunning() 
            => _tasks.Count > 0;

        public void PendTask(Action action)
            => _tasks.Add(action);

        // WE WILL NOT USE mException HERE, Because it loses scope and we don't have access to the stack anymore due to how multi threading works!
        public void TaskLoop() {
            // Get consuming enumerable keeps the foreach loop alive ( it is a long running task ( similar to a background thread )! )
            foreach(Action action in _tasks.GetConsumingEnumerable()) {
                action(); 
            }
        }
    }
}
