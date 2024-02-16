using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DocumentType : IEntity
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string? Name { get; set; }

        public string? Match { get; set; }
        public int? MatchingAlgorithm { get; set; }

        public bool IsInsensitive { get; set; }
        public int DocumentCount { get; set; }

        public int owner { get; set; }

        public bool user_can_change { get; set; }

    }
}
