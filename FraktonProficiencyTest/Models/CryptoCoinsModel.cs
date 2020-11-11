using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProficiencyTest.Models
{
    public class CryptoCoinsModel
    {
        public IList<Data> Data { get; set; }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Supply { get; set; }
        public string MaxSupply { get; set; }
    }
}
