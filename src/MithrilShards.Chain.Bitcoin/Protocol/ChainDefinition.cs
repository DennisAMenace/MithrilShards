﻿using MithrilShards.Core.Network.Protocol;
using System;

namespace MithrilShards.Chain.Bitcoin.Protocol {
   public class BitcoinMain : IChainDefinition {
      public string Name { get; }

      public byte[] MagicBytes { get; }

      public byte[] genesis { get; }

      public BitcoinMain() {
         this.Name = "BitcoinMain";
         this.MagicBytes = BitConverter.GetBytes(0x0709110B);
         this.genesis = null;
      }
   }
}
