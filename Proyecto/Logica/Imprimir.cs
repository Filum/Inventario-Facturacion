using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;

namespace Logica
{
    public class Imprimir
    {
        public Imprimir()
        {

        }
        public void imprimir(DataGrid dataGrid, string title)
        {

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument fd = new FlowDocument();
                
                //Estilo de la letra, tamaño
                Paragraph p = new Paragraph(new Run(title));
                p.FontStyle = dataGrid.FontStyle;
                p.FontFamily = dataGrid.FontFamily;
                p.FontSize = 18;
                fd.Blocks.Add(p);
                

                Table table = new Table();
                TableRowGroup tableRowGroup = new TableRowGroup();
                TableRow r = new TableRow();
                fd.PageWidth = printDialog.PrintableAreaHeight;
                fd.PageHeight = printDialog.PrintableAreaWidth;
                fd.BringIntoView();

                //alineamiento 
                fd.TextAlignment = TextAlignment.Center;
                fd.ColumnWidth = 500;
                table.CellSpacing = 0;
                

                var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();
                List<dynamic> bindList = new List<dynamic>();


                for (int j = 0; j < headerList.Count; j++)
                {
                    //Estilo de los headers 
                    r.Cells.Add(new TableCell(new Paragraph(new Run(headerList[j]))));
                    r.Cells[j].ColumnSpan = 7;
                    r.Cells[j].RowSpan = 7;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.GhostWhite;
                    r.Cells[j].Foreground = Brushes.Black;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                    var binding = (dataGrid.Columns[j] as DataGridBoundColumn).Binding as Binding;

                    bindList.Add(binding.Path.Path);
                }
                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);
                for (int i = 0; i < dataGrid.Items.Count; i++)
                {

                    dynamic row;
                    if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                    { row = (DataRow)dataGrid.Items.GetItemAt(i);}
                    else
                    {
                        
                     row = (DataRowView)dataGrid.Items.GetItemAt(i);
                    }

                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontStyle = dataGrid.FontStyle;
                    table.FontFamily = dataGrid.FontFamily;
                    table.FontSize = 13;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {

                        if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                        {
                             r.Cells.Add(new TableCell(new Paragraph(new Run(row.GetType().GetProperty(bindList[j]).GetValue(row, null)))));

                        }
                        else
                        {

                           r.Cells.Add(new TableCell(new Paragraph(new Run(row.Row.ItemArray[j].ToString()))));

                        }

                        r.Cells[j].ColumnSpan = 7;
                        r.Cells[j].RowSpan = 7;
                        r.Cells[j].Padding = new Thickness(4);

                        r.Cells[j].BorderBrush = Brushes.DarkGray;
                        r.Cells[j].BorderThickness = new Thickness(0, 0, 1, 1);
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);

                }
                fd.Blocks.Add(table);
                
                
                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");

            }
        }
    }
}
