namespace ATMGenerator.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //SuspendLayout();
            //// 
            //// MainForm
            //// 
            //AutoScaleDimensions = new SizeF(7F, 15F);
            //AutoScaleMode = AutoScaleMode.Font;
            //ClientSize = new Size(800, 450);
            //Name = "MainForm";
            //Text = "Form1";
            //Load += this.MainForm_Load;
            //ResumeLayout(false);

            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvTemplates = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTemplates)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // ── panelTop ──────────────────────────────
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.panelTop.Controls.Add(this.lblValor);
            this.panelTop.Controls.Add(this.txtValor);
            this.panelTop.Controls.Add(this.btnGenerar);
            this.panelTop.Controls.Add(this.lblInfo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);

            // ── lblValor ──────────────────────────────
            this.lblValor.AutoSize = true;
            this.lblValor.ForeColor = System.Drawing.Color.White;
            this.lblValor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValor.Location = new System.Drawing.Point(14, 20);
            this.lblValor.Text = "Valor:";

            // ── txtValor ──────────────────────────────
            this.txtValor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtValor.Location = new System.Drawing.Point(70, 16);
            this.txtValor.Size = new System.Drawing.Size(100, 28);
            this.txtValor.MaxLength = 10;

            // ── btnGenerar ────────────────────────────
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.FlatAppearance.BorderSize = 0;
            this.btnGenerar.Location = new System.Drawing.Point(185, 14);
            this.btnGenerar.Size = new System.Drawing.Size(90, 32);
            this.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);

            // ── lblInfo ───────────────────────────────
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Silver;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInfo.Location = new System.Drawing.Point(290, 22);
            this.lblInfo.Text = "valor1 = Ceiling(valor / 1.25)   |   valor2 = valor1 × 3   |   Archivo: ATM{valor1}{valor2}.xml";

            // ── dgvTemplates ──────────────────────────
            this.dgvTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTemplates.BackgroundColor = System.Drawing.Color.FromArgb(20, 20, 20);
            this.dgvTemplates.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dgvTemplates.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.dgvTemplates.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTemplates.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 9F);
            this.dgvTemplates.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.dgvTemplates.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTemplates.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvTemplates.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.dgvTemplates.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.LightGray;
            this.dgvTemplates.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgvTemplates.RowHeadersWidth = 60;
            this.dgvTemplates.EnableHeadersVisualStyles = false;
            this.dgvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // ── MainForm ──────────────────────────────
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.dgvTemplates);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Text = "ATM Template Generator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTemplates)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvTemplates;
        private System.Windows.Forms.Panel panelTop;
    }

        #endregion
    }

