using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        //helper
        [XmlIgnore]
        public List<Uri> Uris { get; set; }//list of links to the photoes of the unit
        public double feeAll { get; set; }//fee for all days
        public int numawful1 { get; set; }//number of awful comment
        public int numbad2 { get; set; }//number of bad comment
        public int numfine3 { get; set; }//number of fine comment
        public int numgood4 { get; set; }//number of good comment
        public int numperfect5 { get; set; }//number of perfect comment
        public double medsatisfication { get; set; }//avarage of comment

        public int maxGuest { get; set; }//maximum guests for unit
        public double priceAdults { get; set; }//price for one adult
        public double priceChildren { get; set; } //price for one child or baby
        public areaHosting SubArea { get; set; }//sub area of hosting unit  
        public int HostingUnitKey { get; set; }//the hosting unit key
        public Host Owner { get; set; }//the owner of hosting unit
        public string HostingUnitName { get; set; }//the name of hosting unit
        [XmlIgnore]
        public bool[,] Diary { get; set; }//the diary of the hosting unit that contains the avaiable days

        [XmlArray("Diary")]

        public XElement Diaryxml
        {
            get { return Diary.Flatten<XElement>(); }
            set
            {
                Diary = value.Expand<XElement>(12);
            }
        }
   //     for (int i = 0; i<12*30; i++)
			//{

			//}
        //public HostingUnit()
        //{
        //    this.Owner = new Host();
        //    this.Uris = new List<Uri>();
        //}
        public override string ToString()//tostring
        {

            string str ="";//string of occupied days
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (Diary[i, j] == true)
                    {//if the day is accupied
                        str = str + "the host is not available between:" + (j + 1) + "/" + (i + 1) + " to ";
                        while (Diary[i, j] == true && !((i == 11) && (j == 30)))
                        {//while the date is occupied and we are not at the end of the year it keeps going on the days

                            if (j == 30)
                            {
                                j = 0;
                                i++;
                            }
                            else
                            {
                                j++;
                            }
                        }
                        str = str + (j + 1) + "/" + (i + 1) + "\n";
                    }
                }
            }
            return "HostingUnitKey: " + HostingUnitKey + "\nOwner: " + Owner + "\nHostingUnitName: " + HostingUnitName + "\nDiary: " + str + "\nmaxGuest:"+ maxGuest+ "\npriceAdults:" + priceAdults + "\npriceChildren:" + priceChildren + "\nSubArea:" + SubArea+ "\nfeeAll:"+ feeAll+ "\nnumawful1: "+ numawful1+ "\nnumbad2: "+ numbad2
                + "\nnumfine3: "+ numfine3+ "\nnumgood4: "+ numgood4+ "\nnumperfect5: " + numperfect5+ "\nmedsatisfication: "+ medsatisfication;
        }
    }
}
