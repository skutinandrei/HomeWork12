namespace HomeWork12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dict = new OtusDictionary();
            dict.Add(1, "One");
            dict.Add(33, "Thirty Three"); // Вызовет ресайзинг до 64
            Console.WriteLine(dict[1]);    // "One"
            Console.WriteLine(dict[33]);   // "Thirty Three"
            Console.WriteLine(dict[999]);  // null
        }
    }
}
