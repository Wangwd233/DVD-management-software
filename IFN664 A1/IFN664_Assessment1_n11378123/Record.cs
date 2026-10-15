using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFN664_Assessment1_n11378123
{
    internal class Record
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool Status { get; set; }

        public Record(string firstname, string lastname, bool status)
        {
            Firstname = firstname;
            Lastname = lastname;
            Status = status;
        }
    }
}
