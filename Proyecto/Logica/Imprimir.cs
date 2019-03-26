using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;
using System;

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
                fd.ColumnWidth = 700;
                table.CellSpacing = 0;




                var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();
                List<dynamic> bindList = new List<dynamic>();


                for (int j = 0; j < headerList.Count; j++)
                {
                    //Estilo de los headers 
                    r.Cells.Add(new TableCell(new Paragraph(new Run(headerList[j]))));
                    r.Cells[j].ColumnSpan = 7;
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
                    { row = (DataRow)dataGrid.Items.GetItemAt(i); }
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
                        r.Cells[j].Padding = new Thickness(4);

                        r.Cells[j].BorderBrush = Brushes.DarkGray;
                        r.Cells[j].BorderThickness = new Thickness(0, 0, 1, 1);
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);

                }
                fd.Blocks.Add(table);


                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
                MessageBoxResult v_Result = MessageBox.Show("Se ha enviado correctamente a imprimir.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        public void imprimirFactura(DataGrid dataGrid, Entidades.EntidadFacturas factura, DateTime fechaAct, Entidades.EntidadClientes cliente)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
               
                //ENCABEZADO
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("DelRam S.A"))); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("Delgado Ramos Sociedad Anonima")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("(506)2431 0896    (506)2443 9149")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("delram@delramtech.com"));

                //CODIGO Y FECHA
                Paragraph paragraph1 = new Paragraph();
                paragraph1.TextAlignment = TextAlignment.Right;
                paragraph1.Inlines.Add(new Bold(new Run(factura.v_Codigo.ToString()))); paragraph1.Inlines.Add(new LineBreak());
                paragraph1.Inlines.Add(new Run(fechaAct.ToString("dddd, dd MMMM yyyy")));
                

                //CLIENTE
                Paragraph paragraph2 = new Paragraph();
                paragraph2.Inlines.Add(new Run(factura.v_Cliente + "      " + cliente.v_Cedula)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run(cliente.v_NombreCompleto)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run(cliente.v_Representante)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run(cliente.v_Direccion)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run(cliente.v_Teleoficina.ToString() + "     " + cliente.v_Telemovil.ToString())); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run(cliente.v_Correo)); paragraph2.Inlines.Add(new LineBreak());

                //TABLA DETALLES
                Table table = new Table();
                TableRowGroup rowGroup = new TableRowGroup();
                TableRow row = new TableRow();

                //HEADERS
                /*var headers = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();  //OBTENEMOS LOS TITULOS (HEADERS) DEL DATAGRID
                
                for(int j = 0; j < headers.Count; j++)
                {
                    row.Cells.Add(new TableCell(new Paragraph(new Run(headers[j]))));
                    row.Cells[j].ColumnSpan = 7;
                    row.Cells[j].Padding = new Thickness(4);
                    row.Cells[j].FontWeight = FontWeights.Bold;
                    row.Cells[j].FontSize = 12;
                    row.Cells[j].Foreground = Brushes.Black;
                    

                }*/

                row.Cells.Add(new TableCell(new Paragraph(new Run("Tipo"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("Codigo"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("Descripcion Producto/Servicio"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("Cantidad"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("Precio Unitario"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("% Descuento"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("% IV"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run("Total Linea"))));
                
                

                row.Background = Brushes.LightGray;
                row.FontSize = 10;

                rowGroup.Rows.Add(row);
    

                //AGREGA VALORES DEL DATAGRID A LA TABLA DEL DOCUMENTO...
                foreach(var item in dataGrid.Items)
                {
                    DataGridRow gridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    TableRow tableRow = new TableRow();
                    rowGroup.Rows.Add(tableRow);

                    for(int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        var valor = ((TextBlock)dataGrid.Columns[j].GetCellContent(item)).Text;
                        TableCell tableCell = new TableCell(new Paragraph(new Run(valor)));
                        tableRow.Cells.Add(tableCell);
                    }
                    
                }
                table.RowGroups.Add(rowGroup);

                table.BorderBrush = Brushes.Black;
                table.BorderThickness = new Thickness(1);

                //SUBTOTALES , DESCUENTO Y TOTAL
                Paragraph paragraph3 = new Paragraph();
                paragraph3.Inlines.Add(new Run(factura.v_Subtotal)); paragraph3.Inlines.Add(new LineBreak());
                paragraph3.Inlines.Add(new Run(factura.v_Descuento)); paragraph3.Inlines.Add(new LineBreak());
                paragraph3.Inlines.Add(new Run(factura.v_SubtotalNeto)); paragraph3.Inlines.Add(new LineBreak());
                paragraph3.Inlines.Add(new Run(factura.v_Impuesto)); paragraph3.Inlines.Add(new LineBreak());
                paragraph3.Inlines.Add(new Run(factura.v_Total)); paragraph3.Inlines.Add(new LineBreak());
                paragraph3.TextAlignment = TextAlignment.Right;

                //CREDITO
                Paragraph paragraph4 = new Paragraph();
                paragraph4.Inlines.Add(new Run(factura.v_Codigo.ToString() + "      " + fechaAct.ToString("dd/MM/yyyy"))); paragraph4.Inlines.Add(new LineBreak());
                if(Convert.ToInt32(factura.v_diasCredito) > 0)
                {
                    paragraph4.Inlines.Add(new Run("Credito a " + factura.v_diasCredito + " dias" + "           " + "Vence " + factura.v_fechaPago));
                }
                paragraph4.TextAlignment = TextAlignment.Center;

                //HORA DE EMISION
                Paragraph paragraph5 = new Paragraph();
                paragraph5.Inlines.Add("Fecha Emision: " + fechaAct.ToString("dddd, dd MMMM yyyy, HH:mm:ss"));
                paragraph5.TextAlignment = TextAlignment.Right;


                //AGREGAMOS CADA PARTE AL DOCUMENTO
                FlowDocument document = new FlowDocument();
                document.Blocks.Add(paragraph);
                document.Blocks.Add(paragraph1);
                document.Blocks.Add(paragraph2);
                document.Blocks.Add(table);
                document.Blocks.Add(paragraph3);
                document.Blocks.Add(paragraph4);
                document.Blocks.Add(paragraph5);

                document.ColumnWidth = 700;
                

                //AGREGAMOS UN CONTROLADOR AL DOCUMENTO
                FlowDocumentReader myFlowDocumentReader = new FlowDocumentReader();
                myFlowDocumentReader.Document = document;


                //UBICAMOS EL GUARDADO O CONFIRMAMOS LA IMPRESION
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "");
                MessageBoxResult v_Result = MessageBox.Show("Listo.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            



        }
    }
}