using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.Moduls
{
    public class Team
    {
        public Team(int id, string country, string alternateName, string fifa_Code, int groupId, string groupLetter)
        {
            Id = id;
            Country = country;
            AlternateName = alternateName;
            Fifa_Code = fifa_Code;
            GroupId = groupId;
            GroupLetter = groupLetter;
        }

        public int Id { get; private set; }
        public string Country { get; private set; }
        public string AlternateName { get; private set; }
        public string Fifa_Code { get; private set; }
        public int GroupId { get; private set; }
        public string GroupLetter { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Team team &&
                   Id == team.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
