namespace QuartEnMer
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
            string choix = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(" Que souhaitez-vous faire ?").AddChoices("1. Consulter le journal de bord", "2. Faire la ronde de sécurité", "3. Assurer la veille à la coupée", "4. Déclarer un incident", "0. Terminer le quart"));

            switch (choix)
            {
                case "1. Consulter le journal de bord":
                    {
                        AnsiConsole.Write(new Rule("[bold blue]Journal de bord[/]\n").LeftJustified());

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
                        break;
                    }
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
    static void FaireRonde(
        Navire navire,
        RondeurSecurite rondeur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AnsiConsole.Write(new Rule("[bold blue]Ronde de sécurité[/]\n").LeftJustified());

        foreach (string compartiment in navire.Compartiments)
        {
            string observation = AnsiConsole.Ask<string>($"Observation à {compartiment} (RAS ou description de l'anomalie) :");

            Navire.FaireRonde(rondeur, compartiment, observation);
            journal.AjouterEntree(DateTime.Now,
                                  rondeur.Nom,
                                  rondeur.PosteAffecte,
                                  $"Ronde en {compartiment} - {observation}");

            bool autreAnomalie = AnsiConsole.Confirm($"Anomalie à signaler suite à la ronde de {compartiment} ?", false);
            while (autreAnomalie)
            {
                string typeAnomalie = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Type d'anomalie constatée ?").AddChoices("Panne électrique", "Avarie mécanique"));

                string gravite = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Gravité de l'anomalie ?").AddChoices("mineur", "majeur", "critique"));

                string descriptionAnomalie = AnsiConsole.Ask<string>("Description de cette anomalie :");

                Incident incident;
                if (typeAnomalie == "Panne électrique")
                {
                    string equipement = AnsiConsole.Ask<string>("Équipement concerné :");
                    incident = new PanneElectrique(gravite, descriptionAnomalie, equipement);
                }
                else
                {
                    string equipement = AnsiConsole.Ask<string>("Équipement concerné :");
                    incident = new AvarieMecanique(gravite, descriptionAnomalie, equipement);
                }
                string couleur = CouleurGravite(gravite);
                AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]\n");
                AnsiConsole.Write(new Rule("[DarkKhaki]Réactions de l'équipage[/]\n").LeftJustified());
                centreAlerte.Notifier(incident);
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
            _ => "white",
        };

    }
    static void AssurerVeille(
        VeilleurCoupee veilleur,
        JournalDeBord journal,
        CentreAlerte centreAlerte)
    {
        AnsiConsole.Write(new Rule("[bold blue]Veille à la coupée[/]\n").LeftJustified());

        string etatAmarres = AnsiConsole.Ask<string>("État des amarres :");
        string observation = AnsiConsole.Ask<string>("Observation à la coupée (RAS ou Description) :");

        veilleur.SurveillerQuai(etatAmarres, observation);
        journal.AjouterEntree(DateTime.Now,
                              veilleur.Nom,
                              veilleur.PosteAffecte,
                              $"Veille à la coupée - amarres {etatAmarres} - {observation}");

        bool autreMenace = AnsiConsole.Confirm("Cette observation nécessite-t-elle de déclarer une menace ?", false);
        while (autreMenace)
        {
            string type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Type de menace ?").AddChoices("Incident sûreté", "Alerte météo"));

            string gravite = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Gravité de la menace ?").AddChoices("mineur", "majeur", "critique"));

            string descriptionIncident = AnsiConsole.Ask<string>("Description de la menace :");

            Incident incident;
            if (type == "Incident sûreté")
            {
                string menace = AnsiConsole.Ask<string>("Nature de la menace :");
                incident = new IncidentSurete(gravite, descriptionIncident, menace);
            }
            else
            {
                string phenomene = AnsiConsole.Ask<string>("Phénomène observé :");
                incident = new AlerteMeteo(gravite, descriptionIncident, phenomene);
            }

            string couleur = CouleurGravite(gravite);
            AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]\n");
            AnsiConsole.Write(new Rule("[DarkKhaki]Réactions de l'équipage[/]\n").LeftJustified());
            centreAlerte.Notifier(incident);
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
        AnsiConsole.Write(new Rule("[bold blue]Déclaration d'incident[/]\n").LeftJustified());
        string type = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Quel type d'incident déclarer ?").AddChoices("Panne électrique", "Avarie mécanique", "Alerte météo", "Incident sûreté"));

        string gravite = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Gravité de l'incident ?").AddChoices("mineur", "majeur", "critique"));

        string description = AnsiConsole.Ask<string>("Description de l'incident :");

        Incident incident = type switch
        {
            "Panne électrique" => new PanneElectrique(gravite, description, AnsiConsole.Ask<string>("Équipement concerné :")),
            "Avarie mécanique" => new AvarieMecanique(gravite, description, AnsiConsole.Ask<string>("Équipement concerné :")),
            "Alerte météo" => new AlerteMeteo(gravite, description, AnsiConsole.Ask<string>("Phénomène observé :")),
            "Incident sûreté" => new IncidentSurete(gravite, description, AnsiConsole.Ask<string>("Nature de la menace :")),
            _ => throw new InvalidOperationException($"Type d'incident invalide : {type}")
        };

        string couleur = CouleurGravite(gravite);
        AnsiConsole.MarkupLine($"[{couleur}]--- Incident déclaré : {Markup.Escape(incident.Decrire())} ---[/]\n");
        AnsiConsole.Write(new Rule("[DarkKhaki]Réaction de l'équipage[/]\n").LeftJustified());

        centreAlerte.Notifier(incident);
        journal.AjouterEntree(DateTime.Now,
                              "Centre d'alerte",
                              "Alertes",
                              $"Incident déclaré : " +
                              $"{incident.Decrire()}");
    }
}





