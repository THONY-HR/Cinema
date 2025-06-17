DROP DATABASE IF EXISTS cinema;
CREATE DATABASE cinema;
USE cinema;

-- Table des utilisateurs
CREATE TABLE Utilisateur (
    idUser INT PRIMARY KEY AUTO_INCREMENT,
    nom VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    pwd VARCHAR(20) NOT NULL,
    dtn DATE NOT NULL
);

-- Table pour la classification des films
CREATE TABLE Classification (
    idClassification INT PRIMARY KEY AUTO_INCREMENT,
    ageClassification INT NOT NULL
);

-- Table pour les catégories des films
CREATE TABLE Categorie (
    idCategorie INT PRIMARY KEY AUTO_INCREMENT,
    typeCategorie VARCHAR(100) NOT NULL
);

-- Table des films
CREATE TABLE Film (
    idFilm INT PRIMARY KEY AUTO_INCREMENT,
    titre VARCHAR(150) NOT NULL,
    idClassification INT NOT NULL,
    idCategorie INT NOT NULL,
    synopsis TEXT,
    prixDiffusion double,
    duree TIME NOT NULL, -- Durée du film
    FOREIGN KEY (idClassification) REFERENCES Classification(idClassification) ON DELETE CASCADE,
    FOREIGN KEY (idCategorie) REFERENCES Categorie(idCategorie) ON DELETE CASCADE
);

-- Table des salles
CREATE TABLE Salle (
    idSalle VARCHAR(20) PRIMARY KEY,
    nomSalle VARCHAR(50) NOT NULL,
    ouverture TIME NOT NULL,
    fermeture TIME NOT NULL
);

-- Table pour configurer les colonnes
CREATE TABLE ConfigurationColonnes (
    idConfig VARCHAR(20) PRIMARY KEY,
    idSalle VARCHAR(20) NOT NULL,
    colonne INT NOT NULL,
    hauteur INT NOT NULL,
    placesParLigne INT NOT NULL,
    FOREIGN KEY (idSalle) REFERENCES Salle(idSalle) ON DELETE CASCADE
);

-- Table des statuts des sièges
CREATE TABLE StatutSiege (
    idStatut INT PRIMARY KEY AUTO_INCREMENT,
    libelle VARCHAR(50) NOT NULL UNIQUE -- Exemple : 'libre', 'reserve', 'hors_service'
);

-- Table pour gérer les sièges dans une salle
CREATE TABLE DetailSalle (
    idDetailSalle VARCHAR(20) PRIMARY KEY,
    idSalle VARCHAR(20) NOT NULL,
    colonne INT NOT NULL,
    ligne INT NOT NULL,
    position INT NOT NULL,
    FOREIGN KEY (idSalle) REFERENCES Salle(idSalle) ON DELETE CASCADE
);

-- Table des diffusions
CREATE TABLE Diffusion (
    idDiffusion INT PRIMARY KEY AUTO_INCREMENT,
    idSalle VARCHAR(20) NOT NULL,
    idFilm INT NOT NULL,
    heureDebut TIME NOT NULL,
    heureFin TIME NOT NULL, -- Calculée en fonction de la durée du film
    paf double,
    daty Date,
    FOREIGN KEY (idSalle) REFERENCES Salle(idSalle) ON DELETE CASCADE,
    FOREIGN KEY (idFilm) REFERENCES Film(idFilm) ON DELETE CASCADE
);

CREATE TABLE MethodePayement (
    idMethodePayement INT PRIMARY KEY AUTO_INCREMENT,
    methode VARCHAR(50)
);

CREATE TABLE Reservation (
    idReservation INT PRIMARY KEY AUTO_INCREMENT,
    idUser INT NOT NULL,
    idDiffusion INT NOT NULL,
    idDetailSalle VARCHAR(20),
    idStatut INT DEFAULT 2, -- Valeur par défaut est le statut 'réservé' (idStatut = 2)
    datyReservation DATETIME DEFAULT CURRENT_TIMESTAMP, -- Date et heure d'insertion par défaut
    FOREIGN KEY (idUser) REFERENCES Utilisateur(idUser) ON DELETE CASCADE,
    FOREIGN KEY (idDiffusion) REFERENCES Diffusion(idDiffusion) ON DELETE CASCADE,
    FOREIGN KEY (idDetailSalle) REFERENCES DetailSalle(idDetailSalle) ON DELETE CASCADE,
    FOREIGN KEY (idStatut) REFERENCES StatutSiege(idStatut) ON DELETE CASCADE
);


