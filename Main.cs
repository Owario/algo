using System;

namespace algo
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph_ a;
            TarjanLGA b;
            Graph c;
            Random bcd=new Random();
            int input=1;

            /*
            double keks=0;
            for(int i=0;i<50;++i)
            {
            a = new Graph_(5000,100000);
            b= new TarjanLGA(ref a);
            keks+=b.DFStime();
            }
            keks=keks/50;
            Console.WriteLine("{0} мой дфс",keks);
            keks=0;

            for(int i=0;i<50;++i)
            {
            a = new Graph_(7500,150000);
            b= new TarjanLGA(ref a);
            keks+=b.DFStime();
            }
            keks=keks/50;
            Console.WriteLine("{0} мой дфс",keks);
            keks=0;


            c = new Graph(5000);
            for(int i=0;i<300;++i){
            for(int j=0;j<100000;++j)
            {
                c.AddEdge(bcd.Next(0,5000),bcd.Next(0,5000));
            }
            keks+=c.DFS(0);
            }
            keks=keks/300;
            Console.WriteLine("{0}",keks);
            keks=0;

            c = new Graph(7500);
            for(int i=0;i<300;++i){
            for(int j=0;j<150000;++j)
            {
                c.AddEdge(bcd.Next(0,7500),bcd.Next(0,7500));
            }
            keks+=c.DFS(0);
            }
            keks=keks/300;
            Console.WriteLine("{0}",keks);
            keks=0;
            */
            


            while(input!=0)
            {
                Console.WriteLine("\nМеню:\n1)Среднее время работы для v вершин и e дуг\n2)Создать граф с параметрами v,e и графически отобразить его\n0)Выход");
                int e,v,count;
                double average_time,average_time2;
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
                    average_time2=0;
                    
                    for(int i=0;i<count;++i)
                    {
                        a = new Graph_(v,e);
                        b = new TarjanLGA(ref a);
                        average_time += b.TarjanSSC()/count;     
                        average_time2+=b.DFStime()/count;
                        Console.WriteLine(b.DFStime());
                    }
                    Console.WriteLine("Среднее время: {0}, {1}",average_time,average_time2);
                    break;
                    case 2:
                    Console.WriteLine("Введите кол-во вершин v:");
                    v = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите кол-во дуг e:");
                    e = Convert.ToInt32(Console.ReadLine());
                    a = new Graph_(v,e);
                    b= new TarjanLGA(ref a);
                    //a.ShowList();
                    //a.ShowStronglyConnectedComponents();
                    //a.PrintGraphToFile();
                    //a.PrintSearchedComponentsToFile();
                    Console.WriteLine("\nВремя выполнения в мс. {0}, {1}",b.TarjanSSC(),b.DFStime());
                    Console.WriteLine("\nЧтобы посмотреть на граф, запуститие файл GraphCreator.py, данные о компонентах сильной связнности графа есть в файле Graph_SCC.txt");
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
