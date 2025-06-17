function getRandomImage() {
    const randomNumber = Math.floor(Math.random() * 23) + 1;
    return `../image/poster/${randomNumber}.jpg`;
}

// Récupérer toutes les images avec la classe 'movie'
const movies = document.querySelectorAll('.movie');

// Appliquer une image aléatoire à chaque image
movies.forEach(movie => {
    movie.src = getRandomImage();
});

document.addEventListener('DOMContentLoaded', function() {
    // Ajout de l'événement au clic sur l'image du profil utilisateur
    document.getElementById('user-profile').addEventListener('click', function() {
        var popup = document.getElementById('user-popup');
        popup.classList.toggle('show'); // Afficher ou masquer la pop-up avec la classe 'show'
    });

    // Gestion du bouton de déconnexion
    document.getElementById('logout-btn').addEventListener('click', function() {
        window.location.href = '/Requete/Logout'; // Rediriger vers la déconnexion
    });
});

