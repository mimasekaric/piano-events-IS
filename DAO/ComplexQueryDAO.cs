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
                  WITH UkupniKoncerti AS (
                    SELECT
                        ms.id_msk,
                        ms.naz_msk,
                        COALESCE(SUM(CAST(ka.cijena_krt AS NUMERIC)), 0) AS UkupnaZarada,
                        COUNT(DISTINCT k.id_dog) AS UkupanBrojKoncerata
   
                    FROM muzicka_skola ms
                    INNER JOIN sala s ON ms.id_msk = s.muzicka_skola_id_msk
                    INNER JOIN se_realizuje sr ON s.id_sala = sr.sala_id_sala
                    INNER JOIN pijanisticki_dogadjaj pd ON sr.pijanisticki_dogadjaj_id_dog = pd.id_dog
                    INNER JOIN koncert k ON pd.id_dog = k.id_dog
                    INNER JOIN karta ka ON k.karta_rbr_krt = ka.rbr_krt
                    WHERE k.stat = 'aktivan' AND EXTRACT(YEAR FROM pd.dat_poc) = @godina
                    GROUP BY ms.id_msk, ms.naz_msk
                ),
                BeneficijarniKoncerti AS (
                    SELECT
                        ms.id_msk,
                        ms.naz_msk,
     
                        COALESCE(SUM(CAST(ka.cijena_krt AS NUMERIC)), 0) AS ZaradaHumanitarnih
                    FROM muzicka_skola ms
                    INNER JOIN sala s ON ms.id_msk = s.muzicka_skola_id_msk
                    INNER JOIN se_realizuje sr ON s.id_sala = sr.sala_id_sala
                    INNER JOIN pijanisticki_dogadjaj pd ON sr.pijanisticki_dogadjaj_id_dog = pd.id_dog
                    INNER JOIN koncert k ON pd.id_dog = k.id_dog
                    INNER JOIN karta ka ON k.karta_rbr_krt = ka.rbr_krt
                    WHERE k.stat = 'aktivan' AND k.vrst = 'beneficijarni' AND EXTRACT(YEAR FROM pd.dat_poc) = @godina
                    GROUP BY ms.id_msk, ms.naz_msk
                )
                SELECT
                    ms.naz_msk AS NazivSkole,
                    COALESCE(uk.UkupnaZarada, 0) AS UkupnaZarada,
                    COALESCE(bk.ZaradaHumanitarnih, 0) AS ZaradaHumanitarnih,
                    COALESCE(uk.UkupanBrojKoncerata, 0) AS UkupanBrojKoncerata
                FROM muzicka_skola ms 
                LEFT JOIN UkupniKoncerti uk ON ms.id_msk = uk.id_msk 
                LEFT JOIN BeneficijarniKoncerti bk ON ms.id_msk = bk.id_msk 
                ORDER BY COALESCE(uk.UkupnaZarada, 0) DESC;";

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
                COALESCE(d.tip_dipl, 'Nema') AS tip_dipl,
                COALESCE(CAST(d.bod AS TEXT), 'Nema') AS bod,
                COALESCE(SUM(k.trajanje_min), 0) AS ukupno_trajanje_min
            FROM osoba o
            JOIN pijanista p ON o.mbr = p.mbr
            LEFT JOIN nastup n ON p.mbr = n.pijanista_mbr
                AND n.takmicenje_id_dog = @idTak
            LEFT JOIN diploma d ON n.diploma_id_dipl = d.id_dipl
            LEFT JOIN cini c ON n.id_nast = c.nastup_id_nast
            LEFT JOIN kompozicija k ON c.kompozicija_id_komp = k.id_komp
            GROUP BY o.ime, o.prez, o.god, d.tip_dipl, d.bod
            HAVING COALESCE(SUM(k.trajanje_min), 0) BETWEEN 0 AND 20
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
                };
                string bodoviString = reader.GetString(4);
                float bodoviValue;
                if (float.TryParse(bodoviString, out bodoviValue))
                {
                    takmicar.Bodovi = bodoviValue;
                }
                else
                {
                 
                    takmicar.Bodovi = 0;
                }

                takmicar.UkupnoTrajanjeMin = reader.GetFloat(5);
                rezultati.Add(takmicar);
            }

            return rezultati;
        }
    }
}
