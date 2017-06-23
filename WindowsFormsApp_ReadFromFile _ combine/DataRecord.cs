using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApp_ReadFromFile___combine
{
    public class DataRecord : ICloneable
    {
        double Trial_vector;
        double rand_j;
        double mutant;
        int index;
        double target_vecter { get; set; }
        int station { get; set; }
        public bool thisfirst = false;
        public String work { get; set; }
        public String work_before { get; set; }
        public int time { get; set; }
        private List<DataRecord> prev = null;
        private List<DataRecord> after = null;
        public List<string> L_prev = new List<string>();

        public DataRecord ()
        {
            if(work_before == "-")
            {
                prev = null;
            }
            else
            {
                prev = new List<DataRecord>();
            }
            //prev = new List<DataRecord>();
            after = new List<DataRecord>();
        }

        public void set_Before(DataRecord before)
        {
            this.prev.Add(before);
        }

        public void set_After(DataRecord after)
        {
            this.after.Add(after);
        }

        public void set_index(int index)
        {
            this.index = index;
        }

        public int get_index()
        {
            return this.index;
        }

        public void set_Station(int num_station)
        {
            this.station = num_station;
        }

        public int get_Station()
        {
            return this.station;
        }

        public void set_Trial_vector(double trial_vector)
        {
            this.Trial_vector = trial_vector;
        }

        public double get_Trial_vector()
        {
            return this.Trial_vector;
        }

        public void set_rand_j(double rand)
        {
            this.rand_j = rand;
        }

        public double get_rand_j()
        {
            return this.rand_j;
        }

        public void set_TargetVecter(double target_vecter)
        {
            this.target_vecter = target_vecter;
        }

        public double get_TargetVecter()
        {
            return this.target_vecter;
        }

        public List<DataRecord> get_Before()
        {
            return this.prev;
        }

        public List<DataRecord> get_After()
        {
            return this.after;
        }

        public void setMutant(double mu)
        {
            this.mutant = mu;
        }

        public double getMutant()
        {
            return this.mutant;
        }

        public Boolean hasbefore()
        {
            Boolean output = false;
            if(this.prev != null)
            {
                output = true;
            }
            else
            {
                output = false;
            }
            return output;
        }

        public Boolean hasafter()
        {
            Boolean output = false;
            if (this.after != null)
            {
                output = true;
            }
            else
            {
                output = false;
            }
            return output;
        }

        public void intial_L_perv()
        {
            if (work_before.IndexOf("-") != -1)
            {
                //MessageBox.Show(this.work + " Has -");
                thisfirst = true;
                L_prev.Add("-");
            }
            else if (work_before.IndexOf(",") != -1)
            {
                L_prev = work_before.Split(',').ToList();
                /*MessageBox.Show("Has ," + L_prev.Count);
                foreach(string a in L_prev)
                {
                    MessageBox.Show("Has , member is " + a);
                }*/
            }
            else
            {
                L_prev.Add(work_before);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
