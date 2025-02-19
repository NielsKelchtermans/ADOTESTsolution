﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOGemeenschap
{
    public class Film
    {
		private Int32 bandNrValue;
		public bool Changed { get; set; }
		private string titelValue;
		private Int32 genreNrValue;
		private Int32 inVoorraadValue;
		private Int32 uitVoorraadValue;
		private Decimal prijsValue;
		private Int32 totaalVerhuurdValue;
		private string genreValue;

		public string Genre
		{
			get { return genreValue; }
			set { genreValue = value;  }
		}


		public Int32 TotaalVerhuurd
		{
			get { return totaalVerhuurdValue; }
			set { totaalVerhuurdValue = value; Changed = true; }
		}


		public Decimal Prijs
		{
			get { return prijsValue; }
			set { prijsValue = value;  }
		}


		public Int32 UitVoorraad
		{
			get { return uitVoorraadValue; }
			set { uitVoorraadValue = value; Changed = true; }
		}


		public Int32 InVoorraad
		{
			get { return inVoorraadValue; }
			set { inVoorraadValue = value; Changed = true; }
		}


		public Int32 GenreNr
		{
			get { return genreNrValue; }
			set { genreNrValue = value;  }
		}


		public string Titel
		{
			get { return titelValue; }
			set { titelValue = value; }
		}



		public Int32 BandNr
		{
			get { return bandNrValue; }
			set { bandNrValue = value; }
		}

		public Film(Int32 bandNr, string titel, Int32 genreNr, Int32 inVoorraad, Int32 uitVoorraad, decimal prijs, Int32 totaalVerhuurd, string genre)
		{
			bandNrValue = bandNr;
			Titel = titel;
			GenreNr = genreNr;
			InVoorraad = inVoorraad;
			UitVoorraad = uitVoorraad;
			Prijs = prijs;
			TotaalVerhuurd = totaalVerhuurd;
			Changed = false;
			Genre = genre;
		}


		public Film()
		{

		}

	}
}
