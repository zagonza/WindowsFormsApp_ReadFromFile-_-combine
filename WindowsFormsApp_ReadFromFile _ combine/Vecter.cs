using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsFormsApp_ReadFromFile___combine
{
    class Vecter
    {
        List<List<DataRecord>> Data;
        public Vecter(List<List<DataRecord>> Data)
        {
            this.Data = Data;
        }

        public DataRecord GetDataFromPosition(int i)
        {
            int j = 1;
            DataRecord output = new DataRecord();
            foreach (List<DataRecord> d in Data)
            {
                foreach(DataRecord dd in d)
                {
                    if(j==i)
                    {
                        output = dd;
                        goto aa;
                    }
                    j++;
                }
            }
            aa:
            return output;
        }

        public List<List<DataRecord>> get_Data()
        {
            return Data;
        }

        public int Count()
        {
            int j = 0;
            DataRecord output = new DataRecord();
            foreach (List<DataRecord> d in Data)
            {
                foreach (DataRecord dd in d)
                {
                    j++;
                    //Debug.WriteLine(dd.work + "   " + j);
                    
                }
                //Debug.WriteLine("");
            }
            return j;
        }
    }
}
