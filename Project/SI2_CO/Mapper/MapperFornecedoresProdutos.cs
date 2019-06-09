using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace SI2_CO.Mapper
{
    public class MapperFornecedoresProdutos : IMapperFornecedoresProdutos
    {
        private string cs;

        public MapperFornecedoresProdutos()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(FornecedoresProdutos entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO FornecedoresProdutos VALUES(@codigo_produto,@foid)";

                SqlParameter codigoParam = new SqlParameter();
                SqlParameter foidParam = new SqlParameter();

                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(foidParam);

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;

                foidParam.ParameterName = "@foid";
                foidParam.SqlDbType = SqlDbType.Int;

                codigoParam.Value = entity.codigoProduto;
                foidParam.Value = entity.foid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public void Delete(FornecedoresProdutos entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required)) { 

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "delete from FornecedoresProdutos where codigo_produto = @codigo_produto and foid = @foid)";

            SqlParameter codigoParam = new SqlParameter();
            SqlParameter foidParam = new SqlParameter();

            cmd.Parameters.Add(codigoParam);
            cmd.Parameters.Add(foidParam);

            codigoParam.ParameterName = "@codigo_produto";
            codigoParam.SqlDbType = SqlDbType.Int;

            foidParam.ParameterName = "@foid";
            foidParam.SqlDbType = SqlDbType.Int;

            codigoParam.Value = entity.codigoProduto;
            foidParam.Value = entity.foid;

            using (var cn = new SqlConnection(cs))
            {

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();

            }
            ts.Complete();
        }
    
    }

        public FornecedoresProdutos Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(FornecedoresProdutos entity)
        {
            throw new NotImplementedException();
        }
    }
}
