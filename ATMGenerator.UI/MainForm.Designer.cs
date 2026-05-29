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
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvTemplates = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTemplates)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // ── panelTop ──────────────────────────────
            this.panelTop.Controls.Add(this.lblValue);
            this.panelTop.Controls.Add(this.txtValue);
            this.panelTop.Controls.Add(this.btnGenerate);
            this.panelTop.Controls.Add(this.lblInfo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);

            // ── lblValue ──────────────────────────────
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValue.Location = new System.Drawing.Point(14, 20);
            this.lblValue.Text = "Value:";

            // ── txtValue ──────────────────────────────
            this.txtValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtValue.Location = new System.Drawing.Point(70, 16);
            this.txtValue.Size = new System.Drawing.Size(100, 28);
            this.txtValue.MaxLength = 10;

            // ── btnGenerate ───────────────────────────
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.Location = new System.Drawing.Point(185, 14);
            this.btnGenerate.Size = new System.Drawing.Size(90, 32);
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // ── lblInfo ───────────────────────────────
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInfo.Location = new System.Drawing.Point(290, 22);
            this.lblInfo.Text = "stopLoss = Ceiling(value / 1.25)   |   target = stopLoss × 3   |   File: ATM{stopLoss}{target}.xml";

            // ── dgvTemplates ──────────────────────────
            this.dgvTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTemplates.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 9F);
            this.dgvTemplates.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvTemplates.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgvTemplates.RowHeadersWidth = 60;
            this.dgvTemplates.EnableHeadersVisualStyles = true;

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

        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvTemplates;
        private System.Windows.Forms.Panel panelTop;
    }

        #endregion
    }
