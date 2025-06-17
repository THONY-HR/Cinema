using ClassTable;
using Utilitaire;

namespace Function
{
    public class Fonction
    {
        protected BdConnect con;
        protected string nomTable;
        protected double duree;
        protected string key;
        public void DeleteAndGenerateSieges(string idSalle, ConfigurationColonnes col){
            try
            {
                con = new BdConnect();
                bool res = con.DeleteAndGenerateSieges(idSalle,col);
            }
            catch (System.Exception)
            {
                
            }
        }
        public bool Inserer()
        {
            con = new BdConnect();
            try
            {
                if(con.InsertObjectIntoTable(this.GetNomTable(), this)){
                    Console.WriteLine("Insertion réussie dans " + nomTable);
                    return true;  
                }else{
                    Console.WriteLine("Insertion Failed dans " + nomTable);
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erreur: Insertion: " + nomTable + " " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update()
        {
            con = new BdConnect();
            try
            {
                con.UpdateObjectInTable(this.GetNomTable(), this, this.GetKey());
                Console.WriteLine("Mise à jour réussie dans " + nomTable);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Erreur Update: " + nomTable + " " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public string GetLastInsert()
        {
            con = new BdConnect();
            try
            {
                return con.GetLastInsertIdFromTable(this.GetNomTable());
            }
            catch (Exception e)
            {
                throw new Exception("Erreur last Insert: " + nomTable + " " + e.Message);
            }
        }

        public T[] GetObject<T>() where T : new()
        {
            con = new BdConnect();
            try
            {
                return con.GetObjectFromTable<T>(this.GetNomTable());
            }
            catch (Exception e)
            {
                throw new Exception("Erreur Update: " + nomTable + " " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void SetKey(string keys)
        {
            this.key = keys;
        }

        public string GetKey()
        {
            return this.key;
        }

        public string GetNomTable()
        {
            return this.nomTable;
        }

        public double GetLongueurDuree()
        {
            return this.duree;
        }

        public void SetLongueurDuree(double duree)
        {
            this.duree = duree;
        }

        public void SetNomTable(string nomTable)
        {
            this.nomTable = nomTable;
        }

        public int ToInteger(string value)
        {
            return int.Parse(value);
        }

        public double ToDouble(string value)
        {
            return double.Parse(value);
        }

        public DateTime ToDate(string dateStr)
        {
            try
            {
                return DateTime.ParseExact(dateStr, "yyyy-MM-dd", null);
            }
            catch (Exception)
            {
                return DateTime.MinValue; // Ou gérez l'erreur différemment
            }
        }

        public DateTime? ToDateBase(string dateStr)
        {
            try
            {
                return DateTime.Parse(dateStr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ToYear(string yearString)
        {
            try
            {
                return int.Parse(yearString); // Convertit la chaîne en une année
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur : la chaîne '" + yearString + "' n'est pas une année valide.");
                return -1; // Ou gérez l'erreur différemment selon vos besoins
            }
        }

        public TimeSpan ToTime(string time)
        {
            if (time != null && System.Text.RegularExpressions.Regex.IsMatch(time, @"\d{2}:\d{2}"))
            {
                string timeWithSecond = time + ":00";
                return TimeSpan.Parse(timeWithSecond);
            }
            else
            {
                throw new ArgumentException("Le format de l'heure n'est pas valide");
            }
        }

        public string ToStringTime(TimeSpan time)
        {
            if (time == null)
            {
                return "erreur";
            }
            return time.ToString(@"hh\:mm\:ss");
        }

        public byte[] ConvertToBytes(Stream inputStream)
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                inputStream.CopyTo(buffer);
                return buffer.ToArray();
            }
        }
    }
}
