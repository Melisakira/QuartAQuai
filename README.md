## QuartAQuai

Simulation en console interactive (c#) d'un quart de nuit à quai à bord de la frégate F911 Wesdiep (Force Navale Belge). Projet réalisé dans le cadre de l'examen de seconde session de Programmation Orientée Objet.

## Présentation

Le programme met en scène un équipage de garde qui assure le quart de nuit : chaque rôle a sa propre responsabilité et doit réagir à des incidents qui peuvent survenir. Le scénario se déroule à quai, dans le port de Zeebrugge, et met en avant les aspects de sécurité, sûreté et intervention d'urgence.

Le scénario s'appuie sur une expérience vécue d'électricienne de bord.

## Scénario

Cinq membres de l'équipage sont de quart, chacun à un poste précis et fixe, sauf un :

| Rôle | Poste | Se déplace ? |
|---|---|---|
| Officier de garde | Poste de garde | Non - centralise les alertes et coordonne les interventions |
| Elecricien (technicien) | Local électrique | Non - surveille les systèmes électriques au local éléctrique |
| Mécanicien (technicien) | Salle des machines | Non - surveille les systèmes mécaniques au local machine |
| Veilleur de coupée | Coupée | Non - surveille l'extérieur du navire et les amarres |
| Rondeur de sécurité | Oui - Seul rôle mobile, se déplace sur le pont et dans les locaux pour vérifier la sûreté |

## Fonctionnalités (4, indépendantes)

| # | Fonctionnalité | Qui l'exécute| Classe / méthode |
|---|---|---|---|
| 1 |  Consulter le journal de bord (briefing de relève) | tout membre (générique) | `JournalDeBord.ConsulterEntrees()` |
| 2 |  Faire la ronde de sécurité (Coursives, Espaces logistiques et de stockage,Locaux de vie et d'habitation, Locaux techniques et propulsifs,Locaux de commandement et d'accès)| Rondeur de sécurité | `Navire.FaireRonde(RondeurSécurite, string, string)`|            
| 3 |  Déclarer un incident (panne électrique, avarie mécanique, alerte météo, incident de sûreté), avec escalade automatique si critique | déclenché par l'utilisateur ; tout l'équipage réagit ensuite automatiquement (Observer) | `CentreAlerte.Notifier(Incident)` |
| 4 | Assurer la veille (accès, état des amarres) | Veilleur de coupée | `VeilleurCoupée.ReagirAlerte(incident)`|

Chaque fonctionnalités sont indépendantes, **mais non étanches**, dans leur finalités. Elles sont chacune accessible via le menu interactif, et peuvent être testées séparément. Toutefois, la fonctionnalité 3 (déclarer un incident) est le cœur du programme, car elle déclenche les réactions de l'équipage. Tandis que les fonctionnalités 2 et 4 peuvent, sur confirmation de l'utilisateur, enchaîner sur la déclaration d'un incident - sans jamais rendre "Déclarer un incident" dépendante d'être passé par l'un des deux avant. La fonctionnalité 1 est purement informative.

## Architecture - deux hiérarchies d'héritage

### `MembreEquipage` (hiérarchie principale, polymorphisme)

Classe abstraite `MembreEquipage` (nom, grade, poste affecté), qui implémente l'interface `IObservateur` et porte la méthode concrète `MettreAJour`. Elle contient une méthode abstraite `ReagirAlerte()`. Cinq rôles la spécialisent réellement, chacun avec sa propre réaction (selon son posteAffecté) :
`OfficierDeGarde`, `Electricien`, `Mecanicien`, `RondeurDeSecurite`, `VeilleurDeCoupée`.

Réaction du rôle à un incident : chaque rôle réagit différemment selon le type d'incident (panne électrique, avarie mécanique, alerte météo, incident de sûreté). La réaction est implémentée dans la méthode `ReagirAlerte()` de chaque sous-classe. 
- **Cas particuliers** : l'OfficierDeGarde coordonne les interventions, le RondeurDeSecurite peut déclencher un incident mais ne traite pas l'incident lui-même, le VeilleurDeCoupée peut déclencher une alerte météo/incident de sûreté.

### `Incident` (hiérarchie secondaire)

Classe abstraite `Incident` (gravité, description) avec une méthode abstraite `Decrire()`.
Quatre types concrets : `PanneElectrique`, `AvarieMecanique`, `AlerteMeteo`, `IncidentDeSûreté`.

## Design pattern : Observer

**Problème** : quand un incident est déclaré au `CentreAlerte`, plusieurs membres d'équipage doivent réagir automatiquement, sans que le centre d'alerte connaisse à l'avance qui ils sont individuellement (couplage faible).

**Solution retenue** : pattern Observer

- `ISujet` : rôle du CentreAlerte (Abonner, Desabonner, Notifier)
- `IObservateur`: implémenté une seule fois par  `MembreEquipage`(`MettreAJour`) - chaque sous-classe n'a besoin de redéfinir que `ReagirAlerte()`. seule chose qui varie réellement selon le rôle.
- `CentreAlerte` ne connaît que l'interface `IObservateur`, jamais les classes concrètes directement — ajouter un nouveau rôle observateur ne modifie jamais `CentreAlerte`.
- L'Officier de garde ne possède aucune action de menu qui lui soit propre : sa coordination se déclenche uniquement par la notification automatique de l'Observer.

## Organisation du code (namespaces)

| Namespaces | Contenu |
|---|---|
| `QuartAQuai.Equipage`| `MembreEquipage`| et ses 5 dérivées |


## Remarque

Toutes les classes métier (`Equipage/`, `Alertes/`, `AQuai/`) utilisent uniquement `Console.WriteLine`: auncune ne dépend de Spectre.Console. Seul `Program.cs` utilise Spectre.Console pour l'affichage. Cela permet de tester les classes métier indépendamment de l'interface utilisateur.