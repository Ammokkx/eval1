using LegoBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

public class LegoRepository : ILegoRepository
{
    private string connectionString;

    public LegoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public LegoTheme GetLegoTheme(string name)
    {
        /* een beetje commentaar voor branch ABC */
        LegoTheme data;
        string SQL = "SELECT * FROM LegoTheme WHERE name like @voorwaarde";
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = conn.CreateCommand())
        {
            conn.Open();
            cmd.CommandText = SQL;
            cmd.Parameters.AddWithValue("@voorwaarde", $"%{name}%");
            using (IDataReader dr = cmd.ExecuteReader())
            {
                dr.Read();
                data = new LegoTheme((int)dr["id"], (string)dr["name"]);
                dr.Close();
            }
            
        }
        return data;
    }

    //init db
    public void WriteLegoThemes(List<LegoTheme> legoThemes)
    {
        string SQLthemes = "INSERT INTO LegoTheme(name) output INSERTED.ID VALUES (@naam)";
        string SQLsets = "INSERT INTO LegoSet(id, name, year, pieces, minifigs, minage, imageURL, retailprice, themeId) output (@id, @name, @year, @pieces, @minifigs, @minage, @imageURL, @retailprice, @themeID)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmdThemes = conn.CreateCommand())
        using (SqlCommand cmdSets = conn.CreateCommand())
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmdThemes.Transaction = tran;
            cmdSets.Transaction = tran;
            cmdThemes.CommandText = SQLthemes;
            cmdSets.CommandText = SQLsets;

            cmdThemes.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));

            cmdSets.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar));
            cmdSets.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            cmdSets.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            cmdSets.Parameters.Add(new SqlParameter("@pieces", SqlDbType.Int));
            cmdSets.Parameters.Add(new SqlParameter("@minifigs", SqlDbType.Int));
            cmdSets.Parameters.Add(new SqlParameter("@minage", SqlDbType.Int));
            cmdSets.Parameters.Add(new SqlParameter("@imageURL", SqlDbType.NVarChar));
            cmdSets.Parameters.Add(new SqlParameter("@retailprice", SqlDbType.Float));
            cmdSets.Parameters.Add(new SqlParameter("@themeID", SqlDbType.Int));

            int themeID;

            try
            {
                foreach (LegoTheme var in legoThemes)
                {
                    cmdThemes.Parameters["@name"].Value = var.Name;
                    themeID = (int)cmdThemes.ExecuteScalar();
                    cmdSets.Parameters["@themeID"].Value = themeID;

                    foreach (LegoSet var2 in var.LegoSets)
                    {
                        cmdSets.Parameters["@id"].Value = var2.Id;
                        cmdSets.Parameters["@name"].Value = var2.Name;
                        cmdSets.Parameters["@year"].Value = var2.Year;
                        cmdSets.Parameters["@pieces"].Value = var2.Pieces;
                        cmdSets.Parameters["@minifigs"].Value = var2.MiniFigs;
                        cmdSets.Parameters["@minage"].Value = var2.MinAge;
                        cmdSets.Parameters["@imageURL"].Value = var2.ImageUrl;
                        cmdSets.Parameters["@retailprice"].Value = var2.RetailPrice;
                        cmdSets.ExecuteNonQuery();
                    }
                }
                tran.Commit();
            }
            catch(Exception ex) 
            {
                tran.Rollback();
            }

        }

    }
}