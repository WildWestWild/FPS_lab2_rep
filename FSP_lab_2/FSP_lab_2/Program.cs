using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSP_lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] k = {6, 0, -4, -3, 5, 6, -6, -13, 7, 44, 64, 44, 7, -13, -6, 6, 5, -3, -4, 0, 6 };
            FilterSound fsp = new FilterSound(k,100, 21, 2, 0.1);
            // Добавить функцию отчистки 
            fsp.cleanOrCreateExcel();
            fsp.GetSoundPerformance();
        }
    }
}
