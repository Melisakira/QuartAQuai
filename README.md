## QuartAQuai

Simulation en console interactive (C#) d'un quart de nuit à quai à bord de la frégate `F911 Westdiep` (Force Navale Belge). Projet réalisé dans le cadre de l'examen de seconde session de Programmation Orientée Objet.

## Présentation

Le programme met en scène un équipage de garde qui assure le quart de nuit : chaque rôle a sa propre responsabilité et doit réagir à des incidents qui peuvent survenir. Le scénario se déroule à quai, dans le port de Zeebrugge, et met en avant les aspects de sécurité, sûreté et intervention d'urgence.

Le scénario s'appuie sur une expérience vécue d'électricienne de bord.

## Scénario

Cinq membres de l'équipage sont de quart, chacun à un poste précis et fixe, sauf un :

| Rôle | Poste | Se déplace ? |
|---|---|---|
| Officier de garde | Poste de garde | Non - centralise les alertes et coordonne les interventions |
| Électricien (technicien) | Local technique | Non - surveille les systèmes électriques au local technique |
| Mécanicien (technicien) | Salle des machines | Non - surveille les systèmes mécaniques au local machine |
| Veilleur de coupée | Coupée | Non - surveille l'extérieur du navire et les amarres |
| Rondeur de sécurité | Pont et locaux | Oui - Seul rôle mobile, se déplace pour vérifier la sûreté |

## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Qui l'exécute | Classe / méthode |
|---|---|---|---|
| 1 | Consulter le journal de bord, avec possibilité de consigner une prise de quart (nom, poste, heure de début/fin) avant consultation | tout membre (générique) | `JournalDeBord.AjouterEntree(...)` puis `JournalDeBord.ObtenirEntrees()` |
| 2 | Faire la ronde de sécurité (Coursives, Espaces logistiques et de stockage, Locaux de vie et d'habitation, Locaux techniques et propulsifs, Locaux de commandement et d'accès) | Rondeur de sécurité | `Navire.FaireRonde(RondeurSecurite, string, string)` |
| 3 | Déclarer un incident (panne électrique, avarie mécanique, alerte météo, incident de sûreté), avec escalade automatique si critique | déclenché par l'utilisateur ; tout l'équipage réagit ensuite automatiquement (Observer) | `CentreAlerte.Notifier(Incident)` |
| 4 | Assurer la veille (accès, état des amarres) | Veilleur de coupée | `VeilleurCoupee.SurveillerQuai(string, string)` — déclenche `ReagirAlerte()` via l'Observer si une menace est signalée |

Chaque fonctionnalité est indépendante, **mais non étanche**, dans sa finalité. Elles sont chacune accessible via le menu interactif, et peuvent être testées séparément. Toutefois, la fonctionnalité 3 (déclarer un incident) est le cœur du programme, car elle déclenche les réactions de l'équipage. Tandis que les fonctionnalités 2 et 4 peuvent, sur confirmation de l'utilisateur, enchaîner sur la déclaration d'un incident — sans jamais rendre "Déclarer un incident" dépendante d'être passé par l'un des deux avant. La fonctionnalité 1 combine désormais saisie (prise de quart) et consultation.

## Architecture - deux hiérarchies d'héritage

### `MembreEquipage` (hiérarchie principale, polymorphisme)

Classe abstraite `MembreEquipage` (nom, grade, poste affecté), qui implémente l'interface `IObservateur` et porte la méthode concrète `MettreAJour`. Elle contient une méthode abstraite `ReagirAlerte()`. Cinq rôles la spécialisent réellement, chacun avec sa propre réaction (selon son posteAffecté) :
`OfficierDeGarde`, `Electricien`, `Mecanicien`, `RondeurSecurite`, `VeilleurCoupee`.

Réaction du rôle à un incident : chaque rôle réagit différemment selon le type d'incident (panne électrique, avarie mécanique, alerte météo, incident de sûreté). La réaction est implémentée dans la méthode `ReagirAlerte()` de chaque sous-classe.
- **Cas particuliers** : l'`OfficierDeGarde` coordonne les interventions, le `RondeurSecurite` peut déclencher un incident mais ne traite pas l'incident lui-même, le `VeilleurCoupee` peut déclencher une alerte météo/incident de sûreté.

### `Incident` (hiérarchie secondaire)

Classe abstraite `Incident` (gravité, description) avec une méthode abstraite `Decrire()`.
Quatre types concrets : `PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`, `IncidentSurete`.

## Design pattern : Observer

**Problème** : quand un incident est déclaré au `CentreAlerte`, plusieurs membres d'équipage doivent réagir automatiquement, sans que le centre d'alerte connaisse à l'avance qui ils sont individuellement (couplage faible).

**Solution retenue** : pattern Observer

- `ISujet` : rôle du `CentreAlerte` (Abonner, Desabonner, Notifier)
- `IObservateur` : implémenté une seule fois par `MembreEquipage` (`MettreAJour`) - chaque sous-classe n'a besoin de redéfinir que `ReagirAlerte()`, seule chose qui varie réellement selon le rôle.
- `CentreAlerte` ne connaît que l'interface `IObservateur`, jamais les classes concrètes directement — ajouter un nouveau rôle observateur ne modifie jamais `CentreAlerte`.
- L'Officier de garde ne possède aucune action de menu qui lui soit propre : sa coordination se déclenche uniquement par la notification automatique de l'Observer.

## Organisation du code (namespaces)

| Namespace | Contenu |
|---|---|
| `QuartAQuai.Equipage` | `MembreEquipage` (abstraite) et ses 5 dérivées (`OfficierDeGarde`, `Electricien`, `Mecanicien`, `RondeurSecurite`, `VeilleurCoupee`) — implémente `IObservateur` |
| `QuartAQuai.Alertes` | `Incident` (abstraite) et ses 4 dérivées (`PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`, `IncidentSurete`), `IObservateur`, `ISujet`, `CentreAlerte` |
| `QuartAQuai.AQuai` | `Navire`, `JournalDeBord`, `EntreeJournal` |
| `QuartAQuai` (racine) | `Program` — point d'entrée, seule classe à dépendre de Spectre.Console |

## Remarque

Toutes les classes métier (`Equipage/`, `Alertes/`, `AQuai/`) utilisent uniquement `Console.WriteLine` : aucune ne dépend de Spectre.Console. Seul `Program.cs` utilise Spectre.Console pour l'affichage. Cela permet de tester les classes métier indépendamment de l'interface utilisateur.

## Prérequis

- .NET 10 SDK
- Package NuGet `Spectre.Console` (restauré automatiquement via `dotnet restore`)

## Exécution

- git clone https://github.com/Melisakira/QuartAQuai.git
- cd QuartAQuai
- dotnet run

## Diagramme UML

![Diagramme de classes](UML%20diagram.png)

Le fichier source PlantUML est disponible dans [`QuartAQuai_UML.puml`](QuartAQuai_UML.puml).