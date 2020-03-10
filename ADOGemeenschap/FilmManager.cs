﻿using System;
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
                    comFilms.CommandText = "select * from Films";

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

                        while (rdrFilms.Read())
                        {
                            films.Add(new Film(
                                rdrFilms.GetInt32(bandNrPos),
                                rdrFilms.GetString(titelPos),
                                rdrFilms.GetInt32(genreNrPos),
                                rdrFilms.GetInt32(inVoorraadPos),
                                rdrFilms.GetInt32(uitVoorraadPos),
                                rdrFilms.GetDecimal(prijsPos),
                                rdrFilms.GetInt32(totaalVerhuurdPos)));
                        }//while

                    }//rdr
                }//comfilms
            }//conVideo
            return films;

        }
    }
}
