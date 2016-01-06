using System;
using System.Windows.Forms;

namespace Game1
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new starterform());
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
