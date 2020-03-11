using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;

namespace ADOGemeenschap
{
    public class FilmManager
    {
        public ObservableCollection<Film> GetFilms()
        {
            ObservableCollection<Film> films = new ObservableCollection<Film>();
            var manager = new VideoDBManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comFilms = conVideo.CreateCommand())
                {
                    comFilms.CommandType = CommandType.Text;
                    comFilms.CommandText = "select * from Films inner join Genres on Films.GenreNr = Genres.GenreNr";

                    conVideo.Open();
                    using (var rdrFilms = comFilms.ExecuteReader())
                    {
                        Int32 bandNrPos = rdrFilms.GetOrdinal("BandNr");
                        Int32 titelPos = rdrFilms.GetOrdinal("Titel");
                        Int32 genreNrPos = rdrFilms.GetOrdinal("GenreNr");
                        Int32 inVoorraadPos = rdrFilms.GetOrdinal("InVoorraad");
                        Int32 uitVoorraadPos = rdrFilms.GetOrdinal("UitVoorraad");
                        Int32 prijsPos = rdrFilms.GetOrdinal("Prijs");
                        Int32 totaalVerhuurdPos = rdrFilms.GetOrdinal("TotaalVerhuurd");
                        Int32 genrePos = rdrFilms.GetOrdinal("Genre");

                        while (rdrFilms.Read())
                        {
                            films.Add(new Film(
                                rdrFilms.GetInt32(bandNrPos),
                                rdrFilms.GetString(titelPos),
                                rdrFilms.GetInt32(genreNrPos),
                                rdrFilms.GetInt32(inVoorraadPos),
                                rdrFilms.GetInt32(uitVoorraadPos),
                                rdrFilms.GetDecimal(prijsPos),
                                rdrFilms.GetInt32(totaalVerhuurdPos),
                                rdrFilms.GetString(genrePos)));
                        }//while

                    }//rdr
                }//comfilms
            }//conVideo
            return films;

        }
        public void SchrijfVerwijderingen(List<Film> films)
        {
            var manager = new VideoDBManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comDelete = conVideo.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from Films where Titel =@titel";

                    var parTitel = comDelete.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comDelete.Parameters.Add(parTitel);

                    conVideo.Open();
                    foreach (Film eenFilm in films)
                    {
                        parTitel.Value = eenFilm.Titel;
                        if (comDelete.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Verwijderen niet gelukt: " + eenFilm.Titel);
                        }
                    }

                }
            }
        }

        public void SchrijfToevoegingen(List<Film> films)
        {
            var manager = new VideoDBManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comInsert = conVideo.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "Insert into Films (Titel, GenreNr, InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd) " +
                        "values(@titel, @genreNr, @inVoorraad, @uitVoorraad, @prijs, @totaalVerhuurd)";

                    var parTitel = comInsert.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comInsert.Parameters.Add(parTitel);

                    var parGenreNr = comInsert.CreateParameter();
                    parGenreNr.ParameterName = "@genreNr";
                    comInsert.Parameters.Add(parGenreNr);

                    var parInVoorraad = comInsert.CreateParameter();
                    parInVoorraad.ParameterName = "@inVoorraad";
                    comInsert.Parameters.Add(parInVoorraad);

                    var parUitVoorraad = comInsert.CreateParameter();
                    parUitVoorraad.ParameterName = "@uitVoorraad";
                    comInsert.Parameters.Add(parUitVoorraad);

                    var parPrijs = comInsert.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    comInsert.Parameters.Add(parPrijs);

                    var parTotaalVerh = comInsert.CreateParameter();
                    parTotaalVerh.ParameterName = "@totaalVerhuurd";
                    comInsert.Parameters.Add(parTotaalVerh);

                    conVideo.Open();
                    foreach (Film eenFilm in films)
                    {
                        parTitel.Value = eenFilm.Titel;
                        parGenreNr.Value = eenFilm.GenreNr;
                        parInVoorraad.Value = eenFilm.InVoorraad;
                        parUitVoorraad.Value = eenFilm.UitVoorraad;
                        parPrijs.Value = eenFilm.Prijs;
                        parTotaalVerh.Value = eenFilm.TotaalVerhuurd;
                        if (comInsert.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Toevoegen niet gelukt: " + eenFilm.Titel);
                        }
                    }

                }
            }
        }
        public void Schrijfwijzigingen(List<Film> films)
        {
            var manager = new VideoDBManager();
            using (var conVideo = manager.GetConnection())
            {
                using (var comUpdate = conVideo.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "update Films set InVoorraad=@inVoorraad, UitVoorraad=@uitVoorraad, " +
                        "TotaalVerhuurd=@totaalVerhuurd where Titel=@titel";

                    var parInVoorraad = comUpdate.CreateParameter();
                    parInVoorraad.ParameterName = "@inVoorraad";
                    comUpdate.Parameters.Add(parInVoorraad);

                    var parUitVoorraad = comUpdate.CreateParameter();
                    parUitVoorraad.ParameterName = "@uitVoorraad";
                   comUpdate.Parameters.Add(parUitVoorraad);

                    var parTotaalVerh = comUpdate.CreateParameter();
                    parTotaalVerh.ParameterName = "@totaalVerhuurd";
                    comUpdate.Parameters.Add(parTotaalVerh);


                    var parTitel = comUpdate.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comUpdate.Parameters.Add(parTitel);

                    conVideo.Open();
                    foreach (Film eenFilm in films)
                    {
                        parTitel.Value = eenFilm.Titel;
                        
                        parInVoorraad.Value = eenFilm.InVoorraad;
                        parUitVoorraad.Value = eenFilm.UitVoorraad;
                        
                        parTotaalVerh.Value = eenFilm.TotaalVerhuurd;
                        if (comUpdate.ExecuteNonQuery()==0)
                        {
                            throw new Exception("Aanpassingen niet gelukt:" + eenFilm.Titel);
                        }
                    }
                }
            }
        }

    
    public Int32 GeeftGenreNr(string Genre)
    {

        var dbmanager = new VideoDBManager();
        using (var conVideo = dbmanager.GetConnection())
        {
            using (var comGenreNr = conVideo.CreateCommand())
            {
                comGenreNr.CommandType = CommandType.Text;
                comGenreNr.CommandText = "select GenreNr from Genres where Genre=@genre";

                var parGenre = comGenreNr.CreateParameter();
                parGenre.ParameterName = "@genre";
                parGenre.Value = Genre;
                comGenreNr.Parameters.Add(parGenre);
                conVideo.Open();
                Object resultaat = comGenreNr.ExecuteScalar();
                return (Int32)resultaat;
            }
        }
    }
}
}
