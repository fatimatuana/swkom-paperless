using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Correspondent : IEntity
    {
        [Key]
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