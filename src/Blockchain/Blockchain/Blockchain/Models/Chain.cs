using System;
using System.Collections;
using System.Collections.Generic;
using Blockchain.Helpers;

namespace Blockchain.Models
{
    public class Chain
    {
        public List<Block<object>> Blocks { get; set; }
        public int Difficulty { get; set; }

        public Chain(int difficulty)
        {
            Difficulty = difficulty;
            Blocks = new List<Block<object>> {ChainHelper.GenesisBlock};
        }
        
        public Block<object> LatestBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public bool AddBlock(Block<object> block)
        {
            block.PreviousHash = LatestBlock().Hash;
            block.Hash = block.CalculateHash();
            if (block.MineBlock(Difficulty))
            {
                Blocks.Add(block);
                return true;
            }

            return false;
        }

        public bool IsValid()
        {
            for (var i = 1; i < Blocks.Count; i++)
            {
                var currentBlock = Blocks[i];
                var previousBlock = Blocks[i - 1];

                if (currentBlock.CalculateHash() != currentBlock.Hash)
                    return false;

                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }

            return true;
        }
    }
}
