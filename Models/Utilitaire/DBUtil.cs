namespace Utilitaire
{
    public class DBUtil
    {
        public string GetUrl()
        {
            return "Server=localhost;Database=cinema;";
        }

        public string GetUser()
        {
            return "root";
        }

        public string GetPassword()
        {
            return "root";
        }

        // Méthode pour construire la chaîne de connexion complète
        public string GetConnectionString()
        {
            return $"{GetUrl()}User Id={GetUser()};Password={GetPassword()};";
        }
    }
}
