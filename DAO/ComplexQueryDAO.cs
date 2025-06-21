using Npgsql;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DAO
{
    public class ComplexQueryDAO
    {
        private readonly string connectionString;

        public ComplexQueryDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<SkolaZaradaDTO> GetZaradaPoSkolama(int godina)
        {
            var rezultati = new List<SkolaZaradaDTO>();

            string sql = @"
        SELECT 
            ms.naz_msk AS NazivSkole,
            COALESCE(SUM(CAST(ka.cijena_krt AS NUMERIC)), 0) AS UkupnaZarada,
            COALESCE(SUM(CASE WHEN k.vrst = 'beneficijarni' THEN CAST(ka.cijena_krt AS NUMERIC) ELSE 0 END), 0) AS ZaradaHumanitarnih,
            COUNT(DISTINCT k.id_dog) AS UkupanBrojKoncerata
        FROM muzicka_skola ms
        LEFT JOIN sala s ON ms.id_msk = s.muzicka_skola_id_msk
        LEFT JOIN se_realizuje sr ON s.id_sala = sr.sala_id_sala
        LEFT JOIN pijanisticki_dogadjaj pd ON sr.pijanisticki_dogadjaj_id_dog = pd.id_dog
        LEFT JOIN koncert k ON pd.id_dog = k.id_dog
        LEFT JOIN karta ka ON k.karta_rbr_krt = ka.rbr_krt
        WHERE k.stat = 'aktivan' OR k.stat IS NULL
        GROUP BY ms.naz_msk
        HAVING MAX(EXTRACT(YEAR FROM pd.dat_poc)) > @godina OR MAX(pd.dat_poc) IS NULL
        ORDER BY UkupnaZarada DESC;";

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("godina", godina);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dto = new SkolaZaradaDTO
                {
                    NazivSkole = reader.GetString(0),
                    UkupnaZarada = reader.GetDecimal(1),
                    ZaradaHumanitarnih = reader.GetDecimal(2),
                    UkupanBrojKoncerata = reader.GetInt32(3)
                };
                rezultati.Add(dto);
            }

            return rezultati;
        }

        public List<NastupDTO> GetTakmicariPoTakmicenju(int idTakmicenja)
        {
            var rezultati = new List<NastupDTO>();

            string sql = @"
            SELECT
    o.ime,
    o.prez,
    o.god,
    d.tip_dipl,
    d.bod,
    COALESCE(SUM(k.trajanje_min), 0) AS ukupno_trajanje_min
FROM nastup n
JOIN pijanista p ON n.pijanista_mbr = p.mbr
JOIN osoba o ON p.mbr = o.mbr
JOIN diploma d ON n.diploma_id_dipl = d.id_dipl
JOIN cini c ON n.id_nast = c.nastup_id_nast
JOIN kompozicija k ON c.kompozicija_id_komp = k.id_komp
WHERE n.takmicenje_id_dog = 101
  AND n.kateg_nast = 'druga'
GROUP BY o.ime, o.prez, o.god, d.tip_dipl, d.bod
ORDER BY d.bod DESC;";

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("idTak", idTakmicenja);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var takmicar = new NastupDTO
                {
                    Ime = reader.GetString(0),
                    Prezime = reader.GetString(1),
                    GodRodjenja = reader.GetInt32(2),
                    TipDiplome = reader.GetString(3),
                    Bodovi = reader.GetFloat(4),
                    UkupnoTrajanjeMin = reader.GetFloat(5)
                };
                rezultati.Add(takmicar);
            }

            return rezultati;
        }
    }
}
