using Location.Application.IRepository;
using Location.Infrastructure.Config;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using Location.Domain.Entities;

namespace Location.Infrastructure.Repository
{
    public class LocationRepository :ILocationRepository
    {
        //1-propriéte connexion BD
        private readonly ConnectionStrings _connection;

        /// <summary>
        /// Ceci est utilisé comme constructeur d'injection pour lire la chaîne de connexion à partir de appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        public LocationRepository(IOptions<ConnectionStrings> configuration)
        {
            _connection = configuration.Value;
        }

        #region Ovveride Methods

        public async Task<int> Add(Domain.Entities.Location location)
        {
            try
            {

                string sQuery = "Usp_ADDwithUpdateDeleteOrGetLocation";
                using var connection = new SqlConnection(_connection.defaultConnection);
                SqlCommand cmd = new SqlCommand(sQuery, connection);

                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@Action", "Add");
                cmd.Parameters.AddWithValue("@Address", location.StreetAddress);
                cmd.Parameters.AddWithValue("@Postal", location.PostalCode);
                cmd.Parameters.AddWithValue("@City", location.City);
                cmd.Parameters.AddWithValue("@Province", location.Province);
                cmd.Parameters.AddWithValue("@Country", location.Country);
                cmd.Parameters.AddWithValue("@CreateOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdateOn", null);
                cmd.Parameters.AddWithValue("@LocationId", location.locationID);
                connection.Open();
                int count = cmd.ExecuteNonQuery();
                if(count > 0)
                {
                    return count;
                }
                connection.Close();
                return 0;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public void DeleteAsync(int Id)
        {
            string sQuery = "Usp_ADDwithUpdateDeleteOrGetLocation";
            using var connection = new SqlConnection(_connection.defaultConnection);
            try
            {
                SqlCommand cmd = new SqlCommand(sQuery, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@LocationId", Id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
            finally 
            { 
                connection.Close(); 
            }
            
        }

        public List<Domain.Entities.Location> GetAll()
        {
            var locations = new List<Domain.Entities.Location>();
            DataSet ds;
            try
            {
                //Nom de la procédure pour récupérer la liste des Locations
                string sQuery = "Usp_ADDwithUpdateDeleteOrGetLocation";

                //il contient la chaîne de connexion à lire à partir des paramètres d'application appsettings
                using var connection = new SqlConnection(_connection.defaultConnection);
                SqlCommand cmd = new SqlCommand(sQuery, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetAll");
                //ouvert la connection
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                locations = new List<Domain.Entities.Location>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var location = new  Domain.Entities.Location();
                    location.locationID = Convert.ToInt16(ds.Tables[0].Rows[i]["LOCATId"].ToString());
                    location.StreetAddress = ds.Tables[0].Rows[i]["STREET_ADRESS"].ToString();
                    location.PostalCode = ds.Tables[0].Rows[i]["POSTAL_CODE"].ToString();
                    location.City = ds.Tables[0].Rows[i]["CITY"].ToString();
                    location.Province = ds.Tables[0].Rows[i]["PROVINCE"].ToString();
                    location.Country = ds.Tables[0].Rows[i]["COUNTRY"].ToString();

                    locations.Add(location);
                }
                //fermeture connexion
                connection.Close();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
            return locations;
        }

        public async Task<Domain.Entities.Location> GetByIdAsync(int Id)
        {
            var location = new Domain.Entities.Location();
            DataSet ds;
            try
            {
                string sQuery = "Usp_ADDwithUpdateDeleteOrGetLocation";
                using var conx = new SqlConnection(_connection.defaultConnection);
                SqlCommand cmd = new SqlCommand(sQuery, conx);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetData");
                cmd.Parameters.AddWithValue("@LocationId", Id);

                //
                conx.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    location.locationID = Convert.ToInt16(ds.Tables[0].Rows[i]["LOCATId"].ToString());
                    location.StreetAddress = ds.Tables[0].Rows[i]["STREET_ADRESS"].ToString();
                    location.PostalCode = ds.Tables[0].Rows[i]["POSTAL_CODE"].ToString();
                    location.City = ds.Tables[0].Rows[i]["CITY"].ToString();
                    location.Province = ds.Tables[0].Rows[i]["PROVINCE"].ToString();
                    location.Country = ds.Tables[0].Rows[i]["COUNTRY"].ToString();
                }
                conx.Close();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
            return location;
        }
        public async Task<int> UpdateAsync(Domain.Entities.Location location)
        {
            using (SqlConnection con = new SqlConnection(_connection?.defaultConnection))
            {
                SqlCommand cmd = new SqlCommand("Usp_ADDwithUpdateDeleteOrGetLocation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@Address", location.StreetAddress);
                cmd.Parameters.AddWithValue("@Postal", location.PostalCode);
                cmd.Parameters.AddWithValue("@City", location.City);
                cmd.Parameters.AddWithValue("@Province", location.Province);
                cmd.Parameters.AddWithValue("@Country", location.Country);
                cmd.Parameters.AddWithValue("@CreateOn", null);
                cmd.Parameters.AddWithValue("@UpdateOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@LocationId", location.locationID);

                con.Open();
                var count = cmd.ExecuteScalar();
                if (count == null)
                {
                    return 1;
                }
                con.Close();
                return 0;
            }
        }

        #endregion
    }
}
