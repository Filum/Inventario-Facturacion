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
using System.Windows.Media.Imaging;
using System.IO;

namespace Logica
{
    public class Imprimir
    {
        Model datos = new Model();
        public Imprimir()
        {

        }
        public void imprimir(DataGrid dataGrid, string title)
        {

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument fd = new FlowDocument();

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("DELRAM S.A."))); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("DELGADO RAMOS SOCIEDAD ANONIMA")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("(506)2431 0896    (506)2443 9149")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("delram@delramtech.com    3101390765"));
                paragraph.TextAlignment = TextAlignment.Left;

                //Estilo de la letra, tamaño
                Paragraph p = new Paragraph(new Bold(new Run(title)));
                p.FontStyle = dataGrid.FontStyle;
                p.FontFamily = dataGrid.FontFamily;
                p.FontSize = 20;
                p.TextAlignment = TextAlignment.Center;



                Table table = new Table();
                TableRowGroup tableRowGroup = new TableRowGroup();
                TableRow r = new TableRow();
                fd.PageWidth = printDialog.PrintableAreaHeight;
                fd.PageHeight = printDialog.PrintableAreaWidth;
                fd.BringIntoView();

                //alineamiento 
                //fd.TextAlignment = TextAlignment.Center;
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
                    r.Cells[j].BorderThickness = new Thickness(1);
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
                    table.BorderThickness = new Thickness(1, 1, 1, 1);
                    table.FontStyle = dataGrid.FontStyle;
                    table.FontFamily = dataGrid.FontFamily;
                    table.FontSize = 13;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {

                        if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                        {
                            r.Cells.Add(new TableCell(new Paragraph(new Run(row.GetType().GetProperty(bindList[j]).GetValue(row)))));

                        }
                        else
                        {
                            var valor = ((TextBlock)dataGrid.Columns[j].GetCellContent(row)).Text;
                            r.Cells.Add(new TableCell(new Paragraph(new Run(valor))));

                        }

                        r.Cells[j].ColumnSpan = 7;
                        r.Cells[j].Padding = new Thickness(4);

                        r.Cells[j].BorderBrush = Brushes.DarkGray;
                        r.Cells[j].BorderThickness = new Thickness(0, 1, 1, 0);
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);

                }
                fd.Blocks.Add(paragraph);
                fd.Blocks.Add(p); 
                fd.Blocks.Add(table);


                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
                MessageBoxResult v_Result = MessageBox.Show("La acción se realizó con éxito.", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        public void imprimirFactura(DataGrid dataGrid, Entidades.EntidadFacturas factura, DateTime fechaAct, Entidades.EntidadClientes cliente,string moneda, Entidades.EntidadDetalleFactura detalle)
        {
            string tipoFactura = datos.tipoFactura(factura.v_Codigo.ToString());
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
               
                //ENCABEZADO
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("DELRAM S.A."))); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("DELGADO RAMOS SOCIEDAD ANONIMA")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("(506)2431 0896    (506)2443 9149")); paragraph.Inlines.Add(new LineBreak());
                paragraph.Inlines.Add(new Run("delram@delramtech.com    3101390765"));

                //CODIGO Y FECHA
                Paragraph paragraph1 = new Paragraph();
                paragraph1.TextAlignment = TextAlignment.Right;
                paragraph1.Inlines.Add(new Bold(new Run("Número Factura:"+ factura.v_Codigo.ToString()))); paragraph1.Inlines.Add(new LineBreak());
                paragraph1.Inlines.Add(new Run("Fecha: "+fechaAct.ToString("dddd, dd MMMM yyyy")));
                

                //CLIENTE
                Paragraph paragraph2 = new Paragraph();
                paragraph2.BorderBrush = Brushes.Black;
                paragraph2.BorderThickness = new Thickness(1);
                paragraph2.Inlines.Add(new Run("Cliente: " + "      " + cliente.v_Codigo+"             "));
                paragraph2.Inlines.Add(new Run("                                 Cédula: " + "      " + cliente.v_Cedula)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Bold(new Run("                    "+cliente.v_NombreCompleto))); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run("                     "+cliente.v_Representante)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run("Dirección: "+cliente.v_Direccion)); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run("Teléfono:   (506) "+cliente.v_Teleoficina.ToString() + "                          (506) " + cliente.v_Telemovil.ToString())); paragraph2.Inlines.Add(new LineBreak());
                paragraph2.Inlines.Add(new Run("Correo:       "+cliente.v_Correo));
                paragraph2.Margin = new Thickness(0, 0, 250, 0);

                //MONEDA
                Paragraph paragraph3 = new Paragraph();
                paragraph3.Inlines.Add(new Run("Factura en: "+factura.v_Moneda)); paragraph3.Inlines.Add(new Run("       Tipo cambio: " + factura.v_tipoCambio));
                paragraph3.TextAlignment = TextAlignment.Right;


                //TABLA DETALLES
                Table table = new Table();
                TableRowGroup rowGroup = new TableRowGroup();
                if (tipoFactura == "Productos")
                {                   
                    TableRow row = new TableRow();
                    //HEADERS
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Tipo"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Código Producto"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Descripción Producto"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Cantidad"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Precio Unitario"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("% Descuento"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("% IV"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Total Linea"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Moneda"))));

                    row.Background = Brushes.LightGray;
                    for (int n = 0; n < row.Cells.Count; n++)
                    {
                        row.Cells[n].BorderThickness = new Thickness(0, 0, 1, 0);
                        row.Cells[n].BorderBrush = Brushes.Black;
                    }

                    row.FontSize = 10;

                    rowGroup.Rows.Add(row);


                    //AGREGA VALORES DEL DATAGRID A LA TABLA DEL DOCUMENTO...
                    foreach (var item in dataGrid.Items)
                    {
                        DataGridRow gridRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                        TableRow tableRow = new TableRow();
                        rowGroup.Rows.Add(tableRow);

                        for (int j = 0; j < dataGrid.Columns.Count; j++)
                        {
                            var valor = ((TextBlock)dataGrid.Columns[j].GetCellContent(item)).Text;
                            TableCell tableCell = new TableCell(new Paragraph(new Run(valor)));
                            tableCell.BorderBrush = Brushes.Black;
                            tableCell.BorderThickness = new Thickness(0, 1, 1, 0);

                            tableRow.Cells.Add(tableCell);
                        }
                        if (moneda != "")
                        {
                            TableCell tableCell2 = new TableCell(new Paragraph(new Run(moneda)));
                            tableCell2.BorderBrush = Brushes.Black;
                            tableCell2.BorderThickness = new Thickness(0, 1, 1, 0);
                            tableRow.Cells.Add(tableCell2);
                        }

                    }
                    table.RowGroups.Add(rowGroup);


                    table.BorderBrush = Brushes.Black;
                    table.BorderThickness = new Thickness(1.5);
                    table.TextAlignment = TextAlignment.Center;

                } else
                {
                    TableRow row = new TableRow();
                    //HEADERS
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Descripción del servicio realizado: "))));
                    row.Background = Brushes.LightGray;
                    rowGroup.Rows.Add(row);
                    TableRowGroup rowGroup9 = new TableRowGroup();
                    TableRow row9 = new TableRow();
                    row9.Cells.Add(new TableCell(new Paragraph(new Run(datos.Descripcion_servicio(factura.v_Codigo.ToString())))));
                    rowGroup.Rows.Add(row9);
                    table.RowGroups.Add(rowGroup);
                    table.BorderBrush = Brushes.Black;
                    table.BorderThickness = new Thickness(1);
                }
                //SUBTOTALES , DESCUENTO Y TOTAL
                //TABLA DETALLES
                 Table table2 = new Table();

                 TableRowGroup rowGroup2 = new TableRowGroup();
                 TableRow row2 = new TableRow();
                if (tipoFactura == "Servicios")
                {
                    TableRow row10 = new TableRow();
                    row10.Cells.Add(new TableCell(new Paragraph(new Run("Cantidad horas: "))));
                    row10.Cells.Add(new TableCell(new Paragraph(new Run(detalle.cantidad.ToString()))));
                    for (int n = 0; n < row10.Cells.Count; n++)
                    {
                        row10.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                        row10.Cells[n].BorderBrush = Brushes.Black;
                    }
                    TableRow row11 = new TableRow();
                    row11.Cells.Add(new TableCell(new Paragraph(new Run("Precio horas: "))));
                    row11.Cells.Add(new TableCell(new Paragraph(new Run(detalle.precioProducto))));
                    for (int n = 0; n < row11.Cells.Count; n++)
                    {
                        row11.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                        row11.Cells[n].BorderBrush = Brushes.Black;
                    }
                    rowGroup2.Rows.Add(row10);
                    rowGroup2.Rows.Add(row11);
                }
                 row2.Cells.Add(new TableCell(new Paragraph(new Run("Subtotal: "))));
                 row2.Cells.Add(new TableCell(new Paragraph(new Run(factura.v_Subtotal))));
                for (int n = 0; n < row2.Cells.Count; n++)
                {
                    row2.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                    row2.Cells[n].BorderBrush = Brushes.Black;
                }
                TableRow row3 = new TableRow();
                 row3.Cells.Add(new TableCell(new Paragraph(new Run("Descuento: "))));
                 row3.Cells.Add(new TableCell(new Paragraph(new Run(factura.v_Descuento))));
                for (int n = 0; n < row3.Cells.Count; n++)
                {
                    row3.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                    row3.Cells[n].BorderBrush = Brushes.Black;
                }
                TableRow row4 = new TableRow();
                 row4.Cells.Add(new TableCell(new Paragraph(new Run("Subtotal Neto: "))));
                 row4.Cells.Add(new TableCell(new Paragraph(new Run(factura.v_SubtotalNeto))));
                 TableRow row5 = new TableRow();
                for (int n = 0; n < row4.Cells.Count; n++)
                {
                    row4.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                    row4.Cells[n].BorderBrush = Brushes.Black;
                }
                row5.Cells.Add(new TableCell(new Paragraph(new Run("Impuesto: "))));
                 row5.Cells.Add(new TableCell(new Paragraph(new Run(factura.v_Impuesto))));
                for (int n = 0; n < row5.Cells.Count; n++)
                {
                    row5.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                    row5.Cells[n].BorderBrush = Brushes.Black;
                }
                TableRow row6 = new TableRow();
                row6.Cells.Add(new TableCell(new Paragraph(new Run("Total: "))));
                row6.Cells.Add(new TableCell(new Paragraph(new Run(factura.v_Total))));
                for (int n = 0; n < row6.Cells.Count; n++)
                {
                    row6.Cells[n].BorderThickness = new Thickness(0, 1, 1, 0);
                    row6.Cells[n].BorderBrush = Brushes.Black;
                }

                table2.BorderBrush = Brushes.Black;
                table2.BorderThickness = new Thickness(1);
                table2.TextAlignment = TextAlignment.Center;
                table2.Margin = new Thickness(480, 0, 0, 0);
                rowGroup2.Rows.Add(row2);
                rowGroup2.Rows.Add(row3);
                rowGroup2.Rows.Add(row4);
                rowGroup2.Rows.Add(row5);
                rowGroup2.Rows.Add(row6);
                table2.RowGroups.Add(rowGroup2);

                //CREDITO
                Paragraph paragraph5 = new Paragraph();
                if(factura.v_diasCredito != "" )
                {
                    if(int.Parse(factura.v_diasCredito) > 0)
                        paragraph5.Inlines.Add(new Run("Credito a " + factura.v_diasCredito + " dias" + "           " + "Vence " + factura.v_fechaPago));
                }
                paragraph5.TextAlignment = TextAlignment.Left;

                //HORA DE EMISION
                Paragraph paragraph6 = new Paragraph();
                paragraph6.Inlines.Add("Fecha Emisión: " + fechaAct.ToString("dddd, dd MMMM yyyy, HH:mm:ss"));
                paragraph6.TextAlignment = TextAlignment.Right;
                paragraph6.Margin = new Thickness(400,0,0,0);
                paragraph6.Background = Brushes.LightGray;

                //HORA DE RECIBIDO
                Paragraph paragraph7 = new Paragraph();
                paragraph7.Inlines.Add("Recibido Conforme: "); paragraph7.Inlines.Add(new LineBreak()); paragraph7.Inlines.Add(new LineBreak());
                paragraph7.Inlines.Add(new LineBreak());
                paragraph7.TextAlignment = TextAlignment.Left;

                //HORA DE FIRMAS
                Paragraph paragraph8 = new Paragraph();
                paragraph8.Inlines.Add("Nombre                                             Número de Cédula                               Firma");
                paragraph8.Inlines.Add(new LineBreak());
                paragraph8.Inlines.Add(new LineBreak());
                paragraph8.Inlines.Add(new LineBreak());
                paragraph8.Inlines.Add(new LineBreak());
                paragraph8.TextAlignment = TextAlignment.Center;
                paragraph8.BorderBrush = Brushes.Black;
                paragraph8.BorderThickness = new Thickness(0, 1, 0, 0);


                Paragraph paragraph9 = new Paragraph();
                paragraph9.Inlines.Add("Cuenta Corriente en Colones                                                  Cuenta Corriente en US$"); paragraph9.Inlines.Add(new LineBreak()); paragraph9.Inlines.Add(new LineBreak());
                paragraph9.Inlines.Add("        Scotiabank de Costa Rica S.A.                                                Scotiabank de Costa Rica S.A."); paragraph9.Inlines.Add(new LineBreak());
                paragraph9.Inlines.Add("            Cuenta Corriente: 13000677200                                           Cuenta Corriente: 13000577201"); paragraph9.Inlines.Add(new LineBreak());
                paragraph9.Inlines.Add("                          Cuenta Cliente: 123001300067772002                                Cuenta Corriente: 12300130006772019"); paragraph9.Inlines.Add(new LineBreak());
                paragraph9.Inlines.Add("                   IBAN: CR9191230013000772002                                         IBAN: CR20012300130006772019"); paragraph9.Inlines.Add(new LineBreak());
                paragraph9.TextAlignment = TextAlignment.Center;
                //AGREGAMOS CADA PARTE AL DOCUMENTO
                FlowDocument document = new FlowDocument();
                //Image image = new Image();
                //image.Source = new BitmapImage(new Uri("Imagenes /img_DELRAM.png"));
                //document.Blocks.Add(new BlockUIContainer(image));
                document.Blocks.Add(paragraph);
                document.Blocks.Add(paragraph1);
                document.Blocks.Add(paragraph2);
                document.Blocks.Add(paragraph3);
                document.Blocks.Add(table);
                document.Blocks.Add(table2);
                document.Blocks.Add(paragraph5);
                document.Blocks.Add(paragraph6);
                document.Blocks.Add(paragraph7);
                document.Blocks.Add(paragraph8);
                document.Blocks.Add(paragraph9);
                document.ColumnWidth = 700;
                

                //AGREGAMOS UN CONTROLADOR AL DOCUMENTO
                FlowDocumentReader myFlowDocumentReader = new FlowDocumentReader();
                myFlowDocumentReader.Document = document;


                //UBICAMOS EL GUARDADO O CONFIRMAMOS LA IMPRESION
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "");
                MessageBoxResult v_Result = MessageBox.Show("La acción se realizó con éxito.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            



        }
    }
}