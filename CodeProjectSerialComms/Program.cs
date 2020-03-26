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

            string p1s = "1,1,1";
            string p2s = "3,1,1";
            string p3s = "2,2,1";
         
            var position = trilateration.GetIntersectionPoint(p1s, p2s, p3s,"1,1,1");

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
