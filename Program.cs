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

        AnsiConsole.Write(new FigletText("QuartAQuai").Color(Color.RoyalBlue1));
        AnsiConsole.MarkupLine("[bold cadetblue_1] Simulation du quart de nuit (à quai) - F911 Wesdiep, à Zeebrugge[/]\n");

        bool continuerLeQuart = true;
        while (continuerLeQuart)
        {
            string choix = Choisir(" Que souhaitez-vous faire ?",
                                   "1. Consulter le journal de bord",
                                   "2. Faire la ronde de sécurité",
                                   "3. Assurer la veille à la coupée",
                                   "4. Déclarer un incident",
                                   "0. Terminer le quart");

            switch (choix)
            {
                case "1. Consulter le journal de bord":
                    ConsulterJournal(journal);
                    break;
                case "2. Faire la ronde de sécurité":
                    FaireRonde(navire, rondeur, journal, centreAlerte);
                    break;
                case "3. Assurer la veille à la coupée":
                    AssurerVeille(veilleur, journal, centreAlerte);
                    break;
                case "4. Déclarer un incident":
                    DeclarerIncident(journal, centreAlerte);
                    break;
                case "0. Terminer le quart":
                    continuerLeQuart = false;
                    AnsiConsole.MarkupLine("[bold deepskyblue2] Fin du quart.[/]\n");
                    break;
            }

            AnsiConsole.Write(new Rule().RuleStyle("grey"));
        }
    }
    static void ConsulterJournal(JournalDeBord journal)
    {
        AfficherSection("Journal de bord");

        bool prendreLeQuart = AnsiConsole.Confirm("Consigner une prise de quart avant de consulter le journal", false);
        if (prendreLeQuart)
        {
            string nomOfficier = AnsiConsole.Ask<string>("Nom de l'Officier prenant le quart :");
            string poste = AnsiConsole.Ask<string>("Poste occupé :");
            string heureDebut = AnsiConsole.Ask<string>("Heure de début du quart :");
            string heureFin = AnsiConsole.Ask<string>("Heude de fin de quart :");

            journal.AjouterEntree(DateTime.Now,
                                  nomOfficier,
                                  poste,
                                  $"Prise de quart de {heureDebut} à {heureFin}");
        }

        List<EntreeJournal> entrees = journal.ObtenirEntrees();
        if (entrees.Count == 0)
        {
            AnsiConsole.MarkupLine("[grey]Le journal de bord est vide pour l'instant[/]\n");
            return;
        }

        Table table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Nom");
        table.AddColumns("Poste");
        table.AddColumns("Événement");

        foreach (EntreeJournal entree in entrees)
        {
            table.AddRow(
                entree.Date.ToString("dd/MM/yyyy HH:mm:ss"),
                Markup.Escape(entree.Name),
                Markup.Escape(entree.Poste),
                Markup.Escape(entree.Evenement));
        }

        AnsiConsole.Write(table);
    }
    static void FaireRonde(
        Navire navire,
        RondeurSecurite rondeur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AfficherSection("Ronde de sécurité");

        foreach (string compartiment in navire.Compartiments)
        {
            string observation = AnsiConsole.Ask<string>($"Observation à {compartiment} (RAS ou description de l'anomalie) :");

            Navire.FaireRonde(rondeur, compartiment, observation);
            journal.AjouterEntree(DateTime.Now, rondeur, $"Ronde en {compartiment} - {observation}");

            bool autreAnomalie = AnsiConsole.Confirm($"Anomalie à signaler suite à la ronde de {compartiment} ?", false);
            while (autreAnomalie)
            {
                Incident incident = DemanderIncident("Type d'anomalie constatée ?",
                                                     "Gravité de l'anomalie ?",
                                                     "Description de cette anomalie :",
                                                     IncidentFactory.TypePanneElectrique,
                                                     IncidentFactory.TypeAvarieMecanique);

                Declarer(incident, centreAlerte);
                journal.AjouterEntree(DateTime.Now, rondeur, $"Incident signalé lors de la ronde : {incident.Decrire()}");

                autreAnomalie = AnsiConsole.Confirm($"Une autre anomalie à signaler dans {compartiment} ?", false);
            }
        }
    }
    static void AssurerVeille(
        VeilleurCoupee veilleur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AfficherSection("Veille à la coupée");

        string etatAmarres = AnsiConsole.Ask<string>("État des amarres :");
        string observation = AnsiConsole.Ask<string>("Observation à la coupée (RAS ou Description) :");

        veilleur.SurveillerQuai(etatAmarres, observation);
        journal.AjouterEntree(DateTime.Now, veilleur, $"Veille à la coupée - amarres {etatAmarres} - {observation}");

        bool autreMenace = AnsiConsole.Confirm("Cette observation nécessite-t-elle de déclarer une menace ?", false);
        while (autreMenace)
        {
            Incident incident = DemanderIncident("Type de menace ?",
                                                 "Gravité de la menace ?",
                                                 "Description de la menace :",
                                                 IncidentFactory.TypeIncidentSurete,
                                                 IncidentFactory.TypeAlerteMeteo);

            Declarer(incident, centreAlerte);
            journal.AjouterEntree(DateTime.Now, veilleur, $"Incident déclaré depuis la coupée : {incident.Decrire()}");

            autreMenace = AnsiConsole.Confirm("Une autre menace pour cette veille ?", false);
        }
    }
    static void DeclarerIncident(
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AfficherSection("Déclaration d'incident");

        Incident incident = DemanderIncident("Quel type d'incident déclarer ?",
                                             "Gravité de l'incident ?",
                                             "Description de l'incident :",
                                             IncidentFactory.Types);

        Declarer(incident, centreAlerte);
        journal.AjouterEntree(DateTime.Now,
                              "Centre d'alerte",
                              "Alertes",
                              $"Incident déclaré : {incident.Decrire()}");
    }
    private static Incident DemanderIncident(
        string titreType,
        string titreGravite,
        string titreDescription,
        params string[] types)
    {
        string type = Choisir(titreType, types);
        string gravite = Choisir(titreGravite, IncidentFactory.Gravites);
        string description = AnsiConsole.Ask<string>(titreDescription);
        string detail = AnsiConsole.Ask<string>(IncidentFactory.LibelleDetail(type));

        return IncidentFactory.Creer(type, gravite, description, detail);
    }
    private static void Declarer(Incident incident, CentreAlerte centreAlerte)
    {
        string couleur = CouleurGravite(incident.Gravite);
        AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]\n");
        AnsiConsole.Write(new Rule("[DarkKhaki]Réactions de l'équipage[/]\n").LeftJustified());

        centreAlerte.Notifier(incident);
    }
    private static void AfficherSection(string titre)
    {
        AnsiConsole.Write(new Rule($"[bold blue]{titre}[/]\n").LeftJustified());
    }
    private static string Choisir(string titre, params string[] choix)
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>().Title(titre).AddChoices(choix));
    }
    private static string CouleurGravite(string gravite)
    {
        return gravite switch
        {
            IncidentFactory.GraviteMineur => "yellow",
            IncidentFactory.GraviteMajeur => "orange1",
            IncidentFactory.GraviteCritique => "bold red",
            _ => "white",
        };
    }
}
