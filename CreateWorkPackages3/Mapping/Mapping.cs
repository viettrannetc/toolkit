using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CreateWorkPackages3.Configuration;
using CreateWorkPackages3.ProductBacklogItems.Model;
using CreateWorkPackages3.Workpackages.Model;

namespace CreateWorkPackages3.Mapping
{
    internal static class Mapping
    {
        public static WorkpackageModel MappingTask(ProductBacklogItemModel pbiModel)
        {
            WorkpackageModel workpackageModel = new WorkpackageModel();

            int index = pbiModel.AreaPath.IndexOf('\\', 0);
            string area = pbiModel.AreaPath.Substring(index + 1);

            workpackageModel.Title = area + ": " + pbiModel.Id + ": " + pbiModel.Title;
            workpackageModel.AssignedTo = pbiModel.AssignedTo;
            workpackageModel.Estimate = pbiModel.Effort;
            TeamAndRelatedCaseMapping teamAndRelatedCaseMapping = new TeamAndRelatedCaseMapping();
            var trc = teamAndRelatedCaseMapping.Mapping(pbiModel.RelatedFeatureTitle);
            if (trc != null)
            {
                workpackageModel.Team = trc.TeamId;
                workpackageModel.RelatedCase = trc.RelatedCaseId;
            }

            SprintMapping sprintMapping = new SprintMapping();
            var sprint = sprintMapping.Mapping(pbiModel.Sprint);
            if (sprint != null)
            {
                workpackageModel.Sprint = sprint.SprintId;
                workpackageModel.DueDate = sprint.DueDate;
            }

            ApplicationMapping applicationMapping = new ApplicationMapping();
            var app = applicationMapping.Mapping(pbiModel.AreaPath);
            if (app != null)
            {
                workpackageModel.Application = app.ApplicationId;
            }

            return workpackageModel;
        }
    }

    public class TeamAndRelatedCaseMapping
    {
        private readonly AreaPathSettingsCollection _areaPathSettingsCollection;

        public TeamAndRelatedCaseMapping()
        {
            MappingSectionGroup group = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["MappingSection"] as MappingSectionGroup;

            foreach (ConfigurationSection section in group.Sections)
            {
                if (section.GetType() == typeof(AreaPathSection))
                {
                    AreaPathSection c = (AreaPathSection)section;
                    _areaPathSettingsCollection = c.AreaPaths;
                }
            }
        }

        public AreaPathSettings Mapping(string pbiModelAreaPath)
        {
            try
            {
                return _areaPathSettingsCollection.Cast<AreaPathSettings>().Single(x => x.AreaPathKey == pbiModelAreaPath);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class TRC
    {
        public string AreaPathKey { get; set; }
        public int TeamId { get; set; }
        public int RelatedCaseId { get; set; }
        public int ApplicationId { get; set; }
    }

    public class SprintMapping
    {
        private readonly SprintSettingsCollection _sprintSettingsCollectioncoll;

        public SprintMapping()
        {
            MappingSectionGroup group = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["MappingSection"] as MappingSectionGroup;

            foreach (ConfigurationSection section in group.Sections)
            {
                if (section.GetType() == typeof(SprintSection))
                {
                    SprintSection c = (SprintSection)section;
                    _sprintSettingsCollectioncoll = c.Sprints;

                }
            }
        }

        public SprintSettings Mapping(string sprint)
        {
            try
            {
                return _sprintSettingsCollectioncoll.Cast<SprintSettings>().Single(x => x.SprintKey == sprint);
                
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class Sprint
    {
        public string SprintKey { get; set; }
        public int SprintId { get; set; }
        public string DueDate { get; set; }
    }

    public class ApplicationMapping
    {
        private readonly ApplicationSettingsCollection _areaPathSettingsCollection;

        public ApplicationMapping()
        {
            MappingSectionGroup group = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["MappingSection"] as MappingSectionGroup;

            foreach (ConfigurationSection section in group.Sections)
            {
                if (section.GetType() == typeof(ApplicationSection))
                {
                    ApplicationSection a = (ApplicationSection)section;
                    _areaPathSettingsCollection = a.Applications;
                }
            }
        }

        public ApplicationSettings Mapping(string pbiModelAreaPath)
        {
            try
            {
                return _areaPathSettingsCollection.Cast<ApplicationSettings>().Single(x => x.AreaPathKey == pbiModelAreaPath);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

