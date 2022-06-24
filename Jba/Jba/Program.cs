
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;



class Jba
{
    

    static void Main()
    {
        string text = System.IO.File.ReadAllText(@"C:\Users\Cameron\Downloads\jba-software-code-challenge-data-transformation\test1.txt"); //filename to pick

        string[] subs = text.Split("Grid-ref=");

        string[] headers = subs[0].Split("[");

        string startingYear=default;
        List<Precip> precips = new List<Precip>();

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains("Years"))
            {
                startingYear = headers[i].Substring(6).Split("-")[0]; 
                break;
            }
        }

        

        for (int i = 1; i < subs.Length; i++)
        {
            

            string[] data = subs[i].Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //Console.WriteLine("no of data " + data.Length);

            string xRef = default;
            string yRef = default;

            //Console.WriteLine("I= "+i);

            for (int j = 0; j < data.Length; j++)
            {
                if (j == 0)
                {
                    string[] gridRef = data[j].Split(",", StringSplitOptions.TrimEntries);
                    xRef = gridRef[0];
                    yRef = gridRef[1];

                    //Console.WriteLine("getting gridref " + xRef + " " + yRef);
                }
                else
                {
                    int year = Int32.Parse(startingYear) + j-1;
                    string[] precipData = data[j].Split(" ",StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < precipData.Length; k++)
                    {
                        Precip precip = new Precip();
                        precip.xRef = xRef;
                        precip.yRef = yRef;
                        precip.year = year;
                        precip.month = k + 1;
                        precip.precipitation = precipData[k];
                        precips.Add(precip);

                    }
                }
            }
        }

        
        
        

        try
        {
            
            StreamWriter sw = new StreamWriter("predata.txt");
            for (int i = 0; i < precips.Count; i++)
            {
                Console.WriteLine("(" + precips[i].xRef + "," + precips[i].yRef + "," + precips[i].month + "/1/" + precips[i].year + "," + precips[i].precipitation + ")");

                sw.WriteLine(precips[i].xRef + "," + precips[i].yRef + "," + precips[i].month + "/1/" + precips[i].year + "," + precips[i].precipitation);
                
            }
            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

    }
}

class Precip
{
    public string xRef;
    public string yRef;
    public int month;
    public int year;
    public string precipitation;

    


}



