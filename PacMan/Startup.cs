﻿namespace Pacman
{
    using Pacman.Data;
    using System;
    using System.Windows.Forms;

    public static class Startup
    {
        [STAThread]
        public static void Main()
        {
            var context = new PacmanContext();
            context.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.Login());
        }
    }
}