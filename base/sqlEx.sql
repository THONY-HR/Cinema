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
LEFT JOIN 
    StatutSiege SSt ON SSt.idStatut = R.idStatut;