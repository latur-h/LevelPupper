namespace LevelPupper__Parser
{
    internal static class Program
    {
        static readonly string mutexName = "LevelPupper";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            using (Mutex mutex = new(false, mutexName, out bool isNewInstance))
            {
                if (isNewInstance)
                {
                    ApplicationConfiguration.Initialize();
                    Application.Run(new Form1());
                }
            }
        }
    }
}