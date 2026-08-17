using QuartAQuai.Alertes;
using QuartAQuai.AQuai;
using QuartAQuai.Equipage;
using Spectre.Console;

namespace QuartAQuai;

class Program
{
    static int Main(string[] args)
    {
        try
        {
            ExecuterQuart();
            return 0;
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine($"[bold red]Le quart s'interrompt sur une erreur : {Markup.Escape(exception.Message)}[/]");
            AnsiConsole.WriteException(exception, ExceptionFormats.ShortenEverything);
            return 1;
        }
    }
    static void ExecuterQuart()
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
            string choix = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(" Que souhaitez-vous faire ?").AddChoices("1. Consulter le journal de bord", "2. Faire la ronde de sécurité", "3. Assurer la veille à la coupée", "4. Déclarer un incident", "0. Terminer le quart"));

            try
            {
                continuerLeQuart = ExecuterChoix(choix, navire, rondeur, veilleur, journal, centreAlerte);
            }
            catch (Exception exception) when (exception is ArgumentException or InvalidOperationException or AggregateException)
            {
                SignalerErreur(exception);
            }

            AnsiConsole.Write(new Rule().RuleStyle("grey"));
        }
    }
    static bool ExecuterChoix(
        string choix,
        Navire navire,
        RondeurSecurite rondeur,
        VeilleurCoupee veilleur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        switch (choix)
        {
            case "1. Consulter le journal de bord":
                {
                    AnsiConsole.Write(new Rule("[bold blue]Journal de bord[/]").LeftJustified());

                    bool prendreLeQuart = AnsiConsole.Confirm("Consigner une prise de quart avant de consulter le journal", false);
                    if (prendreLeQuart)
                    {
                        string nomOfficier = Demander("Nom de l'Officier prenant le quart :");
                        string poste = Demander("Poste occupé :");
                        string heureDebut = Demander("Heure de début du quart :");
                        string heureFin = Demander("Heude de fin de quart :");

                        journal.AjouterEntree(DateTime.Now,
                           nomOfficier,
                           poste,
                           $"Prise de quart de {heureDebut} à {heureFin}");
                    }
                    IReadOnlyList<EntreeJournal> entrees = journal.ObtenirEntrees();
                    if (entrees.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[grey]Le journal de bord est vide pour l'instant[/]");
                    }
                    else
                    {
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
                    return true;
                }
            case "2. Faire la ronde de sécurité":
                FaireRonde(navire, rondeur, journal, centreAlerte);
                return true;
            case "3. Assurer la veille à la coupée":
                AssurerVeille(veilleur, journal, centreAlerte);
                return true;
            case "4. Déclarer un incident":
                DeclarerIncident(journal, centreAlerte);
                return true;
            case "0. Terminer le quart":
                AnsiConsole.MarkupLine("[bold deepskyblue2] Fin du quart.[/]");
                return false;
            default:
                throw new InvalidOperationException($"Choix de menu non pris en charge : {choix}");
        }
    }
    static string Demander(string question)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>(question)
            .Validate(reponse => string.IsNullOrWhiteSpace(reponse)
                ? ValidationResult.Error("[red]Une réponse non vide est attendue.[/]")
                : ValidationResult.Success()));
    }
    static void SignalerErreur(Exception exception)
    {
        if (exception is AggregateException aggregateException)
        {
            AnsiConsole.MarkupLine($"[bold red]{Markup.Escape(aggregateException.Message)}[/]");
            foreach (Exception echec in aggregateException.InnerExceptions)
            {
                AnsiConsole.MarkupLine($"[red]- {Markup.Escape(echec.Message)}[/]");
                if (echec.InnerException is not null)
                {
                    AnsiConsole.MarkupLine($"[red]  cause : {Markup.Escape(echec.InnerException.Message)}[/]");
                }
            }
            return;
        }

        AnsiConsole.MarkupLine($"[bold red]Action impossible : {Markup.Escape(exception.Message)}[/]");
    }
    static void FaireRonde(
        Navire navire,
        RondeurSecurite rondeur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AnsiConsole.Write(new Rule("[bold blue]Ronde de sécurité[/]").LeftJustified());

        foreach (string compartiment in navire.Compartiments)
        {
            string observation = Demander($"Observation à {compartiment} (RAS ou description de l'anomalie) :");

            Navire.FaireRonde(rondeur, compartiment, observation);
            journal.AjouterEntree(DateTime.Now,
                                  rondeur.Nom,
                                  rondeur.PosteAffecte,
                                  $"Ronde en {compartiment} - {observation}");

            bool autreAnomalie = AnsiConsole.Confirm($"Anomalie à signaler suite à la ronde de {compartiment} ?", false);
            while (autreAnomalie)
            {
                string typeAnomalie = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Type d'anomalie constatée ?").AddChoices("Panne électrique", "Avarie mécanique"));

                string gravite = DemanderGravite("Gravité de l'anomalie ?");

                string descriptionAnomalie = Demander("Description de cette anomalie :");

                Incident incident;
                if (typeAnomalie == "Panne électrique")
                {
                    string equipement = Demander("Équipement concerné :");
                    incident = new PanneElectrique(gravite, descriptionAnomalie, equipement);
                }
                else
                {
                    string equipement = Demander("Équipement concerné :");
                    incident = new AvarieMecanique(gravite, descriptionAnomalie, equipement);
                }
                string couleur = CouleurGravite(gravite);
                AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]");
                AnsiConsole.Write(new Rule("[DarkKhaki]Réactions de l'équipage[/]").LeftJustified());
                NotifierEquipage(centreAlerte, incident);
                journal.AjouterEntree(DateTime.Now,
                                      rondeur.Nom,
                                      rondeur.PosteAffecte,
                                      $"Incident signalé lors de la ronde : {incident.Decrire()}");

                autreAnomalie = AnsiConsole.Confirm($"Une autre anomalie à signaler dans {compartiment} ?", false);
            }
        }
    }
    private static string CouleurGravite(string gravite)
    {
        return gravite switch
        {
            "mineur" => "yellow",
            "majeur" => "orange1",
            "critique" => "bold red",
            _ => throw new ArgumentOutOfRangeException(nameof(gravite), gravite, "Gravité inconnue : aucune couleur d'affichage définie."),
        };
    }
    static string DemanderGravite(string question)
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>().Title(question).AddChoices(Incident.GravitesValides));
    }
    static void NotifierEquipage(CentreAlerte centreAlerte, Incident incident)
    {
        try
        {
            centreAlerte.Notifier(incident);
        }
        catch (AggregateException exception)
        {
            SignalerErreur(exception);
        }
    }
    static void AssurerVeille(
        VeilleurCoupee veilleur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AnsiConsole.Write(new Rule("[bold blue]Veille à la coupée[/]").LeftJustified());

        string etatAmarres = Demander("État des amarres :");
        string observation = Demander("Observation à la coupée (RAS ou Description) :");

        veilleur.SurveillerQuai(etatAmarres, observation);
        journal.AjouterEntree(DateTime.Now,
                              veilleur.Nom,
                              veilleur.PosteAffecte,
                              $"Veille à la coupée - amarres {etatAmarres} - {observation}");

        bool autreMenace = AnsiConsole.Confirm("Cette observation nécessite-t-elle de déclarer une menace ?", false);
        while (autreMenace)
        {
            string type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Type de menace ?").AddChoices("Incident de sûreté", "Alerte météo"));

            string gravite = DemanderGravite("Gravité de la menace ?");

            string descriptionIncident = Demander("Description de la menace :");

            Incident incident;
            if (type == "Incident de sûreté")
            {
                string menace = Demander("Nature de la menace :");
                incident = new IncidentSurete(gravite, descriptionIncident, menace);
            }
            else
            {
                string phenomene = Demander("Phénomène observé :");
                incident = new AlerteMeteo(gravite, descriptionIncident, phenomene);
            }

            string couleur = CouleurGravite(gravite);
            AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]");
            AnsiConsole.Write(new Rule("[DarkKhaki]Réactions de l'équipage[/]").LeftJustified());
            NotifierEquipage(centreAlerte, incident);
            journal.AjouterEntree(DateTime.Now,
                                  veilleur.Nom,
                                  veilleur.PosteAffecte,
                                  $"Incident déclaré depuis la coupée : {incident.Decrire()}");

            autreMenace = AnsiConsole.Confirm("Une autre menace pour cette veille ?", false);
        }
    }
    static void DeclarerIncident(
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AnsiConsole.Write(new Rule("[bold blue]Déclaration d'incident[/]").LeftJustified());
        string type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Quel type d'incident déclarer ?").AddChoices("Panne électrique", "Avarie mécanique", "Alerte météo", "Incident de sûreté"));

        string gravite = DemanderGravite("Gravité de l'incident ?");

        string description = Demander("Description de l'incident :");

        Incident incident = type switch
        {
            "Panne électrique" => new PanneElectrique(gravite, description, Demander("Équipement concerné :")),
            "Avarie mécanique" => new AvarieMecanique(gravite, description, Demander("Équipement concerné :")),
            "Alerte météo" => new AlerteMeteo(gravite, description, Demander("Phénomène observé :")),
            "Incident de sûreté" => new IncidentSurete(gravite, description, Demander("Nature de la menace :")),
            _ => throw new InvalidOperationException($"Type d'incident invalide : {type}")
        };

        string couleur = CouleurGravite(gravite);
        AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]");
        AnsiConsole.Write(new Rule("[DarkKhaki]Réaction de l'équipage[/]").LeftJustified());

        NotifierEquipage(centreAlerte, incident);
        journal.AjouterEntree(DateTime.Now,
                              "Centre d'alerte",
                              "Alertes",
                              $"Incident déclaré : " +
                              $"{incident.Decrire()}");
    }
}





