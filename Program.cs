using QuartAQuai.Alertes;
using QuartAQuai.AQuai;
using QuartAQuai.Equipage;
using Spectre.Console;

namespace QuartAQuai;

class Program
{
    static void Main(string[] args)
    {
        Navire navire = new Navire("F911 Wesdiep", "Frégate");
        OfficierDeGarde officier = new OfficierDeGarde("Dupont", "Enseigne de vaisseau", "Poste de garde");
        Electricien electricien = new Electricien("Brandenbourg", "1er Maître", "Local technique");
        Mecanicien mecanicien = new Mecanicien("Lefevre", "Quartier-Maître", "Salle des machines");
        RondeurSecurite rondeur = new RondeurSecurite("Dubois", "Matelot", "Aucun poste fixe - rondes");
        VeilleurCoupee veilleur = new VeilleurCoupee("Marchand", "1er Matelot", "Coupée");

        CentreAlerte centreAlerte = new CentreAlerte();
        centreAlerte.Abonner(officier);
        centreAlerte.Abonner(electricien);
        centreAlerte.Abonner(mecanicien);
        centreAlerte.Abonner(rondeur);
        centreAlerte.Abonner(veilleur);

        JournalDeBord journal = new JournalDeBord();

        AnsiConsole.Write(new FigletText("QuartAQuai").Color(Color.LightPink3));
        AnsiConsole.MarkupLine("[bold cadetblue_1] Simulation du quart de nuit (à quai) - F911 Wesdiep, à Zeebrugge[/]\n");

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
                    journal.ConsulterEntrees();
                    break;
                case "2":
                    FaireRonde(navire, rondeur, journal, centreAlerte);
                    break;
                case "3":
                    AssurerVeille(veilleur, journal, centreAlerte);
                    break;
                case "4":
                    DeclarerIncident(journal, centreAlerte);
                    break;
                case "0":
                    continuerLeQuart = false;
                    Console.WriteLine("Fin du quart.");
                    break;
                default:
                    Console.WriteLine("Option invalide, veuillez réessayer");
                    break;
            }
        }
    }
    static void FaireRonde(
        Navire navire,
        RondeurSecurite rondeur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        foreach (string compartiment in navire.Compartiments)
        {
            Console.WriteLine($"Observation à {compartiment} (RAS ou description de l'anomalie) :");
            string observation = Console.ReadLine();

            Navire.FaireRonde(rondeur, compartiment, observation);
            journal.AjouterEntree(DateTime.Now,
                                  rondeur.Nom,
                                  rondeur.PosteAffecte,
                                  $"Ronde en {compartiment} - {observation}");

            Console.WriteLine($"Anomalie à signaler suite à la ronde de {compartiment} ? (o/n)");
            string reponseAnomalie = Console.ReadLine();
            bool autreAnomalie = reponseAnomalie != null && reponseAnomalie.Trim().ToLower() == "o";
            while (autreAnomalie)
            {
                Console.WriteLine("Type d'anomalie constatée ?");
                Console.WriteLine("1. Panne électrique");
                Console.WriteLine("2. Avarie électrique");
                string choixType = Console.ReadLine();
                string typeAnomalie = choixType == "1" ? "Panne électrique" : "Avarie mécanique";

                Console.WriteLine("Gravité de l'anomalie");
                Console.WriteLine("1. mineur");
                Console.WriteLine("2. majeur");
                Console.WriteLine("3. critique");
                string choixGravite = Console.ReadLine();
                string gravite = choixGravite
                switch
                {
                    "1" => "mineur",
                    "2" => "majeur",
                    "3" => "critique",
                    _ => "mineur"
                };

                Console.WriteLine("Description de cette anomalie :");
                string descriptionIncident = Console.ReadLine();

                Incident incident;
                if (typeAnomalie == "Panne électrique")
                {
                    Console.WriteLine("Équipement concerné :");
                    string equipement = Console.ReadLine();
                    incident = new PanneElectrique(gravite, descriptionIncident, equipement);
                }
                else
                {
                    Console.WriteLine("Équipement concerné :");
                    string equipement = Console.ReadLine();
                    incident = new AvarieMecanique(gravite, descriptionIncident, equipement);
                }

                Console.WriteLine($" --- Incident déclaré : {incident.Decrire()} ---");
                centreAlerte.Notifier(incident);
                journal.AjouterEntree(DateTime.Now,
                                      rondeur.Nom,
                                      rondeur.PosteAffecte,
                                      $"Incident signalé lors de la ronde : {incident.Decrire()}");

                Console.WriteLine($"Une autre anomalie à signaler dans {compartiment} ? (o/n)");
                string reponseAutre = Console.ReadLine();
                autreAnomalie = reponseAutre != null && reponseAutre.Trim().ToLower() == "o";
            }
        }
    }
    static void AssurerVeille(
        VeilleurCoupee veilleur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        Console.WriteLine("(État des ammares :)");
        string etatAmarres = Console.ReadLine();

        Console.WriteLine("Observation à la coupée (RAS ou Description) :");
        string observation = Console.ReadLine();

        veilleur.SurveillerQuai(etatAmarres, observation);
        journal.AjouterEntree(DateTime.Now,
                              veilleur.Nom,
                              veilleur.PosteAffecte,
                              $"Veille à la coupée - amarres {etatAmarres} - {observation}");

        Console.WriteLine("Cette obervation nécessite-t-elle de déclarer une menace ? (o/n)");
        string reponseMenace = Console.ReadLine();
        bool autreMenace = reponseMenace != null && reponseMenace.Trim().ToLower() == "o";
        while (autreMenace)
        {
            Console.WriteLine("Type de menace");
            Console.WriteLine("1. Incident de sûreté");
            Console.WriteLine("2. Alerte météo");
            string choixType = Console.ReadLine();
            string typeMenace = choixType == "1" ? "Incident de sûreté" : "Alerte météo";
            Console.WriteLine("Gravité de la menace");
            Console.WriteLine("1. mineur");
            Console.WriteLine("2. majeur");
            Console.WriteLine("3. critique");
            string choixGravite = Console.ReadLine();
            string gravite = choixGravite
            switch
            {
                "1" => "mineur",
                "2" => "majeur",
                "3" => "critique",
                _ => "mineur"
            };

            Console.WriteLine("Description de cette menace :");
            string descriptionIncident = Console.ReadLine();

            Incident incident;
            if (typeMenace == "Incident de sûreté")
            {
                Console.WriteLine("Nature de la menace :");
                string menace = Console.ReadLine();
                incident = new IncidentSurete(gravite, descriptionIncident, menace);
            }
            else
            {
                Console.WriteLine("Phénomène obervé:");
                string phenomene = Console.ReadLine();
                incident = new AlerteMeteo(gravite, descriptionIncident, phenomene);
            }

            Console.WriteLine($" --- Incident déclaré : {incident.Decrire()} ---");
            centreAlerte.Notifier(incident);
            journal.AjouterEntree(DateTime.Now,
                                  veilleur.Nom,
                                  veilleur.PosteAffecte,
                                  $"Incident déclaré depuis la coupée : {incident.Decrire()}");

            Console.WriteLine($"Une autre menace pour cette veille ? (o/n)");
            string reponseAutre = Console.ReadLine();
            autreMenace = reponseAutre != null && reponseAutre.Trim().ToLower() == "o";
        }
    }
    static void DeclarerIncident(
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        Console.WriteLine("Quel type d'incident déclarer");
        Console.WriteLine("1. Panne électrique");
        Console.WriteLine("2. Avarie mécanique");
        Console.WriteLine("3. Alerte météo");
        Console.WriteLine("4. Incident de sûreté");
        string choixType = Console.ReadLine();

        Console.WriteLine("Gravité de l'incident ?");
        Console.WriteLine("1. mineur");
        Console.WriteLine("2. majeur");
        Console.WriteLine("3. critique");
        string choixGravite = Console.ReadLine();
        string gravite = choixGravite
        switch
        {
            "1" => "mineur",
            "2" => "majeur",
            "3" => "critique",
            _ => "mineur"
        };

        Console.WriteLine("Description de l'incident :");
        string description = Console.ReadLine();

        Incident incident;
        switch (choixType)
        {
            case "1":
                Console.WriteLine("Système concerné :");
                string systeme = Console.ReadLine();
                incident = new PanneElectrique(gravite, description, systeme);
                break;
            case "2":
                Console.WriteLine("Équipement concerné :");
                string equipement = Console.ReadLine();
                incident = new AvarieMecanique(gravite, description, equipement);
                break;
            case "3":
                Console.WriteLine("Phénomène observé :");
                string phenomene = Console.ReadLine();
                incident = new AlerteMeteo(gravite, description, phenomene);
                break;
            case "4":
                Console.WriteLine("Nature de l'incident de sûreté :");
                string menace = Console.ReadLine();
                incident = new IncidentSurete(gravite, description, menace);
                break;
            default:
                Console.WriteLine("Type d'incident invalide.");
                return;
        }

        Console.WriteLine($"--- Incident déclaré : {incident.Decrire()}---");
        centreAlerte.Notifier(incident);
        journal.AjouterEntree(DateTime.Now,
                              "Centre d'alerte",
                              "Alertes",
                              $"Incident déclaré : {incident.Decrire()}");
    }
}





