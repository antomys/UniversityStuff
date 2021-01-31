using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LongArifm
{
    class Blockchain
    {
        string NONS_CHECK = "abcd";
        int OPERATIONS_COUNT_IN_BLOCK = 5;
        int MAX_SUM = 999;
        double time = 0;
        List<string> users = new List<string> { "Alice", "Bob", "Matilda", "Zuhra" };
        Random rand = new Random();
        SHA256 sha256Hash = SHA256.Create();
        LongCalculator calc = new LongCalculator();
        List<List<string>> merkle = new List<List<string>>();
        List<string> chain = new List<string>();
        public void Start()
        {
            /*{
            string source = "Hello World!";
            string hash = GetHash(sha256Hash, source);

            Console.WriteLine($"The SHA256 hash of {source} is: {hash}.");

            Console.WriteLine("Verifying the hash...");

            if (VerifyHash(sha256Hash, source, hash))
            {
                Console.WriteLine("The hashes are the same.");
            }
            else
            {
                Console.WriteLine("The hashes are not same.");
            }
            }
            */

            //ReadBlockChain("emptychain.txt");
            //ReadMerkleTree("emptymerkle.txt");
            ReadBlockChain("outbch.txt");
            ReadMerkleTree("outmerk.txt");
            Check();
            //TransBefore("20");
           // CurBalance(2);
            //CurBalance();
            //TransAfter("90");
            Console.ReadKey();
            WriteBlockChain("outbch.txt");
            WriteMerkleTree("outmerk.txt");

        }

        void Check()
        {
            for (int i = 1; i < chain.Count; ++i)
            {
                Console.WriteLine(CheckMerkle(chain[i - 1].Split('=')[1] + chain[i].Split('=')[0], 0, i));
            }
        }

        void GenerateBlock()
        {
            string block = "";
            for (int i = 0; i < OPERATIONS_COUNT_IN_BLOCK; ++i)
            {
                time += rand.NextDouble();
                if (i > 0) block += '&';
                block += time.ToString() + " " + users[rand.Next(users.Count())].ToString() + " " +
                    users[rand.Next(users.Count())].ToString() + " " + rand.Next(MAX_SUM).ToString();
            }
            block += '|';
            string[] buf = chain.Last().Split('=');
            string nons = Mining(buf[buf.Length - 1], block);
            block += nons;
            string hash = GetHash(sha256Hash, buf[buf.Length - 1] + block);
            block += '=' + hash;
            chain.Add(block);
            MerkleTreeAdd(hash, 0);
        }

        string Mining(string prev_hesh, string cur_bloc)
        {
            while (true)
            {
                Console.Write('.');
                for (int i = 0; i < users.Count; ++i)
                {
                    string trynons = rand.Next().ToString();
                    string tryhesh = GetHash(sha256Hash, prev_hesh + cur_bloc + trynons);
                    if (CheckHash(tryhesh))
                    {
                        Console.WriteLine($"\nUser {users[i]} found answer {trynons}");
                        if (VerifyNonsByAllUsers(trynons, cur_bloc))
                        {
                            Console.WriteLine($"User {users[i]} really found answer {trynons}\n");
                            return trynons;
                        }
                        else
                        {
                            Console.WriteLine($"User {users[i]} is mudak and tried to obmanut other users\n");
                        }
                    }
                }
            }
        }

        bool VerifyNonsByAllUsers(string nons, string bloc)
        {
            int count = 0;
            for (int i = 0; i < users.Count; ++i)
            {
                if (!CheckHash(GetHash(sha256Hash, chain.Last().Split('=')[1] + bloc + nons)))
                {
                    Console.WriteLine($"User {users[i]} not verified answer");
                }
                else
                {
                    Console.WriteLine($"User {users[i]} verified answer");
                    ++count;
                }
            }
            if (count > users.Count / 2)
            {
                Console.WriteLine($"Answer is correct ({count}/{users.Count} users agreed)");
                return true;
            }

            Console.WriteLine($"Answer is not correct (only {count}/{users.Count} users agreed)");
            return false;
        }

        bool CheckHash(string tryhesh)
        {
            return NONS_CHECK.Equals(tryhesh.Substring(0, NONS_CHECK.Length));
        }

        bool CheckMerkle(string data, int level, int number)
        {
            string hash = GetHash(sha256Hash, data);
            if (level == merkle.Count - 1)
            {
                if (merkle[level][number].Equals(hash))
                {
                    return true;
                }
                return false;
            }
            if (merkle[level][number].Equals(hash))
            {
                if (number % 2 == 0)
                {
                    if (merkle[level].Count - 1 == number) return CheckMerkle(merkle[level][number] + merkle[level][number], level + 1, number / 2);
                    return CheckMerkle(merkle[level][number] + merkle[level][number + 1], level + 1, number / 2);
                }
                else
                {
                    return CheckMerkle(merkle[level][number - 1] + merkle[level][number], level + 1, number / 2);
                }
            }
            else
            {
                return false;
            }
        }

        void MerkleTreeAdd(string hash, int level)
        {
            if (merkle.Count - 1 == level)
            {
                merkle[level].Add(hash);
                merkle.Add(new List<string> { GetHash(sha256Hash, merkle[level][merkle[level].Count - 2] + hash) });
                return;
            }

            if (merkle[level].Count % 2 == 0)
            {
                merkle[level].Add(hash);
                MerkleTreeAdd(GetHash(sha256Hash, hash + hash), level + 1);
            }
            else
            {
                merkle[level].Add(hash);
                MerkleTreeReplace(GetHash(sha256Hash, merkle[level][merkle[level].Count - 2] + hash), level + 1);
            }
        }

        void MerkleTreeReplace(string hash, int level)
        {
            if (merkle.Count == level) return;
            merkle[level][merkle[level].Count - 1] = hash;

            if (merkle[level].Count % 2 == 0)
            {
                MerkleTreeReplace(GetHash(sha256Hash, merkle[level][merkle[level].Count - 2] + merkle[level][merkle[level].Count - 1]), level + 1);
            }
            else
            {
                MerkleTreeReplace(GetHash(sha256Hash, merkle[level][merkle[level].Count - 1] + merkle[level][merkle[level].Count - 1]), level + 1);
            }
        }

        void ReadMerkleTree(string file_path)
        {
            StreamReader sr = new StreamReader(file_path);
            string lines = sr.ReadToEnd();
            lines = lines.Replace(" &", "&");
            List<string> spl = lines.Split('&').ToList();
            spl.Remove("");
            foreach (string s in spl) 
            {
                List<string> buf = new List<string>(s.Split(' ').ToList());
                buf.Remove("");
                merkle.Add(buf);
            }
            sr.Close();
        }

        void WriteMerkleTree(string file_path)
        {
            StreamWriter sw = new StreamWriter(file_path);
            foreach(List<string> ls in merkle)
            {
                foreach(string s in ls)
                {
                    sw.Write(s + ' ');
                }
                sw.Write('&');
            }
            sw.Close();
        }

        void WriteBlockChain(string file_path)
        {
            StreamWriter sw = new StreamWriter(file_path);
            foreach(string s in chain)
            {
                sw.Write(s + '>');
            }
            sw.Close();
        }

        void ReadBlockChain(string file_path)
        {
            StreamReader sr = new StreamReader(file_path);
            string lines = sr.ReadToEnd();
            List<string> spl = lines.Split('>').ToList();
            spl.Remove("");
            chain.AddRange(spl);
            sr.Close();
             if (chain.Count > 1)
            {
                string s = chain.Last();
                s = s.Split('|')[0].Split('&')[OPERATIONS_COUNT_IN_BLOCK - 1].Split(' ')[0];
                time = double.Parse(s);
            }
        }

        void CurBalance()
        {
            Console.WriteLine($"\nCurrent balance");
            for (int i = 0; i < users.Count; ++i)
            {
                string name = users[i];
                string balance = "0";
                foreach (string s in chain)
                {
                    string[] b = s.Split('|');
                    string[] b1 = b[0].Split('&');
                    foreach (string s1 in b1)
                    {
                        if (s1.Contains(name))
                        {
                            string[] buf = s1.Split(' ');
                            if (buf[1].Equals(name))
                            {
                                balance = calc.Sub(balance, buf[3]);
                            }
                            if (buf[2].Equals(name))
                            {
                                balance = calc.Add(balance, buf[3]);
                            }
                        }
                    }
                }
                Console.WriteLine($"{users[i]} -> {balance}");
            }
        }

        void CurBalance(int block_count)
        {
            Console.WriteLine($"\nBalance at block {block_count}");
            for (int i = 0; i < users.Count; ++i)
            {
                string name = users[i];
                string balance = "0";
                for (int j = 0; j < block_count; ++j)
                {
                    string[] b = chain[j].Split('|');
                    string[] b1 = b[0].Split('&');
                    foreach (string s1 in b1)
                    {
                        if (s1.Contains(name))
                        {
                            string[] buf = s1.Split(' ');
                            if (buf[1].Equals(name))
                            {
                                balance = calc.Sub(balance, buf[3]);
                            }
                            if (buf[2].Equals(name))
                            {
                                balance = calc.Add(balance, buf[3]);
                            }
                        }
                    }
                }
                Console.WriteLine($"{users[i]} -> {balance}");
            }
        }

        void TransBefore(string time)
        {
            Console.WriteLine("\nTransactions before " + time);
            foreach (string s in chain)
            {
                string[] b = s.Split('|');
                string[] b1 = b[0].Split('&');
                foreach (string s1 in b1)
                {
                    string[] buf = s1.Split(' ');
                    if (!buf[0].Equals("=0000") && calc.Less(buf[0].Split(',')[0], time))
                    {
                        Console.WriteLine($"Time: {buf[0]}, {buf[1]} -> {buf[2]} sum {buf[3]}");
                    }
                }
            }
        }
        void TransAfter(string time)
        {
            Console.WriteLine("\nTransactions after " + time);
            foreach (string s in chain)
            {
                string[] b = s.Split('|');
                string[] b1 = b[0].Split('&');
                foreach (string s1 in b1)
                {
                    string[] buf = s1.Split(' ');
                    if (!buf[0].Equals("=0000") && calc.More(buf[0].Split(',')[0], time))
                    {
                        Console.WriteLine($"Time: {buf[0]}, {buf[1]} -> {buf[2]} sum {buf[3]}");
                    }
                }
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
