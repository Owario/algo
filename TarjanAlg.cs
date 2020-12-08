using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace algo
{
    class Graph_
    {
        public int v;
        public int e;
        List<int>[] adjacency_list=null;
        Vertex[] vertex_list=null;
        private int component_count;
        
        public Graph_(int v_, int e_)
        {
            v=v_;
            e=e_;
            adjacency_list = new List<int>[v];
            for (int i=0;i<v;++i)
            {
                adjacency_list[i]=new List<int>();
            }

            vertex_list=new Vertex[v];
            for(int i=0;i<v;++i)
            {
                vertex_list[i]=new Vertex(i);
            }
        }
        public void SetAdjacency_List(int vertex_,int value_)
        {
            adjacency_list[vertex_].Add(value_);
        }

        public List<int> GetAdjacency_List_byId(int id)
        {
            return adjacency_list[id];
        }
        
        public ref Vertex GetVertexById(int id_)
        {
            return ref vertex_list[id_];
        }

        public void ShowList()
        {
            for(int i=0;i<v;++i){
                Console.Write("\nДуги с начальной вершиной ({0}):",i);
                foreach (int vertex_ in adjacency_list[i])
                {
                    Console.Write("{0} ",vertex_);
                }
            }
        }
        public void SetComponentCount(int count)
        {
            component_count=count;
        }

        public void ShowStronglyConnectedComponents()
        {
            for(int i=1;i<component_count;++i)
            {
                Console.Write("\nКомпонента сильной связанности {0}:",i);
                foreach(var vertex1 in vertex_list)
                {
                    if (vertex1.Component==i) 
                    {
                        Console.Write("{0} ",vertex1.GetId());
                    }
                }

            }
        }

        public void PrintGraphToFile()
        {
            string writePath = @"D:\GitReps\algo\Graph_data.txt";
            string text ="";
            for(int i=0;i<v;++i)
            {
                foreach (int vertex_ in adjacency_list[i])
                {
                    text+=Convert.ToString(vertex_)+" ";
                }
                text+="\n";
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public void PrintSearchedComponentsToFile()
        {
            string writePath = @"D:\GitReps\algo\Graph_SCC.txt";
            string text ="";
            for(int i=1;i<component_count;++i)
            {
                text+="\nКомпонента сильной связанности "+i+" :";
                foreach(var vertex1 in vertex_list)
                {
                    if (vertex1.Component==i) 
                    {
                        text+=vertex1.GetId().ToString();
                    }
                }

            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }

    class Vertex
    {
        private int id;
        //Универсальный id используется только в программировании
        private int number; 
        //Время(порядок относительно других вершин) посещения вершины
        private int lowLink; 
        //Вершина с минимальным number, доступная из данной вершины
        private int component;
        //К какой компоненте принадлежит вершина, по умолчанию ни к какой
        public Vertex(int id_)
        {
            id=id_;
            number = -1;
            lowLink = 0;
        }

        public int Number
        {
            get
            {
                return number;
            }
            
            set
            {
                number = value;
            }
        }

        public int LowLink
        {
            get
            {
                return lowLink;
            }
            
            set
            {
                lowLink=value;
            }
        }

        public int Component
        {
            get
            {
                return component;
            }
            set
            {
                component=value;
            }

        }

        public int GetId()
        {
            return id;
        }

    }

    class TarjanLGA
    {
        bool[] array_;
        private double resultTime;
        Graph_ graph;
        int step_;
        List<Vertex> unchecked_vertex;
        Stack<Vertex> stack;
        int component_count;
        public TarjanLGA(ref Graph_ graph_,bool seed=false)
        {
            //конструктор получает на вход список вершин и генерирует дуги соединяюшие их
            //если же переменная seed=true это означает что граф уже сгенерирован
            graph = graph_;
            unchecked_vertex = new List<Vertex>();
            var rand = new Random();
            stack = new Stack<Vertex>();
            resultTime=0;
            if (!seed) Generator();
            step_=1;
            component_count=1;
            for(int i=0;i<graph.v;++i)
            {
                unchecked_vertex.Add(graph.GetVertexById(i));
            }
            array_=new bool[graph.v];
            for(int i=0;i<graph.v;++i)
            {
                array_[i] = false;
            }
        }

        public double TarjanSSC()
        {
            //основной этап работы алгоритма, внешне напоминает обычный дфс
            var startTime = new Stopwatch();
            startTime.Start();
            while(unchecked_vertex.Count!=0)
            {
            STRONGCONNECT(unchecked_vertex[0]);
            }
            startTime.Stop();
            resultTime = startTime.ElapsedMilliseconds;
            //алгоритм закончил свою работу
            graph.SetComponentCount(component_count);

            //возвращаем вершины графа в исходное состояние
            for(int i=0;i<graph.v;++i)
            {
                graph.GetVertexById(i).Number=-1;
            }

            return resultTime;
        }

        public void Generator()
        {
            var rand = new Random();
            int v = graph.v;
            int e = graph.e;
            bool[,] adjacency_matrix= new bool[v,v];
            while(e!=0)
            {
                var rand_vertex=rand.Next(0,v);
                var rand_edge=rand.Next(0,v);
                if (!adjacency_matrix[rand_vertex,rand_edge]) 
                {
                    adjacency_matrix[rand_vertex,rand_edge]=true;
                    e--;
                }
            }
            for(int i=0;i<v;++i){
                for(int j=0;j<v;++j){
                    if (adjacency_matrix[i,j])
                    {
                        graph.SetAdjacency_List(i,j);
                    }
                }
            }
        }
        public void DFS(int id_, bool[] visited)
        {
            graph.GetVertexById(id_).Number=step_++;
            visited[id_]=true;
            List<int> vList=graph.GetAdjacency_List_byId(id_);
            foreach(int w in vList)
            {
                if (!visited[w]) 
                {
                    DFS(w,visited);
                }
            }
        }

        public double DFStime()
        {
            bool[] visited= new bool[graph.v];
            step_=1;
            //while(unchecked_vertex.Count!=0)
            //{
            //DFS(unchecked_vertex[0]);
            //}
            var startTime = new Stopwatch();
            startTime.Start();
            for(int i=0;i<graph.v;++i)
            {
                if (visited[i]==false) DFS(i,visited);
            }

            startTime.Stop();
            //unchecked_vertex.Clear();

            //возвращаем вершины графа в исходное состояние
            for(int i=0;i<graph.v;++i)
            {
                if (graph.GetVertexById(i).Number==-1) Console.WriteLine("Кукареку");
                graph.GetVertexById(i).Number=-1;
            }
            //
            return startTime.ElapsedMilliseconds;
            //алгоритм закончил свою работу
        }

        public void STRONGCONNECT(Vertex a)
        {
            //не забывать удалять элемент из списка непосещенных вершин
            a.LowLink=a.Number=step_++;
            stack.Push(a);
            array_[a.GetId()]=true;
            foreach(int w in graph.GetAdjacency_List_byId(a.GetId()))
            {
                if (graph.GetVertexById(w).GetId()==a.GetId()) continue;
                if (graph.GetVertexById(w).Number==-1) 
                {
                    STRONGCONNECT(graph.GetVertexById(w));
                    a.LowLink=Math.Min(a.LowLink,graph.GetVertexById(w).LowLink);
                }
                else if (graph.GetVertexById(w).Number<a.Number)
                {
                    if (array_[w]==true)
                    {
                        a.LowLink=Math.Min(a.LowLink,graph.GetVertexById(w).Number);
                    }
                }
            }
            unchecked_vertex.Remove(a);

            if (a.LowLink==a.Number)
            {
                //вершина а является "корнем" компоненты сильной связности
                //все вершины c number > number корня в стеке относятся к данной компоненте
                //очищаем стек до вершины а включительно
                Vertex b = stack.Peek();
                if (b==a) 
                {
                    a.Component = component_count;
                    stack.Pop();
                    array_[a.GetId()]=false;
                }
                while(b!=a)
                {
                    b = stack.Pop();
                    graph.GetVertexById(b.GetId()).Component = component_count;
                    array_[b.GetId()]=false;
                }
                //начинаем поиск новой компоненты сильной связности
                component_count++;
            }
        }

        


    }
}

