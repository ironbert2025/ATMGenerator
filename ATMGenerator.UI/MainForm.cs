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
        // ?? Casos de uso ???????????????????????????????
        private readonly GenerateAtmTemplateUseCase _generateUseCase;
        private readonly GetAtmTemplatesUseCase _getUseCase;

        // ?? Constantes del grid ????????????????????????
        private const int GridRows = 15;
        private const int GridCols = 10;
        public MainForm()
        {
            InitializeComponent();

            var repo = new AtmTemplateXmlRepository();
            _generateUseCase = new GenerateAtmTemplateUseCase(repo);
            _getUseCase = new GetAtmTemplatesUseCase(repo);
        }

        // ??????????????????????????????????????????????
        //  LOAD
        // ??????????????????????????????????????????????
        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarTemplatesEnGrid();
        }

        // ??????????????????????????????????????????????
        //  BOToN GENERAR
        // ??????????????????????????????????????????????
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtValor.Text.Trim(), out double inputValue) || inputValue <= 0)
            {
                MessageBox.Show("Ingresa un valor num�rico mayor que cero.",
                                "Valor inv�lido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var (creado, saved) = _generateUseCase.Execute(inputValue);

                if (!saved)
                {
                    MessageBox.Show($"El archivo ya existe y no fue sobreescrito:\n\n" +
                                    $"Nombre:    {creado.TemplateName}.xml\n" +
                                    $"StopLoss:  {creado.StopLoss}\n" +
                                    $"Target:    {creado.Target}",
                                    "Archivo existente",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Archivo generado exitosamente:\n\n" +
                                $"Nombre:    {creado.TemplateName}.xml\n" +
                                $"StopLoss:  {creado.StopLoss}\n" +
                                $"Target:    {creado.Target}",
                                "�xito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                CargarTemplatesEnGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el archivo:\n{ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        // ??????????????????????????????????????????????
        //  CONFIGURAR DATAGRIDVIEW
        // ??????????????????????????????????????????????
        private void ConfigurarGrid()
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

            // Encabezados de columnas: 1..10
            for (int c = 0; c < GridCols; c++)
            {
                dgvTemplates.Columns[c].HeaderText = (c + 1).ToString();
                dgvTemplates.Columns[c].MinimumWidth = 80;
            }

            // Crear 15 filas vac�as con encabezado de rango
            for (int r = 0; r < GridRows; r++)
            {
                dgvTemplates.Rows.Add();
                int rangoInicio = (r + 1) * 10;
                int rangoFin = rangoInicio + 9;
                dgvTemplates.Rows[r].HeaderCell.Value = $"{rangoInicio}-{rangoFin}";
            }
        }

        // ??????????????????????????????????????????????
        //  CARGAR ARCHIVOS EN EL GRID
        //  Fila 0 ? StopLoss 10-19
        //  Fila 1 ? StopLoss 20-29  ... etc.
        // ??????????????????????????????????????????????
        private void CargarTemplatesEnGrid()
        {
            // Limpiar celdas
            for (int r = 0; r < GridRows; r++)
                for (int c = 0; c < GridCols; c++)
                    dgvTemplates.Rows[r].Cells[c].Value = string.Empty;

            List<AtmTemplate> templates = _getUseCase.Execute();

            // Contador de posici�n por fila  [filaIndex -> columnaActual]
            var colPorFila = new Dictionary<int, int>();

            foreach (AtmTemplate t in templates)
            {
                // Extraer el n�mero de StopLoss del nombre "ATMXXYY"
                // El nombre tiene formato ATM + stopLoss + target
                // Necesitamos el stopLoss para determinar la fila
                int stopLoss = ExtraerStopLoss(t.TemplateName);
                if (stopLoss < 10) continue;

                // Fila: fila 0 = rango 10-19, fila 1 = 20-29 ...
                int filaIndex = (stopLoss / 10) - 1;
                if (filaIndex < 0 || filaIndex >= GridRows) continue;

                if (!colPorFila.ContainsKey(filaIndex))
                    colPorFila[filaIndex] = 0;

                int col = colPorFila[filaIndex];
                if (col >= GridCols) continue;  // m�s de 10 en la misma fila ? ignorar

                dgvTemplates.Rows[filaIndex].Cells[col].Value = t.TemplateName;
                colPorFila[filaIndex]++;
            }
        }

        /// <summary>
        /// Extrae el StopLoss del nombre. 
        /// El formato es ATM{stopLoss}{target} donde target = stopLoss * 3.
        /// Ejemplo: ATM2060 ? stopLoss=20
        /// Estrategia: iterar posibles largos de stopLoss (2..N) hasta que target == stopLoss*3
        /// </summary>
        private int ExtraerStopLoss(string name)
        {
            if (!name.StartsWith("ATM")) return 0;
            string numeros = name.Substring(3);  // "2060"

            for (int len = 1; len < numeros.Length; len++)
            {
                if (int.TryParse(numeros.Substring(0, len), out int sl) &&
                    int.TryParse(numeros.Substring(len), out int tg) &&
                    tg == sl * 3)
                    return sl;
            }
            return 0;
        }
    }
}
