using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Collections;
using System.Collections.Concurrent;

namespace CheckLinkCLI2
{
    public class Program
    {
        static ConcurrentDictionary<int, int> items = new ConcurrentDictionary<int, int>();

        //static void Main(string[] args)
        //{
        //    /// <summary>
        //    /// Basics on collections
        //    /// </summary>

        //    List<String> customer = new List<string>();
        //    customer.Add("Kim");
        //    customer.Add("John");
        //    customer.Add("Tim");
        //    Console.WriteLine(customer.Count);

        //    foreach (var item in customer)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    Console.WriteLine(customer[1]);

        //    /// <summary>
        //    /// Intro to Dictionary
        //    /// </summary>
        //    ///
        //    Dictionary<string, string> config = new Dictionary<string, string>();

        //    config.Add("resolution", "1920x1080");
        //    config.Add("title", "MyWebsite");

        //    /// This is not possible
        //    //config.Add("title", "MyWebsitea");

        //    Console.WriteLine(config["title"]);

        //    foreach (var setting in config)
        //    {
        //        Console.WriteLine(setting.Value);

        //    }

        //    /// <summary>
        //    /// Running array list and the concept of Boxing/Unboxing
        //    /// </summary>
        //    ///
        //    ArrayList list = new ArrayList();

        //    // this is possible because of boxing; which is converting any type to a regular C# object
        //    list.Add("some string");
        //    string s = (string)list[0];

        //    /// <summary>
        //    /// Difference between HashTable collections
        //    /// </summary>
        //    ///
        //    Hashtable table = new Hashtable();

        //    table.Add("title", "MyWebsite");

        //    var j = (string)table["title"];
        //    Console.WriteLine($"This is a hashtable value {j}");

        //    /// <summary>
        //    /// Learning a Concurrent Dictionary
        //    /// </summary>
        //    ///


        //    Thread thread1 = new Thread(new ThreadStart(AddItem));
        //    Thread thread2 = new Thread(new ThreadStart(AddItem));

        //    //thread1.Start();
        //    //thread2.Start();

        //    bool[] preload = new bool[3] { true, false, true};

        //    BitArray enemyGrid = new BitArray(preload);

        //    //enemyGrid[0] = false;
        //    //enemyGrid[1] = true;
        //    //enemyGrid.Set(2,false);
        //    //enemyGrid

        //    foreach (var item in enemyGrid)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    /// <summary>
        //    /// Learning a Concurrent Dictionary and Tuples
        //    /// </summary>
        //    ///

        //    Tuple<int, string, bool> myTuple = new Tuple<int, string, bool>(1, "hello", true);

        //    Console.WriteLine(myTuple.Item2);

        //    /// <summary>
        //    /// Learning about Stack
        //    /// </summary>
        //    ///

        //    Stack<string> pancakes = new Stack<string>();

        //    pancakes.Push("first pancake made");
        //    pancakes.Push("second pancake made");
        //    pancakes.Push("third pancake made");

        //    foreach (var item in pancakes)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    Console.WriteLine(pancakes.Pop());
        //    Console.WriteLine(pancakes.Peek()); // Stack.Peek() looks at the last item without removing it
        //    Console.WriteLine(pancakes.Peek());

        //    /// <summary>
        //    /// Learning about Queue
        //    /// </summary>
        //    ///

        //    Queue<int> myQueue = new Queue<int>();

        //    myQueue.Enqueue(1);
        //    myQueue.Enqueue(2);
        //    myQueue.Enqueue(3);

        //    foreach (var queue in myQueue)
        //    {
        //        Console.WriteLine(queue);
        //    }

        //    Console.WriteLine(myQueue.Dequeue());
        //    Console.WriteLine(myQueue.Peek()); // Queue.Peek() does the same as Stack.Peek()

        //    /// <summary>
        //    /// Learning about HashSet
        //    /// </summary>
        //    ///

        //    var myHash = new HashSet<string>();

        //    myHash.Add("hello");
        //    myHash.Add("hello");

        //    string[] l = new string[] { "hello" };

        //    Console.WriteLine(myHash.Count);
        //    Console.WriteLine(myHash.Overlaps(l)); //Overlaps allows us to see and compare whether we have the same data

        //}

        public static void AddItem()
        {
            items.TryAdd(1, 2);
            Console.WriteLine(items.Count);
        }
    }
}