CREATE TABLE Payement(
    idPayement INT PRIMARY KEY AUTO_INCREMENT,
    idDiffusion INT,
    idReservation INT,
    idMethodePayement INT NOT NULL,
    paf double,
    FOREIGN KEY (idMethodePayement) REFERENCES MethodePayement(idMethodePayement) ON DELETE CASCADE,
    FOREIGN KEY (idDiffusion) REFERENCES Diffusion(idDiffusion) ON DELETE CASCADE,
    FOREIGN KEY (idReservation) REFERENCES Reservation(idReservation) ON DELETE CASCADE
);

-- triger

-- Trigger pour générer un ID alphanumérique pour les tables
DELIMITER //
CREATE TRIGGER generate_salle_id
BEFORE INSERT ON Salle
FOR EACH ROW
BEGIN
    SET NEW.idSalle = CONCAT('S', LPAD(CAST((SELECT COUNT(*) + 1 FROM Salle) AS CHAR), 5, '0'));
END //
DELIMITER ;

DELIMITER //
CREATE TRIGGER generate_config_colonnes_id
BEFORE INSERT ON ConfigurationColonnes
FOR EACH ROW
BEGIN
    SET NEW.idConfig = CONCAT('CONFIG', LPAD(CAST((SELECT COUNT(*) + 1 FROM ConfigurationColonnes) AS CHAR), 5, '0'));
END //
DELIMITER ;

DELIMITER //
CREATE TRIGGER generate_detail_salle_id
BEFORE INSERT ON DetailSalle
FOR EACH ROW
BEGIN
    SET NEW.idDetailSalle = CONCAT('D', LPAD(CAST((SELECT COUNT(*) + 1 FROM DetailSalle) AS CHAR), 5, '0'));
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS genererSieges;
DELIMITER //

CREATE PROCEDURE genererSieges(idSalleParam VARCHAR(20))
BEGIN
    DECLARE col INT; -- Numéro de colonne
    DECLARE h INT; -- Hauteur (nombre de lignes dans la colonne)
    DECLARE places INT; -- Nombre de places par ligne
    DECLARE i INT; -- Index pour les lignes
    DECLARE j INT; -- Index pour les places
    DECLARE done INT DEFAULT 0;

    -- Récupérer les colonnes et leurs configurations
    DECLARE cur CURSOR FOR
        SELECT colonne, hauteur, placesParLigne 
        FROM ConfigurationColonnes 
        WHERE idSalle = idSalleParam;

    -- Handler pour gérer la fin du curseur
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    -- Ouvrir le curseur
    OPEN cur;

    boucle: LOOP
        FETCH cur INTO col, h, places;
        IF done THEN
            LEAVE boucle;
        END IF;

        -- Générer les sièges pour chaque configuration
        SET i = 1;
        WHILE i <= h DO -- Parcourir les lignes
            SET j = 1;
            WHILE j <= places DO -- Parcourir les places sur chaque ligne
                -- Insérer un siège avec un ID unique
                INSERT INTO DetailSalle (idDetailSalle, idSalle, colonne, ligne, position)
                VALUES (
                    CONCAT('D', idSalleParam, '_', col, '_', i, '_', j), -- ID unique
                    idSalleParam,
                    col,
                    i,
                    j
                );
                SET j = j + 1;
            END WHILE;
            SET i = i + 1;
        END WHILE;
    END LOOP;

    -- Fermer le curseur
    CLOSE cur;
END //

DELIMITER ;

/*les vue*/
CREATE OR REPLACE VIEW FilmDetails AS
SELECT 
    f.idFilm,               -- Identifiant du film
    f.titre,                -- Titre du film
    f.synopsis,             -- Synopsis du film
    f.duree,                -- Durée du film
    f.idClassification,     -- Identifiant de la classification
    c.ageClassification,    -- Âge de classification
    f.idCategorie,          -- Identifiant de la catégorie
    cat.typeCategorie       -- Type de catégorie
FROM 
    Film f
JOIN 
    Classification c ON f.idClassification = c.idClassification
JOIN 
    Categorie cat ON f.idCategorie = cat.idCategorie;


CREATE OR REPLACE VIEW FilmBenefices AS
SELECT 
    f.idFilm,
    f.titre,
    f.prixDiffusion,
    COUNT(r.idReservation) AS nombreDeRegard,
    (COUNT(r.idReservation) * d.paf) AS totalObtenu,
    ((COUNT(r.idReservation) * d.paf) - f.prixDiffusion) AS benefice
