using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace NGestao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string local = "Provider=PostgreSQL; User ID=postgres; Password=teste; Host=localhost; Port=5432; Database=ngestao; Pooling=false; Min Pool Size=0; Max Pool Size=100; Connection Lifetime=0;";
        string redmine = "Provider=PostgreSQL; User ID=rei; Password=teste; Host=torres.reitech.com.br; Port=5432; Database=redmine; Pooling=false; Min Pool Size=0; Max Pool Size=100; Connection Lifetime=0;";

        private void btnTeste_Click(object sender, EventArgs e)
        {
            var conLocal = new Devart.Data.Universal.UniConnection(local);
            var conRedmine = new Devart.Data.Universal.UniConnection(redmine);

            string resultLocal = string.Empty;
            string resultRedmine = string.Empty;

            try
            {
                conLocal.Open();
                resultLocal = "OK";
            }
            catch (Exception ex)
            {
                resultLocal = ex.Message;
            }

            try
            {
                conRedmine.Open();
                resultRedmine = "OK";
            }
            catch (Exception ex)
            {
                resultRedmine = ex.Message;

            }

            var formularioResult = new TesteConexao(resultLocal, resultRedmine);
            formularioResult.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var conectLocal = new Devart.Data.Universal.UniConnection(local);
                var conectRedmine = new Devart.Data.Universal.UniConnection(redmine);

                conectLocal.Open();
                conectRedmine.Open();

                var command = conectRedmine.CreateCommand();
                command.CommandText =
                    @" SELECT 
                          atribuido,
                          ( select project_id from issues where issues.id = v_issues.id ) project_id,
                          ( select assigned_to_id from issues where issues.id = v_issues.id ) atribuido_id,
                          projeto,
                          ( SUM ( CASE WHEN (status = 'Feedback' 					AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) feedback,
                          ( SUM ( CASE WHEN (status = 'Feedback' 					AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) feedback_urgencias,
                          ( SUM ( CASE WHEN (status = 'Resolvida' 					AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) resolvidas_urgencias,
                          ( SUM ( CASE WHEN (status = 'Resolvida' 					AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) resolvidas,
                          ( SUM ( CASE WHEN (status = 'Feedback-Cliente'			AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) feedback_cliente_urgencias,
                          ( SUM ( CASE WHEN (status = 'Feedback-Cliente'			AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) GiveFeedback_cliente,
                          ( SUM ( CASE WHEN (status = 'Nova aberto'					AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) nova_aberto_urgencias,
                          ( SUM ( CASE WHEN (status = 'Nova aberto'					AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) nova_aberto,
                          ( SUM ( CASE WHEN (status = 'Aguardando Desenvolvimento'	AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) aguardando_dev_urgencias,
                          ( SUM ( CASE WHEN (status = 'Aguardando Desenvolvimento'	AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) aguardando_dev,
                          ( SUM ( CASE WHEN (status = 'Em andamento'				AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) em_andamento_urgencias,
                          ( SUM ( CASE WHEN (status = 'Em andamento'				AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) em_andamento,
                          ( SUM ( CASE WHEN (status = 'Rejeitada'					AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) rejeitadas_urgencias,
                          ( SUM ( CASE WHEN (status = 'Rejeitada'					AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) rejeitadas,
                          ( SUM ( CASE WHEN (status = 'Fechada'						AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) fechadas_urgencias,
                          ( SUM ( CASE WHEN (status = 'Fechada'						AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) fechadas,
                          ( SUM ( CASE WHEN (status = 'Aguardando Atualização'		AND v_issues.prioridade NOT IN (5, 6, 7)	) THEN 1 ELSE 0 END ) ) aguardando_atu_urgencias,
                          ( SUM ( CASE WHEN (status = 'Aguardando Atualização'		AND v_issues.prioridade IN (5, 6, 7)		) THEN 1 ELSE 0 END ) ) aguardando_atu
                       FROM v_issues
                       GROUP BY atribuido_id, atribuido, project_id, projeto ";

                var tabela = new DataTable();

                var dataReader = command.ExecuteReader();
                tabela.Load(dataReader);

                dataReader.Close();

                conectRedmine.Close();

               
                foreach (DataRow dataRow in tabela.Rows)
                {
                    string atribuido = GetString(dataRow["atribuido"]);
                    int projetoId = GetInt(dataRow["project_id"]);
                }






            }
            catch (Exception)
            {

            }

            using (var novaConexao = new Devart.Data.Universal.UniConnection(local))
            {


            }




            object x = 10;
            object y = "meu texto";

        }

        public string GetString(object objeto)
        {
            if (DBNull.Value.Equals(objeto))
            {
                return string.Empty;
            }

            return objeto.ToString();
        }

        public int GetInt(object objeto)
        {
            if (DBNull.Value.Equals(objeto))
            {
                return 0;
            }

            return Convert.ToInt32(objeto);

            Convert.ToInt32("10");
            Convert.ToInt32("vai se ferrar!");
        }



    }
}
