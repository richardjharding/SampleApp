using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprydon.Portal.models
{
    public class User
    {
        public int Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string EditedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastEdited { get; set; }

        public byte[] RowVersion { get; set; }
    }
}