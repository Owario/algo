using System;

namespace algo
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph_ a;
            TarjanLGA b;
            Random bcd=new Random();
            int input=1;
            

            while(input!=0)
            {
                Console.WriteLine("\nМеню:\n1)Среднее время работы для v вершин и e дуг\n2)Создать граф с параметрами v,e и графически отобразить его\n3)Среднее время работы для v вершин и e дуг с настраиваемым шагом\n0)Выход");
                int e,v,count;
                double average_time;
                input = Convert.ToInt32(Console.ReadLine());
                switch(input)
                {
                    case 1:
                    Console.WriteLine("Введите кол-во измерений n:");
                    count=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите число вершин v:");
                    v=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите число дуг e:");
                    e=Convert.ToInt32(Console.ReadLine());
                    average_time=0;
                    
                    for(int i=0;i<count;++i)
                    {
                        a = new Graph_(v,e);
                        b = new TarjanLGA(ref a);
                        average_time += b.TarjanSSC();     
                    }
                    average_time/=count;
                    Console.WriteLine("Среднее время: {0}",average_time);
                    break;
                    case 2:
                    double time;
                    Console.WriteLine("Введите кол-во вершин v:");
                    v = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите кол-во дуг e:");
                    e = Convert.ToInt32(Console.ReadLine());
                    a = new Graph_(v,e);
                    b= new TarjanLGA(ref a);
                    time=b.TarjanSSC();
                    a.ShowList();
                    a.ShowStronglyConnectedComponents();
                    a.PrintGraphToFile();
                    a.PrintSearchedComponentsToFile();
                    Console.WriteLine("\nВремя выполнения в мс. {0}",time);
                    Console.WriteLine("\nЧтобы посмотреть на граф, запуститие файл GraphCreator.py, данные о компонентах сильной связнности графа есть в файле Graph_SCC.txt");
                    break;

                    case 3:
                    int v_step,e_step;
                    Console.WriteLine("Введите кол-во измерений n:");
                    count=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите начально число вершин v:");
                    v=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите начальное число дуг e:");
                    e=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите шаг для переменной v:");
                    v_step=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите шаг для переменной e:");
                    e_step=Convert.ToInt32(Console.ReadLine());
                    int step1=v;
                    int step2=e;

                    do
                    {
                        average_time=0;
                        Console.WriteLine("{0}, {1}",step1,step2);
                        for(int i=0;i<count;++i)
                        {
                            a = new Graph_(step1,step2);
                            b = new TarjanLGA(ref a);
                            average_time += b.TarjanSSC();
                        }
                        average_time/=count;
                        Console.WriteLine("\nВремя выполнения в мс. {0}, V={1}, E={2}",average_time,step1,step2);
                        step1+=v_step;
                        step2+=e_step;
                    }while((step1<13000)&&(step2<200000));
                    break;

                    case 0:
                    break;

                    default:
                    Console.WriteLine("некорректный ввод!");
                    break;
                }
            }
         
        }
    }
}
