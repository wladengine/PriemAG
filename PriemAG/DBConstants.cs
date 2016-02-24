using System;
using System.Collections.Generic;
using System.Text;

namespace Priem
{
    public class DBConstants
    {
        public const string CS_PRIEM = "Data Source=81.89.183.112;Initial Catalog=Priem2016Test;Integrated Security=SSPI";
        //public const string CS_PRIEM = "Data Source=81.89.183.112;Initial Catalog=test;Integrated Security=SSPI"; 
        //public const string CS_PRIEM = "Data Source=81.89.183.112;Initial Catalog=Priem2012;Integrated Security=false; user=superman; password=spiderman;Connect Timeout=10";
        
        public const string CS_PriemONLINE = "Data Source=81.89.183.103;Initial Catalog=OnlinePriem2012;Integrated Security=false; user=Priem2012User; password=2012Priem!Okay,kids;Connect Timeout=10";

        public const string CS_PRIEM_FAC = "Data Source=81.89.183.112;Initial Catalog=Priem;Integrated Security=false; user=superman; password=spiderman;Connect Timeout=10";

        public const string CS_STUDYPLAN = "Data Source=81.89.183.112;Initial Catalog=Education;Integrated Security=true";
    }

    public enum PriemType
    {
        Priem,
        PriemAspirant
    }
}
 