FROM 
    Film f
LEFT JOIN 
    Diffusion d ON f.idFilm = d.idFilm
LEFT JOIN 
    Reservation r ON d.idDiffusion = r.idDiffusion
GROUP BY 
    f.idFilm, f.prixDiffusion, d.paf;


-- benefice = prixDiffusion - tatalObtenuDuSommeParFilm

-- IdFilm PrixDiffusion nombredeRegard benefice

CREATE  OR REPLACE VIEW ReservationsParJour AS
SELECT
    D.idSalle,
    D.daty AS jour,
    COUNT(R.idReservation) AS nombreReservations,
    D.paf * COUNT(R.idReservation) AS montantTotal
FROM
    Diffusion D
JOIN
    Reservation R ON D.idDiffusion = R.idDiffusion
WHERE
    R.idStatut = 3
GROUP BY
    D.idSalle, D.daty;


CREATE OR REPLACE VIEW LesDiffusions AS
SELECT 
    d.idDiffusion,                         -- ID de la diffusion
    d.idSalle,                              -- ID de la salle
    s.nomSalle,                             -- Nom de la salle
    d.heureDebut,  
    d.paf,                         -- Heure de début de la diffusion
    d.heureFin,                             -- Heure de fin de la diffusion
    d.daty,                                 -- Date de la diffusion
    f.idFilm,                               -- ID du film
    f.titre AS filmTitre,                   -- Titre du film (provenant de la vue FilmDetails)
    f.synopsis,                             -- Synopsis du film (provenant de la vue FilmDetails)
    f.duree AS filmDuree,                   -- Durée du film (provenant de la vue FilmDetails)
    f.idClassification,                     -- ID de la classification du film (provenant de la vue FilmDetails)
    f.ageClassification,                    -- Age de classification du film (provenant de la vue FilmDetails)
    f.idCategorie,                          -- ID de la catégorie du film (provenant de la vue FilmDetails)
    f.typeCategorie,                        -- Type de catégorie du film (provenant de la vue FilmDetails)
    s.ouverture AS salleOuverture,          -- Heure d'ouverture de la salle
    s.fermeture AS salleFermeture           -- Heure de fermeture de la salle
FROM 
    Diffusion d
JOIN 
    FilmDetails f ON d.idFilm = f.idFilm   -- Joindre avec la vue FilmDetails
JOIN 
    Salle s ON d.idSalle = s.idSalle       -- Jointure avec la table Salle
;


CREATE OR REPLACE VIEW ReservationDetails AS
SELECT 
    r.idReservation,                        -- ID de la réservation
    r.idUser,                               -- ID de l'utilisateur
    r.idDiffusion,                          -- ID de la diffusion
    r.idDetailSalle,                        -- ID du détail de la salle
    r.idStatut,                             -- Statut de la réservation
    r.datyReservation,                      -- Date de la réservation
    ld.idSalle,                             -- ID de la salle
    ld.nomSalle,                            -- Nom de la salle
    ld.heureDebut AS diffusionHeureDebut,   -- Heure de début de la diffusion
    ld.heureFin AS diffusionHeureFin,       -- Heure de fin de la diffusion
    ld.daty AS diffusionDate,               -- Date de la diffusion
    ld.filmTitre AS filmTitre,              -- Titre du film
    ld.synopsis AS filmSynopsis,            -- Synopsis du film
    ld.filmDuree AS filmDuree,              -- Durée du film
    ld.ageClassification AS filmAge,        -- Classification par âge du film
    ld.typeCategorie AS filmCategorie,      -- Catégorie du film
    ld.salleOuverture AS salleOuverture,    -- Heure d'ouverture de la salle
    ld.salleFermeture AS salleFermeture     -- Heure de fermeture de la salle
FROM 
    Reservation r
JOIN 
    LesDiffusions ld ON r.idDiffusion = ld.idDiffusion
;


CREATE OR REPLACE VIEW Vue_Places_Diffusion AS
SELECT 
    D.idDiffusion,
    D.daty AS dateDiffusion,
    D.heureDebut,
    D.heureFin,
    S.idSalle,
    S.nomSalle,
    F.titre AS titreFilm,
    DS.colonne,
    DS.ligne,
    DS.position,
    DS.idDetailSalle,
    COALESCE(R.idStatut, (SELECT idStatut FROM StatutSiege WHERE libelle = 'libre')) AS idStatut,
    COALESCE(SSt.libelle, 'libre') AS statutSiege
