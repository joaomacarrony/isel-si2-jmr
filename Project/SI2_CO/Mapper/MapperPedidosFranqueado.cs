using SI2;
using SI2_CO.Mapper_Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SI2_CO.Mapper
{
    class MapperPedidosFranqueado : IMapperPedidosFranqueado
    {
        private string cs;

        public MapperPedidosFranqueado()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(PedidosFranqueado entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert into PedidosFranqueados output inserted.pfid values(@fid,@codigo_produto,@quantidade)";

                SqlParameter fidParam = new SqlParameter();
                SqlParameter codigoParam = new SqlParameter();
                SqlParameter quantidadeParam = new SqlParameter();

                cmd.Parameters.Add(fidParam);
                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(quantidadeParam);


                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;
                quantidadeParam.ParameterName = "@quantidade";
                quantidadeParam.SqlDbType = SqlDbType.Int;

                fidParam.Value = entity.fid;
                codigoParam.Value = entity.codigo_produto;
                quantidadeParam.Value = entity.quantidade;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    entity.pfid = (int)cmd.ExecuteScalar();

                }
                ts.Complete();
            }
        }

        public void Delete(PedidosFranqueado entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from PedidosFranqueados where pfid = @pfid";

                SqlParameter pfidParam = new SqlParameter();

                cmd.Parameters.Add(pfidParam);

                pfidParam.ParameterName = "@pfid";
                pfidParam.SqlDbType = SqlDbType.Int;
      
                pfidParam.Value = entity.pfid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public PedidosFranqueado Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PedidosFranqueado entity)
        {
            throw new NotImplementedException();
        }
    }
}
