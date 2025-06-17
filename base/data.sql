INSERT INTO StatutSiege (libelle)
VALUES 
('libre'),
('reserve'),
('payer'),
('annuler');

INSERT INTO Classification (ageClassification)
VALUES 
    (0),   -- Tous publics
    (14);   -- À partir de 7 an

INSERT INTO MethodePayement (methode)
VALUES
    ('MVola'), -- MVola
    ('Orange Money'), -- MVola
    ('Airtel Money');-- MVola
    
INSERT INTO Categorie (typeCategorie)
VALUES 
    ('Action'), 
    ('Science-fiction'), 
    ('Comédie'), 
    ('Drame'), 
    ('Animation'), 
    ('Horreur'), 
    ('Thriller'), 
    ('Fantastique'), 
    ('Romance'), 
    ('Documentaire');
    
INSERT INTO Utilisateur (nom, email, dtn, pwd)
VALUES
    ('Anthony Herinantenaina', 'rantonirinaanthony@gmail.com', '2003-11-06','031013dter'),
    ('Bob Martin', 'bob.martin@example.com', '1985-11-22','031013dter');


-- Insertion des salles
INSERT INTO Salle (idSalle, nomSalle, ouverture, fermeture) 
VALUES
    ('S00001', 'Salle 1', '08:00:00', '22:00:00'),  -- Salle 1 avec 150 places
    ('S00002', 'Salle 2', '09:00:00', '23:00:00'),  -- Salle 2 avec 120 places
    ('S00003', 'Salle 3', '10:00:00', '20:00:00');  -- Salle 3 avec 200 places

-- Insertion de configurations de colonnes pour les salles
INSERT INTO ConfigurationColonnes (idSalle, colonne, hauteur, placesParLigne)
VALUES
    ('S00001', 1, 10, 10),  -- Salle 1, Colonne 1, 10 hauteurs, 10 places par ligne
    ('S00001', 2, 12, 10),  -- Salle 1, Colonne 2, 12 hauteurs, 10 places par ligne
    ('S00002', 1, 8, 8),    -- Salle 2, Colonne 1, 8 hauteurs, 8 places par ligne
    ('S00002', 2, 10, 12),  -- Salle 2, Colonne 2, 10 hauteurs, 12 places par ligne
    ('S00003', 1, 15, 12),  -- Salle 3, Colonne 1, 15 hauteurs, 12 places par ligne
    ('S00003', 2, 18, 10);  -- Salle 3, Colonne 2, 18 hauteurs, 10 places par ligne

-- Appel de la procédure pour une salle exemple
CALL genererSieges('S00001');
CALL genererSieges('S00002');
CALL genererSieges('S00003');

INSERT INTO Film (titre, idClassification, idCategorie, synopsis, duree)
VALUES 
    ('Inception', 1, 1, 'Un voleur qui manipule les rêves est engagé pour implanter une idée dans l’esprit d’un PDG.', '02:28:00'),
    ('Le Roi Lion', 2, 2, 'Le parcours initiatique de Simba, un lionceau destiné à devenir roi.', '01:58:00'),
    ('Matrix', 1, 1, 'Un pirate informatique découvre la vérité sur la réalité et mène une révolte contre les machines.', '02:16:00'),
    ('Titanic', 3, 3, 'Une romance tragique à bord du célèbre paquebot Titanic.', '03:15:00'),
    ('Avengers: Endgame', 1, 4, 'Les Avengers s’unissent pour affronter Thanos et sauver l’univers.', '03:01:00'),
    ('Coco', 2, 2, 'Un jeune garçon explore le monde des morts pour découvrir les secrets de sa famille.', '01:45:00'),
    ('Joker', 1, 5, 'L’histoire d’un comédien raté qui sombre dans la folie et devient un criminel notoire.', '02:02:00'),
    ('Les Indestructibles', 2, 2, 'Une famille de super-héros jongle entre leurs responsabilités et leur combat contre le crime.', '01:55:00');

INSERT INTO Diffusion (idSalle, idFilm, heureDebut, daty, paf)
VALUES
    ('S00001', 1, '14:00:00', '2024-12-10', 1500), -- Inception
    ('S00001', 2, '17:00:00', '2024-12-10', 2000), -- Le Roi Lion
    ('S00002', 4, '13:30:00', '2024-12-11', 800); -- Titanic



INSERT INTO Reservation (idUser, idDiffusion, idDetailSalle)
VALUES
    (1, 2, 'D00205'), -- Réservation pour l'utilisateur 1, diffusion 1, siège spécifique, statut "libre"
    (2, 2, 'D00185'), -- Réservation pour l'utilisateur 2, diffusion 2, siège spécifique, statut "réservé"
    (2, 2, 'D00170'), -- Réservation pour l'utilisateur 3, diffusion 3, siège spécifique, statut "hors service"
    (1, 2, 'D00138'), -- Réservation pour l'utilisateur 4, diffusion 1, siège spécifique, statut "libre"
    (1, 2, 'D00166'); -- Réservation pour l'utilisateur 5, diffusion 4, siège spécifique, statut "réservé"


SELECT * FROM Reservation;
SELECT * FROM Diffusion;
SELECT * FROM TempReservationUpdate;
SELECT * FROM DetailSalle;

