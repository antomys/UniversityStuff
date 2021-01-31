using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LongArifm
{
    class Program
    {
        static List<RSA> users = new List<RSA>();

        static void SendMessage(string message, int from_id, int to_id)
        {
            Console.WriteLine($"\n{users[to_id].name} received message from {users[from_id].name}");
            Console.WriteLine($"Message before decryption:\n{message}");
            message = users[to_id].DecryptMessage(message, users[from_id].e, users[from_id].n);
            Console.WriteLine($"Message after decryption:\n{message}");
        }

        static void Main(string[] args)
        {
            Blockchain myBlockchain = new Blockchain();
            myBlockchain.Start();

            Console.WriteLine("Welcome!\n\n");

            users.Add(new RSA("Alice"));
            users.Add(new RSA("Bob"));
            users.Add(new RSA("Matilda"));

            while (true)
            {
                Console.WriteLine("Send message from");
                for (int i = 0; i < users.Count; ++i)
                {
                    Console.WriteLine($"{i + 1} -> {users[i].name}");
                }
                Console.Write("> ");
                string str = Console.ReadLine();
                int id = int.Parse(str) - 1;
                int sid = id;
                if (id < 0 || id > users.Count - 1) continue;

                Console.WriteLine("Send message to\n0 -> eneryone");
                for (int i = 0; i < users.Count; ++i)
                {
                    Console.WriteLine($"{i + 1} -> {users[i].name}");
                }
                Console.Write("> ");
                str = Console.ReadLine();
                id = int.Parse(str) - 1;
                int rid = id;
                if (id < -1 || id > users.Count - 1) continue;

                Console.WriteLine($"{users[sid].name}, enter your message:");
                Console.Write("> ");
                string message = Console.ReadLine();

                
                if (id == -1)//ввели 0
                {
                    Console.WriteLine($"\n\n{users[sid].name} will send message to everyone");
                    for (rid = 0; rid < users.Count; ++rid)
                    {
                        Console.WriteLine($"\n{users[sid].name} send message to {users[rid].name}");
                        Console.WriteLine($"Message before encryption:\n{message}");
                        string mes = users[sid].EncryptMessageToSend(users[sid].name, message, users[rid].e, users[rid].n);
                        Console.WriteLine($"Message after encryption:\n{mes}");
                        SendMessage(mes, sid, rid);
                    }
                    Console.WriteLine($"\n\n");
                }
                else
                {
                    Console.WriteLine($"\n\n{users[sid].name} send message to {users[id].name}");
                    Console.WriteLine($"Message before encryption:\n{message}");
                    message = users[sid].EncryptMessageToSend(users[sid].name, message, users[rid].e, users[rid].n);
                    Console.WriteLine($"Message after encryption:\n{message}");
                    SendMessage(message, sid, rid);
                    Console.WriteLine($"\n\n");
                }
            }
        }
    }
}
