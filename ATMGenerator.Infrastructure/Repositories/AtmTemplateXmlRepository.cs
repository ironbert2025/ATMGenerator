using ATMGenerator.Application.Interfaces;
using ATMGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ATMGenerator.Infrastructure.Repositories
{
    public class AtmTemplateXmlRepository : IAtmTemplateRepository
    {
        private const string TemplateFolder = @"C:\Template";

        // ──────────────────────────────────────────────
        //  GUARDAR
        // ──────────────────────────────────────────────
        public void Save(AtmTemplate template)
        {
            EnsureFolderExists();

            string filePath = Path.Combine(TemplateFolder, $"{template.TemplateName}.xml");
            template.FilePath = filePath;

            XDocument doc = BuildXml(template);
            doc.Save(filePath);
        }

        // ──────────────────────────────────────────────
        //  LEER TODOS
        // ──────────────────────────────────────────────
        public List<AtmTemplate> GetAll()
        {
            EnsureFolderExists();

            var result = new List<AtmTemplate>();

            foreach (string file in Directory.GetFiles(TemplateFolder, "ATM*.xml")
                                             .OrderBy(f => f))
            {
                string nameOnly = Path.GetFileNameWithoutExtension(file);
                result.Add(new AtmTemplate
                {
                    TemplateName = nameOnly,
                    FilePath = file
                });
            }

            return result;
        }

        // ──────────────────────────────────────────────
        //  PRIVADOS
        // ──────────────────────────────────────────────
        private void EnsureFolderExists()
        {
            if (!Directory.Exists(TemplateFolder))
                Directory.CreateDirectory(TemplateFolder);
        }

        /// <summary>
        /// Construye el XML con el mismo formato que ATM1030.xml
        /// reemplazando Template, StopLoss y Target.
        /// </summary>
        private XDocument BuildXml(AtmTemplate t)
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("NinjaTrader",
                    new XElement("AtmStrategy",
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XElement("IsVisible", "true"),
                        new XElement("calculate2", "OnBarClose"),
                        new XElement("AreLinesConfigurable", "true"),
                        new XElement("ArePlotsConfigurable", "true"),
                        new XElement("BarsToLoad", "0"),
                        new XElement("Calculate", "OnBarClose"),
                        new XElement("Displacement", "0"),
                        new XElement("DisplayInDataBox", "true"),
                        new XElement("From", "2099-12-01T00:00:00"),
                        new XElement("IsAutoScale", "true"),
                        new XElement("Lines"),
                        new XElement("MaximumBarsLookBack", "TwoHundredFiftySix"),
                        new XElement("Name", "AtmStrategy"),
                        new XElement("Panel", "0"),
                        new XElement("Plots"),
                        new XElement("ScaleJustification", "Right"),
                        new XElement("ShowTransparentPlotsInDataBox", "false"),
                        new XElement("To", "1800-01-01T00:00:00"),
                        new XElement("IsDataSeriesRequired", "false"),
                        new XElement("IsOverlay", "false"),
                        new XElement("SelectedValueSeries", "0"),
                        new XElement("Gtd", "1800-01-01T00:00:00"),
                        new XElement("Template", t.TemplateName),   // ← dinámico
                        new XElement("TimeInForce", "Gtc"),
                        new XElement("BarsRequiredToTrade", "0"),
                        new XElement("Category", "Atm"),
                        new XElement("ConnectionLossHandling", "KeepRunning"),
                        new XElement("DaysToLoad", "1"),
                        new XElement("DefaultQuantity", "1"),
                        new XElement("DisconnectDelaySeconds", "0"),
                        new XElement("EntriesPerDirection", "1"),
                        new XElement("EntryHandling", "AllEntries"),
                        new XElement("ExitOnSessionCloseSeconds", "0"),
                        new XElement("IncludeCommission", "false"),
                        new XElement("IsAggregated", "false"),
                        new XElement("IsExitOnSessionCloseStrategy", "false"),
                        new XElement("IsFillLimitOnTouch", "false"),
                        new XElement("IsOptimizeDataSeries", "false"),
                        new XElement("IsStableSession", "false"),
                        new XElement("IsTickReplay", "false"),
                        new XElement("IsTradingHoursBreakLineVisible", "false"),
                        new XElement("IsWaitUntilFlat", "false"),
                        new XElement("NumberRestartAttempts", "0"),
                        new XElement("OptimizationPeriod", "10"),
                        new XElement("OrderFillResolution", "High"),
                        new XElement("OrderFillResolutionType", "Tick"),
                        new XElement("OrderFillResolutionValue", "1"),
                        new XElement("RestartsWithinMinutes", "0"),
                        new XElement("SetOrderQuantity", "Strategy"),
                        new XElement("Slippage", "0"),
                        new XElement("StartBehavior", "AdoptAccountPosition"),
                        new XElement("StopTargetHandling", "PerEntryExecution"),
                        new XElement("SupportsOptimizationGraph", "false"),
                        new XElement("TestPeriod", "28"),
                        new XElement("TradingHoursSerializable"),
                        new XElement("Brackets",
                            new XElement("Bracket",
                                new XElement("Quantity", "1"),
                                new XElement("StopLoss", t.StopLoss),   // ← dinámico
                                new XElement("Target", t.Target)      // ← dinámico
                            )
                        ),
                        new XElement("CalculationMode", "Ticks"),
                        new XElement("ChaseLimit", "0"),
                        new XElement("EntryQuantity", "1"),
                        new XElement("InitialTickSize", "0"),
                        new XElement("IsChase", "false"),
                        new XElement("IsChaseIfTouched", "false"),
                        new XElement("IsTargetChase", "false"),
                        new XElement("ReverseAtStop", "false"),
                        new XElement("ReverseAtTarget", "false"),
                        new XElement("UseMitForProfit", "false"),
                        new XElement("UseStopLimitForStopLossOrders", "false"),
                        new XElement("AtmSelector", "6016d70bb13d40cb978214e0fb22a858"),
                        new XElement("OnBehalfOf"),
                        new XElement("ReverseAtStopStrategyId", "-1"),
                        new XElement("ReverseAtTargetStrategyId", "-1"),
                        new XElement("ShadowStrategyStrategyId", "-1"),
                        new XElement("ShadowTemplate")
                    )
                )
            );
        }
    }
}
