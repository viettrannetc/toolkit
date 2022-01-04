using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateWorkPackages3.TimeEntries.Model;
using Toggl.Extensions;
using Toggl.Services;

namespace CreateWorkPackages3.TimeEntries
{
    class TimeEntries
    {
        private Toggl.Workspace _workspace;

        public void Connect(string togglApitoken, string workspaceName)
        {

            //"919a9da3009a2339e8866fca28f44f2a"
            InitializeTogglServices(togglApitoken);

            _workspace = GetWorkspace(workspaceName);
        }

        public void InitializeTogglServices(string key)
        {
            ApiService = new ApiService(key);
            //ClientService = new ClientService(ApiService);
            ProjectService = new ProjectService(ApiService);
            TimeEntryService = new TimeEntryService(ApiService);
            WorkspaceService = new WorkspaceService(ApiService);
        }

        public Toggl.Workspace GetWorkspace(string workspaceName)
        {

            Toggl.Workspace workspace = null;
            //try
            //{
                workspace = WorkspaceService.List().First(x => x.Name == workspaceName);
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}

            return workspace;
        }

        public int? GetProjectId(string projectName)
        {
            Toggl.Project project = null;
            try
            {
                project = ProjectService.List().First(x => x.Name == projectName);
            }
            catch (Exception)
            {
                var tempProject = new Toggl.Project()
                {
                    Name = projectName,
                    //    ClientId = client1.Id, 
                        WorkspaceId = _workspace.Id,
                };
                ProjectService.Add(tempProject);
                project = ProjectService.List().First(x => x.Name == projectName);
            }

            return project.Id;
        }

        public bool CreateTimeEntries(List<TimeEntryModel> timeEntrySelectedList)
        {
            bool result = true;
            foreach (var te in timeEntrySelectedList)
            {
                try
                {
                    TimeEntryService.Add(new Toggl.TimeEntry
                    {
                        //IsBillable = true,
                        CreatedWith = "TimeEntryTestAdd", // mandatory
                        Description = te.Description + DateTime.Now.Ticks,
                        Duration = 0,
                        Start = DateTime.Now.ToIsoDateStr(), // mandatory
                        ProjectId = GetProjectId(te.ProjectName),
                        WorkspaceId = _workspace.Id
                    });
                }
                catch (Exception)
                {
                    result = false;
                }
    
            }
            return result;
        }

        public TimeEntryService TimeEntryService { get; set; }
        public ApiService ApiService { get; set; } 
        public WorkspaceService WorkspaceService { get; set; }
        public ProjectService ProjectService { get; set; }
    }
}
