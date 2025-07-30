using Npgsql;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DAO
{
    public class TransactionDAO
    {
        private readonly string _connectionString;

        public TransactionDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<TakmicenjeDTO> GetSvaTakmicenja()
        {
            var takmicenja = new List<TakmicenjeDTO>();

            string query = @"
                SELECT DISTINCT 
                    pd.id_dog,
                    pd.dat_poc,
                    pd.tip_dog,
                    ms.naz_msk
                FROM pijanisticki_dogadjaj pd
                JOIN se_realizuje sr ON pd.id_dog = sr.pijanisticki_dogadjaj_id_dog
                JOIN sala s ON sr.sala_id_sala = s.id_sala
                JOIN muzicka_skola ms ON s.muzicka_skola_id_msk = ms.id_msk
                WHERE pd.tip_dog = 'takmicenje'
                ORDER BY pd.dat_poc DESC;
            ";

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var dto = new TakmicenjeDTO
                {
                    IdDog = reader.GetInt32(0),
                    Datum = reader.GetDateTime(1),
                    TipTakmicenja = reader.GetString(2),
                    NazivSkole = reader.GetString(3)
                };

                takmicenja.Add(dto);
            }

            return takmicenja;
        }

        public bool UplataINastup(long mbr, int idTakmicenja, DateTime izabranoTakmicenjeDatum)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var tx = conn.BeginTransaction();

            try
            {
        
                int noviIdUplate;
                using (var cmd = new NpgsqlCommand("SELECT COALESCE(MAX(id_uplt), 0) FROM uplata", conn, tx))
                {
                    noviIdUplate = (int)cmd.ExecuteScalar() + 1;
                }

                // 1. Ubacujem uplatu sa statusom 'obrada' 
                var uplataCmd = new NpgsqlCommand(@"
                INSERT INTO uplata (id_uplt, iznos_uplt, stat_uplt, dat_uplt, pijanista_mbr)
                VALUES (@idUplata, 6000, 'obrada', CURRENT_DATE, @mbr)", conn, tx);
                uplataCmd.Parameters.AddWithValue("idUplata", noviIdUplate);
                uplataCmd.Parameters.AddWithValue("mbr", mbr);
                uplataCmd.ExecuteNonQuery(); 
                Console.WriteLine("Uplata dodana. ID uplate: " + noviIdUplate);



                int noviIdNastupa;
                using (var cmd = new NpgsqlCommand("SELECT COALESCE(MAX(id_nast), 0) FROM nastup", conn, tx))
                {
                    noviIdNastupa = (int)cmd.ExecuteScalar() + 1;
                }

                // 2. Ubacujem nastup vezan za takmičenje
                var nastupCmd = new NpgsqlCommand(@"
                INSERT INTO nastup (id_nast, dat_nast, kateg_nast, takmicenje_id_dog, pijanista_mbr)
                VALUES (@idNastup, @date, 'prva', @idTak, @mbr)", conn, tx);
                nastupCmd.Parameters.AddWithValue("idNastup", noviIdNastupa);
                nastupCmd.Parameters.AddWithValue("idTak", idTakmicenja);
                nastupCmd.Parameters.AddWithValue("date", izabranoTakmicenjeDatum);
                nastupCmd.Parameters.AddWithValue("mbr", mbr);
                nastupCmd.ExecuteNonQuery();
                Console.WriteLine("Nastup uspešno dodat. ID nastupa: " + noviIdNastupa);


                // 3. Ažuriram finansijsku karticu
                var updateOsobaFinKartiCmd = new NpgsqlCommand(@"
                UPDATE osoba
                SET fin_kartica = fin_kartica - 6000
                WHERE mbr = @mbrOsoba;", conn, tx);
                updateOsobaFinKartiCmd.Parameters.AddWithValue("mbrOsoba", mbr);
                int updatedRows = updateOsobaFinKartiCmd.ExecuteNonQuery();
                if (updatedRows > 0)
                {
                    Console.WriteLine("Finansijski karton osobe ažuriran.");
                }
                else
                {
                    Console.WriteLine("Upozorenje: Nije pronađena osoba za ažuriranje finansijskog kartona (MBR: " + mbr + ").");
                }


                // 4. Ažuriraj uplatu - postavi status na 'uspjesna'
                var updateUplataCmd = new NpgsqlCommand(@"
                UPDATE uplata SET stat_uplt = 'uspjesna'
                WHERE id_uplt = @idUplata;", conn, tx);
                updateUplataCmd.Parameters.AddWithValue("idUplata", noviIdUplate); 
                updateUplataCmd.ExecuteNonQuery();

                Console.WriteLine("Status uplate ažuriran na 'uspjesna'.");

                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška tokom transakcije: " + ex.Message);
                tx.Rollback();
                return false;
            }
        }   }
}
