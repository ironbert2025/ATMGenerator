using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ATMGenerator.Application.UseCases;
using ATMGenerator.Domain.Entities;
using ATMGenerator.Infrastructure.Repositories;


namespace ATMGenerator.UI
{
    public partial class MainForm : Form
    {
        // Use cases
        private readonly GenerateAtmTemplateUseCase _generateUseCase;
        private readonly GetAtmTemplatesUseCase _getUseCase;

        // Grid constants
        private const int GridRows = 15;
        private const int GridCols = 10;

        public MainForm()
        {
            InitializeComponent();

            var repo = new AtmTemplateXmlRepository();
            _generateUseCase = new GenerateAtmTemplateUseCase(repo);
            _getUseCase = new GetAtmTemplatesUseCase(repo);
        }

        // ──────────────────────────────────────────────
        //  LOAD
        // ──────────────────────────────────────────────
        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            LoadTemplatesInGrid();
        }

        // ──────────────────────────────────────────────
        //  GENERATE BUTTON
        // ──────────────────────────────────────────────
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtValue.Text.Trim(), out double inputValue) || inputValue <= 0)
            {
                MessageBox.Show("Enter a numeric value greater than zero.",
                                "Invalid value",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (created, saved) = _generateUseCase.Execute(inputValue);

                if (!saved)
                {
                    MessageBox.Show($"File already exists and was not overwritten:\n\n" +
                                    $"Name:      {created.TemplateName}.xml\n" +
                                    $"StopLoss:  {created.StopLoss}\n" +
                                    $"Target:    {created.Target}",
                                    "File already exists",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"File generated successfully:\n\n" +
                                $"Name:      {created.TemplateName}.xml\n" +
                                $"StopLoss:  {created.StopLoss}\n" +
                                $"Target:    {created.Target}",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                LoadTemplatesInGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating file:\n{ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // ──────────────────────────────────────────────
        //  CONFIGURE DATAGRIDVIEW
        // ──────────────────────────────────────────────
        private void ConfigureGrid()
        {
            dgvTemplates.Rows.Clear();
            dgvTemplates.Columns.Clear();

            dgvTemplates.ColumnCount = GridCols;
            dgvTemplates.AllowUserToAddRows = false;
            dgvTemplates.AllowUserToDeleteRows = false;
            dgvTemplates.ReadOnly = true;
            dgvTemplates.RowHeadersVisible = true;
            dgvTemplates.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvTemplates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTemplates.Font = new Font("Segoe UI", 9);

            // Column headers: 1..10
            for (int c = 0; c < GridCols; c++)
            {
                dgvTemplates.Columns[c].HeaderText = (c + 1).ToString();
                dgvTemplates.Columns[c].MinimumWidth = 80;
            }

            // Create 15 empty rows with range headers
            for (int r = 0; r < GridRows; r++)
            {
                dgvTemplates.Rows.Add();
                int rangeStart = (r + 1) * 10;
                int rangeEnd = rangeStart + 9;
                dgvTemplates.Rows[r].HeaderCell.Value = $"{rangeStart}-{rangeEnd}";
            }
        }

        // ──────────────────────────────────────────────
        //  LOAD FILES INTO GRID
        //  Row 0 → StopLoss 10-19
        //  Row 1 → StopLoss 20-29  ... etc.
        // ──────────────────────────────────────────────
        private void LoadTemplatesInGrid()
        {
            // Clear cells
            for (int r = 0; r < GridRows; r++)
                for (int c = 0; c < GridCols; c++)
                    dgvTemplates.Rows[r].Cells[c].Value = string.Empty;

            List<AtmTemplate> templates = _getUseCase.Execute();

            // Column position counter per row [rowIndex -> currentColumn]
            var colPerRow = new Dictionary<int, int>();

            foreach (AtmTemplate t in templates)
            {
                // Extract StopLoss number from name "ATMXXYY"
                // Name format: ATM + stopLoss + target
                // We need stopLoss to determine the row
                int stopLoss = ExtractStopLoss(t.TemplateName);
                if (stopLoss < 10) continue;

                // Row: row 0 = range 10-19, row 1 = 20-29 ...
                int rowIndex = (stopLoss / 10) - 1;
                if (rowIndex < 0 || rowIndex >= GridRows) continue;

                if (!colPerRow.ContainsKey(rowIndex))
                    colPerRow[rowIndex] = 0;

                int col = colPerRow[rowIndex];
                if (col >= GridCols) continue;  // more than 10 in the same row → skip

                dgvTemplates.Rows[rowIndex].Cells[col].Value = t.TemplateName;
                colPerRow[rowIndex]++;
            }
        }

        // Extracts StopLoss from the template name.
        // Format: ATM{stopLoss}{target} where target = stopLoss * 3.
        // Example: ATM2060 → stopLoss=20
        // Strategy: iterate possible stopLoss lengths (2..N) until target == stopLoss * 3
        private int ExtractStopLoss(string name)
        {
            if (!name.StartsWith("ATM")) return 0;
            string numbers = name.Substring(3);  // "2060"

            for (int len = 1; len < numbers.Length; len++)
            {
                if (int.TryParse(numbers.Substring(0, len), out int sl) &&
                    int.TryParse(numbers.Substring(len), out int tg) &&
                    tg == sl * 3)
                    return sl;
            }
            return 0;
        }
    }
}
