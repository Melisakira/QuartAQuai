using System.Globalization;

namespace QuartAQuai.AQuai;

public class EntreeJournal
{
     public string Name { get;}
        public string Poste { get;}
        public DateTime Date { get;}
        public string Evenement {  get;}
   

    public EntreeJournal(string name, string poste, DateTime date, string evenement)
    { Name = name; Poste = poste; Date = date; Evenement = evenement; 
    }
}
