using egz.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace egz.Services
{
    public class SqlServerDbService : DbService
    {
        private const string ConString = "Data Source=DESKTOP-RSTT48M\\SQLEXPRESS;Initial Catalog=egz;Integrated Security=True";

        public List<GetPrescriptionsResponse> GetPrescriptions(int id)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
               var  resp = new List<GetPrescriptionsResponse>();
                
                com.CommandText = "select dbo.Prescription.IdPrescription, Date, DueDate, dbo.Prescription.IdPatient, dbo.Prescription.IdDoctor, Dose, Details, Name, Description, Type from dbo.Prescription  join dbo.Prescription_Medicament on dbo.Prescription_Medicament.IdPrescription = dbo.Prescription.IdPrescription join dbo.Medicament on dbo.Prescription_Medicament.IdMedicament = dbo.Medicament.IdMedicament where dbo.Prescription_Medicament.IdMedicament ='" + id + "' order by Date desc";
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    throw new ArgumentException("Improper Medicament Id");
                }


                do
                {
                    var pres = new GetPrescriptionsResponse();
                    pres.IdPrescription = (int)dr["IdPrescription"];
                    pres.IdPatient = (int)dr["IdPatient"];
                    pres.IdDoctor = (int)dr["IdDoctor"];
                    pres.Date = (DateTime)dr["Date"];
                    pres.DueDate = (DateTime)dr["DueDate"];
                    pres.Description = (string)dr["Description"];
                    pres.Type = (string)dr["Type"];
                    pres.Dose = (int)dr["Dose"];
                    pres.Name = (string)dr["Name"];
                    pres.Details = (string)dr["Details"];
                    resp.Add(pres);
                } while (dr.Read());
                dr.Close();

                
                return (resp);

            }
            
        }

        void DbService.DelPatient(int id)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                var tran = client.BeginTransaction();
                com.Transaction = tran;

                com.CommandText = "select 1 from dbo.Patient where IdPatient = '"+ id +"'";
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    tran.Rollback();
                    throw new ArgumentException("Improper Patient Id");

                }
                dr.Close();
                com.CommandText = "select IdPrescription from dbo.Prescription where IdPatient = '" + id + "'";
                var dr2 = com.ExecuteReader();
                while (dr2.Read())
                {
                    using (var client1 = new SqlConnection(ConString))
                    using (var com1 = new SqlCommand())
                    {

                        com1.Connection = client1;
                        client1.Open();
                        com1.CommandText = "delete from dbo.Prescription_Medicament where IdPrescription = '" + dr2["IdPrescription"] + "'";
                        var dr3 = com1.ExecuteNonQuery();
                        
                    }
                    
                }
                dr2.Close();
                com.CommandText = "delete from dbo.Prescription where IdPatient = '" + id + "'";
                var dr1 = com.ExecuteNonQuery();
                com.CommandText = "delete from dbo.Patient where IdPatient = '" + id + "'";
                var dr4 = com.ExecuteNonQuery();

                tran.Commit();

            }



        }
    }
}
