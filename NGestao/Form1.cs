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

        string local = "Provider=PostgreSQL; User ID=rei; Password=teste; Host=ricardo-noronha; Port=5432; Database=ngestao2; Pooling=false; Min Pool Size=0; Max Pool Size=100; Connection Lifetime=0;";
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

                var comandoCod = conectRedmine.CreateCommand();
                comandoCod.CommandText = "SET CLIENT_ENCODING TO 'LATIN1'";
                comandoCod.ExecuteNonQuery();
                
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

                using (var connectLocal = new Devart.Data.Universal.UniConnection(local))
                {
                    connectLocal.Open();

                    var comandoEncoding = conectLocal.CreateCommand();
                    comandoEncoding.CommandText = "SET CLIENT_ENCODING TO 'LATIN1'";
                    comandoEncoding.ExecuteNonQuery();

                    conectLocal.BeginTransaction();


                    foreach (DataRow dataRow in tabela.Rows)
                    {
                        string atribuido = GetString(dataRow["atribuido"]);
                        string projeto = GetString(dataRow["projeto"]);
                        int projetoId = GetInt(dataRow["project_id"]);
                        int atribuidoId = GetInt(dataRow["atribuido_id"]);
                        int feedback = GetInt(dataRow["feedback"]);
                        int feedbackUrgencias = GetInt(dataRow["feedback_urgencias"]);
                        int resolvidasUrgencias = GetInt(dataRow["resolvidas_urgencias"]);
                        int resolvidas = GetInt(dataRow["resolvidas"]);
                        int feedbackClienteUrgencias = GetInt(dataRow["feedback_cliente_urgencia"]);
                        int giveFeedbackCliente = GetInt(dataRow["GiveFeedback_cliente"]);
                        int novaAbertoUrgencias = GetInt(dataRow["nova_aberto_urgencias"]);
                        int novaAberto = GetInt(dataRow["nova_aberto"]);
                        int aguardandDevUrgencias = GetInt(dataRow["aguardando_dev_urgencias"]);
                        int aguardandoDev = GetInt(dataRow["aguardando_dev"]);
                        int emAndamentoUrgencias = GetInt(dataRow["em_andamento_urgencias"]);
                        int emAndamento = GetInt(dataRow["em_andamento"]);
                        int rejeitadasUrgencias = GetInt(dataRow["rejeitadas_urgencias"]);
                        int rejeitadas = GetInt(dataRow["rejeitadas"]);
                        int fechadasUrgencias = GetInt(dataRow["fechadas_urgencias"]);
                        int fechadas = GetInt(dataRow["fechadas"]);
                        int aguardandoAtuUrgencias = GetInt(dataRow["aguardando_atu_urgencias"]);
                        int aguardandoAtu = GetInt(dataRow["aguardando_atu"]);

                        var comando = conectLocal.CreateCommand();
                        comando.CommandText =
                                             @"INSERT INTO r_sumario_tarefas 
                                        ( atribuido_id,  atribuido,  projeto_id,  projeto,  feedback,  feedback_urgencias,  resolvidas,  resolvidas_urgencias,
                                          feedback_cliente,  feedback_cliente_urgencias,  nova_aberto,  nova_aberto_urgencias,  aguardando_dev,  aguardando_dev_urgencias,
                                          em_andamento,  em_andamento_urgencias,  rejeitadas,  rejeitadas_urgencias,  fechadas,  fechadas_urgencias,
                                          aguardando_atu,  aguardando_atu_urgencias
                                        ) 
                                            VALUES 
                                        (
                                          @atribuido_id,  @atribuido,  @projeto_id,  @projeto,  @feedback,  @feedback_urgencias,  @resolvidas,  @resolvidas_urgencias,
                                          @feedback_cliente,  @feedback_cliente_urgencias,  @nova_aberto,  @nova_aberto_urgencias,  @aguardando_dev,  @aguardando_dev_urgencias,
                                          @em_andamento,  @em_andamento_urgencias,  @rejeitadas,  @rejeitadas_urgencias,  @fechadas,  @fechadas_urgencias,  @aguardando_atu,
                                          @aguardando_atu_urgencias)";

                        comando.Parameters.Add("@atribuido_id", atribuidoId);
                        comando.Parameters.Add("@atribuido", atribuido);
                        comando.Parameters.Add("@projeto_id", projetoId);
                        comando.Parameters.Add("@projeto", projeto);
                        comando.Parameters.Add("@feedback", feedback);
                        comando.Parameters.Add("@feedback_urgencias", feedbackUrgencias);
                        comando.Parameters.Add("@resolvidas", resolvidas);
                        comando.Parameters.Add("@resolvidas_urgencias", resolvidasUrgencias);
                        comando.Parameters.Add("@feedback_cliente", giveFeedbackCliente);
                        comando.Parameters.Add("@feedback_cliente_urgencias", feedbackClienteUrgencias);
                        comando.Parameters.Add("@nova_aberto", novaAberto);
                        comando.Parameters.Add("@nova_aberto_urgencias", novaAbertoUrgencias);
                        comando.Parameters.Add("@aguardando_dev", aguardandoDev);
                        comando.Parameters.Add("@aguardando_dev_urgencias", aguardandDevUrgencias);
                        comando.Parameters.Add("@em_andamento", emAndamento);
                        comando.Parameters.Add("@em_andamento_urgencias", emAndamentoUrgencias);
                        comando.Parameters.Add("@rejeitadas", rejeitadas);
                        comando.Parameters.Add("@rejeitadas_urgencias", rejeitadasUrgencias);
                        comando.Parameters.Add("@fechadas", fechadas);
                        comando.Parameters.Add("@fechadas_urgencias", fechadasUrgencias);
                        comando.Parameters.Add("@aguardando_atu", aguardandoAtu);
                        comando.Parameters.Add("@aguardando_atu_urgencias", aguardandoAtuUrgencias);

                    }

                    conectLocal.Commit();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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


        }



    }
}
