using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FSP_lab_2
{
    struct ArgumentAndFunc
    {
        public double y;
        public int t;
    }
    class FilterSound
    {
        const int MathConst = 1000;
        // Массив коэффициентов фильтра [k[21]]
        int[] arrayOfCoef;
        // lineTime - отрезок количества тактов. [100]
        long lineTime;
        // filterLine - количество этапов фильтрации [21]
        long filterLine;
        double w;
        double stepW;
        const string path = @"C:\lab2\note.txt";
        public FilterSound(int[] arrayOfCoef, long lineTime, long filterLine, double w, double stepW)
        {
            this.arrayOfCoef = arrayOfCoef;
            this.lineTime = lineTime;
            this.filterLine = filterLine;
            this.w = w;
            this.stepW = stepW;
            if (arrayOfCoef.Length != filterLine)
            {
                throw new Exception("Длина массива должна соответствовать количеству этапов фильтрации");
            }
        }
        private double GetSin(double w, double t)
        {
            double result = MathConst * Math.Sin(w * t);
            return result;
        }
        private double GetY(double[] arrayOfX)
        {
            double sum = 0;
            //Расчёт Y - количество коэффициентов фильтра на аргумент
            for (int i = 0; i < filterLine; i++)
            {
                sum += arrayOfCoef[i] * arrayOfX[i];
            }
            return sum;
        }
        public void GetSoundPerformance()
        {
            List<ArgumentAndFunc[]> schedule = new List<ArgumentAndFunc[]>();
            for (double i = 0; i < w; i+=stepW)
            {
                schedule.Add(DuringLineTime(i));
            }
            OnExcel(schedule);
        }
        
        private ArgumentAndFunc[] DuringLineTime(double w)
        {
            List<ArgumentAndFunc> arrayOfY = new List<ArgumentAndFunc>();
            for (int displacement = 0; displacement < lineTime; displacement++)
            {
                ArgumentAndFunc newStruct = new ArgumentAndFunc();
                // Создаём массив ИКСов по getSin, считаем по массиву Y по функции getY и записываем Y в arrayOfY
                newStruct.y = GetY(GetArrayOfX(displacement, w));
                // displacement - в данном контексте количество тактов
                newStruct.t = displacement;
                arrayOfY.Add(newStruct); 
            }
            return arrayOfY.ToArray();
        }
        public void cleanOrCreateExcel()
        {
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {

            }
            using (FileStream fstream = new FileStream(path, FileMode.Truncate))
            {

            }
            
        }
        private double[] GetArrayOfX(int displacement, double w)
        {
            // Получение 20-ти иксов.
            List<double> resultArray = new List<double>();
            // displacement - это сдвиг, то есть начальное и конечное значение на оси времени, к которому прибавляется filterLine
            // Начальное значение зависит от времени, из duringLineTime, a конечное от времени + filterLine
            for (int t = displacement; t < filterLine + displacement; t++)
            {
                // Добавление их в массив.
                // X = 1000* sin(w, t)
                resultArray.Add(GetSin(w, t));
            }
            return resultArray.ToArray();
        }
        private void OnExcel(List<ArgumentAndFunc[]> data)
        {
            // запись в файл
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (var array in data)
                {
                    foreach (var item in array)
                    {
                        string str = item.t + ";" + item.y + "\n";
                        sw.Write(str);
                        Console.Write(str);
                    }
                }
            }
        }

    }
}
