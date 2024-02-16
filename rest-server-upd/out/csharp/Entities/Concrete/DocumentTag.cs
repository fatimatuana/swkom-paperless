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
    public class DocumentTag : IEntity
    {
            public long Id { get; set; }
            public string? Slug { get; set; }
            public string? Name { get; set; }
            public string? Match { get; set; }
            public long Matching_Algorithm { get; set; }
            public bool IsInsensitive { get; set; }
            public long DocumentCount { get; set; }
            public DateTime? LastCorrespondence { get; set; }
    }
}
