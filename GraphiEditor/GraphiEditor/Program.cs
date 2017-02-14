namespace GraphiEditor
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// main app class
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Editor());
        }
    }
}
