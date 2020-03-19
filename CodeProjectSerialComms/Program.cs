using System;
using System.Windows.Forms;

namespace CodeProjectSerialComms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Trilateration trilateration = new Trilateration();

            string p1s = "120,68,0";
            string p2s = "203,30,0";
            string p3s = "310,120,0";
         
            var position = trilateration.GetIntersectionPoint(p1s, p2s, p3s,"587.22,601.9,545.24");

            if (position.valid)
            {
                Console.WriteLine(position);
            }
            else
            {
                Console.WriteLine("Nan");
            }
        }
    }
}
