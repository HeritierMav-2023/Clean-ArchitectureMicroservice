using CleanArchJobs.Application.Interfaces;
using CleanArchJobs.Domain.Entities;
using CleanArchJobs.Infrastructure.DataConfig;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;


namespace CleanArchJobs.Infrastructure.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        //1-propriéte connexion BD
        private readonly ConnectionString? _connection;

        //2-Constructeur DI
        public JobsRepository(IOptions<ConnectionString> configuration)
        {
            _connection = configuration.Value;
        }

        #region Ovverides Methods

        public async Task<int> AddAsync(Jobs item)
        {
            using (SqlConnection con = new SqlConnection(_connection?.DefaultConnection))
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDeleteOrGetAllJob", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", 3);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@ShortTitle", item.ShortTitle);
                cmd.Parameters.AddWithValue("@LongTitle", item.LongTitle);
                cmd.Parameters.AddWithValue("@MinSal", item.MinSalary);
                cmd.Parameters.AddWithValue("@MaxSal", item.MaxSalary);
                cmd.Parameters.AddWithValue("@DateAdded", null);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                con.Open();
                var count = cmd.ExecuteScalar();
                if(count == null)
                {
                    return 0;
                }
                con.Close();
                return 1;
            }
           
        }

        public void DeleteAsync(int Id)
        {
            using (SqlConnection con = new SqlConnection(_connection?.DefaultConnection))
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDeleteOrGetAllJob", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", 5);
                cmd.Parameters.AddWithValue("Id", Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<Jobs> GetAll()
        {
            var jobList = new List<Jobs>();
            DataSet ds;

            try
            {
                using var conx = new SqlConnection(_connection?.DefaultConnection);
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDeleteOrGetAllJob", conx);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", 1);
                cmd.Parameters.AddWithValue("@ShortTitle", null);
                cmd.Parameters.AddWithValue("@LongTitle", null);

                //ouvert la connection
                conx.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                jobList = new List<Jobs>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Jobs job = new Jobs();

                    job.Id = Convert.ToInt16(ds.Tables[0].Rows[i]["JOB_ID"].ToString());
                    job.ShortTitle = ds.Tables[0].Rows[i]["JOB_SHORT_TITLE"].ToString();
                    job.LongTitle = ds.Tables[0].Rows[i]["JOB_LONG_TITLE"].ToString();
                    job.MinSalary = Convert.ToDouble( ds.Tables[0].Rows[i]["MIN_SALARY"].ToString());
                    job.MaxSalary = Convert.ToDouble(ds.Tables[0].Rows[i]["MAX_SALARY"].ToString());

                    jobList.Add(job);
                }
                //fermeture connexion
                conx.Close();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }

            return jobList;
        }

        public async Task<Jobs> GetByIdAsync(int Id)
        {
            var job = new Jobs();
            string sQuery = "Usp_InsertUpdateDeleteOrGetAllJob";
            DataSet ds;

            try
            {
                using var conx = new SqlConnection(_connection?.DefaultConnection);
                SqlCommand cmd = new SqlCommand(sQuery, conx);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", 2);
                cmd.Parameters.AddWithValue("@Id", Id);
                //
                conx.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    job.Id = Convert.ToInt16(ds.Tables[0].Rows[i]["JOB_ID"].ToString());
                    job.ShortTitle = ds.Tables[0].Rows[i]["JOB_SHORT_TITLE"].ToString();
                    job.LongTitle = ds.Tables[0].Rows[i]["JOB_LONG_TITLE"].ToString();
                    job.MinSalary = Convert.ToDouble(ds.Tables[0].Rows[i]["MIN_SALARY"].ToString());
                    job.MaxSalary = Convert.ToDouble(ds.Tables[0].Rows[i]["MAX_SALARY"].ToString());

                }
                conx.Close();

            }
            catch (Exception exp)
            {

                throw new Exception(exp.Message,exp);
            }

            return job;
        }

        public async Task<int> UpdateAsync(Jobs item)
        {
            using (SqlConnection con = new SqlConnection(_connection?.DefaultConnection))
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDeleteOrGetAllJob", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", 4);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@ShortTitle", item.ShortTitle);
                cmd.Parameters.AddWithValue("@LongTitle", item.LongTitle);
                cmd.Parameters.AddWithValue("@MinSal", item.MinSalary);
                cmd.Parameters.AddWithValue("@MaxSal", item.MaxSalary);
                cmd.Parameters.AddWithValue("@DateAdded", null);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                con.Open();
                var count = cmd.ExecuteScalar();
                if (count == null)
                {
                    return 0;
                }
                con.Close();
                return 1;
            }
        }
        #endregion

    }
}
