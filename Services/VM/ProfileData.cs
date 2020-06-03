using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.VM
{
    public class ProfileData
    {
        public Model.Users User { get; set; }
        public List<Model.JobOffer> TakenbyUser { get; set; }
        public List<Model.JobOffer> AddandTaken { get; set; }
        public List<Model.JobOffer> AddedByUser { get; set; }
    }
}
