namespace NGestao
{
    partial class TesteConexao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtConexaoLocal = new System.Windows.Forms.TextBox();
            this.txtConexaoRedmine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConexaoLocal
            // 
            this.txtConexaoLocal.Enabled = false;
            this.txtConexaoLocal.Location = new System.Drawing.Point(114, 12);
            this.txtConexaoLocal.Multiline = true;
            this.txtConexaoLocal.Name = "txtConexaoLocal";
            this.txtConexaoLocal.Size = new System.Drawing.Size(367, 95);
            this.txtConexaoLocal.TabIndex = 0;
            // 
            // txtConexaoRedmine
            // 
            this.txtConexaoRedmine.Enabled = false;
            this.txtConexaoRedmine.Location = new System.Drawing.Point(114, 113);
            this.txtConexaoRedmine.Multiline = true;
            this.txtConexaoRedmine.Name = "txtConexaoRedmine";
            this.txtConexaoRedmine.Size = new System.Drawing.Size(367, 95);
            this.txtConexaoRedmine.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Conexão Local";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Conexão Redmine";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(264, 226);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(217, 61);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // TesteConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 298);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConexaoRedmine);
            this.Controls.Add(this.txtConexaoLocal);
            this.Name = "TesteConexao";
            this.Text = "TesteConexao";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConexaoLocal;
        private System.Windows.Forms.TextBox txtConexaoRedmine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFechar;
    }
}