using SustavZaLijecnike.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SustavZaLijecnike
{
    public class Repository
    {
        //private static readonly string cs = "Server=domzdravlja.database.windows.net;Database=DomZdravlja;Uid=marko;Pwd=Alg€br@2021";
        private static readonly string cs = "Server=DESKTOP-FVTL4F1;Database=DomZdravlja;Uid=sa;Pwd=SQL";
        //private static readonly string cs = "Server=.;Database=DomZdravlja;Trusted_Connection=True;";

        public static Kredencijal GetKredencijali(string korisnickoIme)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetKredencijali);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("korisnickoIme", System.Data.SqlDbType.NVarChar).Value = korisnickoIme;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Kredencijal(
                                    (int)dr[nameof(Kredencijal.IDKredencijal)],
                                    dr[nameof(Kredencijal.KorisnickoIme)].ToString(),
                                    dr[nameof(Kredencijal.Hash)].ToString(),
                                    dr[nameof(Kredencijal.Salt)].ToString());
                        }
                        return null;
                    }
                }
            }
        }
        public static Ambulanta GetAmbulanta(Zaposlenik zaposlenik)
        {
            string procedureName;
            string parameterName;
            if (zaposlenik.TipZaposlenikaID == 1)
            {
                procedureName = "GetAmbulantaByLijecnik";
                parameterName = "lijecnikId";
            }
            else
            {
                procedureName = "GetAmbulantaBySestra";
                parameterName = "sestraId";
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = procedureName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(parameterName, System.Data.SqlDbType.Int).Value = zaposlenik.IDZaposlenik;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Ambulanta(
                                    (int)dr[nameof(Ambulanta.IDAmbulanta)],
                                    GetZaposlenikByID((int)dr["LijecnikID"]),
                                    GetZaposlenikByID((int)dr["SestraID"])
                                    );
                        }
                        return null;
                    }
                }
            }
        }

        public static Dijagnoza GetDijagnoza(int dijagnozaId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetDijagnoza);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("dijagnozaId", System.Data.SqlDbType.Int).Value = dijagnozaId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr[nameof(Dijagnoza.Naziv)].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );

                        }
                    }
                }
                return null;
            }
        }

        public static Lijek GetLijek(int lijekId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetLijek);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("lijekId", System.Data.SqlDbType.Int).Value = lijekId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Proizvodac proizvodac = new Proizvodac(
                                (int)dr[nameof(Proizvodac.IDProizvodac)],
                                dr["ProizvodacNaziv"].ToString()
                                );

                            return new Lijek(
                                (int)dr[nameof(Lijek.IDLijek)],
                                proizvodac,
                                dr[nameof(Lijek.Naziv)].ToString(),
                                dr[nameof(Lijek.Sastav)].ToString()
                                );

                        }
                    }
                }
                return null;
            }
        }

        public static IEnumerable<Pacijent> GetPacijenti(int lijecnikId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPacijenti);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = lijecnikId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Prebivaliste prebivalistePacijent = new Prebivaliste((int)dr[nameof(Pacijent.Prebivaliste.IDPrebivaliste)],
                                dr[nameof(Pacijent.Prebivaliste.UlicaBroj)].ToString(),
                                dr[nameof(Pacijent.Prebivaliste.Grad)].ToString());

                            Kredencijal kredencijalPacijent = new Kredencijal(
                                    (int)dr[nameof(Kredencijal.IDKredencijal)],
                                    dr[nameof(Kredencijal.KorisnickoIme)].ToString(),
                                    null,
                                    null);

                            Zaposlenik lijecnik = new Zaposlenik(
                                (int)dr[nameof(Zaposlenik.IDZaposlenik)],
                                dr["LijecnikIme"].ToString(),
                                dr["LijecnikPrezime"].ToString(),
                                (int)dr[nameof(Zaposlenik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Zaposlenik.DatumZaposlenja)],
                                null
                                );

                            yield return new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    prebivalistePacijent,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    lijecnik,
                                    kredencijalPacijent,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);
                        }
                    }
                }
            }
        }

        public static Pacijent GetPacijentByID(int pacijentId)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPacijentByID);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            Prebivaliste prebivalistePacijent = new Prebivaliste((int)dr[nameof(Pacijent.Prebivaliste.IDPrebivaliste)],
                                dr[nameof(Pacijent.Prebivaliste.UlicaBroj)].ToString(),
                                dr[nameof(Pacijent.Prebivaliste.Grad)].ToString());

                            Zaposlenik lijecnik = new Zaposlenik(
                                (int)dr[nameof(Pacijent.Lijecnik.IDZaposlenik)],
                                dr["LijecnikIme"].ToString(),
                                dr["LijecnikPrezime"].ToString(),
                                (int)dr[nameof(Pacijent.Lijecnik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Pacijent.Lijecnik.DatumZaposlenja)],
                                null
                                );

                            return new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    prebivalistePacijent,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    lijecnik,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);
                        }
                        return null;
                    }
                }
            }
        }

        public static Pacijent GetPacijent(string korisnickoIme, string lozinka)
        {
            Kredencijal kredencijal = GetKredencijali(korisnickoIme);

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPacijent);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("kredencijalId", System.Data.SqlDbType.Int).Value = kredencijal.IDKredencijal;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            Prebivaliste prebivalistePacijent = new Prebivaliste((int)dr[nameof(Pacijent.Prebivaliste.IDPrebivaliste)],
                                dr[nameof(Pacijent.Prebivaliste.UlicaBroj)].ToString(),
                                dr[nameof(Pacijent.Prebivaliste.Grad)].ToString());

                            Zaposlenik lijecnik = new Zaposlenik(
                                (int)dr[nameof(Pacijent.Lijecnik.IDZaposlenik)],
                                dr["LijecnikIme"].ToString(),
                                dr["LijecnikPrezime"].ToString(),
                                (int)dr[nameof(Pacijent.Lijecnik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Pacijent.Lijecnik.DatumZaposlenja)],
                                null
                                );

                            return new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    prebivalistePacijent,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    lijecnik,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);
                        }
                        return null;
                    }
                }
            }
        }

        public static void AddPacijent(Pacijent pacijent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddPacijent);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("ime", System.Data.SqlDbType.NVarChar).Value = pacijent.Ime;
                    cmd.Parameters.Add("prezime", System.Data.SqlDbType.NVarChar).Value = pacijent.Prezime;
                    cmd.Parameters.Add("datumRodenja", System.Data.SqlDbType.DateTime).Value = pacijent.DatumRodenja;
                    cmd.Parameters.Add("spolId", System.Data.SqlDbType.Int).Value = pacijent.SpolID;
                    cmd.Parameters.Add("ulicaBroj", System.Data.SqlDbType.NVarChar).Value = pacijent.Prebivaliste.UlicaBroj;
                    cmd.Parameters.Add("grad", System.Data.SqlDbType.NVarChar).Value = pacijent.Prebivaliste.Grad;
                    cmd.Parameters.Add("telefon", System.Data.SqlDbType.NVarChar).Value = pacijent.Telefon;
                    cmd.Parameters.Add("email", System.Data.SqlDbType.NVarChar).Value = pacijent.Email;
                    cmd.Parameters.Add("mbo", System.Data.SqlDbType.NVarChar).Value = pacijent.MBO;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = pacijent.Lijecnik.IDZaposlenik;
                    cmd.Parameters.Add("korisnickoIme", System.Data.SqlDbType.NVarChar).Value = pacijent.Kredencijal.KorisnickoIme;
                    cmd.Parameters.Add("hash", System.Data.SqlDbType.NVarChar).Value = pacijent.Kredencijal.Hash;
                    cmd.Parameters.Add("salt", System.Data.SqlDbType.NVarChar).Value = pacijent.Kredencijal.Salt;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdatePacijent(Pacijent pacijent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(UpdatePacijent);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("idPacijent", System.Data.SqlDbType.Int).Value = pacijent.IDPacijent;
                    cmd.Parameters.Add("ime", System.Data.SqlDbType.NVarChar).Value = pacijent.Ime;
                    cmd.Parameters.Add("prezime", System.Data.SqlDbType.NVarChar).Value = pacijent.Prezime;
                    cmd.Parameters.Add("datumRodenja", System.Data.SqlDbType.DateTime).Value = pacijent.DatumRodenja;
                    cmd.Parameters.Add("spolId", System.Data.SqlDbType.Int).Value = pacijent.SpolID;
                    cmd.Parameters.Add("prebivalisteId", System.Data.SqlDbType.Int).Value = pacijent.Prebivaliste.IDPrebivaliste;
                    cmd.Parameters.Add("ulicaBroj", System.Data.SqlDbType.NVarChar).Value = pacijent.Prebivaliste.UlicaBroj;
                    cmd.Parameters.Add("grad", System.Data.SqlDbType.NVarChar).Value = pacijent.Prebivaliste.Grad;
                    cmd.Parameters.Add("telefon", System.Data.SqlDbType.NVarChar).Value = pacijent.Telefon;
                    cmd.Parameters.Add("email", System.Data.SqlDbType.NVarChar).Value = pacijent.Email;
                    cmd.Parameters.Add("mbo", System.Data.SqlDbType.NVarChar).Value = pacijent.MBO;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = pacijent.Lijecnik.IDZaposlenik;
                    cmd.Parameters.Add("aktivan", System.Data.SqlDbType.Bit).Value = pacijent.Aktivan;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateKredencijal(Kredencijal kredencijal)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(UpdateKredencijal);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("kredencijalId", System.Data.SqlDbType.Int).Value = kredencijal.IDKredencijal;
                    cmd.Parameters.Add("hash", System.Data.SqlDbType.NVarChar).Value = kredencijal.Hash;
                    cmd.Parameters.Add("salt", System.Data.SqlDbType.NVarChar).Value = kredencijal.Salt;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeletePacijent(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(DeletePacijent);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("idPacijent", System.Data.SqlDbType.Int).Value = pacijentId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void OtpustiPacijenta(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(OtpustiPacijenta);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("idPacijent", System.Data.SqlDbType.Int).Value = pacijentId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<Zaposlenik> GetLijecnici()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetLijecnici);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Zaposlenik(
                                 (int)dr[nameof(Zaposlenik.IDZaposlenik)],
                                 dr[nameof(Zaposlenik.Ime)].ToString(),
                                 dr[nameof(Zaposlenik.Prezime)].ToString(),
                                 (int)dr[nameof(Zaposlenik.TipZaposlenikaID)],
                                 (DateTime)dr[nameof(Zaposlenik.DatumZaposlenja)],
                                 null
                                 );
                        }
                    }
                }
            }
        }

        public static Zaposlenik GetZaposlenik(int kredencijalID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetZaposlenik);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("kredencijalId", System.Data.SqlDbType.Int).Value = kredencijalID;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Zaposlenik(
                                (int)dr[nameof(Zaposlenik.IDZaposlenik)],
                                dr[nameof(Zaposlenik.Ime)].ToString(),
                                dr[nameof(Zaposlenik.Prezime)].ToString(),
                                (int)dr[nameof(Zaposlenik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Zaposlenik.DatumZaposlenja)],
                                null
                                );
                        }
                        return null;
                    }
                }
            }
        }

        public static Zaposlenik GetZaposlenikByID(int zaposlenikId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetZaposlenikByID);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("zaposlenikId", System.Data.SqlDbType.Int).Value = zaposlenikId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Zaposlenik(
                                zaposlenikId,
                                dr[nameof(Zaposlenik.Ime)].ToString(),
                                dr[nameof(Zaposlenik.Prezime)].ToString(),
                                (int)dr[nameof(Zaposlenik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Zaposlenik.DatumZaposlenja)],
                                null
                                );
                        }
                        return null;
                    }
                }
            }
        }

        public static IEnumerable<Pregled> GetPregledi(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPregledi);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dijagnoza dijagnoza = new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr[nameof(Dijagnoza.Naziv)].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );

                            yield return new Pregled(
                                (int)dr[nameof(Pregled.IDPregled)],
                                pacijentId,
                                (DateTime)dr[nameof(Pregled.Datum)],
                                dijagnoza,
                                dr[nameof(Pregled.Napomena)].ToString());
                        }
                    }
                }
            }
        }

        public static Pregled GetPregled(int pregledID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPregled);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pregledId", System.Data.SqlDbType.Int).Value = pregledID;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dijagnoza dijagnoza = new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr[nameof(Dijagnoza.Naziv)].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );

                            return new Pregled(
                                (int)dr[nameof(Pregled.IDPregled)],
                                (int)dr[nameof(Pregled.PacijentId)],
                                (DateTime)dr[nameof(Pregled.Datum)],
                                dijagnoza,
                                dr[nameof(Pregled.Napomena)].ToString());
                        }
                        return null;
                    }
                }
            }
        }

        public static void AddPregled(int pacijentId, int dijagnozaId, string napomena)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddPregled);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    cmd.Parameters.Add("dijagnozaId", System.Data.SqlDbType.Int).Value = dijagnozaId;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = napomena;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<Recept> GetRecepti(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetRecepti);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Pacijent pacijent = new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    null,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    null,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);

                            Dijagnoza dijagnoza = new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr["DijagnozaNaziv"].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );

                            Proizvodac proizvodac = new Proizvodac(
                                (int)dr[nameof(Proizvodac.IDProizvodac)],
                                dr["ProizvodacNaziv"].ToString()
                                );

                            Lijek lijek = new Lijek(
                                (int)dr[nameof(Lijek.IDLijek)],
                                proizvodac,
                                dr["LijekNaziv"].ToString(),
                                dr[nameof(Lijek.Sastav)].ToString()
                                );

                            yield return new Recept(
                                (int)dr[nameof(Recept.IDRecept)],
                                pacijent,
                                dijagnoza,
                                lijek,
                                (DateTime)dr[nameof(Recept.Datum)],
                                dr[nameof(Recept.Napomena)].ToString(),
                                (bool)dr[nameof(Recept.Ponavljajuci)],
                                (bool)dr[nameof(Recept.Odobren)]);
                        }
                    }
                }
            }
        }

        public static IEnumerable<Recept> GetReceptiNarudzbe(int lijecnikId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetReceptiNarudzbe);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = lijecnikId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Pacijent pacijent = new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    null,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    null,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);

                            Dijagnoza dijagnoza = new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr["DijagnozaNaziv"].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );

                            Proizvodac proizvodac = new Proizvodac(
                                (int)dr[nameof(Proizvodac.IDProizvodac)],
                                dr["ProizvodacNaziv"].ToString()
                                );

                            Lijek lijek = new Lijek(
                                proizvodac,
                                dr["LijekNaziv"].ToString(),
                                dr[nameof(Lijek.Sastav)].ToString()
                                );

                            yield return new Recept(
                                (int)dr[nameof(Recept.IDRecept)],
                                pacijent,
                                dijagnoza,
                                lijek,
                                (DateTime)dr[nameof(Recept.Datum)],
                                dr[nameof(Recept.Napomena)].ToString(),
                                (bool)dr[nameof(Recept.Ponavljajuci)],
                                (bool)dr[nameof(Recept.Odobren)]);
                        }
                    }
                }
            }
        }

        public static void AddRecept(Recept recept)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddRecept);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = recept.Pacijent.IDPacijent;
                    cmd.Parameters.Add("dijagnozaId", System.Data.SqlDbType.Int).Value = recept.Dijagnoza.IDDijagnoza;
                    cmd.Parameters.Add("lijekId", System.Data.SqlDbType.Int).Value = recept.Lijek.IDLijek;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = recept.Napomena;
                    cmd.Parameters.Add("ponavljajuci", System.Data.SqlDbType.Bit).Value = recept.Ponavljajuci;
                    cmd.Parameters.Add("odobren", System.Data.SqlDbType.Bit).Value = recept.Odobren;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateRecept(Recept recept)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(UpdateRecept);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("receptId", System.Data.SqlDbType.Int).Value = recept.IDRecept;
                    cmd.Parameters.Add("dijagnozaId", System.Data.SqlDbType.Int).Value = recept.Dijagnoza.IDDijagnoza;
                    cmd.Parameters.Add("lijekId", System.Data.SqlDbType.Int).Value = recept.Lijek.IDLijek;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = recept.Napomena;
                    cmd.Parameters.Add("ponavljajuci", System.Data.SqlDbType.Bit).Value = recept.Ponavljajuci;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void OdobriRecept(int receptId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(OdobriRecept);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("receptId", System.Data.SqlDbType.Int).Value = receptId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRecept(int receptId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(DeleteRecept);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("idRecept", System.Data.SqlDbType.Int).Value = receptId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<Uputnica> GetUputnice(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetUputnice);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Ustanova ustanova = new Ustanova(
                                (int)dr[nameof(Ustanova.IDUstanova)],
                                dr[nameof(Ustanova.Naziv)].ToString()
                                );

                            Dijagnoza dijagnoza = new Dijagnoza(
                                 (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                 dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                 dr["DijagnozaNaziv"].ToString(),
                                 dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                 );

                            TipUputnice tipUputnice = new TipUputnice(
                                (int)dr[nameof(TipUputnice.IDTipUputnice)],
                                 dr[nameof(TipUputnice.Tip)].ToString()
                                );

                            yield return new Uputnica(
                                (int)dr[nameof(Uputnica.IDUputnica)],
                                tipUputnice,
                                pacijentId,
                                dijagnoza,
                                ustanova,
                                (DateTime)dr[nameof(Uputnica.Datum)],
                                dr[nameof(Uputnica.Napomena)].ToString());
                        }
                    }
                }
            }
        }

        public static IEnumerable<TipUputnice> GetTipUputnica()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetTipUputnica);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new TipUputnice(
                                (int)dr[nameof(TipUputnice.IDTipUputnice)],
                                dr[nameof(TipUputnice.Tip)].ToString()
                                );
                        }
                    }
                }
            }
        }

        public static void AddUputnica(Uputnica uputnica)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddUputnica);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = uputnica.PacijentID;
                    cmd.Parameters.Add("tipUputniceId", System.Data.SqlDbType.Int).Value = uputnica.TipUputnice.IDTipUputnice;
                    cmd.Parameters.Add("dijagnozaId", System.Data.SqlDbType.Int).Value = uputnica.Dijagnoza.IDDijagnoza;
                    cmd.Parameters.Add("ustanovaId", System.Data.SqlDbType.Int).Value = uputnica.Ustanova.IDUstanova;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = uputnica.Napomena;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<Termin> GetTerminiPacijenta(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetTerminiPacijenta);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Prebivaliste prebivalistePacijent = new Prebivaliste((int)dr[nameof(Pacijent.Prebivaliste.IDPrebivaliste)],
                                dr[nameof(Pacijent.Prebivaliste.UlicaBroj)].ToString(),
                                dr[nameof(Pacijent.Prebivaliste.Grad)].ToString());

                            Zaposlenik lijecnik = new Zaposlenik(
                                (int)dr[nameof(Pacijent.Lijecnik.IDZaposlenik)],
                                dr["LijecnikIme"].ToString(),
                                dr["LijecnikPrezime"].ToString(),
                                (int)dr[nameof(Pacijent.Lijecnik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Pacijent.Lijecnik.DatumZaposlenja)],
                                null
                                );

                            Pacijent pacijent = new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    prebivalistePacijent,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    lijecnik,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);

                            yield return new Termin(
                                (int)dr[nameof(Termin.IDTermin)],
                                pacijent,
                                lijecnik,
                                (DateTime)dr[nameof(Termin.Datum)],
                                dr[nameof(Termin.Vrijeme)].ToString(),
                                dr[nameof(Termin.Napomena)].ToString());
                        }
                    }
                }
            }
        }

        public static IEnumerable<Termin> GetTerminiLijecnika(int lijecnikId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetTerminiLijecnika);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = lijecnikId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Prebivaliste prebivalistePacijent = new Prebivaliste((int)dr[nameof(Pacijent.Prebivaliste.IDPrebivaliste)],
                                dr[nameof(Pacijent.Prebivaliste.UlicaBroj)].ToString(),
                                dr[nameof(Pacijent.Prebivaliste.Grad)].ToString());

                            Zaposlenik lijecnik = new Zaposlenik(
                                (int)dr[nameof(Pacijent.Lijecnik.IDZaposlenik)],
                                dr["LijecnikIme"].ToString(),
                                dr["LijecnikPrezime"].ToString(),
                                (int)dr[nameof(Pacijent.Lijecnik.TipZaposlenikaID)],
                                (DateTime)dr[nameof(Pacijent.Lijecnik.DatumZaposlenja)],
                                null
                                );

                            Pacijent pacijent = new Pacijent(
                                    (int)dr[nameof(Pacijent.IDPacijent)],
                                    dr[nameof(Pacijent.Ime)].ToString(),
                                    dr[nameof(Pacijent.Prezime)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumRodenja)],
                                    dr[nameof(Pacijent.MBO)].ToString(),
                                    prebivalistePacijent,
                                    (int)dr[nameof(Pacijent.SpolID)],
                                    dr[nameof(Pacijent.Telefon)].ToString(),
                                    dr[nameof(Pacijent.Email)].ToString(),
                                    (DateTime)dr[nameof(Pacijent.DatumUpisa)],
                                    lijecnik,
                                    null,
                                    (bool)dr[nameof(Pacijent.Aktivan)]);

                            yield return new Termin(
                                (int)dr[nameof(Termin.IDTermin)],
                                pacijent,
                                lijecnik,
                                (DateTime)dr[nameof(Termin.Datum)],
                                dr[nameof(Termin.Vrijeme)].ToString(),
                                dr[nameof(Termin.Napomena)].ToString());
                        }
                    }
                }
            }
        }

        public static void AddTermin(Termin termin)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddTermin);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = termin.Pacijent.IDPacijent;
                    cmd.Parameters.Add("lijecnikId", System.Data.SqlDbType.Int).Value = termin.Lijecnik.IDZaposlenik;
                    cmd.Parameters.Add("vrijeme", System.Data.SqlDbType.NVarChar).Value = termin.Vrijeme;
                    cmd.Parameters.Add("datum", System.Data.SqlDbType.DateTime).Value = termin.Datum;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = termin.Napomena;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<string> GetPredefiniraniTermini()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetPredefiniraniTermini);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return dr["PocetakTermina"].ToString();
                        }
                    }
                }
            }
        }

        public static IEnumerable<Cjepivo> GetCjepiva()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetCjepiva);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Cjepivo(
                                (int)dr[nameof(Cjepivo.IDCjepivo)],
                                null,
                                dr[nameof(Cjepivo.Naziv)].ToString(),
                                dr[nameof(Cjepivo.Sastav)].ToString()
                                );
                        }
                    }
                }
            }
        }

        public static IEnumerable<Cijepljenje> GetCijepljenja(int pacijentId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetCijepljenja);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = pacijentId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Proizvodac proizvodac = new Proizvodac(
                                (int)dr[nameof(Proizvodac.IDProizvodac)],
                                dr["ProizvodacNaziv"].ToString()
                                );

                            Cjepivo cjepivo = new Cjepivo(
                                (int)dr[nameof(Cjepivo.IDCjepivo)],
                                proizvodac,
                                dr[nameof(Cjepivo.Naziv)].ToString(),
                                dr[nameof(Cjepivo.Sastav)].ToString()
                                );

                            yield return new Cijepljenje(
                                (int)dr[nameof(Cijepljenje.IDCijepljenje)],
                                pacijentId,
                                cjepivo,
                                (DateTime)dr[nameof(Cijepljenje.Datum)],
                                dr[nameof(Cijepljenje.Napomena)].ToString());
                        }
                    }
                }
            }
        }

        public static void AddCijepljenje(Cijepljenje cijepljenje)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddCijepljenje);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("pacijentId", System.Data.SqlDbType.Int).Value = cijepljenje.PacijentID;
                    cmd.Parameters.Add("cjepivoId", System.Data.SqlDbType.Int).Value = cijepljenje.Cjepivo.IDCjepivo;
                    cmd.Parameters.Add("datum", System.Data.SqlDbType.DateTime).Value = cijepljenje.Datum;
                    cmd.Parameters.Add("napomena", System.Data.SqlDbType.NVarChar).Value = cijepljenje.Napomena;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<Dijagnoza> GetDijagnoze()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetDijagnoze);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Dijagnoza(
                                (int)dr[nameof(Dijagnoza.IDDijagnoza)],
                                dr[nameof(Dijagnoza.MKBSifra)].ToString(),
                                dr[nameof(Dijagnoza.Naziv)].ToString(),
                                dr[nameof(Dijagnoza.NazivLatinski)].ToString()
                                );
                        }
                    }
                }
            }
        }

        public static IEnumerable<Lijek> GetLijekovi()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetLijekovi);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Proizvodac proizvodac = new Proizvodac(
                                (int)dr[nameof(Proizvodac.IDProizvodac)],
                                dr["ProizvodacNaziv"].ToString()
                                );

                            yield return new Lijek(
                                (int)dr[nameof(Lijek.IDLijek)],
                                proizvodac,
                                dr[nameof(Lijek.Naziv)].ToString(),
                                dr[nameof(Lijek.Sastav)].ToString()
                                );

                        }
                    }
                }
            }
        }
        public static IEnumerable<Ustanova> GetUstanove()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetUstanove);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Ustanova(
                                (int)dr[nameof(Ustanova.IDUstanova)],
                                dr[nameof(Ustanova.Naziv)].ToString()
                                );
                        }
                    }
                }
            }
        }
    }
}
