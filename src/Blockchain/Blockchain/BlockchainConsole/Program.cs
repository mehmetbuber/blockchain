using System;
using System.Collections.Generic;
using System.Reflection;
using Blockchain.Models;
using BlockchainConsole.Models;

namespace BlockchainConsole
{
    class Program
    {
        private static Chain _chain;

        private static void Main(string[] args)
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Blockchain started");
            Console.WriteLine("----------------");

            Initialize();

            Console.ReadKey();
        }

        private static void Initialize()
        {
            Console.Clear();

            _chain = new Chain(3);
            Console.WriteLine("Initialized Block with default values.");

            var ok = true;

            while (ok)
            {
                Console.Write("What do you want to do(add(a), list(l), validate(v), corrupt(c), exit(e)): ");
                string execType = Console.ReadLine();

                execType = execType.ToLower();
                switch (execType)
                {
                    case "a":
                        AddBlock();
                        break;
                    case "l":
                        ListBlocks();
                        break;
                    case "v":
                        ValidateChain();
                        break;
                    case "c":
                        CorruptData();
                        break;
                    case "e":
                        Console.WriteLine("Exiting");
                        ok = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }

        private static void CorruptData()
        {
            Console.Clear();
            _chain.Blocks[1].Data = "test";
            _chain.Blocks[1].CalculateHash();

            Console.WriteLine("Block chain data corrupted.");
        }

        private static void ValidateChain()
        {
            Console.Clear();
            var str = "";
            if (_chain.IsValid())
                str += "valid";
            else
                str += "invalid";

            Console.WriteLine("Block chain is " + str);
        }

        private static void ListBlocks()
        {
            Console.Clear();
            for (var i = 0; i < _chain.Blocks.Count; i++)
            {
                Block<object> ch = _chain.Blocks[i];
                Type myType = ch.Data.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(ch.Data, null);
                    Console.WriteLine($"Block { i }: { propValue }");
                }
                Console.WriteLine($"Hash { i }: { ch.Hash }");
                Console.WriteLine($"PreviousHash { i }: { ch.PreviousHash }");
                Console.WriteLine($"Timestamp { i }: { ch.Timestamp }");
                Console.WriteLine("-----------------");
            }
        }

        private static void AddBlock()
        {
            Console.Clear();
            Console.Write("Data: ");
            string str = Console.ReadLine();
            object data = new BlockData
            {
                BlockValue = str
            };
            Console.Clear();
            Console.WriteLine("Mining Block...");
            var block = new Block<object>(DateTime.Now, data,"");
            var c = _chain.AddBlock(block);
            Console.WriteLine("Block mined.");
        }
    }
}