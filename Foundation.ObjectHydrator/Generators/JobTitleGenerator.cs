using System;
using System.Collections.Generic;
using Foundation.ObjectHydrator.Interfaces;

namespace Foundation.ObjectHydrator.Generators
{
    public class JobTitleGenerator : IGenerator<string>
    {
        private readonly FromListGetSingleGenerator<JobLevel> _jobLevelGenerator;
        private readonly FromListGetSingleGenerator<string> _highLevels;
        private readonly FromListGetSingleGenerator<string> _middleLevels;
        private readonly FromListGetSingleGenerator<string> _regularLevels;
        private readonly FromListGetSingleGenerator<string> _types;
        private readonly FromListGetSingleGenerator<string> _suffixes;

        public JobTitleGenerator(Func<IJobTitleGeneratorOptionsBuilder, IJobTitleGeneratorOptionsBuilder> optionsBuilder = null)
        {
            _highLevels = new FromListGetSingleGenerator<string>(HighLevelList);
            _middleLevels = new FromListGetSingleGenerator<string>(MiddleLevelList);
            _regularLevels = new FromListGetSingleGenerator<string>(RegularLevelList);
            _types = new FromListGetSingleGenerator<string>(AreaList);
            _suffixes = new FromListGetSingleGenerator<string>(Suffix);

            var options = new JobTitleGeneratorOptions();
            if (optionsBuilder != null)
            {
                optionsBuilder(options);
            }

            var jobLevels = new List<JobLevel>();

            foreach (JobLevel jobLevel in Enum.GetValues(typeof(JobLevel)))
            {
                var frequency = options.GetFrequencyFor(jobLevel);
                for (int i = 0; i < frequency; i++)
                {
                    jobLevels.Add(jobLevel);
                }
            }
            _jobLevelGenerator = new FromListGetSingleGenerator<JobLevel>(jobLevels);
        }

        public string Generate()
        {
            var levelToGenerate = _jobLevelGenerator.Generate();
            
            string level;
            string type = _types.Generate();
            string suffix = _suffixes.Generate();

            switch (levelToGenerate)
            {
                case JobLevel.High:
                    level = _highLevels.Generate();
                    break;
                case JobLevel.Middle:
                    level = _middleLevels.Generate();
                    break;
                case JobLevel.Regular:
                    level = _regularLevels.Generate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return levelToGenerate == JobLevel.High
                ? string.Format("{0} for {1}", level, type)
                : string.Format("{0} {1} {2}", level, type, suffix);
        }

        private static readonly string[] AreaList =  new[]
        {
            "Solutions",
            "Program",
            "Brand",
            "Security",
            "Research",
            "Marketing",
            "Directives",
            "Implementation",
            "Integration",
            "Functionality",
            "Response",
            "Paradigm",
            "Tactics",
            "Identity",
            "Markets",
            "Group",
            "Resonance",
            "Applications",
            "Optimization",
            "Operations",
            "Infrastructure",
            "Intranet",
            "Communications",
            "Web",
            "Branding",
            "Quality",
            "Assurance",
            "Impact",
            "Mobility",
            "Ideation",
            "Data",
            "Creative",
            "Configuration",
            "Accountability",
            "Interactions",
            "Factors",
            "Usability",
            "Metrics",
            "Team",
            "Corporate",
            "Manufacturing",
            "European",
            "Americas",
            "Logistics",
            "Tax",
            "Consumables",
            "Catering",
            "Offshore Catering",
            "Investor Relations",
            "Affairs",
            "Corporate Synergy",
            "Digital Transformation",
            "Expendable Telemetry",
            "Cultural Transformation",
            "Arts",
            "Rural Affairs",
            "Waste Management",
        };
        
        private static readonly string[] HighLevelList = new[]
        {
            "Chief Executive",
            "President",
            "Vice-President",
            "Managing Director",
            "Financial Director",
            "Company Secretary",
            "Technical Director",
            "Principal Architext",
            "Commander",
            "Director",
            "Chairman",
            "Vice Chairman",
            "Executive Vice Chairman",
            "Non-Executive Vice Chairman",
        };
        
        private static readonly string[] MiddleLevelList = new[]
        {
            "Lead",
            "Senior",
            "Chief",
            "Principal",
            "Global",
            "International",
            "Corporate",
            "Regional",
        };

        private static readonly string[] RegularLevelList = new[]
        {
            "Trainee",
            "Junior",
            "Graduate",
            "Direct",
            "Dynamic",
            "Future",
            "Product",
            "National",
            "District",
            "Central",
            "Relational",
            "Customer",
            "Investor",
            "Dynamic",
            "Legacy",
            "Forward",
            "Interactive",
            "Internal",
            "Human",
        };

        private static readonly string[] Suffix = new[]
        {
            "Supervisor",
            "Associate",
            "Executive",
            "Liason",
            "Officer",
            "Manager",
            "Engineer",
            "Specialist",
            "Director",
            "Coordinator",
            "Administrator",
            "Architect",
            "Analyst",
            "Designer",
            "Planner",
            "Synergist",
            "Orchestrator",
            "Technician",
            "Developer",
            "Producer",
            "Consultant",
            "Assistant",
            "Facilitator",
            "Agent",
            "Representative",
            "Strategist",
        };
    }
}
