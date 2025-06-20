using Npgsql;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DAO
{
    public class GostDAO
    {
        private readonly string connectionString;

        public GostDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<GostKartaDTO> VratiStatistikuKartiPoGostima()
        {
            var lista = new List<GostKartaDTO>();

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string query = @"
                SELECT 
                    g.mbr,
                    o.ime,
                    o.prez,
                    COUNT(k.rbr_krt) AS broj_karata,
                    COALESCE(AVG(k.cijena_krt), 0) AS prosjecna_cijena
                FROM gost g
                JOIN osoba o ON g.mbr = o.mbr
                LEFT JOIN karta k ON g.mbr = k.gost_mbr
                GROUP BY g.mbr, o.ime, o.prez
                ORDER BY broj_karata DESC;
            ";

            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new GostKartaDTO
                {
                    Mbr = reader.GetInt32(0),
                    Ime = reader.GetString(1),
                    Prezime = reader.GetString(2),
                    BrojKarata = reader.GetInt32(3),
                    ProsjecnaCijena = reader.GetDouble(4)
                });
            }

            return lista;
        }
    
}
}
