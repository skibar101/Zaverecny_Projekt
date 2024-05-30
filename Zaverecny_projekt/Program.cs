namespace Zaverecny_projekt
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
        
            Application.Run(new Form3()); // Running login form, start of the app
        }
    }
}