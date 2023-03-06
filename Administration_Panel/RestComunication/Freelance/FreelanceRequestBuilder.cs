namespace Client.RestComunication.Freelance
{
    public class FreelanceRequestBuilder
    {
        private FreelanceRouter _router = new FreelanceRouter();

        //BookmarkedJobs
        public string DeleteBookmarkedJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedJobs_Delete + $"id={id}";
        }
        public string GetAllBookmarkedJobsRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedJobs_GetAll;
        }
        public string SaveBookmarkedJobsRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedJobs_Save;
        }
        public string GetBookmarkedJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedJobs_GetById + $"id={id}";
        }

        //BookmarkedTasks
        public string DeleteBookmarkedTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedTasks_Delete + $"id={id}";
        }
        public string GetAllBookmarkedTasksRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedTasks_GetAll;
        }
        public string SaveBookmarkedTasksRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedTasks_Save;
        }
        public string GetBookmarkedTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedTasks_GetById + $"id={id}";
        }

        //BookmarkedUsers
        public string DeleteBookmarkedUsersByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedUsers_Delete + $"id={id}";
        }
        public string GetAllBookmarkedUsersRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedUsers_GetAll;
        }
        public string SaveBookmarkedUsersRequestBuilder(string uri)
        {
            return uri + _router.BookmarkedUsers_Save;
        }
        public string GetBookmarkedUsersByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.BookmarkedUsers_GetById + $"id={id}";
        }

        //FilesToJobs
        public string DeleteFilesToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FilesToJobs_Delete + $"id={id}";
        }
        public string GetAllFilesToJobsRequestBuilder(string uri)
        {
            return uri + _router.FilesToJobs_GetAll;
        }
        public string SaveFilesToJobsRequestBuilder(string uri)
        {
            return uri + _router.FilesToJobs_Save;
        }
        public string GetFilesToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FilesToJobs_GetById + $"id={id}";
        }

        //JobApplications
        public string DeleteJobApplicationsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.JobApplications_Delete + $"id={id}";
        }
        public string GetAllJobApplicationsRequestBuilder(string uri)
        {
            return uri + _router.JobApplications_GetAll;
        }
        public string SaveJobApplicationsRequestBuilder(string uri)
        {
            return uri + _router.JobApplications_Save;
        }
        public string GetJobApplicationsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.JobApplications_GetById + $"id={id}";
        }

        //Jobs
        public string DeleteJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Jobs_Delete + $"id={id}";
        }
        public string GetAllJobsRequestBuilder(string uri)
        {
            return uri + _router.Jobs_GetAll;
        }
        public string SaveJobsRequestBuilder(string uri)
        {
            return uri + _router.Jobs_Save;
        }
        public string GetJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Jobs_GetById + $"id={id}";
        }

        //JobTypes
        public string DeleteJobTypesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.JobTypes_Delete + $"id={id}";
        }
        public string GetAllJobTypesRequestBuilder(string uri)
        {
            return uri + _router.JobTypes_GetAll;
        }
        public string SaveJobTypesRequestBuilder(string uri)
        {
            return uri + _router.JobTypes_Save;
        }
        public string GetJobTypesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.JobTypes_GetById + $"id={id}";
        }

        //SkillsToJobs
        public string DeleteSkillsToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToJobs_Delete + $"id={id}";
        }
        public string GetAllSkillsToJobsRequestBuilder(string uri)
        {
            return uri + _router.SkillsToJobs_GetAll;
        }
        public string SaveSkillsToJobsRequestBuilder(string uri)
        {
            return uri + _router.SkillsToJobs_Save;
        }
        public string GetSkillsToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToJobs_GetById + $"id={id}";
        }

        //TagsToJobs
        public string DeleteTagsToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.TagsToJobs_Delete + $"id={id}";
        }
        public string GetAllTagsToJobsRequestBuilder(string uri)
        {
            return uri + _router.TagsToJobs_GetAll;
        }
        public string SaveTagsToJobsRequestBuilder(string uri)
        {
            return uri + _router.TagsToJobs_Save;
        }
        public string GetTagsToJobsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.TagsToJobs_GetById + $"id={id}";
        }

        //Categories
        public string DeleteCategoriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Categories_Delete + $"id={id}";
        }
        public string GetAllCategoriesRequestBuilder(string uri)
        {
            return uri + _router.Categories_GetAll;
        }
        public string SaveCategoriesRequestBuilder(string uri)
        {
            return uri + _router.Categories_Save;
        }
        public string GetCategoriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Categories_GetById + $"id={id}";
        }

        //FreelanceFiles
        public string DeleteFreelanceFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FreelanceFiles_Delete + $"id={id}";
        }
        public string GetAllFreelanceFilesRequestBuilder(string uri)
        {
            return uri + _router.FreelanceFiles_GetAll;
        }
        public string SaveFreelanceFilesRequestBuilder(string uri)
        {
            return uri + _router.FreelanceFiles_Save;
        }
        public string GetFreelanceFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FreelanceFiles_GetById + $"id={id}";
        }

        //Notes
        public string DeleteNotesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Notes_Delete + $"id={id}";
        }
        public string GetAllNotesRequestBuilder(string uri)
        {
            return uri + _router.Notes_GetAll;
        }
        public string SaveNotesRequestBuilder(string uri)
        {
            return uri + _router.Notes_Save;
        }
        public string GetNotesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Notes_GetById + $"id={id}";
        }

        //Reviews
        public string DeleteReviewsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Reviews_Delete + $"id={id}";
        }
        public string GetAllReviewsRequestBuilder(string uri)
        {
            return uri + _router.Reviews_GetAll;
        }
        public string SaveReviewsRequestBuilder(string uri)
        {
            return uri + _router.Reviews_Save;
        }
        public string GetReviewsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Reviews_GetById + $"id={id}";
        }

        //Skills
        public string DeleteSkillsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Skills_Delete + $"id={id}";
        }
        public string GetAllSkillsRequestBuilder(string uri)
        {
            return uri + _router.Skills_GetAll;
        }
        public string SaveSkillsRequestBuilder(string uri)
        {
            return uri + _router.Skills_Save;
        }
        public string GetSkillsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Skills_GetById + $"id={id}";
        }

        //SkillsToCategories
        public string DeleteSkillsToCategoriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToCategories_Delete + $"id={id}";
        }
        public string GetAllSkillsToCategoriesRequestBuilder(string uri)
        {
            return uri + _router.SkillsToCategories_GetAll;
        }
        public string SaveSkillsToCategoriesRequestBuilder(string uri)
        {
            return uri + _router.SkillsToCategories_Save;
        }
        public string GetSkillsToCategoriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToCategories_GetById + $"id={id}";
        }

        //Tags
        public string DeleteTagsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Tags_Delete + $"id={id}";
        }
        public string GetAllTagsRequestBuilder(string uri)
        {
            return uri + _router.Tags_GetAll;
        }
        public string SaveTagsRequestBuilder(string uri)
        {
            return uri + _router.Tags_Save;
        }
        public string GetTagsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Tags_GetById + $"id={id}";
        }

        //FilesToTasks
        public string DeleteFilesToTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FilesToTasks_Delete + $"id={id}";
        }
        public string GetAllFilesToTasksRequestBuilder(string uri)
        {
            return uri + _router.FilesToTasks_GetAll;
        }
        public string SaveFilesToTasksRequestBuilder(string uri)
        {
            return uri + _router.FilesToTasks_Save;
        }
        public string GetFilesToTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.FilesToTasks_GetById + $"id={id}";
        }

        //SkillsToTasks
        public string DeleteSkillsToTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToTasks_Delete + $"id={id}";
        }
        public string GetAllSkillsToTasksRequestBuilder(string uri)
        {
            return uri + _router.SkillsToTasks_GetAll;
        }
        public string SaveSkillsToTasksRequestBuilder(string uri)
        {
            return uri + _router.SkillsToTasks_Save;
        }
        public string GetSkillsToTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.SkillsToTasks_GetById + $"id={id}";
        }

        //TaskBids
        public string DeleteTaskBidsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.TaskBids_Delete + $"id={id}";
        }
        public string GetAllTaskBidsRequestBuilder(string uri)
        {
            return uri + _router.TaskBids_GetAll;
        }
        public string SaveTaskBidsRequestBuilder(string uri)
        {
            return uri + _router.TaskBids_Save;
        }
        public string GetTaskBidsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.TaskBids_GetById + $"id={id}";
        }

        //Tasks
        public string DeleteTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Tasks_Delete + $"id={id}";
        }
        public string GetAllTasksRequestBuilder(string uri)
        {
            return uri + _router.Tasks_GetAll;
        }
        public string SaveTasksRequestBuilder(string uri)
        {
            return uri + _router.Tasks_Save;
        }
        public string GetTasksByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Tasks_GetById + $"id={id}";
        }
    }
}
