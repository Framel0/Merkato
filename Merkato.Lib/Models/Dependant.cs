using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Dependant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
        public string SecondaryEmergencyContactRelationship { get; set; }
    }
}
