namespace HTTPServer
{
    abstract class BaseThread
    {
        private Thread _thread;
        private AutoResetEvent waitHandle;
        private bool running;


        public BaseThread()
        {
            running = true;
            _thread = new Thread(new ThreadStart(this.Run));
            waitHandle = new AutoResetEvent(false);
        }

        public void Start() => _thread.Start();
        public void Stop() => running = false;
        public void Pause() => waitHandle.WaitOne();
        public void Resume() => waitHandle.Set();
        public void Join() => _thread.Join();
        public bool IsAlive => _thread.IsAlive;

        public void Run()
        {            
            RunThread();
        }

        public abstract void RunThread();

    }
}