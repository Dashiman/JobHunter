using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.VM
{
    public class ProfileData
    {
        public List<JobOffer> TakenbyUser { get; set; }
        public List<JobOffer> AddandTaken { get; set; }
        public List<JobOffer> AddedByUser { get; set; }
    }
}
