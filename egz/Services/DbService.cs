using egz.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egz.Services
{
    public interface DbService
    {
        List<GetPrescriptionsResponse> GetPrescriptions(int id);
        void DelPatient(int id);


    }
}
