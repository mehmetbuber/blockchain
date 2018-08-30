using System;
using Blockchain.Helpers;

namespace Blockchain.Models
{
    public class Block<T>
    {
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public T Data { get; set; }
        public DateTime Timestamp { get; set; }
        public int Nonce { get; set; }

        public Block(DateTime timestamp, T data, string previousHash)
        {
            Data = data;
            Timestamp = timestamp;
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            var dataString = Newtonsoft.Json.JsonConvert.SerializeObject(Data);
            return EncryptionHelper.GetSha(Nonce + PreviousHash + dataString + Timestamp);
        }

        public bool MineBlock(int difficulty)
        {
            while (Hash.Substring(0, difficulty) != new string('0', difficulty))
            {
                Nonce++;
                Hash = CalculateHash();
            }

            return true;
        }
    }
}
