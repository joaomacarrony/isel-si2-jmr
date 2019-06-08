using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace SI2_CO.Mapper
{
    class MapperStock : IMapperStock
    {
        private string cs;

        public MapperStock()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(Stock entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Stock VALUES(@codigo_produto,@preco,@quantidade,@quantidade_minima,@quantidade_maxima,@fid)";

                SqlParameter codigoParam = new SqlParameter();
                SqlParameter precoParam = new SqlParameter();
                SqlParameter quantParam = new SqlParameter();
                SqlParameter quantMinParam = new SqlParameter();
                SqlParameter quantMaxParam = new SqlParameter();
                SqlParameter fidParam = new SqlParameter();

                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(precoParam);
                cmd.Parameters.Add(quantParam);
                cmd.Parameters.Add(quantMinParam);
                cmd.Parameters.Add(quantMaxParam);
                cmd.Parameters.Add(fidParam);

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;

                precoParam.ParameterName = "@preco";
                precoParam.SqlDbType = SqlDbType.Float;

                quantParam.ParameterName = "@quantidade";
                quantParam.SqlDbType = SqlDbType.Int;

                quantMinParam.ParameterName = "@quantidade_minima";
                quantMinParam.SqlDbType = SqlDbType.Int;

                quantMaxParam.ParameterName = "@quantidade_maxima";
                quantMaxParam.SqlDbType = SqlDbType.Int;

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;

                codigoParam.Value = entity.codigo_produto;
                precoParam.Value = entity.preco;
                quantParam.Value = entity.quantidade;
                quantMinParam.Value = entity.quantidade_minima;
                quantMaxParam.Value = entity.quantidade_maxima;
                fidParam.Value = entity.fid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public void Delete(Stock entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from Stock where codigo_produto = @codigo_produto && fid = @fid;";

                SqlParameter codigoParam = new SqlParameter();
                SqlParameter fidParam = new SqlParameter();


                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(fidParam);



                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                

                codigoParam.Value = entity.codigo_produto;
                fidParam.Value = entity.fid;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public Stock Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Stock entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update from Stock set preco = @preco," +
                                                        "quantidade = @quantidade," +
                                                        "quantidade_minima = @quantidade_minima," +
                                                        "quantidade_maxima = @quantidade_maxima," +
                                                        "where codigo_produto = @codigo_produto " +
                                                        "&& fid = @fid)";

                SqlParameter codigoParam = new SqlParameter();
                SqlParameter precoParam = new SqlParameter();
                SqlParameter quantParam = new SqlParameter();
                SqlParameter quantMinParam = new SqlParameter();
                SqlParameter quantMaxParam = new SqlParameter();
                SqlParameter fidParam = new SqlParameter();

                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(precoParam);
                cmd.Parameters.Add(quantParam);
                cmd.Parameters.Add(quantMinParam);
                cmd.Parameters.Add(quantMaxParam);
                cmd.Parameters.Add(fidParam);

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;

                precoParam.ParameterName = "@preco";
                precoParam.SqlDbType = SqlDbType.Float;

                quantParam.ParameterName = "@quantidade";
                quantParam.SqlDbType = SqlDbType.Int;

                quantMinParam.ParameterName = "@quantidade_minima";
                quantMinParam.SqlDbType = SqlDbType.Int;

                quantMaxParam.ParameterName = "@quantidade_maxima";
                quantMaxParam.SqlDbType = SqlDbType.Int;

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;

                codigoParam.Value = entity.codigo_produto;
                precoParam.Value = entity.preco;
                quantParam.Value = entity.quantidade;
                quantMinParam.Value = entity.quantidade_minima;
                quantMaxParam.Value = entity.quantidade_maxima;
                fidParam.Value = entity.fid;

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
