using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsFormsApp_ReadFromFile___combine
{
    class Combine
    {
        public static List<List<List<DataRecord>>> Last_Result = new List<List<List<DataRecord>>>();
        public static List<List<List<DataRecord>>> Last_Result2 = new List<List<List<DataRecord>>>();
        public Combine(List<List<DataRecord>> one_member, List<List<List<DataRecord>>> all_member)
        {
            Last_Result = new List<List<List<DataRecord>>>();
        Last_Result2 = new List<List<List<DataRecord>>>();
        Last_Result.Add(one_member);
            foreach (List<List<DataRecord>> d in all_member)
            {
                foreach (List<DataRecord> a in d)
                {
                    //Console.Write("Test Data : " + a[0] + a[1]);
                    GenMatch(a, d, one_member);
                    List<List<DataRecord>> TTT = new List<List<DataRecord>>();
                    TTT.Add(a);
                    Last_Result.Add(TTT);
                    //Console.WriteLine();
                    
                }
            }
            Last_Result = removeDup2(Last_Result);

            foreach (List<List<DataRecord>> L in Last_Result)
            {
                List<List<DataRecord>> E = new List<List<DataRecord>>();
                foreach (List<DataRecord> e in L)
                {
                    E.Add(e);
                }
                foreach (List<DataRecord> e in Combine_Single(one_member, L))
                {
                    E.Add(e);
                }
                Last_Result2.Add(E);
            }
            Last_Result2.Sort((a, b) => a.Count - b.Count);
        }



        public List<List<List<DataRecord>>> getResult()
        {
            return Last_Result2;
        }

        static public void GenMatch(List<DataRecord> target, List<List<DataRecord>> source, List<List<DataRecord>> single)
        {
            List<List<DataRecord>> buffer = new List<List<DataRecord>>();
            List<List<DataRecord>> buffer2 = new List<List<DataRecord>>();
            List<List<DataRecord>> output = new List<List<DataRecord>>();
            List<List<List<DataRecord>>> Result = new List<List<List<DataRecord>>>();



            foreach (List<DataRecord> ss in source) //create Matching List
            {
                if (!IsDup(target, ss))
                {
                    buffer.Add(ss);
                }
            }

            buffer2.Add(target);

            foreach (List<DataRecord> bb in buffer)
            {

                foreach (List<DataRecord> vv in buffer2.ToList())
                {
                    foreach (List<DataRecord> cc in buffer)
                    {
                        if (!IsDup(vv, cc))
                        {
                            //Console.Write("!dup");
                            //printlist(vv);
                            //Console.Write(":");
                            //printlist(cc);
                            //Console.WriteLine();
                            //buffer2.Add(cc);
                            List<List<DataRecord>> array_list = new List<List<DataRecord>>();
                            array_list.Add(vv);
                            array_list.Add(cc);
                            Result.Add(array_list);
                            buffer2.Clear();
                            //chkNestDup(Result, buffer);
                        }
                    }
                }
                //buffer2.Add(bb);
                //printlist(bb);
            }

            //Console.WriteLine("buffer");
            foreach (List<DataRecord> a in buffer)
            {

                //printlist(a);
            }

            //Console.WriteLine("Result size : " + Result.Count);
            foreach (List<List<DataRecord>> rr in Result)
            {
                List<List<string>> E = new List<List<string>>();
                /*foreach(List<string>e in rr)
                {
                    E.Add(e);
                }
                foreach (List<string> e in Combine_Single(single, rr))
                {
                    E.Add(e);
                }
                Last_Result.Add(E);*/
                Last_Result.Add(rr);
                //Console.WriteLine("Result : ");
                foreach (List<DataRecord> r in rr)
                {

                    //printlist(r);
                }
                //Console.WriteLine();
            }
            //chkNestDup(Result, buffer);
            List<List<List<DataRecord>>> Results = new List<List<List<DataRecord>>>();
            int i = 0;
            foreach (List<List<DataRecord>> rrr in Result)
            {
                List<DataRecord> R = rrr[rrr.Count - 1];

                foreach (List<DataRecord> a in buffer)
                {
                    if (!IsDup(a, R))
                    {
                        //Console.Write("i : " + i + " ");
                        //printlist(R);
                        //Console.Write(" : ");
                        //printlist(a);
                        //Console.Write(" add ");
                        List<List<DataRecord>> QQQ = new List<List<DataRecord>>();
                        foreach (List<DataRecord> RR in rrr)
                        {
                            QQQ.Add(RR);
                            //printlist(RR);
                        }

                        QQQ.Add(a);
                        //Console.Write(" || ");
                        foreach (List<DataRecord> RR in QQQ)
                        {

                            //printlist(RR);
                        }
                        Results.Add(QQQ);
                        //Console.WriteLine();
                        break;
                    }

                }

                foreach (DataRecord r in rrr[rrr.Count - 1])
                {
                    //Console.Write(r);
                }
                //Console.WriteLine();
                i++;
            }
            /*
            //show 2 member

            foreach (List<List<string>> rr in Result)
            {
                Console.WriteLine("Result : ");
                foreach (List<string> r in rr)
                {
                    printlist(r);
                }
                Console.WriteLine();
            }
            //Show 3 member

            foreach (List<List<string>> rr in Results)
            {
                Console.WriteLine("Results : ");
                //Result.Add(rr);
                foreach (List<string> r in rr)
                {
                    printlist(r);
                }
                Console.WriteLine();
            }*/

            //check dup Group  
            //////////////////////////////////////////////////////////////

            for (int j = 0; j <= Results.Count - 1; j++)
            {
                for (int k = j + 1; k <= Results.Count - 1; k++)
                {
                    List<bool> ress = new List<bool>();
                    foreach (List<DataRecord> rr in Results[j])
                    {
                        List<bool> res = new List<bool>();
                        foreach (List<DataRecord> rrr in Results[k])
                        {
                            //printlist(rr);
                            //Console.Write(" : ");
                            //printlist(rrr);

                            if (rr == rrr)
                            {
                                //Console.Write("t");
                                res.Add(true);
                            }
                            else
                            {
                                //Console.Write("f");
                                res.Add(false);
                            }
                            //Console.Write("\t");
                        }
                        bool xxxx = true;
                        foreach (bool ppp in res)
                        {

                            if (ppp)
                            {
                                xxxx = true;
                                break;
                            }
                            else
                            {
                                xxxx = false;
                            }

                        }

                        ress.Add(xxxx);
                        //Console.WriteLine();
                    }
                    if (ress.All(c => c == true))
                    {
                        //Console.Write(j + " " + k);
                        Results.RemoveAt(k);
                    }
                    //Console.WriteLine();
                }
            }



            //Show 3 member after remove
            foreach (List<List<DataRecord>> rr in Results)
            {
                //Console.WriteLine("Results : ");
                List<List<string>> E = new List<List<string>>();
                /*foreach (List<string> e in rr)
                {
                    E.Add(e);
                }
                foreach (List<string> e in Combine_Single(single, rr))
                {
                    E.Add(e);
                }
                Last_Result.Add(E);*/

                Last_Result.Add(rr);
                //Result.Add(rr);
                foreach (List<DataRecord> r in rr)
                {
                    //printlist(r);
                }
                //Console.WriteLine();
            }



            /*

            foreach (List<List<string>> R in Result)
            {
                var objects = buffer.Where(z => !R.Contains(z)).ToList();
                foreach(List<string> o in objects)
                {
                    printlist(o);
                }
                
                Console.WriteLine();
                */
            /*
            foreach (List<string> a in buffer)
            {

                foreach (List<string> r in R)
                {

                    foreach (string aa in a)
                    {
                        List<string> unique = new List<string>();
                        bool outputs = false;
                        printlist(r);
                        Console.Write(":");
                        var matches = r.Where(p => p != aa).ToList();
                        // printlist(matches);
                        // Console.WriteLine();

                        var objects =r.Where(m => !aa.Contains(m)).ToList();
                        //printlist(objects);
                        Console.WriteLine();
                        /*
                        foreach (string rr in r)
                        {



                            if(aa == rr)
                            {
                                //Console.Write(aa + " : " + rr);
                                outputs = true;
                                unique = new List<string>();
                                //goto endloop;
                                break;
                            }
                            else
                            {
                                unique.Add(rr);
                            }
                            //Console.WriteLine();
                        }
                        //endloop:;
                        if (outputs == false)
                        {
                            foreach (List<string> RRRR in R)
                            {
                                printlist(RRRR);
                            }
                            Console.Write(" : ");
                            printlist(unique);
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                }   *             
            }   
        }*/

        }

        public string printlist(List<DataRecord> z)
        {
            DataRecord last = z.Last();
            string aa = "";
            aa += "{";
            Debug.Write("{");
            foreach (DataRecord a in z)
            {
                aa += a.work;
                Debug.Write(a.work);
                if (!a.work.Equals(last.work))
                {
                    Debug.Write(",");
                    aa += ",";
                }
                
            }
            aa += "}";
            Debug.Write("}");
            return aa;
        }

        static public bool IsDup(List<DataRecord> source, List<DataRecord> target)
        {
            bool output = false;
            foreach (DataRecord a in source)
            {
                foreach (DataRecord b in target)
                {
                    if (a.work == b.work)
                    {

                        output = true;
                        goto endloop;
                    }
                    else
                    {
                        //Console.WriteLine(a + " !== " + b);
                    }
                }
            }
            endloop:

            return output;
        }
        static public List<List<List<DataRecord>>> removeDup2(List<List<List<DataRecord>>> Results)
        {
            for (int j = 0; j <= Results.Count - 1; j++)
            {
                for (int k = j + 1; k <= Results.Count - 1; k++)
                {
                    if (Results[j].Count == Results[k].Count)
                    {
                        List<bool> ress = new List<bool>();
                        foreach (List<DataRecord> rr in Results[j])
                        {
                            List<bool> res = new List<bool>();
                            foreach (List<DataRecord> rrr in Results[k])
                            {
                                //printlist(rr);
                                //Console.Write(" : ");
                                //printlist(rrr);

                                if (rr == rrr)
                                {
                                    //Console.Write("t");
                                    res.Add(true);
                                }
                                else
                                {
                                    //Console.Write("f");
                                    res.Add(false);
                                }
                                //Console.Write("\t");
                            }
                            bool xxxx = true;
                            foreach (bool ppp in res)
                            {

                                if (ppp)
                                {
                                    xxxx = true;
                                    break;
                                }
                                else
                                {
                                    xxxx = false;
                                }

                            }

                            ress.Add(xxxx);
                            //Console.WriteLine();
                        }
                        if (ress.All(c => c == true))
                        {
                            //Console.Write(j + " " + k);
                            Results.RemoveAt(k);
                        }
                    }

                    //Console.WriteLine();
                }
            }
            return Results;
        }
        static public List<List<DataRecord>> Combine_Single(List<List<DataRecord>> single, List<List<DataRecord>> input)
        {
            List<List<DataRecord>> output = new List<List<DataRecord>>();
            List<List<DataRecord>> buffer = new List<List<DataRecord>>();
            foreach (List<DataRecord> inputs in input)
            {
                foreach (DataRecord s in inputs)
                {
                    buffer.Add(new List<DataRecord>(new DataRecord[] { s }));
                }
            }


            foreach (List<DataRecord> inputs in buffer)
            {
                //printlist(inputs);
            }
            List<DataRecord> buffer2 = new List<DataRecord>();
            List<DataRecord> buffer3 = new List<DataRecord>();
            foreach (List<DataRecord> inputs in input)
            {
                foreach (DataRecord s in inputs)
                {
                    buffer2.Add(s);
                }
            }
            foreach (List<DataRecord> inputs in single)
            {
                foreach (DataRecord s in inputs)
                {
                    buffer3.Add(s);
                }
            }

            var firstNotSecond = buffer3.Except(buffer2).ToList();

            foreach (DataRecord a in firstNotSecond)
            {
                output.Add(new List<DataRecord>(new DataRecord[] { a }));
            }

            //Console.WriteLine();
            //printlist(firstNotSecond);


            foreach (List<DataRecord> inputs in output)
            {
                //printlist(inputs);
            }
            return output;
        }
    }
}
