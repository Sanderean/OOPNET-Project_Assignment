using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.Moduls
{
    public class Results : Team
    {
        public Results(int id, string country, string alternateName, string fifa_Code, int groupId, string groupLetter, int wins, int draws, int losses, int games_played, int points, int goals_for, int goals_against, int goal_differential) : base(id, country, alternateName, fifa_Code, groupId, groupLetter)
        {
            this.wins = wins;
            this.draws = draws;
            this.losses = losses;
            this.games_played = games_played;
            this.points = points;
            this.goals_for = goals_for;
            this.goals_against = goals_against;
            this.goal_differential = goal_differential;
        }

        public int wins { get; private set; }
        public int draws { get; private set; }
        public int losses { get; private set; }
        public int games_played { get; private set; }
        public int points { get; private set; }
        public int goals_for { get; private set; }
        public int goals_against { get; private set; }
        public int goal_differential { get; private set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
