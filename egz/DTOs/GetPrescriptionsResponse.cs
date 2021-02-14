using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egz.DTOs
{
    public class GetPrescriptionsResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor {get;set; }
        public int Dose { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
