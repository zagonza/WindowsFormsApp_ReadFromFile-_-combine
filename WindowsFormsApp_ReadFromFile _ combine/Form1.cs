using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp_ReadFromFile___combine
{
    public partial class Form1 : Form
    {
        List<Vecter> VV;
        List<List<List<DataRecord>>> Vector;
        List<List<List<DataRecord>>> Last_Result;
        List<List<List<DataRecord>>> Vector2;
        List<List<int>> RandomVector;
        Combine c;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox1.Clear();
            textBox3.Clear();
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                MessageBox.Show(file);
                Flow flow = new Flow(file);
                List<DataRecord> LDR = flow.getList();
                PrintTree(LDR[0]);
                //flow.Combination(17);
                flow.Combination(Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)));

                foreach (List<DataRecord> a in flow.getOneMember())
                {
                    flow.printlist(a);
                }

                c = new Combine(flow.getOneMember(), flow.getResultListExcpOne());
                Last_Result = c.getResult();
                int i = 1;
                Debug.WriteLine("");

                foreach (List<List<DataRecord>> a in Last_Result)
                {
                    Debug.Write(i + " total Work : " + a.Count + "  | ");
                    textBox3.Text += i + " total Work : " + a.Count + "  | ";
                    foreach (List<DataRecord> LL in a)
                    {
                        textBox3.Text += c.printlist(LL);

                    }
                    textBox3.AppendText(Environment.NewLine);
                    Debug.WriteLine("");
                    i++;
                }

                textBox2.Text = flow.combine_text;
                numericUpDown1.Maximum = Last_Result.Count;
            }

        }
        public void PrintTree(DataRecord DR)
        {
            List<DataRecord> firstStack = new List<DataRecord>();
            firstStack.Add(DR);

            List<List<DataRecord>> childListStack = new List<List<DataRecord>>();
            childListStack.Add(firstStack);
            while (childListStack.Count > 0)
            {

                List<DataRecord> childStack = childListStack[childListStack.Count - 1];

                if (childStack.Count == 0)
                {
                    childListStack.RemoveAt(childListStack.Count - 1);
                }
                else
                {

                    DR = childStack[0];
                    childStack.RemoveAt(0);

                    string indent = "";
                    for (int i = 0; i < childListStack.Count - 1; i++)
                    {
                        indent += (childListStack[i].Count > 0) ? "|  " : "   ";
                    }

                    string a = indent + "+- " + DR.work + "(" + DR.time.ToString() + ")";
                    textBox1.AppendText(a);
                    textBox1.AppendText(Environment.NewLine);
                    if (DR.get_After().Count > 0)
                    {
                        childListStack.Add(new List<DataRecord>(DR.get_After()));
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            Vector = new List<List<List<DataRecord>>>();

            List<List<int>> sort1 = new List<List<int>>();
            List<List<int>> sort2 = new List<List<int>>();
            for (int i = 0; i <= Convert.ToInt32(numericUpDown1.Value) - 1; i++)
            {
                Vector.Add(Last_Result[i]);
            }
            foreach (List<List<DataRecord>> a in Vector)
            {
                List<int> sortt = new List<int>();
                foreach (List<DataRecord> LL in a)
                {

                    int lowest_price = LL.Min(car => car.get_index());
                    sortt.Add(lowest_price);
                }
                sort1.Add(sortt);
            }

            foreach (List<int> a in sort1)
            {
                foreach (int aa in a)
                {
                    Debug.Write(aa + " ");
                }
                Debug.WriteLine("");
            }
            /*
            var data = new List<int>();
            var rankings = data.OrderByDescending(x => x)
                               .GroupBy(x => x)
                               .SelectMany((g, i) =>
                                   g.Select(ee => new { Col1 = ee, Rank = i + 1 }))
                               .ToList();*/
            //List<List<List<DataRecord>>> Vector3 = new List<List<List<DataRecord>>>();
            foreach (List<int> a in sort1)
            {
                var data = a;
                var rankings = data.OrderBy(x => x).GroupBy(x => x)
                                   .SelectMany((g, i) =>
                                       g.Select(ee => new { Col1 = ee, Rank = i + 1 }))
                                   .ToList();
                Debug.WriteLine("");
                List<int> sort2buffer = new List<int>();
                //int AA in a
                foreach (int AA in a)
                {
                    //Debug.WriteLine(aa.Col1 + " " + aa.Rank);
                    foreach (var aa in rankings)
                    {
                        if (aa.Col1 == AA)
                        {
                            sort2buffer.Add(aa.Rank);
                        }
                    }


                }
                sort2.Add(sort2buffer);
                Debug.WriteLine("");
            }



            Vector2 = new List<List<List<DataRecord>>>();
            for (int ii = 0; ii <= Vector.Count - 1; ii++)
            {
                List<int> qqq = sort2[ii];
                List<List<DataRecord>> r = Vector[ii];
                List<List<DataRecord>> rr = new List<List<DataRecord>>();
                for (int i = 0; i <= r.Count - 1; i++)
                {

                    for (int jj = 0; jj <= qqq.Count - 1; jj++)
                    {
                        if (qqq[jj] == i + 1)
                        {
                            Debug.WriteLine("Found i = " + i + " index is " + jj);
                            rr.Add(r[jj]);
                        }
                    }
                }
                Vector2.Add(rr);
            }



            int j = 1;
            foreach (List<List<DataRecord>> a in Vector2)
            {
                //Debug.Write(j + " total Work : " + a.Count + "  | ");
                textBox4.Text += j + " total Work : " + a.Count + "  | ";
                foreach (List<DataRecord> LL in a)
                {
                    textBox4.Text += c.printlist(LL);

                }
                textBox4.AppendText(Environment.NewLine);
                Debug.WriteLine("");
                j++;
            }

            foreach (List<int> yy in sort2)
            {
                foreach (int y in yy)
                {
                    Debug.Write(y + " ");
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Random random = new Random();
            foreach (List<List<DataRecord>> a in Vector2)
            {
                
                foreach (List<DataRecord> LL in a)
                {
                    
                    foreach (DataRecord L in LL)
                    {
                        
                        L.set_TargetVecter(Math.Round(random.NextDouble() * (1 - 0) + 0, 2));
                    }
                }
            }

            DataTable DT = new DataTable();
            DT.Columns.Add("Vecter");
            DT.Columns.Add("Station");
            DT.Columns.Add("Work");
            DT.Columns.Add("Time");
            DT.Columns.Add("Targer Vector");
            int V_num = 1;
            foreach (List<List<DataRecord>> a in Vector2)
            {
                int S_num = 1;
                foreach (List<DataRecord> LL in a)
                {
                    foreach (DataRecord L in LL)
                    {
                        DT.Rows.Add(V_num, S_num, L.work, L.time, L.get_TargetVecter());
                    }
                    S_num++;
                }
                V_num++;
            }
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dataGridView1.DataSource = DT;

            //Initial List<Vecter> for Cal Mutant
            VV = new List<Vecter>();
            foreach (List<List<DataRecord>> LLD in Vector2)
            {
                VV.Add(new Vecter(LLD));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var data = new List<int> { 4, 6, 3, 2, 1 };
            var rankings = data.GroupBy(x => x)
                               .SelectMany((g, i) =>
                                   g.Select(ee => new { Col1 = ee, Rank = i + 1 }))
                               .ToList();
            foreach (var a in rankings)
            {
                Debug.WriteLine(a.Col1 + " " + a.Rank);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            List<List<int>> myList = new List<List<int>>();
            myList.Add(new List<int> { 1, 4, 5 });
            myList.Add(new List<int> { 2, 3, 5 });
            myList.Add(new List<int> { 1, 3, 5 });
            List<int> mytest = new List<int> { 2, 4, 5 };


            Random rr = new Random();
            RandomVector = new List<List<int>>();
            while (RandomVector.Count != numericUpDown1.Value)
            {
                List<int> randomList = new List<int>();
                //for (int j = 0; j <= 3 - 1; j++)
                //{
                while (randomList.Count != 3)
                {
                    int MyNumber = 0;
                    MyNumber = rr.Next(1, 6);
                    //Debug.Write(MyNumber + " ");
                    if (!randomList.Contains(MyNumber))
                    {
                        randomList.Add(MyNumber);

                    }

                }
                randomList.Sort();
                RandomVector.Add(randomList);
            }

            DataTable XrN_table = new DataTable();
            XrN_table.Columns.Add("V id");
            XrN_table.Columns.Add("Xr1");
            XrN_table.Columns.Add("Xr2");
            XrN_table.Columns.Add("Xr3");
            int jjj = 1;
            foreach (List<int> a in RandomVector)
            {                
                XrN_table.Rows.Add(jjj,a[0],a[1],a[2]);
                jjj++;
            }
            dataGridView2.DataSource = XrN_table;
        }

        private bool CheckMutation(List<List<int>> Source ,List<int> ck)
        {
            bool output = false;
            foreach (List<int> SS in Source)
            {
                //Debug.WriteLine("s " + SS[0] + SS[1] + SS[2]);

                if (SS[0] == ck[0]&& SS[1] == ck[1]&& SS[2] == ck[2])
                {
                    output = true;
                    break;
                }
            }
            return output;
        }

       
        
        double F;
        private void button8_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            F = Math.Round(random.NextDouble() * (2 - 0) + 0, 2);
            label3.Text = "F : "+F.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string test = "";
            for(int i = 0;i<= RandomVector.Count-1;i++)
            {
                for(int j = 0;j<= VV[0].Count()-1;j++)
                {
                    double xr1 = VV[RandomVector[i][0] - 1].GetDataFromPosition(j + 1).get_TargetVecter();
                    double xr2 = VV[RandomVector[i][1] - 1].GetDataFromPosition(j + 1).get_TargetVecter();
                    double xr3 = VV[RandomVector[i][2] - 1].GetDataFromPosition(j + 1).get_TargetVecter();
                    double mutant = xr1 + (F * (xr2 - xr3));
                    mutant = Math.Round(mutant, 3);
                    test += "index: " + (j+1) + " Xr1 : " + RandomVector[i][0] + " Value : " + xr1 + " ";
                    test += "| Xr2 : " + RandomVector[i][1] + " Value : " + xr2 + " ";
                    test += "| Xr3 : " + RandomVector[i][2] + " Value : " + xr3 + "\t";
                    test += "| Mutation : " + mutant;
                    test += "| i : " + i + " j : " + j;
                    VV[i].GetDataFromPosition(j+1).setMutant(mutant);
                    test += Environment.NewLine;
                }
                test += Environment.NewLine;
            }
            textBox5.Text = test;



            DataTable data = new DataTable();
            data.Columns.Add("Vector");
            data.Columns.Add("Position");
            data.Columns.Add("work");
            data.Columns.Add("time");
            data.Columns.Add("Target Vector");
            data.Columns.Add("mutant");

            /*
             int V_num = 1;
            foreach (List<List<DataRecord>> a in Vector2)
            {
                int S_num = 1;
                foreach (List<DataRecord> LL in a)
                {
                    foreach (DataRecord L in LL)
                    {
                        DT.Rows.Add(V_num, S_num, L.work, L.time, L.get_TargetVecter());
                    }
                    S_num++;
                }
                V_num++;
            }
            */
            int Vector = 1;
            foreach (Vecter V in VV)
            {
                int position = 1;
                foreach (List<DataRecord> lldr in V.get_Data())
                {
                    
                    foreach (DataRecord L in lldr)
                    {
                        data.Rows.Add(Vector,position, L.work,L.time,L.get_TargetVecter(),L.getMutant());
                        position++;
                    }
                    
                }
                Vector++;
            }
            dataGridView3.DataSource = data;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            Vector = new List<List<List<DataRecord>>>();

            List<List<int>> sort1 = new List<List<int>>();
            List<List<int>> sort2 = new List<List<int>>();
            for (int i = 0; i <= Convert.ToInt32(numericUpDown1.Value) - 1; i++)
            {
                //Vector.Add(Last_Result[i]);
                List<List<DataRecord>> LLdr = new List<List<DataRecord>>();
                foreach (List<DataRecord> aaa in Last_Result[i])
                {
                    //Debug.Write("{");
                    List<DataRecord> Ldr = new List<DataRecord>();
                    foreach (DataRecord aa in aaa)
                    {
                        //Debug.Write(aa.work + ",");
                        Ldr.Add((DataRecord)aa.Clone());
                        Debug.WriteLine(Ldr.Count);
                    }
                    LLdr.Add(Ldr);
                    //Debug.Write("}");
                }
                Vector.Add(LLdr);
                //Debug.WriteLine("");
            }
            foreach (List<List<DataRecord>> a in Vector)
            {
                List<int> sortt = new List<int>();
                foreach (List<DataRecord> LL in a)
                {

                    int lowest_price = LL.Min(car => car.get_index());
                    sortt.Add(lowest_price);
                }
                sort1.Add(sortt);
            }

            foreach (List<int> a in sort1)
            {
                foreach (int aa in a)
                {
                    Debug.Write(aa + " ");
                }
                Debug.WriteLine("");
            }
            /*
            var data = new List<int>();
            var rankings = data.OrderByDescending(x => x)
                               .GroupBy(x => x)
                               .SelectMany((g, i) =>
                                   g.Select(ee => new { Col1 = ee, Rank = i + 1 }))
                               .ToList();*/
            //List<List<List<DataRecord>>> Vector3 = new List<List<List<DataRecord>>>();
            foreach (List<int> a in sort1)
            {
                var data = a;
                var rankings = data.OrderBy(x => x).GroupBy(x => x)
                                   .SelectMany((g, i) =>
                                       g.Select(ee => new { Col1 = ee, Rank = i + 1 }))
                                   .ToList();
                Debug.WriteLine("");
                List<int> sort2buffer = new List<int>();
                //int AA in a
                foreach (int AA in a)
                {
                    //Debug.WriteLine(aa.Col1 + " " + aa.Rank);
                    foreach (var aa in rankings)
                    {
                        if (aa.Col1 == AA)
                        {
                            sort2buffer.Add(aa.Rank);
                        }
                    }


                }
                sort2.Add(sort2buffer);
                Debug.WriteLine("");
            }



            Vector2 = new List<List<List<DataRecord>>>();
            for (int ii = 0; ii <= Vector.Count - 1; ii++)
            {
                List<int> qqq = sort2[ii];
                List<List<DataRecord>> r = Vector[ii];
                List<List<DataRecord>> rr = new List<List<DataRecord>>();
                for (int i = 0; i <= r.Count - 1; i++)
                {

                    for (int jj = 0; jj <= qqq.Count - 1; jj++)
                    {
                        if (qqq[jj] == i + 1)
                        {
                            Debug.WriteLine("Found i = " + i + " index is " + jj);
                            rr.Add(r[jj]);
                        }
                    }
                }
                Vector2.Add(rr);
            }



            int j = 1;
            foreach (List<List<DataRecord>> a in Vector2)
            {
                //Debug.Write(j + " total Work : " + a.Count + "  | ");
                textBox4.Text += j + " total Work : " + a.Count + "  | ";
                foreach (List<DataRecord> LL in a)
                {
                    textBox4.Text += c.printlist(LL);

                }
                textBox4.AppendText(Environment.NewLine);
                Debug.WriteLine("");
                j++;
            }

            foreach (List<int> yy in sort2)
            {
                foreach (int y in yy)
                {
                    Debug.Write(y + " ");
                }
                Debug.WriteLine("");
            }

            Debug.WriteLine("");
        }
        double cr;
        private void button2_Click_1(object sender, EventArgs e)
        {
            Random random = new Random();
            cr = Math.Round(random.NextDouble() * (1 - 0) + 0, 2);
            label4.Text = "CR : " + cr.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            foreach (Vecter V in VV)
            {
                
                foreach (List<DataRecord> lldr in V.get_Data())
                {
                    
                    foreach (DataRecord L in lldr)
                    {                       
                        L.set_rand_j(Math.Round(random.NextDouble() * (1 - 0) + 0,2));
                    }
                }
            }

            foreach (Vecter V in VV)
            {
                foreach (List<DataRecord> lldr in V.get_Data())
                {

                    foreach (DataRecord L in lldr)
                    {
                        Debug.Write(L.work + " rand : " + L.get_rand_j().ToString() + " ");
                        if(L.get_rand_j() <= cr)
                        {
                            L.set_Trial_vector(L.getMutant());
                        }
                        else
                        {
                            L.set_Trial_vector(L.get_rand_j());
                        }
                    }
                }
                Debug.WriteLine("");
            }


            DataTable data = new DataTable();
            data.Columns.Add("Vector");
            data.Columns.Add("Position");
            data.Columns.Add("Station");
            data.Columns.Add("work");
            data.Columns.Add("time");
            data.Columns.Add("Target Vector");
            data.Columns.Add("mutant");
            data.Columns.Add("rand j");
            data.Columns.Add("Trial Vector");

            int Vector = 1;
            foreach (Vecter V in VV)
            {
                int position = 1;
                int station = 1;
                foreach (List<DataRecord> lldr in V.get_Data())
                {

                    foreach (DataRecord L in lldr)
                    {
                        data.Rows.Add(Vector, position,station, L.work, L.time, L.get_TargetVecter(), L.getMutant(),L.get_rand_j(),L.get_Trial_vector());
                        position++;
                    }
                    station++;
                }                
                Vector++;
                Debug.WriteLine("Vector : " + Vector +" station : " + (station-1) + " Count :"+V.get_Data().Count);
                if (station < min_station)
                {
                    min_station = station;
                }
            }
            Debug.WriteLine("mins is " + (min_station-1));
            dataGridView4.DataSource = data;

             data2 = new DataTable();
            data2.Columns.Add("Vector");
            data2.Columns.Add("Station");
            data2.Columns.Add("work");
            data2.Columns.Add("time");
            data2.Columns.Add("Trial Vector");


            int Vectorr = 1;
            foreach (Vecter V in VV)
            {
                if (V.get_Data().Count == min_station - 1)
                {
                    int station = 1;
                    foreach (List<DataRecord> lldr in V.get_Data())
                    {

                        foreach (DataRecord L in lldr)
                        {
                            data2.Rows.Add(Vectorr, station, L.work, L.time, L.get_Trial_vector());
                        }
                        station++;

                    }
                }
                Vectorr++;
            }
            dataGridView5.DataSource = data2;
        }
        int min_station = 100;
        DataTable data2;
        private void button12_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

           
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;
            using (var textWriter = File.CreateText(name))
            using (var csv = new CsvWriter(textWriter))
            {
                // Write columns
                foreach (DataColumn column in data2.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write row values
                foreach (DataRow row in data2.Rows)
                {
                    for (var i = 0; i < dataGridView5.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
        }

    }
}
