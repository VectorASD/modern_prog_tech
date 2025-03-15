namespace ConsoleApp {
    internal class Program {
        static void Main() {
            const string s1 = "cat";
            const string s2 = "dog";

            MultiMap<string, string> m1 = new();
            m1.Add("animal", s1);
            m1.Add("animal", s2);
            m1.Add("mineral", "calcium"); // 'm' > 'h'
            m1.Add("human", "tom");
            m1.Add("human", "tim");

            // m1.Remove("animal", s2); работает

            foreach (string k in m1.Keys)
                foreach (string v in m1[k])
                    Console.WriteLine(k + "=" + v);

            Console.WriteLine("~~~~~~~~~~~~~~~~");
            UAbonentList contacts = new();
            contacts.Load();
            if (contacts.Empty) {
                contacts.AddRecord("name1", "meow1");
                contacts.AddRecord("name1", "meow2");
                contacts.AddRecord("name2", "meow1");
                contacts.RemoveRecord("name1", "meow2");
            }

            // Console.ReadLine();
        }
    }
}
