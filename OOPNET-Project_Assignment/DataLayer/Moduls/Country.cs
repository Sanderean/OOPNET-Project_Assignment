using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.Moduls
{
    public class Country
    {
        public class AwayTeam
        {
            public AwayTeam(string country, string code, int goals, int penalties)
            {
                this.country = country;
                this.code = code;
                this.goals = goals;
                this.penalties = penalties;
            }

            public string country { get; set; }
            public string code { get; set; }
            public int goals { get; set; }
            public int penalties { get; set; }
        }

        public class AwayTeamEvent
        {
            public AwayTeamEvent(int id, string type_of_event, string player, string time)
            {
                this.id = id;
                this.type_of_event = type_of_event;
                this.player = player;
                this.time = time;
            }

            public int id { get; set; }
            public string type_of_event { get; set; }
            public string player { get; set; }
            public string time { get; set; }
        }

        public class AwayTeamStatistics
        {
            public AwayTeamStatistics(string country, int? attempts_on_goal, int? on_target, int? off_target, int? blocked, int? woodwork, int? corners, int? offsides, int? ball_possession, int? pass_accuracy, int? num_passes, int? passes_completed, int? distance_covered, int? balls_recovered, int? tackles, int? clearances, int? yellow_cards, int? red_cards, int? fouls_committed, string tactics, List<StartingEleven> starting_eleven, List<Substitute> substitutes)
            {
                this.country = country;
                this.attempts_on_goal = attempts_on_goal;
                this.on_target = on_target;
                this.off_target = off_target;
                this.blocked = blocked;
                this.woodwork = woodwork;
                this.corners = corners;
                this.offsides = offsides;
                this.ball_possession = ball_possession;
                this.pass_accuracy = pass_accuracy;
                this.num_passes = num_passes;
                this.passes_completed = passes_completed;
                this.distance_covered = distance_covered;
                this.balls_recovered = balls_recovered;
                this.tackles = tackles;
                this.clearances = clearances;
                this.yellow_cards = yellow_cards;
                this.red_cards = red_cards;
                this.fouls_committed = fouls_committed;
                this.tactics = tactics;
                this.starting_eleven = starting_eleven;
                this.substitutes = substitutes;
            }

            public string country { get; set; }
            public int? attempts_on_goal { get; set; }
            public int? on_target { get; set; }
            public int? off_target { get; set; }
            public int? blocked { get; set; }
            public int? woodwork { get; set; }
            public int? corners { get; set; }
            public int? offsides { get; set; }
            public int? ball_possession { get; set; }
            public int? pass_accuracy { get; set; }
            public int? num_passes { get; set; }
            public int? passes_completed { get; set; }
            public int? distance_covered { get; set; }
            public int? balls_recovered { get; set; }
            public int? tackles { get; set; }
            public int? clearances { get; set; }
            public int? yellow_cards { get; set; }
            public int? red_cards { get; set; }
            public int? fouls_committed { get; set; }
            public string tactics { get; set; }
            public List<StartingEleven> starting_eleven { get; set; }
            public List<Substitute> substitutes { get; set; }
        }

        public class HomeTeam
        {
            public HomeTeam(string country, string code, int goals, int penalties)
            {
                this.country = country;
                this.code = code;
                this.goals = goals;
                this.penalties = penalties;
            }

            public string country { get; set; }
            public string code { get; set; }
            public int goals { get; set; }
            public int penalties { get; set; }
        }

        public class HomeTeamEvent
        {
            public HomeTeamEvent(int id, string type_of_event, string player, string time)
            {
                this.id = id;
                this.type_of_event = type_of_event;
                this.player = player;
                this.time = time;
            }

            public int id { get; set; }
            public string type_of_event { get; set; }
            public string player { get; set; }
            public string time { get; set; }
        }

        public class HomeTeamStatistics
        {
            public HomeTeamStatistics(string country, int? attempts_on_goal, int? on_target, int? off_target, int? blocked, int? woodwork, int? corners, int? offsides, int? ball_possession, int? pass_accuracy, int? num_passes, int? passes_completed, int? distance_covered, int? balls_recovered, int? tackles, int? clearances, int? yellow_cards, int? red_cards, int? fouls_committed, string tactics, List<StartingEleven> starting_eleven, List<Substitute> substitutes)
            {
                this.country = country;
                this.attempts_on_goal = attempts_on_goal;
                this.on_target = on_target;
                this.off_target = off_target;
                this.blocked = blocked;
                this.woodwork = woodwork;
                this.corners = corners;
                this.offsides = offsides;
                this.ball_possession = ball_possession;
                this.pass_accuracy = pass_accuracy;
                this.num_passes = num_passes;
                this.passes_completed = passes_completed;
                this.distance_covered = distance_covered;
                this.balls_recovered = balls_recovered;
                this.tackles = tackles;
                this.clearances = clearances;
                this.yellow_cards = yellow_cards;
                this.red_cards = red_cards;
                this.fouls_committed = fouls_committed;
                this.tactics = tactics;
                this.starting_eleven = starting_eleven;
                this.substitutes = substitutes;
            }

            public string country { get; set; }
            public int? attempts_on_goal { get; set; }
            public int? on_target { get; set; }
            public int? off_target { get; set; }
            public int? blocked { get; set; }
            public int? woodwork { get; set; }
            public int? corners { get; set; }
            public int? offsides { get; set; }
            public int? ball_possession { get; set; }
            public int? pass_accuracy { get; set; }
            public int? num_passes { get; set; }
            public int? passes_completed { get; set; }
            public int? distance_covered { get; set; }
            public int? balls_recovered { get; set; }
            public int? tackles { get; set; }
            public int? clearances { get; set; }
            public int? yellow_cards { get; set; }
            public int? red_cards { get; set; }
            public int? fouls_committed { get; set; }
            public string tactics { get; set; }
            public List<StartingEleven> starting_eleven { get; set; }
            public List<Substitute> substitutes { get; set; }
        }

        public class Root
        {
            public Root(string venue, string location, string status, string time, string fifa_id, Weather weather, string attendance, List<string> officials, string stage_name, string home_team_country, string away_team_country, string datetime, string winner, string winner_code, HomeTeam home_team, AwayTeam away_team, List<HomeTeamEvent> home_team_events, List<AwayTeamEvent> away_team_events, HomeTeamStatistics home_team_statistics, AwayTeamStatistics away_team_statistics, DateTime? last_event_update_at, DateTime? last_score_update_at)
            {
                this.venue = venue;
                this.location = location;
                this.status = status;
                this.time = time;
                this.fifa_id = fifa_id;
                this.weather = weather;
                this.attendance = attendance;
                this.officials = officials;
                this.stage_name = stage_name;
                this.home_team_country = home_team_country;
                this.away_team_country = away_team_country;
                this.datetime = datetime;
                this.winner = winner;
                this.winner_code = winner_code;
                this.home_team = home_team;
                this.away_team = away_team;
                this.home_team_events = home_team_events;
                this.away_team_events = away_team_events;
                this.home_team_statistics = home_team_statistics;
                this.away_team_statistics = away_team_statistics;
                this.last_event_update_at = last_event_update_at;
                this.last_score_update_at = last_score_update_at;
            }

            public string venue { get; set; }
            public string location { get; set; }
            public string status { get; set; }
            public string time { get; set; }
            public string fifa_id { get; set; }
            public Weather weather { get; set; }
            public string attendance { get; set; }
            public List<string> officials { get; set; }
            public string stage_name { get; set; }
            public string home_team_country { get; set; }
            public string away_team_country { get; set; }
            public string datetime { get; set; }
            public string winner { get; set; }
            public string winner_code { get; set; }
            public HomeTeam home_team { get; set; }
            public AwayTeam away_team { get; set; }
            public List<HomeTeamEvent> home_team_events { get; set; }
            public List<AwayTeamEvent> away_team_events { get; set; }
            public HomeTeamStatistics home_team_statistics { get; set; }
            public AwayTeamStatistics away_team_statistics { get; set; }
            public DateTime? last_event_update_at { get; set; }
            public DateTime? last_score_update_at { get; set; }
        }

        public class StartingEleven
        {
            public StartingEleven(string name, bool captain, int shirt_number, string position)
            {
                this.name = name;
                this.captain = captain;
                this.shirt_number = shirt_number;
                this.position = position;
            }

            public string name { get; set; }
            public bool captain { get; set; }
            public int shirt_number { get; set; }
            public string position { get; set; }

            public override bool Equals(object obj)
            {
                return obj is StartingEleven eleven &&
                       name == eleven.name &&
                       captain == eleven.captain &&
                       shirt_number == eleven.shirt_number &&
                       position == eleven.position;
            }

            public override int GetHashCode()
            {
                int hashCode = 1764647528;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
                hashCode = hashCode * -1521134295 + captain.GetHashCode();
                hashCode = hashCode * -1521134295 + shirt_number.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(position);
                return hashCode;
            }

            public override string ToString()
           => $"Name: {name}, Is capitan: {(captain ? "Yes" : "No")}, Shirt number: {shirt_number}, Position: {position}";
        }

        public class Substitute
        {
            public Substitute(string name, bool captain, int shirt_number, string position)
            {
                this.name = name;
                this.captain = captain;
                this.shirt_number = shirt_number;
                this.position = position;
            }

            public string name { get; set; }
            public bool captain { get; set; }
            public int shirt_number { get; set; }
            public string position { get; set; }

            public override bool Equals(object obj)
            {
                return obj is Substitute substitute &&
                       name == substitute.name &&
                       captain == substitute.captain &&
                       shirt_number == substitute.shirt_number &&
                       position == substitute.position;
            }

            public override int GetHashCode()
            {
                int hashCode = 1764647528;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
                hashCode = hashCode * -1521134295 + captain.GetHashCode();
                hashCode = hashCode * -1521134295 + shirt_number.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(position);
                return hashCode;
            }

            public override string ToString()
           => $"Name: {name}, Is capitan: {(captain? "Yes" : "No")}, Shirt number: {shirt_number}, Position: {position}";
        }

        public class Weather
        {
            public Weather(string humidity, string temp_celsius, string temp_farenheit, string wind_speed, string description)
            {
                this.humidity = humidity;
                this.temp_celsius = temp_celsius;
                this.temp_farenheit = temp_farenheit;
                this.wind_speed = wind_speed;
                this.description = description;
            }

            public string humidity { get; set; }
            public string temp_celsius { get; set; }
            public string temp_farenheit { get; set; }
            public string wind_speed { get; set; }
            public string description { get; set; }
        }


    }
}
