using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDataGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable tabla = new DataTable("Costumers");

        void cargaTabla() {

            tabla.Columns.Add("ID", typeof(int));
            tabla.Columns.Add("Producto", typeof(string));
            tabla.Columns.Add("Inventario", typeof(int));

            tabla.Columns.Add("Cant_Consumido", typeof(int));
            tabla.Columns.Add("Consumido", typeof(bool));

            tabla.Columns.Add("Cant_Abierto", typeof(int));
            tabla.Columns.Add("Abierto", typeof(bool));

            //tabla.Columns.Add("Folios", typeof(string));
            // Here we add five DataRows.
            //             ID Producto Inventario  Cant_Consumido   Consumido  Cant_Abierto     Abierto
            tabla.Rows.Add(1, "Cola", 0, 0, 1, 0, 0);
            tabla.Rows.Add(1, "Cola", 0, 0, 1, 0, 1);
            tabla.Rows.Add(1, "Cola", 0, 0, 1, 0, 1);

            tabla.Rows.Add(2, "Tequila", 0, 0, 1, 0, 0);
            tabla.Rows.Add(2, "Tequila", 0, 0, 1, 0, 1);
            tabla.Rows.Add(2, "Tequila", 0, 0, 1, 0, 1);
            tabla.Rows.Add(2, "Tequila", 0, 0, 1, 0, 0);

            tabla.Rows.Add(3, "Jugo", 0, 0, 1, 0, 1);
            tabla.Rows.Add(3, "Jugo", 0, 0, 1, 0, 1);

            var t = from row in tabla.AsEnumerable()
                    group row by row.Field<Int32>("ID") into pro
                    orderby pro.Key, pro.Count()
                    select new
                    {
                        Accion = "+",
                        ID = pro.Key,
                        Producto = pro.FirstOrDefault().Field<string>("Producto"),
                        Inventario = pro.Count(),
                        Cant_Consumido = pro.Count(x => x.Field<bool>("Consumido") == true),
                        Cant_Abierto = pro.Count(x => x.Field<bool>("Abierto") == true),
                        //Folios  = pro.FirstOrDefault().Field<String>("Folios")
                    };


            DataTable table = new DataTable();
            table = Utilities.ToDataTable(t.ToList());
            
            tabla.Merge(table, false, MissingSchemaAction.Add);
            
            tabla = orderByTable("ID ASC, Inventario DESC");

            dataGridView1.DataSource = tabla;
        }

        private DataTable orderByTable(String criterio) {
            DataView dv = tabla.DefaultView;
            dv.Sort = criterio;
            return dv.ToTable();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(23, 101, 201);//Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            cargaTabla();
            ocultar();
        }

        void ocultar() {

            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                
                if (dataGridView1.Rows[row].Cells["Accion"].Value != null && !dataGridView1.Rows[row].Cells["Accion"].Value.Equals("+"))
                {
                    dataGridView1.CurrentCell = null;
                    dataGridView1.Rows[row].Visible = false;

                    var cell = ((DataGridViewButtonCell)dataGridView1.Rows[row].Cells["Accion"]);
                     
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Style.ForeColor = Color.DarkGray;
                    cell.ReadOnly = true;

                   
                    //DataGridViewCell cell_ = dataGridView1.Rows[row].Cells[7];
                    //DataGridViewCheckBoxCell chkCell = cell_ as DataGridViewCheckBoxCell;
                    //chkCell.Value = false;
                    //chkCell.FlatStyle = FlatStyle.Flat;
                    //chkCell.Style.ForeColor = Color.DarkGray;
                    //cell.ReadOnly = true;










                }
                else if (dataGridView1.Rows[row].Cells["Accion"].Value != null && dataGridView1.Rows[row].Cells["Accion"].Value.Equals("+")) {
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.Gray;
                    dataGridView1.Rows[row].Cells["Accion"].Style.BackColor = Color.DarkGray;

                  

                    var check = ((DataGridViewCheckBoxCell)dataGridView1.Rows[row].Cells[4]);
                    check.FlatStyle = FlatStyle.Flat;
                    check.Style.ForeColor = Color.DarkGray;
                    check.ReadOnly = true;


                    var check2 = ((DataGridViewCheckBoxCell)dataGridView1.Rows[row].Cells[6]);
                    check2.FlatStyle = FlatStyle.Flat;
                    check2.Style.ForeColor = Color.DarkGray;
                    check2.ReadOnly = true;

                }

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
            if (e.RowIndex != -1 && dataGridView1.Rows[e.RowIndex].Cells[7].Value != null && dataGridView1.Columns[e.ColumnIndex].Name == "Accion")
            {
                int index = e.RowIndex;

                String ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
          
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {

                    //if (dataGridView1.Rows[row].Cells[7].ReadOnly != true)
                    //{
                        if (dataGridView1.Rows[row].Cells[7].Value != null && !dataGridView1.Rows[row].Cells[7].Value.Equals("+"))
                        {
                            if (dataGridView1.Rows[row].Visible == false && dataGridView1.Rows[row].Cells[0].Value.ToString().Equals(ID)
                                && dataGridView1.Rows[row].Cells[7].Style.BackColor != Color.DarkGray)
                            {
                                dataGridView1.CurrentCell = null;
                                dataGridView1.Rows[row].Visible = true;
                                //DataGridViewButtonCell bc = new DataGridViewButtonCell();
                                //bc.FlatStyle = FlatStyle.Flat;
                                //bc.Style.BackColor = Color.Blue;
                                //bc.ReadOnly = true;
                            }
                            else if (dataGridView1.Rows[row].Visible == true && 
                                    dataGridView1.Rows[row].Cells[0].Value.ToString().Equals(ID)
                                    && dataGridView1.Rows[index].Cells[7].Style.BackColor == Color.DarkGray ){ 

                                    dataGridView1.CurrentCell = null;
                                    dataGridView1.Rows[row].Visible = false;
                                    //DataGridViewButtonCell bc = new DataGridViewButtonCell();
                                    //bc.FlatStyle = FlatStyle.Flat;
                                    //bc.Style.BackColor = Color.Blue;
                                    //bc.ReadOnly = true;
                            }
                        }
                    //}
                   
                }
            }
        }
    }
}
