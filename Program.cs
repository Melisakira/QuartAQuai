using QuartAQuai.Alertes;
using QuartAQuai.AQuai;
using QuartAQuai.Equipage;

namespace QuartAQuai;

class Program
{
    static void Main(string[] args)
    {
        Navire navire = new Navire("F911 Wesdiep", "Fr&gate");

        Electricien electricien = new Electricien("Brandenbourg", "1er Maître", "Local technique");
        Mecanicien mecanicien = new Mecanicien("Lefevre", "Quartier-Maître", "Salle des machines");
        OfficierDeGarde officier = new OfficierDeGarde("Dupont", "Enseigne de vaisseau", "Poste de garde");
        RondeurSecurite rondeur = new RondeurSecurite("Dubois", "Matelot", "Aucun poste fixe - rondes");
        VeilleurCoupee veilleur = new VeilleurCoupee("Marchand", "1er Matelot", "Coupée");

        CentreAlerte centreAlerte = new CentreAlerte();
        centreAlerte.Abonner(electricien);
        centreAlerte.Abonner(mecanicien);
        centreAlerte.Abonner(officier);
        centreAlerte.Abonner(rondeur);
        centreAlerte.Abonner(veilleur);

        JournalDeBord journal = new JournalDeBord();

        bool continuerLeQuart = true;
        while (continuerLeQuart)
        {
            Console.WriteLine("1. Consulter le journal de bord");
            Console.WriteLine("2. Faire la ronde de sécurité");
            Console.WriteLine("3. Assurer la veille à la coupée");
            Console.WriteLine("4. Déclarer un incident");
            Console.WriteLine("0. Terminer le quart");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.WriteLine("(à coder)"); break;
                case "2":
                    FaireRonde(navire, rondeur, journal, centreAlerte); break;
                case "3":
                    AssurerVeille(navire, veilleur, journal, centreAlerte); break;
                case "4":
                    DeclarerIncident(journal, centreAlerte); break;
                case "0":
                    continuerLeQuart = false;
                    Console.WriteLine("Fin du quart."); break;
            }
        }
    }

    static void FaireRonde(Navire navire, RondeurSecurite rondeur, JournalDeBord journal, CentreAlerte centreAlerte)
    {
        Console.WriteLine("(à coder)");
    }
    static void AssurerVeille(Navire navire, VeilleurCoupee veilleur, JournalDeBord journal, CentreAlerte centreAlerte)
    {
        Console.WriteLine("(à coder)");
    }
    static void DeclarerIncident(JournalDeBord journal, CentreAlerte centreAlerte)
    {
        Console.WriteLine("(à coder)");
    }
}


