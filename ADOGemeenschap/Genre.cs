using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOGemeenschap
{
    public class Genre
    {
		private Int32 genreNrValue;
		private string genreNaamValue;

		public string GenreNaam
		{
			get { return genreNaamValue; }
			set { genreNaamValue = value; }
		}

		public Int32 GenreNr
		{
			get { return genreNrValue; }
			set { genreNrValue = value; }
		}
		public Genre(Int32 genreNr, string genreNaam)
		{
			GenreNr = genreNr;
			GenreNaam = genreNaam;
		}

		public Genre()
		{

		}

		
		


		


	}
}