FROM 
    Diffusion D
JOIN 
    Salle S ON D.idSalle = S.idSalle
JOIN 
    Film F ON D.idFilm = F.idFilm
JOIN 
    DetailSalle DS ON DS.idSalle = S.idSalle
LEFT JOIN 
    Reservation R ON R.idDetailSalle = DS.idDetailSalle
    AND R.idDiffusion = D.idDiffusion
    AND R.datyReservation = (
        SELECT MAX(R2.datyReservation)
        FROM Reservation R2
        WHERE R2.idDetailSalle = R.idDetailSalle
        AND R2.idDiffusion = R.idDiffusion
    )
LEFT JOIN 
    StatutSiege SSt ON SSt.idStatut = R.idStatut;


DELIMITER //

CREATE TRIGGER before_insert_diffusion
BEFORE INSERT ON Diffusion
FOR EACH ROW
BEGIN
    DECLARE dureeFilm TIME;
    
    -- Récupérer la durée du film
    SELECT duree 
    INTO dureeFilm
    FROM Film
    WHERE idFilm = NEW.idFilm;

    -- Calculer l'heure de fin
    SET NEW.heureFin = ADDTIME(NEW.heureDebut, dureeFilm);
END;
//

DELIMITER ;

-- Création de la table ConfigurationDuree
CREATE TABLE ConfigurationDuree (
    idConfig INT PRIMARY KEY AUTO_INCREMENT,
    dureeEnMinutes INT NOT NULL DEFAULT 1 -- Valeur par défaut de 1 minute
);

-- Insertion d'une durée de 2 minutes
INSERT INTO ConfigurationDuree (dureeEnMinutes)
VALUES (60);  -- Valeur par défaut, ici 2 minutes

-- Création de la table TempReservationUpdate
CREATE TABLE TempReservationUpdate (
    idReservation INT,
    datyReservation DATETIME,
    PRIMARY KEY (idReservation)
);

-- Changer le DELIMITER pour la création du trigger
DELIMITER //

-- Trigger après l'insertion dans la table Reservation
CREATE TRIGGER update_statut_after_duree
AFTER INSERT ON Reservation
FOR EACH ROW
BEGIN
    DECLARE duree INT;
    
    -- Vérifier si la diffusion existe dans la table Diffusion
    IF EXISTS (SELECT 1 FROM Diffusion WHERE idDiffusion = NEW.idDiffusion) THEN
    
        -- Récupérer la durée depuis la table de configuration
        SELECT dureeEnMinutes INTO duree FROM ConfigurationDuree WHERE idConfig = 1;

        -- Ajouter l'ID de la réservation à la table TempReservationUpdate
        INSERT INTO TempReservationUpdate (idReservation, datyReservation)
        VALUES (NEW.idReservation, NEW.datyReservation);
        
    END IF;
END //

-- Revenir à la valeur par défaut du DELIMITER
DELIMITER ;

-- Activer l'Event Scheduler (si ce n'est pas déjà fait)
SET GLOBAL event_scheduler = ON;

-- Changer le DELIMITER pour la création de l'événement
DELIMITER //

-- Création de l'événement qui met à jour le statut des réservations
CREATE EVENT update_statut_event
ON SCHEDULE EVERY 1 MINUTE
DO
BEGIN
    DECLARE duree INT;
    
    -- Récupérer la durée depuis la table de configuration
    SELECT dureeEnMinutes INTO duree FROM ConfigurationDuree WHERE idConfig = 1;

    -- Mettre à jour les réservations dans TempReservationUpdate si le temps écoulé est supérieur ou égal à la durée
    UPDATE Reservation r
    JOIN TempReservationUpdate t ON r.idReservation = t.idReservation
    SET r.idStatut = 4  -- Mettre le statut à "Annulé" (idStatut = 4)
    WHERE TIMESTAMPDIFF(MINUTE, t.datyReservation, NOW()) >= duree
    AND r.idStatut = 2;  -- Vérifie si le statut est encore "Réservé" (idStatut = 2)

    -- Supprimer les réservations traitées de TempReservationUpdate
    DELETE FROM TempReservationUpdate WHERE TIMESTAMPDIFF(MINUTE, datyReservation, NOW()) >= duree;
    
END //

-- Revenir à la valeur par défaut du DELIMITER
DELIMITER ;




