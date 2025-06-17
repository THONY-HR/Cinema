using ClassTable;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utilitaire
{
    public class BdConnect
    {
        private MySqlConnection connexion; // Déclaré comme nullable

        // Constructeur
        public BdConnect()
        {
            try
            {
                // Utiliser DBUtil pour obtenir les détails de la connexion
                DBUtil dbUtil = new DBUtil();
                this.connexion = new MySqlConnection(dbUtil.GetConnectionString());
                this.connexion.Open();
                Console.WriteLine("Connexion réussie à la base de données.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de connexion à la base de données : " + e.Message);
            }
        }

        // Méthode pour récupérer la connexion
        public MySqlConnection? GetConnection()
        {
            return this.connexion;
        }

        // Méthode généralisée pour récupérer des objets à partir de n'importe quelle table
        public T[] GetObjectFromTable<T>(string tableName) where T : new()
        {
            List<T> results = new List<T>();
            try
            {
                string query = $"SELECT * FROM {tableName}";
                if (connexion == null)
                {
                    throw new InvalidOperationException("Connexion est nulle");
                }

                MySqlCommand command = new MySqlCommand(query, connexion);
                MySqlDataReader reader = command.ExecuteReader();

                // Obtenir les champs de la classe
                PropertyInfo[] properties = typeof(T).GetProperties();

                // Itérer sur les lignes du résultat
                while (reader.Read())
                {
                    T instance = new T();

                    // Assigner les valeurs du résultat aux propriétés de l'instance
                    foreach (PropertyInfo property in properties)
                    {
                        string propertyName = property.Name;
                        object value = reader[propertyName];

                        // Convertir BigDecimal en double si le champ cible est de type double
                        if (value is decimal && property.PropertyType == typeof(double))
                        {
                            value = Convert.ToDouble(value);
                        }

                        // Assigner la valeur convertie (ou non convertie) à la propriété
                        if (value != DBNull.Value)
                        {
                            property.SetValue(instance, value);
                        }
                    }

                    results.Add(instance);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'exécution de la requête SQL : " + e.Message);
            }
            return results.ToArray();
        }

        // Insérer un objet dans une table
        public Boolean InsertObjectIntoTable<T>(string tableName, T obj)
        {
            try
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();

                // Construire la partie des colonnes pour la requête SQL
                StringBuilder columns = new StringBuilder();
                StringBuilder values = new StringBuilder();

                // Construire les colonnes et les valeurs pour la requête
                foreach (PropertyInfo property in properties)
                {
                    columns.Append($"`{property.Name}`").Append(", ");
                    values.Append($"@{property.Name}, ");
                }

                // Supprimer la dernière virgule
                columns.Length -= 2;
                values.Length -= 2;

                string sql = $"INSERT INTO `{tableName}` ({columns}) VALUES ({values})";
                MySqlCommand command = new MySqlCommand(sql, connexion);

                // Assigner les valeurs des propriétés aux paramètres de la requête
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(obj);
                    command.Parameters.AddWithValue($"@{property.Name}", value ?? DBNull.Value);
                }

                // Exécuter l'insertion
                command.ExecuteNonQuery();
                Console.WriteLine("Insertion réussie.");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'insertion dans la base de données : " + e.Message);
                return false;
            }
        }


        // Mettre à jour un objet dans une table
        public void UpdateObjectInTable<T>(string tableName, T obj, string primaryKeyField)
        {
            try
            {
                // Obtenir les propriétés de l'objet
                PropertyInfo[] properties = obj.GetType().GetProperties();
                StringBuilder query = new StringBuilder($"UPDATE {tableName} SET ");
                List<object> nonNullValues = new List<object>();
                List<string> parameterNames = new List<string>();
                object primaryKeyValue = null;

                // Construire la partie "SET" de la requête
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(obj);

                    // Vérifier si la propriété correspond à la clé primaire
                    if (property.Name.Equals(primaryKeyField, StringComparison.OrdinalIgnoreCase))
                    {
                        primaryKeyValue = value;
                        continue; // Ne pas inclure la clé primaire dans les champs à mettre à jour
                    }

                    if (value != null)
                    {
                        string paramName = $"@{property.Name}";
                        query.Append($"{property.Name} = {paramName}, ");
                        nonNullValues.Add(value);
                        parameterNames.Add(paramName);
                    }
                }

                // Valider la clé primaire
                if (primaryKeyValue == null)
                {
                    Console.WriteLine("Erreur : la clé primaire ne peut pas être nulle.");
                    return;
                }

                // S'assurer qu'il y a des champs à mettre à jour
                if (nonNullValues.Count == 0)
                {
                    Console.WriteLine("Aucune propriété non nulle à mettre à jour.");
                    return;
                }

                // Retirer la dernière virgule et ajouter la clause WHERE
                query.Length -= 2; // Supprimer ", "
                query.Append($" WHERE {primaryKeyField} = @primaryKey");

                // Créer et configurer la commande MySQL
                MySqlCommand command = new MySqlCommand(query.ToString(), connexion);

                // Ajouter les paramètres des valeurs non nulles
                for (int i = 0; i < nonNullValues.Count; i++)
                {
                    command.Parameters.AddWithValue(parameterNames[i], nonNullValues[i]);
                }

                // Ajouter le paramètre pour la clé primaire
                command.Parameters.AddWithValue("@primaryKey", primaryKeyValue);

                // Exécuter la requête et afficher le résultat
                int affectedRows = command.ExecuteNonQuery();
                Console.WriteLine($"Mise à jour réussie, lignes affectées : {affectedRows}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la mise à jour : " + e.Message);
            }
        }


        // Récupérer l'ID du dernier enregistrement inséré
        public string GetLastInsertIdFromTable(string tableName)
        {
            string lastInsertId = "";
            try
            {
                string sql = $"SELECT LAST_INSERT_ID() AS lastId FROM {tableName}";
                MySqlCommand command = new MySqlCommand(sql, connexion);
                lastInsertId = command.ExecuteScalar()?.ToString() ?? string.Empty;
                Console.WriteLine($"Dernier ID inséré dans la table {tableName} : {lastInsertId}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la récupération du dernier ID inséré : " + e.Message);
            }
            finally
            {
                this.Close();
            }

            return lastInsertId;
        }

        // Récupérer une seule valeur via une requête
        public object? GetSingleValueFromQuery(string sql)
        {
            object? result = null;
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connexion);
                result = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'exécution de la requête SQL : " + e.Message);
            }
            finally
            {
                this.Close();
            }
            return result;
        }
        public bool DeleteAndGenerateSieges(string idSalle,ConfigurationColonnes config)
        {
            try
            {
                if (connexion == null)
                {
                    throw new InvalidOperationException("La connexion à la base de données n'est pas initialisée.");
                }

                // Supprimer les enregistrements existants dans DetailSalle pour la salle donnée
                string deleteQuery = "DELETE FROM DetailSalle WHERE idSalle = @idSalle";
                using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connexion))
                {
                    deleteCommand.Parameters.AddWithValue("@idSalle", idSalle);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} enregistrements supprimés dans DetailSalle pour la salle {idSalle}.");
                }

                config.SetNomTable("ConfigurationColonnes");
                config.Inserer();
                
                // Appeler la procédure stockée pour générer les sièges
                string callProcedureQuery = "CALL genererSieges(@idSalle)";
                using (MySqlCommand callCommand = new MySqlCommand(callProcedureQuery, connexion))
                {
                    callCommand.Parameters.AddWithValue("@idSalle", idSalle);
                    callCommand.ExecuteNonQuery();
                    Console.WriteLine($"Procédure stockée 'genererSieges' exécutée pour la salle {idSalle}.");
                }

                return true; // Indique que l'opération s'est terminée avec succès
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'exécution de la fonction DeleteAndGenerateSieges : " + ex.Message);
                return false; // Indique une erreur
            }
        }

        // Fermer la connexion
        public void Close()
        {
            try
            {
                if (connexion != null && connexion.State != System.Data.ConnectionState.Closed)
                {
                    connexion.Close();
                    Console.WriteLine("Connexion fermée.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la fermeture de la connexion : " + e.Message);
            }
        }
    }
}
