## QuartAQuai

Simulation en console interactive (c#) d'un quart de nuit à quai à bord de la frégate F911 Wesdiep (Force Navale Belge). Projet réalisé dans le cadre de l'examen de seconde session de Programmation Orientée Objet.

## Présentation

Le programme met en scène un équipage de garde qui assure le quart de nuit : chaque rôle a sa propre responsabilité et doit réagir à des incidents qui peuvent survenir. Le scénario se déroule à quai, dans le port de Zeebrugge, et met en avant les aspects de sécurité, sûreté et intervention d'urgence.

Le scénario s'appuie sur une expérience vécue d'électricienne de bord.

## Scénario

Cinq membres de l'équipage sont de quart, chacun à un poste précis et fixe, sauf un :

| Rôle | Poste | Se déplace ? |
|---|---|---|
| Officier de garde (ODG) | Poste de garde | Non - centralise les alertes et coordonne les interventions |
| Elecricien (technicien) | Local électrique | Non - surveille les systèmes électriques au local éléctrique |
| Mécanicien (technicien) | Salle des machines | Non - surveille les systèmes mécaniques au local machine |
| Veilleur de coupée | Coupée | Non - surveille l'extérieur du navire et les amarres |
| Rondeur de sécurité | Oui - Seul rôle mobile, se déplace sur le pont et dans les locaux pour vérifier la sûreté |


## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Qui l'exécute| Classe / méthode |
|---|---|---|---|
| 1 |  Consulter le journal d ebord (briefeing de relève) | tout membre (générique) | `JournalDeBord.ConsulterEntrees()` |
| 2 |  Faire la ronde de sécurité (coursives, soutes, cafétaria, locaux techniques) | Rondeur de sécurité | `Navire.FaireRonde(RondeurSécurite, string, string)`|
| 3 |  Déclarer un incident (panne électrique, avarie mécanique, alerte météo), avec escalade automatique si critique | déclenché par l'utilisateur ; tout l'équipage réagit ensuite automatiquement (Observer) | `CentreAlerte.DeclarerIncident(Incident)` |
| 4 | Assurer la veille (accès, état des amarres) | Veilleur de coupée | `VeilleurCoupée.SurveillerQuai(string, string)` |



## Architecture - deux hiérarchies d'héritage

### `MembreEquipage` (hiérarchie principale, polymorphisme)

Classe abstraite `MembreEquipage` (nom, grade, poste affecté), avec une méthode abstraite
`ReagirAlerte()`. Quatre rôles la spécialisent réellement, chacun avec sa propre réaction :
`Officier`, `Electricien`, `Mecanicien`, `Matelot`.

### `Incident` (hiérarchie secondaire)

Classe abstraite `Incident` (gravité, description) avec une méthode abstraite `Decrire()`.
Trois types concrets : `PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`.

## Design pattern : Observer

Problème : quand un incident est déclaré au CentreAlerte, plusieurs membres d'équipage doivent réagir automatiquement, sans que le centre d'alerte connaisse à l'avance qui ils sont individuellement (couplage faible).

Solution retenue : pattern Observer

ISujet : rôle du CentreAlerte (Abonner, Desabonner, Notifier)
IObservateur : implémenté par chaque sous-classe de MembreEquipage (MettreAJour)
CentreAlerte ne connaît que l'interface IObservateur, jamais les classes concrètes directement — ajouter un nouveau rôle observateur ne modifie jamais CentreAlerte.


## Remarque

Toutes les classes métier (`Equipage/`, `Alertes/`, `Navigation/`) utilisent uniquement `Console.WriteLine`: auncune ne dépend de Spectre.Console. Seul `Program.cs` utilise Spectre.Console pour l'affichage. Cela permet de tester les classes métier indépendamment de l'interface utilisateur.