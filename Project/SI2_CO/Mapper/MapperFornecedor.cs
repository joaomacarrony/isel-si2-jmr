using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;



namespace SI2_CO.Mapper
{
    class MapperFornecedor : IMapperFornecedor
    {

        private string cs;

        public MapperFornecedor()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(Fornecedor forn)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO FORNECEDOR(Nif,Nome) output inserted.foid VALUES(@Nif,@Nome)";
                SqlParameter NifParam = new SqlParameter();
                SqlParameter NomeParam = new SqlParameter();

                cmd.Parameters.Add(NifParam);
                cmd.Parameters.Add(NomeParam);

                NifParam.ParameterName = "@Nif";
                NifParam.SqlDbType = SqlDbType.Int;
                NomeParam.ParameterName = "@Nome";
                NomeParam.SqlDbType = SqlDbType.VarChar;
                NomeParam.Size = 40;

                NifParam.Value = forn.Nif;
                NomeParam.Value = forn.Nome;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    forn.Foid = (int)cmd.ExecuteScalar();

                }
                ts.Complete();
            }
        }

        public Fornecedor Read(int foid)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM FORNECEDOR WHERE Foid = @Foid";

                SqlParameter FoidParam = new SqlParameter();
                cmd.Parameters.Add(FoidParam);
                FoidParam.ParameterName = "@Foid";
                FoidParam.SqlDbType = SqlDbType.Int;
                FoidParam.Value = foid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }

            return null;
        }

        public void Update(Fornecedor forn)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE FORNECEDOR SET Nif = @Nif, Nome = @Nome WHERE Foid = @Foid";
                SqlParameter NifParam = new SqlParameter();
                SqlParameter NomeParam = new SqlParameter();
                SqlParameter FoidParam = new SqlParameter();


                cmd.Parameters.Add(NifParam);
                cmd.Parameters.Add(NomeParam);
                cmd.Parameters.Add(FoidParam);

                NifParam.ParameterName = "@Nif";
                NifParam.SqlDbType = SqlDbType.Int;
                NomeParam.ParameterName = "@Nome";
                NomeParam.SqlDbType = SqlDbType.VarChar;
                NomeParam.Size = 40;
                FoidParam.ParameterName = "@Foid";
                FoidParam.SqlDbType = SqlDbType.Int;

                NifParam.Value = forn.Nif;
                NomeParam.Value = forn.Nome;
                FoidParam.Value = forn.Foid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
        public void Delete(Fornecedor forn)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM FORNECEDOR WHERE Foid = @Foid";

                SqlParameter FoidParam = new SqlParameter();

                cmd.Parameters.Add(FoidParam);

                FoidParam.ParameterName = "@Foid";
                FoidParam.SqlDbType = SqlDbType.Int;

                FoidParam.Value = forn.Foid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

    }
}